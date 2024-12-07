module Commandline
    type CommandLineOptions = {
        field: int
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
                            eprintfn "missing num"
                            failwith "missing num"
            | _ -> options

    let parseCmdLine args =
        let defaultOptions = {
            field = 1
            filename = ""
        }

        parseCmdLineRec (Array.toList args) defaultOptions
