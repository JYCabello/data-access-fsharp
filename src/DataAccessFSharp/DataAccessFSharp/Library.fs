open System
open System.Data.SqlClient
open Dapper

[<CLIMutable>]
type Person =
  { ID: int
    LastName: string
    FirstName: string }

[<CLIMutable>]
type PersonInsert = { LastName: string; FirstName: string }

let connstring =
  "Server=localhost,1433;Database=datafsharp;User Id=sa;Password=abcdABCD1234;"

let conn = new SqlConnection(connstring)

let anonInsertSql = "INSERT INTO People (LastName, FirstName) VALUES (@a, @b)"

conn.Execute(
  anonInsertSql,
  {| A = Guid.NewGuid().ToString()
     B = Guid.NewGuid().ToString() |}
)
|> ignore

let typedInsertSql =
  "INSERT INTO People (LastName, FirstName) VALUES (@lastName, @firstName)"

conn.Execute(
  typedInsertSql,
  { LastName = Guid.NewGuid().ToString()
    FirstName = Guid.NewGuid().ToString() }
)
|> ignore

let sql = "SELECT TOP 6 ID, LastName, FirstName FROM People ORDER BY ID DESC"
let people = conn.Query<Person> sql

people |> Seq.iter Console.WriteLine
