open System.Collections.Generic
open System

//definicja listy laczonej
type LinkedList<'T> =
    | Empty
    | Node of 'T * LinkedList<'T>

//======MODUL======//
module LinkedList = 
    //tworzenie listy z elementow usera
    let rec fromList =
        function
        | [] -> Empty
        | x :: xs -> Node(x, fromList xs)

    //drukowanie   
    let rec printList list =
        match list with
            | Empty -> () //pusta wartosc
            | Node (value, next) -> printfn "%A" value; printList next

    //suma elementow (z2)
    let rec sumList =
        function
            | Empty -> 0
            | Node (x, xs) -> x + sumList xs

    //max i min (z3)
    let rec minMaxList list =
        match list with
            | Empty -> failwith "The list is empty."
            | Node (value, tail) ->
                let rec helper currentMin currentMax remaining =
                    match remaining with
                    | Empty -> currentMin, currentMax
                    | Node (v, t) -> 
                        let newMin = min currentMin v
                        let newMax = max currentMax v
                        helper newMin newMax t
                helper value value tail

    //odwrocenie listy (z4)
    let rec reverseList list =
        let rec helper acc =
            function
            | Empty -> acc
            | Node (v, t) -> helper (Node (v, acc)) t
        helper Empty list

    //szukanie elementu (z5)
    let rec findElement list element =
        match list with
        | Empty -> false
        | Node (v, t) -> v = element || findElement t element

    //indeks elementu (z6)
    let rec indexOfElement list element =
        let rec helper index =
            function
            | Empty -> -1
            | Node (v, t) -> if v = element then index else helper (index + 1) t
        helper 0 list

    //ile razy wystepuje element (z7)
    let rec countElement list element =
        let rec helper count =
            function
            | Empty -> count
            | Node (v, t) -> helper (if v = element then count + 1 else count) t
        helper 0 list

    //laczenie list (z8)
    let rec appendList list1 list2 =
        match list1 with
        | Empty -> list2
        | Node (v, t) -> Node (v, appendList t list2)

    //porownanie liczb miedzy listami (z9)
    exception ListLengthMismatchException of string

    let compareLists list1 list2 =
        let rec toList linkedList =
            match linkedList with
            | Empty -> []
            | Node (v, t) -> v :: toList t
        let l1 = toList list1
        let l2 = toList list2
        if List.length l1 <> List.length l2 then
            raise (ListLengthMismatchException "Lists have different lengths")
        else
            List.map2 (>) l1 l2

    //elementy spelniajace warunek (z10)
    let rec filterList list predicate =
        match list with
        | Empty -> Empty
        | Node (v, t) -> if predicate v then Node (v, filterList t predicate) else filterList t predicate

    //usuniecie duplikatow (z11)
    let rec removeDuplicates list =
        let rec helper acc =
            function
            | Empty -> acc
            | Node (v, t) -> if findElement acc v then helper acc t else helper (Node (v, acc)) t
        helper Empty list
    
    //dzielenie listy na pol wedlug warunku (z12)
    let rec splitList list predicate =
        let rec helper acc1 acc2 =
            function
            | Empty -> acc1, acc2
            | Node (v, t) -> if predicate v then helper (Node (v, acc1)) acc2 t else helper acc1 (Node (v, acc2)) t
        helper Empty Empty list
//======koniec modulu======//

//wczytanie z klawy (z1)
let rec readUserList() =
    printf "Podaj elementy przez spacje: "
    let input = Console.ReadLine()
    let items =
        input.Split(' ')
        |> Array.choose(fun x ->
            match Int32.TryParse(x) with
            | true, v -> Some v
            | _ -> None)
        |> Array.toList
    LinkedList.fromList items

let rec menu a = 
    printfn "\n======== MENU ========"
    printfn "1. Wczytaj liste"
    printfn "2. Wypisz liste"
    printfn "3. Suma elementow listy"
    printfn "4. Max i Min"
    printfn "5. Odwrocenie listy"
    printfn "6. Szukanie elementu"
    printfn "7. Indeks elementu"
    printfn "8. Ile razy wystepuje element"
    printfn "9. Laczenie list"
    printfn "10. Porownanie list"
    printfn "11. Elementy spelniajace warunek"
    printfn "12. Usuniecie duplikatow"
    printfn "13. Podzial listy na pol"
    printfn "0. Wyjscie"
    printf "Wybierz opcje: "
    match Console.ReadLine() with
    | "1" -> 
        let newList = readUserList()
        menu newList
    | "2" -> 
        printfn "\nElementy listy:"
        LinkedList.printList a
        menu a
    | "3" -> 
        let suma = LinkedList.sumList a
        printfn "\nSuma elementow listy: %d" suma
        menu a
    | "4" -> 
        let minVal, maxVal = LinkedList.minMaxList a
        printfn "\nNajmniejsza liczba to %A." minVal
        printfn "Najwieksza liczba to %A." maxVal
        menu a
    | "5" -> 
        let reversedList = LinkedList.reverseList a
        printfn "\nOdwrocona lista:"
        LinkedList.printList reversedList
        menu a
    | "6" -> 
        printf "Szukaj element:  "
        let element = Console.ReadLine() |> Int32.Parse
        let found = LinkedList.findElement a element
        printfn "Element znaleziony: %A" found
        menu a
    | "7" -> 
        printf "Szukaj indeks elementu:  "
        let element = Console.ReadLine() |> Int32.Parse
        let index = LinkedList.indexOfElement a element
        printfn "Indeks elementu: %A" index
        menu a
    | "8" -> 
        printf "Oblicz ilosc wystepowan elementu:  "
        let element = Console.ReadLine() |> Int32.Parse
        let count = LinkedList.countElement a element
        printfn "Element wystepuje %A razy" count
        menu a
    | "9" -> 
        printf "Podaj elementy dla DRUGIEJ listy przez spacje, aby ja zlaczyc: "
        let input = Console.ReadLine()
        let items =
            input.Split(' ')
            |> Array.choose(fun x ->
                match Int32.TryParse(x) with
                | true, v -> Some v
                | _ -> None)
            |> Array.toList
        let newList = LinkedList.fromList items
        let appendedList = LinkedList.appendList a newList
        printfn "Polaczone listy:"
        LinkedList.printList appendedList
        menu a
    | "10" -> 
        printf "Podaj elementy dla DRUGIEJ listy przez spacje, aby je porownac: "
        let input = Console.ReadLine()
        let items =
            input.Split(' ')
            |> Array.choose(fun x ->
                match Int32.TryParse(x) with
                | true, v -> Some v
                | _ -> None)
            |> Array.toList
        let newList = LinkedList.fromList items
        try
            let comparedList = LinkedList.compareLists a newList
            printfn "Porownanie list: %A" comparedList
        with
            | LinkedList.ListLengthMismatchException msg -> printfn "Listy sa roznej dlugosci"
        menu a
    | "11" -> 
        printf "Lista po filtracji musi miec elementy wieksze niz: "
        let predicate = Console.ReadLine()
        let filteredList = LinkedList.filterList a (fun x -> x > Int32.Parse(predicate))
        printfn "Elementy spelniajace warunek:"
        LinkedList.printList filteredList
        menu a
    | "12" -> 
        let removedDuplicatesList = LinkedList.removeDuplicates a
        printfn "Lista bez duplikatow:"
        LinkedList.printList removedDuplicatesList
        menu a
    | "13" -> 
        printf "Pierwsza lista musi miec elementy wieksze niz: "
        let predicate = Console.ReadLine()
        let (list1, list2) = LinkedList.splitList a (fun x -> x > Int32.Parse(predicate))
        printfn "Lista 1:"
        LinkedList.printList list1
        printfn "Lista 2:"
        LinkedList.printList list2
        menu a
    | "0" -> 
        printfn "Koniec dzialania programu"
        0
    | _ -> 
        printfn "Nie ma takiej opcji"
        menu a

[<EntryPoint>]
let main argv =
    let mutable userList = LinkedList.Empty
    userList <- readUserList()
    menu userList
    0
