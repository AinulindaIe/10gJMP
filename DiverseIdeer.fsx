let mutable action = "Default"
    this.updateReproduction ()
    this.updateHunger ()

    match this.position with
    | Some pos -> 
      match m with
      | Some m  -> if this.reproduction = 0 then action <- "Reproduce" else action <- "Eat"
      | None    ->
        match p with
        | Some p  -> if this.reproduction = 0 then action <- "Reproduce" else action <- "Move"
        | None    -> action <- "Move"
    | None -> action <- "Die"

    match action with
    | "Reproduce" -> 
      this.resetReproduction ()
      match p with
      | Some p  -> 
        let w = new wolf(repLen, hungLen)
        w.position <- Some p
        Some w
      | None    -> None
    | "Eat" -> 
      match m with
      | Some m -> 
        this.position <- m.position
        this.resetHunger ()
        m.position <- None
        None
      | None -> 
        printfn "eating went wrong"
        None
    | "Move" -> 
      match p with
      | Some p -> 
        this.position <- Some p
        None
      | None   -> None
    | "Die" -> None
    | _ -> 
      printfn "wolf action went wrong"
      None



this.updateReproduction ()
    let mutable action = "Default"
    match p with
    | Some p  -> if this.reproduction = 0 then action <- "Reproduce" else action <- "Move"
    | None    -> action <- "Fail reproduction"

    match action with
    | "Reproduce"         -> 
      this.resetReproduction ()
      let m = new moose(repLen)
      m.position <- p
      Some m
    | "Move"              ->
      this.position <- p
      None
    | "Fail reproduction" ->
      this.resetReproduction () 
      None
    | _ -> 
      printfn "moose action failed"
      None


let availablePos (p : position option) (a : 'a) (b : board) = 
      p
    let uppercasting (ani : 'a) : animal =
      ani :> animal

    match m.tick() with
    | Some moo -> 
      match availablePos m.position with
      | Some p -> 
        moo.position <- Some p
        this.board.moose <- moo :: this.board.moose
      | None        ->
        printfn "No available position to breed"
    | None     -> printfn "updated"

    let wo = w.tick()
    match w.position with
    | Some p  ->
      match wo with
      | Some wol  ->
        this.board.wolves <- wol :: this.board.wolves
      | None      ->
    | None    -> printf "wolf died"