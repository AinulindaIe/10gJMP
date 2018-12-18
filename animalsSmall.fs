module animals
open System.Runtime.InteropServices
open System.Net

type symbol = char
type position = int * int
type neighbour = position * symbol

let mSymbol : symbol = 'm'
let wSymbol : symbol = 'w'
let eSymbol : symbol = ' '
let rnd = System.Random ()

/// An animal is a base class. It has a position and a reproduction counter.
type animal (symb : symbol, repLen : int) =
  let mutable _reproduction = rnd.Next(1,repLen)
  let mutable _pos : position option = None
  let _symbol : symbol = symb

  member this.symbol = _symbol
  member this.position
    with get () = _pos
    and set aPos = _pos <- aPos
  member this.reproduction = _reproduction
  member this.updateReproduction () =
    _reproduction <- _reproduction - 1
  member this.resetReproduction () =
    _reproduction <- repLen
  override this.ToString () =
    string this.symbol

/// A moose is an animal
type moose (repLen : int) =
  inherit animal (mSymbol, repLen)

  member this.tick () : moose option =
    this.updateReproduction()
    if this.reproduction = 0 then Some (new moose(repLen))
    else None
    

/// A wolf is an animal with a hunger counter
type wolf (repLen : int, hungLen : int) =
  inherit animal (wSymbol, repLen)
  let mutable _hunger = hungLen

  member this.hunger = _hunger
  member this.updateHunger () =
    _hunger <- _hunger - 1
    if _hunger <= 0 then
      this.position <- None // Starve to death
  member this.resetHunger () =
    _hunger <- hungLen
  member this.tick () : wolf option =
    this.updateReproduction()
    this.updateHunger()
    if this.reproduction = 0 then Some (new wolf(repLen, hungLen))
    else None
    // Intentionally left blank. Insert code that updates the wolf's age and optionally an offspring.

/// A board is a chess-like board implicitly representedy by its width and coordinates of the animals.
type board =
  {width : int;
   mutable moose : moose list;
   mutable wolves : wolf list;}

/// An environment is a chess-like board with all animals and implenting all rules.
type environment (boardWidth : int, NMooses : int, mooseRepLen : int, NWolves : int, wolvesRepLen : int, wolvesHungLen : int, verbose : bool) =
  let _board : board = {
    width = boardWidth;
    moose = List.init NMooses (fun i -> moose(mooseRepLen));
    wolves = List.init NWolves (fun i -> wolf(wolvesRepLen, wolvesHungLen));
  }

  /// Project the list representation of the board into a 2d array.
  let draw (b : board) : char [,] =
    let arr = Array2D.create<char> boardWidth boardWidth eSymbol
    for m in b.moose do
      Option.iter (fun p -> arr.[fst p, snd p] <- mSymbol) m.position
    for w in b.wolves do
      Option.iter (fun p -> arr.[fst p, snd p] <- wSymbol) w.position
    arr

  /// return the coordinates of any empty field on the board.
  let anyEmptyField (b : board) : position =
    let arr = draw b
    let mutable i = rnd.Next b.width
    let mutable j = rnd.Next b.width
    while arr.[i,j] <> eSymbol do
      i <- rnd.Next b.width
      j <- rnd.Next b.width
    (i,j)

  // Shuffels moose and wolf list
  let shuffleList (xs : 'a list) : 'a list =
    let listToArr = List.toArray xs
    let swap (a: _[]) x y =
      let tmp = a.[x]
      a.[x] <- a.[y]
      a.[y] <- tmp
    Array.iteri (fun i _ -> swap listToArr i (rnd.Next(i, Array.length listToArr))) listToArr
    Array.toList listToArr

  // populate the board with animals placed at random.
  do for m in _board.moose do
       m.position <- Some (anyEmptyField _board)
  do for w in _board.wolves do
       w.position <- Some (anyEmptyField _board)

  // Finds free postitions around an animal
  member this.upperCaster (ani : 'a) : animal = ani :> animal
  member this.givePos ((x, y): position) (ani : animal) (charArray : char [,])= 
    let mutable noAvailablePosition = true
    let mutable positions = []
    for i = x-1 to x+1 do 
      for j = y-1 to y+1 do
        if i > -1 && i < this.board.width && j > -1 && j < this.board.width && not(i = x && j = y) then
          if charArray.[i, j] = eSymbol then 
            positions <- Some (i, j) :: positions
            noAvailablePosition <- false
    if noAvailablePosition then ani.position <- None
    else
      let i = rnd.Next(0, positions.Length)
      ani.position <- positions.[i]
    
      
  member this.size = boardWidth*boardWidth
  member this.count = _board.moose.Length + _board.wolves.Length
  member this.board = _board
  member this.tick () = 
    let w = this.board.wolves.[0]
    let m = this.board.moose.[0]
    
    () // Intentionally left blank. Insert code that process animals here.

  override this.ToString () =
    let arr = draw _board
    let mutable ret = "  "
    for j = 0 to _board.width-1 do
      ret <- ret + string (j % 10) + " "
    ret <- ret + "\n"
    for i = 0 to _board.width-1 do
      ret <- ret + string (i % 10) + " "
      for j = 0 to _board.width-1 do
        ret <- ret + string arr.[i,j] + " "
      ret <- ret + "\n"
    ret

