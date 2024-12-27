open System.Collections.Generic
open System

//definicja listy laczonej
type LinkedList<'T> =
    | Empty
    | Node of 'T * LinkedList<'T>


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
            | Empty -> failwith "puste..............."
            | Node (value, tail) ->
                let rec helper currentMin currentMax remaining =
                    match remaining with
                    | Empty -> currentMin, currentMax
                    | Node (v, t) ->
                        let newMin = min currentMin v
                        let newMax = max currentMax v
                        helper newMin newMax t
                helper value value tail
                            
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

[<EntryPoint>]
let main argv =
    let mutable userList = LinkedList.Empty
    userList <- readUserList()

    printf "\nElementy listy: \t"
    LinkedList.printList userList

    let suma = LinkedList.sumList userList
    printf "\nSuma elementow listy: %d" suma


    0