open System

let ileSlow (text: string) =
    text.Split([|' '; '.'; ','; '\n'|], StringSplitOptions.RemoveEmptyEntries).Length

let ileZnakow (text: string) =
    Seq.filter (fun c -> not (Char.IsWhiteSpace(c))) text
    |> Seq.length

[<EntryPoint>]
let main argv =
    printf "Podaj tekst.......... "
    let tekst = Console.ReadLine()

    let slowa = ileSlow tekst
    let znaki = ileZnakow tekst

    printfn "Liczba słów: %d" slowa
    printfn "Liczba znaków: %d" znaki

    0