open System

type User = {
    Height: float
    Weight: float
}

let bmi (user: User) = //funkcja która przyjmuje typ danych User
    let HeightM = user.Height / 100.0
    let bmi = user.Weight / (HeightM ** 2)
    bmi //nie pisac return

let getBMICat bmi = 
   match bmi with
    | x when x < 18.5 -> "Skinny legend"
    | x when x >= 18.5 && x < 24.9 -> "ok"
    | x when x >= 25.0 && x < 29.9 -> "Do siłowni na szybko"
    | _ -> "???"

[<EntryPoint>] //main
let main argv =
    printf "podaj wagę w kg......."
    let weightIn = Console.ReadLine()
    printf "podaj wzrost w cm......."
    let heightIn = Console.ReadLine()

    let weight =
        match Double.TryParse(weightIn) with
        | (true, value) -> value
        | _ -> printfn "haj arabisz"; 0.0

    let height =
        match Double.TryParse(heightIn) with
        | (true, value) -> value
        | _ -> printfn "haj arabisz tylko wzorst"; 0.0

    if weight > 0.0 && height > 0.0 then
        let user = {Height = height; Weight = weight}
        let userBmi = bmi user
        let cater = getBMICat userBmi
        printfn "Twoje BMI to %.2f" userBmi
        printfn "Kategoria: %s" cater
    else
        printfn "?!."
    0 // zerowanie wszystkiego i kończenie