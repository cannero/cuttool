open Commandline

let getContent file =
    System.IO.File.ReadLines(file)

let getField options (line:string) =
    let fields = line.Split options.delimiter
    if Array.length fields > options.field-1 then
        fields.[options.field-1]
    else
        ""

let printFields options =
    options.filename
    |> getContent
    |> Seq.map (getField options)
    |> Seq.iter (printfn "%s")

[<EntryPoint>]
let main(args) =    
    let options = parseCmdLine args
    printfn "%A" options
    match options.filename with
        | "" -> failwith "missing file"
        | _ -> printFields options
    0
