open System

let RemoveDuplic (words: string list) =
    words |> List.distinct

[<EntryPoint>]
let main argv =
    printf "Daj słowa.............."
    let input = Console.ReadLine()
    if String.IsNullOrWhiteSpace(input) then
        printfn "Puste!!!!!!!!!!!!!"
    else
        let slowa = input.Split([|' '|], StringSplitOptions.RemoveEmptyEntries) |> Array.toList
        let wynik = RemoveDuplic slowa
        printfn "Unikatowe: %A" wynik
    0