[<EntryPoint>]
let main (args: string array) =
  let boardWidth    = int args.[0]
  let NMooses       = int args.[1]
  let mooseRepLen   = int args.[2]
  let NWolves       = int args.[3]
  let wolvesRepLen  = int args.[4]
  let wolvesHungLen = int args.[5]
  // ? Maybe make this as an if/else to take care of the situation where the verbose isn't given as an agurment 
  let verbose       = int args.[6]
  let calc x y = x + y

  printfn "Chosen boardwidht %d" boardWidth
  printfn "Chosen NMooses %d" NMooses
  printfn "Chosen mooseRepLen %d" mooseRepLen
  printfn "Chosen NWolves %d" NWolves
  printfn "Chosen wolvesRepLen %d" wolvesRepLen
  printfn "Chosen wolvesHungLen %d" wolvesHungLen
  printfn "Chosen verbose %d" verbose
  printfn "Total number of animals %d" (calc NMooses NWolves)
  0