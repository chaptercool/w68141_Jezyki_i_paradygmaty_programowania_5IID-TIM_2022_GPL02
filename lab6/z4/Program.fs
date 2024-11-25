open System

let transformator (entry: string) =
    let czesci = entry.Split(';')
    if czesci.Length = 3 then
        let imie = czesci.[0].Trim()
        let nazwisko = czesci.[1].Trim()
        let wiek = czesci.[2].Trim()
        sprintf "%s %s ma %s lat" nazwisko imie wiek
    else
        "Nie znam takich..............................."

[<EntryPoint>]
let main argv =
    printfn "Wpisz dane rodzielając przez ';'"
    printfn "Po zakończeniu, wpisz pustą linię: "

    let rec wczytac acc =
        let input = Console.ReadLine()
        if String.IsNullOrWhiteSpace(input) then
            acc
        else
            wczytac (input :: acc)

    let dane = wczytac []
    let przeksztalcone = dane |> List.rev |> List.map transformator
    printfn "Mam znajomych:"
    przeksztalcone |> List.iter (printfn "%s")
    0