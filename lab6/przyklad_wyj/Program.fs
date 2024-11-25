open System
open System.Net.WebRequestMethods
//Obsługa wyjątków
//Try...with

//W F# używamy bloku try...with, aby przechwycić wyjątki:

let divide x y =
    try
        x / y
    with
    | :? System.DivideByZeroException ->
        printfn "Error: Division by zero."
        0
    | ex ->
        printfn "Unexpected error: %s" ex.Message
        0

let result = divide 10 0  // Wypisze: Error: Division by zero.


//Typy wyników: Result

// Typ Result<'T, 'Error> jest funkcjonalnym podejściem do obsługi błędów.

let safeDivide x y =
    if y = 0 then
        Error "Division by zero"
    else
        Ok (x / y)

match safeDivide 10 0 with
| Ok result -> printfn "Result: %d" result
| Error msg -> printfn "Error: %s" msg

//Łączenie operacji z Result.bind
let parseInt (input: string) =
    match System.Int32.TryParse(input) with
    | (true, value) -> Ok value
    | _ -> Error "Invalid number"

let safeDivision (x: string) (y: string) =
    parseInt x
    |> Result.bind (fun numX ->
        parseInt y
        |> Result.bind (fun numY -> safeDivide numX numY))

match safeDivision "10" "0" with
| Ok result -> printfn "Result: %d" result
| Error msg -> printfn "Error: %s" msg


//Typy opcjonalne: Option

//Typ Option<'T> reprezentuje wartość, która może być obecna (Some) lub brakująca (None).
let safeParseInt (input: string) =
    match System.Int32.TryParse(input) with
    | (true, value) -> Some value
    | _ -> None

match safeParseInt "123" with
| Some value -> printfn "Parsed: %d" value
| None -> printfn "Invalid input"


//Łączenie operacji z Option.bind
let divideOption x y =
    if y = 0 then None else Some (x / y)

let safeDivision (x: string) (y: string) =
    safeParseInt x
    |> Option.bind (fun numX ->
        safeParseInt y
        |> Option.bind (fun numY -> divideOption numX numY))

match safeDivision "10" "2" with
| Some result -> printfn "Result: %d" result
| None -> printfn "Error in computation"


//Własne wyjątki

//Możesz definiować własne wyjątki za pomocą exception:

exception CustomError of string

let riskyOperation x =
    if x < 0 then
        raise (CustomError "Negative value not allowed")
    else
        x * 2

try
    printfn "Result: %d" (riskyOperation -1)
with
| CustomError msg -> printfn "Caught custom error: %s" msg

//Obsługa błędów w operacjach IO

//Podczas pracy z plikami lub innymi operacjami IO warto obsługiwać typowe błędy:
open System.IO

let readFileSafe (filePath: string) =
    try
        let content = File.ReadAllText(filePath)
        Ok content
    with
    | :? FileNotFoundException -> Error "File not found"
    | :? UnauthorizedAccessException -> Error "Access denied"
    | ex -> Error $"Unexpected error: {ex.Message}"

match readFileSafe "example.txt" with
| Ok content -> printfn "File content: %s" content
| Error msg -> printfn "Error: %s" msg
