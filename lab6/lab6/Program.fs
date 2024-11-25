open System
// Podstawowe operacje na łańcuchach znaków

// Tworzenie i łączenie łańcuchów
let str1 = "Hello"
let str2 = "World"
let combined = str1 + " " + str2 
printf "%s" combined // "Hello World"

//Interpolacja
let name = "John"
let message = $"Hello, {name}!" 
printf "\n%s" message // "Hello, John!"

//Operacje na znakach 
let firstChar = str1.[0]         // 'H'
let upperStr = str1.ToUpper()    // "HELLO"
let lowerStr = str2.ToLower()    // "world"

printf "\n%s" upperStr
printf "\n%s" lowerStr

// Dzielenie łańcucha
let sentence = "F# is great"
let words = sentence.Split ' '  // [|"F#"; "is"; "great"|]

// Zamiana tekstu
let replaced = sentence.Replace("great", "awesome") // "F# is awesome"
printf "\n%s" replaced


//Zaawansowane manipulacje tekstowe
//Filtrowanie i przekształcanie tekstu
let sentences = ["F# is great"; "I do not love functional programming"; "F# is powerful"]
let filtered = sentences |> List.filter (fun s -> s.Contains "F#")
// ["F# is great"; "F# is powerful"]

//Liczenie słów
let wordCount (text: string) =
    text.Split([|' '; '.'; ','; '\n'|], System.StringSplitOptions.RemoveEmptyEntries)
    |> Array.length

let count = wordCount "F# is fun. F# is also powerful." // 7

//Tworzenie nowego tekstu
let joinedText = String.Join(", ", ["apple"; "banana"; "cherry"])
// "apple, banana, cherry"


//Parsowanie danych z tekstu
//Parsowanie liczb z łańcucha znaków
let tryParseInt (input: string) =
    match System.Int32.TryParse(input) with
    | (true, value) -> Some value
    | _ -> None

let number = tryParseInt "123" // Some 123
let invalid = tryParseInt "abc" // None

//Parsowanie CSV
let parseCsv (line: string) =
    line.Split ',' |> Array.toList

let csvLine = "John,Doe,30"
let parsed = parseCsv csvLine // ["John"; "Doe"; "30"]