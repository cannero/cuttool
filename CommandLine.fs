module Commandline
    open System

    type CommandLineOptions = {
        fields: int list
        delimiter: string
        filename: string
    }

    let getFields (fieldString: string) =
        let fields =
            if fieldString.Contains(',') then
                fieldString.Split(',', StringSplitOptions.RemoveEmptyEntries)
            else
                fieldString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
        fields
        |> Array.toList
        |> List.map int

    let rec parseCmdLineRec args options =
        match args with
            | [ x ] -> { options with filename = x }
            | e :: xs when e.StartsWith("-f") ->
                if String.length e > 2 then
                    parseCmdLineRec xs { options with fields = e.Substring(2) |> getFields }
                else
                    match xs with
                        | num :: xss ->
                            parseCmdLineRec xss { options with fields = num |> getFields }
                        | _ ->
                            failwith "missing num"
            | e :: xs when e.StartsWith("-d") ->                        
                if String.length e > 2 then
                    parseCmdLineRec xs { options with delimiter = e.Substring(2) }
                else
                    match xs with
                        | del :: xss ->
                            parseCmdLineRec xss { options with delimiter = del }
                        | _ ->
                            failwith "missing delimiter"

            | _ -> options

    let parseCmdLine args =
        let defaultOptions = {
            fields = [1]
            delimiter = "\t"
            filename = ""
        }

        parseCmdLineRec (Array.toList args) defaultOptions
