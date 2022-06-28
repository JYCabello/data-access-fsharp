// For more information see https://aka.ms/fsharp-console-apps
open System
open System.Reflection
open DbUp

let connstring =
  "Server=localhost,1433;Database=datafsharp;User Id=sa;Password=abcdABCD1234;"

let upgrader =
  DeployChanges
    .To
    .SqlDatabase(connstring)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .LogToConsole()
    .Build()

let result = upgrader.PerformUpgrade()

match result.Successful with
| true -> "All good"
| false -> result.Error.Message
|> Console.WriteLine
