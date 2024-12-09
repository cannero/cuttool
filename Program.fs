open Commandline

let getContent file =
    System.IO.File.ReadLines(file)

let getContentStdin () =
    fun _ -> stdin.ReadLine()
    |> Seq.initInfinite
    |> Seq.takeWhile ((<>) null)

let getField (fields: string array) fieldType =
    let len = Array.length fields
    match fieldType with
        | Of(field) ->
            if len > field-1 then
                seq { fields.[field-1] }
            else
                seq {""}
        | To(field) ->
            seq { 0 .. (min field (len - 1)) }
            |> Seq.map (fun i -> fields.[i])
        | From(field) ->
            seq { field - 1 .. len - 1 }
            |> Seq.map (fun i -> fields.[i])
        | FromTo(fieldFrom, fieldTo) ->
            seq { fieldFrom - 1 .. (min (len - 1) fieldTo) }
            |> Seq.map (fun i -> fields.[i])

let getFields options (line:string) =
    let fields = line.Split options.delimiter
    options.fields
    |> List.map (getField fields)
    |> Seq.concat

let printFields options getFunc =
    getFunc
    |> Seq.map (getFields options)
    |> Seq.map (String.concat options.delimiter)
    |> Seq.iter (printfn "%s")

[<EntryPoint>]
let main(args) =    
    let options = parseCmdLine args
    printfn "%A" options
    match options.filename with
        | "" -> printFields options (getContentStdin ())
        | _ -> printFields options (options.filename |> getContent)
    0
