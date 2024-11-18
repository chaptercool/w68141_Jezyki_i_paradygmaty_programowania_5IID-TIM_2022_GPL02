let rec QuickSort xs =
    match xs with
    | [] -> []
    | x :: xs ->
        let smaller, larger = List.partition (fun y -> y <= x) xs
        QuickSort smaller @ [x] @ QuickSort larger

let cyferki = [2; 8; 6; 4; 5]
let qs_cyferki = QuickSort cyferki
printfn "%A" qs_cyferki