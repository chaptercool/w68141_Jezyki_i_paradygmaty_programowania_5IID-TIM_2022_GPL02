let x = 5 //stała
let name = "ala" //stała
let floatA = 3.14 //stała
let mutable liczba = 10 //zmienna
liczba <- 123 //~ this w C#

printfn "Witaj %s" name
printfn "Liczba x ma wartość %d a wartość float %.2f" x floatA //do 2 znaków po przecinku
// %s - string, %d - całkowita, %f - float, %i - int

//printf "Podaj imię: "
//let Name = System.Console.ReadLine() // Bez open System
//(*let Name2 = Console.ReadLine() //*) (*z open System*)

//printf "Podaj liczbę: "
//let input = Console.ReadLine()
//let mutable liczba2 = 0
//if Int32.TryParse(input, &liczba2) then //konwersja string na int bo ReadLine odczytuje tylko i wyłącznie string
//    printfn "Wprowadzono liczbę %d" liczba2
//else
//    printfn "Nie wprowadzono liczby"

// if a = b
// if a <> b nie równa się (!=)
// ! to not

//switch ↓ case to "|"
//match x with
//| 1 -> printfn "Jeden"
//| 2 -> printfn "Dwa"
//| _ -> printfn "Inna liczba" czyli default

let listaA = [1; 2; 3; 4]
let listaB = 8 :: listaA //dodanie elementu na początku listy, ALE jako nowa lista
let listaC = listaA @ listaB //łączenie list
let listaD = List.append listaA [123]  //jak lista B tylko na koniec
List.rev(listaA) //odwrócenie listy
for element in listaA do
    printfn "%d" element //iterowanie

printfn "%A" listaA
printfn "%A" listaB
printfn "%A" listaC
//%A - "any" ale jako string

type Osoba = {
    Imie: string
    Wiek: int
}

let osoba1 = {Imie = "Ala"; Wiek = 20}
printfn "Witaj %s \t Twój wiek to %d" osoba1.Imie osoba1.Wiek

let krotka = [1, "Ala", 3.14] //przechowuje różne typy
