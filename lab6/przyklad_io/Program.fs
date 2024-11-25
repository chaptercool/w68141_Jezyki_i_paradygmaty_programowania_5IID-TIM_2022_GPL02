open System.IO

//Odczyt z pliku
//Odczyt całego pliku jako jednego łańcucha tekstu

let readFileAsText (filePath: string) =
    File.ReadAllText(filePath)

let content = readFileAsText "example.txt"
printfn "Content: %s" content

//Odczyt pliku linia po linii
let readFileAsLines (filePath: string) =
    File.ReadAllLines(filePath) |> Array.toList

let lines = readFileAsLines "example.txt"
lines |> List.iter (printfn "Line: %s")

//Odczyt pliku w trybie sekwencji (lazy loading)
let readFileAsSeq (filePath: string) =
    File.ReadLines(filePath)

let seqLines = readFileAsSeq "example.txt"
seqLines |> Seq.iter (printfn "Line: %s")


// Zapis do pliku
//Zapis łańcucha tekstu do pliku

let writeTextToFile (filePath: string) (content: string) =
    File.WriteAllText(filePath, content)

writeTextToFile "output.txt" "Hello, F# World!"

//Zapis listy linii do pliku
let writeLinesToFile (filePath: string) (lines: string list) =
    File.WriteAllLines(filePath, lines |> List.toArray)

writeLinesToFile "output.txt" ["Line 1"; "Line 2"; "Line 3"]


//Dopisywanie do istniejącego pliku
let appendTextToFile (filePath: string) (content: string) =
    File.AppendAllText(filePath, content + System.Environment.NewLine)

appendTextToFile "output.txt" "This is a new line."

//Dopisywanie listy linii na końcu pliku
let appendLinesToFile (filePath: string) (lines: string list) =
    File.AppendAllLines(filePath, lines |> List.toArray)

appendLinesToFile "output.txt" ["Another line"; "Yet another line"]


//Przykład kompleksowy: kopiowanie pliku linia po linii
let copyFile (sourcePath: string) (destinationPath: string) =
    let lines = File.ReadLines(sourcePath) // Odczyt sekwencji linii
    File.WriteAllLines(destinationPath, lines |> Seq.toArray) // Zapis do nowego pliku

copyFile "example.txt" "copy_of_example.txt"

//Obsługa błędów
let safeReadFile (filePath: string) =
    try
        Some (File.ReadAllText(filePath))
    with
    | :? System.IO.FileNotFoundException ->
        printfn "File not found: %s" filePath
        None
    | ex ->
        printfn "An error occurred: %s" ex.Message
        None

match safeReadFile "example.txt" with
| Some content -> printfn "File content: %s" content
| None -> printfn "Failed to read file."
