let get_content file =
    System.IO.File.ReadLines(file)

let get_field (line:string) =
    let fields = line.Split '\t'
    if Array.length fields > 2 then
        fields.[1]
    else
        ""

[<EntryPoint>]
let main(args) =    
    printfn "args: %A" args
    Array.last args
    |> get_content
    |> Seq.map get_field
    |> Seq.iter (printfn "%s")
    0
