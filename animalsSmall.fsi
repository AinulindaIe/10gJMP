module animals

type symbol = char
type position = int * int
type neighbour = position * symbol

val mSymbol : symbol
val wSymbol : symbol
val eSymbol : symbol
val rnd : System.Random

type animal =
  class
    /// <summary>Create a new animal represented with symbol symb and which reproduces every repLen ticks.</summary>
    /// <param name="symb">A symbol (character) used to represent the animal type when the board is printed.</param>
    /// <param name="repLen">The time in ticks until the animal attempts to produce an offspring.</param>
    
    new : symb:symbol * repLen:int -> animal
    /// The symbol as string for printing.
    override ToString : unit -> string
    /// The symbol representing this animal.
    member symbol : symbol
    /// Get the position of this animal. If position is None, then the animal is not on the board.
    member position : position option
    /// Set the position of this animal.
    member position : position option with set
    /// Get the reproduction counter in ticks. Starts as repLen and is counted down with every tick. The animals age is repLen - reproduction.
    member reproduction : int
    /// Set the reproduction counter to repLen.
    member resetReproduction : unit -> unit
    /// Reduce the reproduction counter by a tick.
    member updateReproduction : unit -> unit
  end

/// A moose is an animal with methods for updating its age and producing offspring.
type moose =
  class
    inherit animal
    /// <summary>Create a moose with symbol 'm'.</summary>
    /// <param name="repLen">The number of ticks until a moose attempts to produce an offspring.</param>

    new : repLen:int -> moose
    /// Perform a tick for this moose, i.e., call updateReproduction.
    member tick : unit -> moose option
  end

/// A wolf is an animal with hunger and methods for updating its age and hunger and for reproducing offspring. If the wolf has not eaten in a specified number of ticks, then it is taken off the board.
type wolf =
  class
    inherit animal
     /// <summary>Create a wolf with symbol 'w'.</summary>
    /// <param name="repLen">The number of ticks until a wolf attempts to produce an offspring.</param>
    /// <param name="hungLen">The number of ticks since it last ate until a wolf dies.</param>

    new : repLen:int * hungLen:int -> wolf
    /// Reduce the reproduction counter by a tick. If repLen is 0 then a new wolf is returned and the counter is reset to repLen.
    member hunger : int
    /// Set the hunger counter to hungLen.
    member resetHunger : unit -> unit
    /// Reduce the hunger counter by a tick. If hunger reaches 0, then the wolf is removed from the board.
    member updateHunger : unit -> unit
    /// Perform a tick for this wolf, i.e., call updateReproduction and updateHunger and possibly returns cub. 
    member tick : unit -> wolf option
  end

/// A square board with length width. The board is implicitly represented by its width and the coordinates in the animals.
type board =
  {
    /// The width of the board. The board size is width x width.
    width: int;
    /// The list of moose. If a moose has position None, then it is not on the board.
    mutable moose: moose list;
    /// The list of wolves. If a wolf has position None, then it is not on the board.
    mutable wolves: wolf list;}

/// An environment. Animals that have no position are considered dead.
type environment =
  class
    /// <summary>Create a new environment.</summary>
    /// <param name="boardWidth">The width of the board.</param>
    /// <param name="NMooses">The initial number of moose.</param>
    /// <param name="NWolves">The initial number of wolves.</param>
    /// <param name="mooseRepLen">The number of ticks until a moose attempts to produce an offspring.</param>
    /// <param name="wolvesRepLen">The number of ticks until a wolf attempts to produce an offspring.</param>
    /// <param name="wolvesHungLen">The number of ticks since it last ate until a wolf dies.</param>
    /// <param name="verbose">If the verbose flag is true, then messages are printed on screen at key events.</param>

    new : boardWidth:int * NMooses:int * mooseRepLen:int * NWolves:int *
          wolvesRepLen:int * wolvesHungLen:int * verbose:bool -> environment
    /// A board as a matrix of symbols for moose and wolves.    
    override ToString : unit -> string
    /// The board.
    member board : board
    /// The number of animals on the board.
    member count : int
    /// The positions on the board.
    member size : int

    /// <summary>Checks the positions on the board, around the given position and returns a list of any given positions in the area
    /// that match the symbol.</summary>
    /// <param name="p">Position indicating the area (positions) to check</param>
    /// <param name="charArray">A char[,] used to check for symbols</param>
    /// <param name="sym">symbol to check for in the area</param>
    /// <returns>A positioin option list option. Some list if this list contains elements, None if it's empty</returns>
    member
      givePos : p:position ->
                  charArray:char [,] ->
                    sym:symbol -> position option list option

    /// <summary>If the environment is verbose then print the string</summary>
    /// <param name="s">String to print</param>
    member verbPrint : string -> unit

                    
    /// <summary>Perform the appropiate action of a moose in a tick and updates the list of mooses in _board.
    /// it checks if the moose is alive. If so then we check if there are any available positions. If so then we check if 
    /// we need to breed or not. If so then breed otherwise move. if there is no available positions just perform the moose.tick.
    /// if the moose is dead then don't do anything (unless verbose).
    /// </summary>
    /// <param name="m">The moose which action to decide and perform</param>
    /// <param name="boardState">a char[,] to find appropiate positions on (decided by what action to take)</param>
    /// <returns>unit</returns>
    member mooseMethod : m:moose -> boardState:char [,] -> unit

    /// <summary>Perform the appropiate action of a wolf in a tick and updates the list of wolves in _board.
    /// first we check if the wolf is alive.
    ///   if so then check if we need to breed
    ///     if so then check if there is available positions
    ///       if so then breed at that position
    ///   otherwise check if we can eat
    ///     if so then eat
    ///   otherwise check if we can move
    ///       if so then move
    ///   otherwise just perform the wolf.tick
    /// otherwise dont' do anything (unless verbose).
    /// </summary>
    /// <param name="m">The wolf which action to decide and perform</param>
    /// <param name="boardState">a char[,] to find appropiate positions on (decided by what action to take)</param>
    /// <returns>unit</returns>
    member wolfMethod : w:wolf -> boardState:char [,] -> unit

    
    /// <summary> Perform a tick by performing all animal's ticks in random order. 
    /// Animals perform the following actions: 
    ///   - Calves and cubs are added if there is room in a neighbouring position. 
    ///   - Wolves eat a random Moose in a neighbouring position. 
    ///   - If animals do not give birth, eat or are eaten, then they move to an available neighbouring position.
    /// </summary>
    /// <returns>unit</returns>
    member tick : unit -> unit
  end

