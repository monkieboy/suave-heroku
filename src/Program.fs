// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open System
open Suave
open Suave.Operators
open Suave.Filters

let home = """<!doctype html>
<html>
    <head>
        <meta charset="utf-8">
        <script src="https://cdn.auth0.com/js/lock-9.0.min.js"></script>
        <script src="//use.typekit.net/iws6ohy.js"></script>
        <script>try{Typekit.load();}catch(e){}</script>
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- font awesome from BootstrapCDN -->
        <link href="https://netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css" rel="stylesheet">
        <link href="http://netdna.bootstrapcdn.com/font-awesome/4.0.3/css/font-awesome.css" rel="stylesheet">
        <script src="auth0-variables.js"> </script>
        <link href="app.css" rel="stylesheet">
    </head>
    <body class="home">
        <div class="container">
            <div class="login-page clearfix">
              <div id="login-box" class="login-box auth0-box before">
                <img src="https://i.cloudup.com/StzWWrY34s.png" />
                <h3>Auth0 Example</h3>
                <p>Zero friction identity infrastructure, built for developers</p>
                <a id="btn-login" class="btn btn-primary btn-lg btn-block">Log in</a>
              </div>
              <div id="logged-in-box" class="logged-in-box auth0-box logged-in" style="display: none;">
                <h1 id="logo"><img src="auth0_logo_final_blue_RGB.png" /></h1>
                <h2>Welcome <span id="nick" class="nickname"></span></h2>
                <button id="btn-api" class="btn btn-lg btn-primary btn-api">Call API</button>
              </div>
            </div>
        </div>
        <script src="app.js"> </script>
    </body>
</html>"""

let homePage = 
    [ yield """<html>"""
      yield """ <head><title>test</title></head>"""
      yield """ <body>"""
      yield """ <h1>Sample Web App</h1>"""
      yield """  <table class="table table-striped">"""
      yield """   <thead><tr><th>Page</th><th>Link</th></tr></thead>"""
      yield """   <tbody>"""
      yield """      <tr><td>Endangered Animals</td><td><a href="/animals">Link to animals</a></td></tr>""" 
      yield """      <tr><td>Things</td><td><a href="/things/10">Link to things (10)</a></td></tr>""" 
      yield """      <tr><td>Things</td><td><a href="/things/100">Link to things (100)</a></td></tr>""" 
      yield """      <tr><td>API JSON</td><td><a href="/api/json/100">Link to result (100)</a></td></tr>"""
      yield """      <tr><td>API XML</td><td><a href="/api/xml/100">Link to result (100)</a></td></tr>"""
      yield """      <tr><td>API JSON</td><td><a href="/api/json/10">Link to result (10)</a></td></tr>"""
      yield """      <tr><td>API XML</td><td><a href="/api/xml/10">Link to result (10)</a></td></tr>"""
      yield """      <tr><td>Goodbye</td><td><a href="/goodbye">Link</a></td></tr>"""
      yield """   </tbody>"""
      yield """  </table>"""
      yield """ </body>""" 
      yield """</html>""" ]
    |> String.concat "\n"

[<EntryPoint>]
let main args =
    let port = UInt16.Parse <| (if (args |> Array.length > 0) then args.[0] else Environment.GetEnvironmentVariable("PORT"))
    let website = 
        choose [
            GET >=> path "/" >=> Successful.OK "Home"
            GET >=> path "/home" >=> Successful.OK homePage
            GET >=> path "/callback" >=> Successful.OK "callback"
            GET >=> path "/logout" >=> Successful.OK "logout"
            GET >=> path "/test" >=> Successful.OK "test"
            Files.browseHome ] 
    let conf = { defaultConfig with bindings = [ HttpBinding.mk HTTP Net.IPAddress.Any port ] }

    startWebServer conf website

    0 // return an integer exit code

