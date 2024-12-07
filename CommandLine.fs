module Commandline
    type CommandLineOptions = {
        field: int
        delimiter: string
        filename: string
    }

    let rec parseCmdLineRec args options =
        match args with
            | [ x ] -> { options with filename = x }
            | e :: xs when e.StartsWith("-f") ->
                if String.length e > 2 then
                    parseCmdLineRec xs { options with field = e.Substring(2) |> int }
                else
                    match xs with
                        | num :: xss ->
                            parseCmdLineRec xss { options with field = num |> int }
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
            field = 1
            delimiter = "\t"
            filename = ""
        }

        parseCmdLineRec (Array.toList args) defaultOptions
