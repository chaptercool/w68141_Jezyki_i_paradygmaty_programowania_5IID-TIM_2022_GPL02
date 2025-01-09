open System
//1
let rec suma n =
    if n = 0 then 0
    else if n = 1 then 1
    else n + suma(n - 1)

printf "Podaj liczbę dla sumy od 1 do n: "
let b = int(Console.ReadLine())
printfn "Suma liczb od 1 do %d wynosi %d\n" b (suma b)

//2
let pierwsza a =
    if a % 2 = 0 then false
    else true

printf "Podaj liczbę dla sprawdzenia: "
let a = int(Console.ReadLine())
printfn "Wynik %b\n" (pierwsza a)

//3
type Uczen = {
    Imie: string
    Ocena: float list
}

let wczytajDane() = 
    [
        { Imie = "Jan"; Ocena = [3.0; 4.0; 5.0] }
        { Imie = "Anna"; Ocena = [4.0; 4.5; 5.0] }
        { Imie = "Piotr"; Ocena = [2.0; 3.0; 3.5] }
    ]

let raport (uczniowie: Uczen list) =
    uczniowie
    |> List.map (fun uczen ->
        let srednia = List.average uczen.Ocena
        sprintf "%s - średnia: %.2f" uczen.Imie srednia
    )
    |> List.iter Console.WriteLine

let uczniowie = wczytajDane()
raport uczniowie

//4
let rec quicksort = function
    | [] -> []
    | head :: tail ->
        let left, right = List.partition (fun x -> x < head) tail
        quicksort left @ [head] @ quicksort right

let lista = [3; 1; 4; 1; 5; 9; 2; 6; 5; 3; 5]
let posortowana = quicksort lista
printfn "Przed sortowaniem: %A" lista
printfn "Po sortowaniu: %A\n" posortowana

//5
//brak

//6
let rec silnia n =
    let mutable liczba = n
    if n < 0 then 
        printfn "Liczba ujemna, liczę z modułu liczby..."
        liczba <- n * (-1)
        liczba * silnia(liczba - 1)
    else if n = 0 then 1
    else n * silnia (n - 1) //iloczyn liczb całkowitych dodatnich mniejszych bądź równych n
                            //czyli wzor (n - 1)! * n

printf "Podaj liczbę: "
let n = int(Console.ReadLine())

printfn "Silnia z %d wynosi %d\n" n (silnia n)

//7
let slowa = ["CUdOwNiE"; "zamało"; "cyrk"; "KUCHNiA"]
let duzeLitery = List.map (fun (x: string) -> x.ToUpper()) slowa
printfn "Stara lista: %A" slowa
printfn "Nowa lista: %A\n" duzeLitery

//8
let rec nwd a b =
    if b = 0 then a
    else nwd b (a % b)

let c = 6
let d = 4
printfn "NWD z %d i %d wynosi %d\n" c d (nwd c d)