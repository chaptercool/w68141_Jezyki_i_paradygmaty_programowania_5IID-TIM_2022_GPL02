let rec permute list =
    match list with
    | [] -> [[]]
    | head :: tail -> 
        tail
        |> permute
        |> List.collect (fun tailPerm ->
            [0..List.length tailPerm]
            |> List.map (fun i ->
                let (left, right) = List.splitAt i tailPerm
                left @ (head :: right)))

let cyferki = [2; 8; 6; 4; 5]
let perm = permute cyferki

printfn "%A" perm