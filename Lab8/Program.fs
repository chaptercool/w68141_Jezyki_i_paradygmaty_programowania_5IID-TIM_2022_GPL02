open System.Collections.Generic

//definicja listy laczonej
type LinkedList<'T> =
    | Empty
    | Node of 'T * LinkedList<'T>

//pobranie "glowy" listy
let Head = function
    | Empty -> failwith "puste............?"
    | Node (Head, _) -> Head

//pobranie "ogona" listy
let Tail = function
    | Empty -> failwith "puste............?"
    | Node (Tail, _) -> Tail

//dodanie nowej "glowy"
let AddHead value list =
    Node(value, list)

let rec printList list =
    match list with
    | Empty -> () //pusta wartosc
    | Node (value, next) -> printfn "%A" value; printList next

let rec numberElements =
    function
    | Empty -> 0
    | Node (_, Tail) -> numberElements Tail + 1

[<EntryPoint>]
let main argv =
    let list1 = Empty
    let list2 = AddHead 1 list1
    let list3 = AddHead 2 list2
    let list4 = AddHead 3 list3

    printList list4

    let ilosc = numberElements list4
    printfn "Ilosc elementow: %d" ilosc

    0