open System

let loteria (input: string) =
    let slowa = input.Split([|' '; '\t'; '\n'; ','; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    if slowa.Length > 0 then
        let najdluzsze = slowa |> Array.maxBy (fun x -> x.Length)
        (najdluzsze, najdluzsze.Length)
    else
        ("", 0)

[<EntryPoint>]
let main argv =
    printfn "Czekam na słówka........"
    let input = Console.ReadLine()
    let (slowo, dlugosc) = loteria input
    if dlugosc > 0 then
        printfn "To jest najdłuższe słowo: %s, %d znaków" slowo dlugosc
    else
        printfn "Nie ma słów......................................napewno"
    0