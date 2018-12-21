open  animals
open System.IO

[<EntryPoint>]
let main (args: string array) =
  let T     = int args.[0]  // Ticks
  let fPath =     args.[1]  // FilePath (if only filename default folder is used)
  let n     = int args.[2]  // Boardwidth
  let e     = int args.[3]  // # Mooses
  let felg  = int args.[4]  // Moose reproduction length
  let u     = int args.[5]  // # Wolves
  let fulv  = int args.[6]  // Wolf reproduction length
  let s     = int args.[7]  // Hunger length

  let plotify (lst) : string =
    let mutable moosestr = ""
    let mutable wolfstr = ""
    let mutable i = 0
    for elem in lst do
      moosestr <- moosestr  + (sprintf "(%d,%d)" (i+1) (fst elem))
      wolfstr  <- wolfstr   + (sprintf "(%d,%d)" (i+1) (snd elem))
      i <- i+1

    "\t\\begin{tikzpicture}\n "                                                                            +
    "\t\t\\begin{axis}[xmin = 0, xmax = 51, ymin = 0, ymax = 200, xlabel=tick, ylabel=animals]\n"          +
    (sprintf "\t\t\t\\addplot[color=brown] coordinates {%s};\n\t\t\t\\addlegendentry{Moose}\n" moosestr)   +
    (sprintf "\t\t\t\\addplot[color=red]   coordinates {%s};\n\t\t\t\\addlegendentry{Wolf}\n" wolfstr)     +
    "\t\t\\end{axis}\n\t\\end{tikzpicture}\n"

  let mutable lst = []
  let mutable str = ""

  let envi = new environment(n, e, felg, u, fulv, s, false)
  lst <- []
  for k = 0 to T do
    let m : int = envi.board.moose.Length
    let w : int = envi.board.wolves.Length
    lst <- List.append lst [(m, w)]
    envi.tick()
  str <- str + "\n" + (plotify lst)
  //Define the filepath to the file in which to save
  let sw = new StreamWriter(fPath)
  sw.Write("\\documentclass{article}\n\\usepackage{tikz}\n\\usepackage{pgfplots}\n\\begin{document}\n")
  sw.Write(str)
  sw.Write("\\end{document}")
  sw.Close()

  0
