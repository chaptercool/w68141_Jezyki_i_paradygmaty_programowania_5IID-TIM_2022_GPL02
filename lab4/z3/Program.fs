open System

let slowniczek (kawalek: String) = 
    let slowa = kawalek.Split([|'.'; ' '; ','|])
    printfn "Ilość słów: %d" slowa.Length

    let znaki = Seq.filter (fun c -> not (c = ' ' || c = ',' || c = '.' || c = '?')) kawalek
    printfn "Ilość znaków: %d" (Seq.length(znaki))

    let czesteslowo (input: string) =
        let slowa = 
            input.Split([| ' '; ','; '.'; '?' |], StringSplitOptions.RemoveEmptyEntries)
            |> Seq.map (fun slowo -> slowo.ToLower())

        let liczslowo = 
            slowa
            |> Seq.countBy id
            |> Seq.maxBy snd
        liczslowo

    let slowko, liczba = czesteslowo kawalek
    printfn "Najczęściej występujące słowo to: %s, występuję %d raz(y)" slowko liczba

[<EntryPoint>]
let main argv = 
    let zdanie = "Za najważniejsze projekty Stowarzyszenia Podkarpacka Dolina Wodorowa jego prezes uznał powstałe w 2023 roku laboratorium wodorowe Politechniki Rzeszowskiej, którego pełna nazwa Laboratorium Badań Materiałów dla Przemysłu Lotniczego określa, na rzecz jakiego przemysłu ono pracuje. W laboratorium zainwestowano do tej pory 6 mln zł. Kolejne miliony na rozwój jednostki są w drodze z Unii Europejskiej."
    slowniczek zdanie
    0