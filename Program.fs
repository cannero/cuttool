open Commandline

let getContent file =
    System.IO.File.ReadLines(file)

let getField nField (line:string) =
    let fields = line.Split '\t'
    if Array.length fields > nField then
        fields.[nField]
    else
        ""

let printFields options =
    options.filename
    |> getContent
    |> Seq.map (getField options.field)
    |> Seq.iter (printfn "%s")

[<EntryPoint>]
let main(args) =    
    let options = parseCmdLine args
    printfn "%A" options
    match options.filename with
        | "" -> failwith "missing file"
        | _ -> printFields options
    0
