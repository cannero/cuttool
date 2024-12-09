module Commandline
    open System

    // Which fields should be printed, 1-based
    type FieldType =
        | Of of field : int
        | From of field : int
        | To of field : int
        | FromTo of fieldFrom : int * fieldTo : int

    type CommandLineOptions = {
        fields: FieldType list
        delimiter: string
        filename: string
    }

    let getField (field: string) =
        let field = field.Trim()
        if field.StartsWith('-') then
            To(field = max 1 (field.Substring(1) |> int))
        elif field.EndsWith('-') then
            From(field = max 1 ((field.Substring(0, (String.length field - 1))) |> int))
        elif field.Contains('-') then
            let parts = field.Split('-')
            FromTo(fieldFrom = max 1 (parts.[0] |> int), fieldTo = max 1 (parts.[1] |> int))
        else
            Of(field = max 1 (field |> int))

    let getFields (fieldString: string) =
        let fields =
            if fieldString.Contains(',') then
                fieldString.Split(',', StringSplitOptions.RemoveEmptyEntries)
            else
                fieldString.Split(' ', StringSplitOptions.RemoveEmptyEntries)
        fields
        |> Array.toList
        |> List.map getField

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
            fields = [Of(1)]
            delimiter = "\t"
            filename = ""
        }

        parseCmdLineRec (Array.toList args) defaultOptions
