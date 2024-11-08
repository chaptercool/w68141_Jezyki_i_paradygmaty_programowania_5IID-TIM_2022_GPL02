open System
open System.Globalization

let kursy = Map[("USD", 1.0);("EUR", 1.08); ("CHF", 0.86); ("GBP", 1.30)]

let pobieranie (zrodlo: string) (docelowe: string) =
    match kursy.TryFind(zrodlo), kursy.TryFind(docelowe) with
    | Some(z), Some(d) -> z / d
    | _ -> 0.0

let konwertacja kwota zrodlo docelowe =
    match pobieranie zrodlo docelowe with
    | kurs -> Some (kwota * kurs)
    | _ -> None


[<EntryPoint>]
let main argv = 
    printf "Podaj kwotę: "
    let kwota = Console.ReadLine() |> fun input -> Double.Parse(input, CultureInfo.InvariantCulture)

    printfn "Dostępne opcję do wymiany: USD, EUR, CHF, GBP"

    printf "Podaj walutę źródłową: "
    let src = Console.ReadLine().ToUpper()

    printf "Podaj walutę docelową: "
    let dst = Console.ReadLine().ToUpper()

    match konwertacja kwota src dst with
    | Some(kwota) -> printfn "Wynik operacji: %.2f" kwota
    | None -> printfn "Error!"

    0