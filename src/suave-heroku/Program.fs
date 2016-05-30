// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System
open Suave
open Suave.Operators
open Suave.Filters

[<EntryPoint>]
let main argv =
    let website = 
        choose [
            GET >=> path "/" >=> Successful.OK "root"
            ] 
    let conf = { defaultConfig with bindings = [ HttpBinding.mk HTTP Net.IPAddress.Any 8083us ] }

    startWebServer conf website

    0 // return an integer exit code

