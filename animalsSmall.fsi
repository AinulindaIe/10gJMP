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
    new : symb:symbol * repLen:int -> animal
    override ToString : unit -> string
    member position : position option
    member reproduction : int
    member symbol : symbol
    member resetReproduction : unit -> unit
    member position : position option with set
    member updateReproduction : unit -> unit
  end
type moose =
  class
    inherit animal
    new : repLen:int -> moose
    member tick : unit -> moose option
  end
type wolf =
  class
    inherit animal
    new : repLen:int * hungLen:int -> wolf
    member hunger : int
    member resetHunger : unit -> unit
    member tick : unit -> wolf option
    member updateHunger : unit -> unit
  end
type board =
  {width: int;
   mutable moose: moose list;
   mutable wolves: wolf list;}
type environment =
  class
    new : boardWidth:int * NMooses:int * mooseRepLen:int * NWolves:int *
          wolvesRepLen:int * wolvesHungLen:int * verbose:bool -> environment
    override ToString : unit -> string
    member board : board
    member count : int
    member size : int
    member
      givePos : p:position ->
                  charArray:char [,] ->
                    sym:symbol -> position option list option
    member mooseMethod : m:moose -> boardState:char [,] -> unit
    member tick : unit -> unit
    member wolfMethod : w:wolf -> boardState:char [,] -> unit
  end

