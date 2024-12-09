open Commandline

let getContent file =
    System.IO.File.ReadLines(file)

let getContentStdin () =
    fun _ -> stdin.ReadLine()
    |> Seq.initInfinite
    |> Seq.takeWhile ((<>) null)

let getField fields field =
    if Array.length fields > field-1 then
        fields.[field-1]
    else
        ""


let getFields options (line:string) =
    let fields = line.Split options.delimiter
    options.fields |> List.map (getField fields)


let printFields options getFunc =
    getFunc
    |> Seq.map (getFields options)
    |> Seq.iter (printfn "%A")

[<EntryPoint>]
let main(args) =    
    let options = parseCmdLine args
    printfn "%A" options
    match options.filename with
        | "" -> printFields options (getContentStdin ())
        | _ -> printFields options (options.filename |> getContent)
    0
