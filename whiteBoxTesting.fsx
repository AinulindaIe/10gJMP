open animals
open System.Runtime.InteropServices
open System.Net

// Test af animal og moose funktioner
type mooseTester(_repLen:int) =
    let repLen = _repLen
    member this.testUpdateReproduction() = 
        let animalToTest = new moose(repLen)
        let tempReproductionVal = animalToTest.reproduction
        animalToTest.updateReproduction()
        printfn "Test for updateReproduction"
        printfn "Equality to repLen minus 1: %b" ((tempReproductionVal - 1) = animalToTest.reproduction)
    member this.testResetReproduction() = 
        let animalToTest = new moose(repLen)
        animalToTest.resetReproduction()
        printfn "Test for resetReproduction"
        printfn "Equality to repLen: %b" (animalToTest.reproduction = repLen)
    member this.testMooseTick() =
        let animalToTest = new moose(repLen)
        let mutable iter = 0
        let mutable born = false
        printfn "Test for mooseTick"
        while iter <= repLen && born = false do
          if not (animalToTest.tick() = None) then
            born <- true
            printfn "Animal born: %A" born
            iter <- iter + 1
          else 
            printfn "Animal born: %A" born
            iter <- iter + 1
          
// test af animal og wolf metoder
type wolfTester (_hungLen:int) = 
    let hungLen = _hungLen
    let repLen = 10
    member this.testUpdateHunger() =
      let wolfToTest = new wolf(repLen, hungLen)
      wolfToTest.updateHunger()
      printfn "Test for updateHunger"
      printfn "Equality to hungLen minus 1: %b" (wolfToTest.hunger = hungLen - 1)
    member this.testResetHunger() =
      let wolfToTest = new wolf(repLen, hungLen)
      wolfToTest.updateHunger()
      wolfToTest.resetHunger()
      printfn "Test for resetHunger"
      printfn "Hunger is equal to hungLen: %b" (wolfToTest.hunger = hungLen)
    member this.testWolfTick() =
      let wolfToTest = new wolf(repLen, hungLen)
      let mutable iter = 0
      let mutable born = false
      printfn "Test for wolfTick"
      while iter <= repLen && born = false do
        if not (wolfToTest.tick() = None) then
          born <- true
          printfn "Wolf born: %b" born
          iter <- iter + 1
        else
          printfn "Wolf is born: %b" born
          iter <- iter + 1

// Test af environment funktioner og metoder
type environmentTester(i:int) =
  member this.testGivePos() = 
    let environmentToTest = new environment(3,2,10,2,10,10,false)   
    let testArr = Array2D.create<char> (environmentToTest.board.width) (environmentToTest.board.width) eSymbol
    testArr.[2,0] <- mSymbol
    printfn "%A" testArr
    printfn "Test for givePos"
    printfn "Finder symboler på plads: %A" (environmentToTest.givePos(1,1) testArr mSymbol)
  member this.testMooseMethod() =
    let pos = Some (1,1)
    let mooseToTest = new moose(10)
    mooseToTest.position <- pos
    let environmentToTest = new environment(3,2,10,2,10,10,false)
    let filledArr = Array2D.create<char> (environmentToTest.board.width) (environmentToTest.board.width) mSymbol
    printfn "%A" filledArr    
    environmentToTest.mooseMethod mooseToTest filledArr // returns "no available position"
    // Tests when array not filled
    let emptyArr = Array2D.create<char> (environmentToTest.board.width) (environmentToTest.board.width) eSymbol
    environmentToTest.mooseMethod mooseToTest emptyArr
    printfn "Test for mooseMethod"
    printfn "Moose has moved when array is empty: %b" (mooseToTest.position <> pos)
    

// Test for moose and animal methods
let mooseIsTest = mooseTester(10)
mooseIsTest.testUpdateReproduction()
mooseIsTest.testResetReproduction()
// testMooseTick() outputter mange linjer i terminalen
mooseIsTest.testMooseTick()
 
let wolfIsTest = wolfTester(10)
wolfIsTest.testUpdateHunger()
wolfIsTest.testResetHunger()
// testWolfTick outputter mange linjer i terminalen
wolfIsTest.testWolfTick()

let environmentIsTest = environmentTester(10)
// testGivePos outputter en tabel i terminalen
environmentIsTest.testGivePos()
environmentIsTest.testMooseMethod()

 
 