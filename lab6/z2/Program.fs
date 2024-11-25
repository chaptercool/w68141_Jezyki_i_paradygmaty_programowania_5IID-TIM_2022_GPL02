open System

let reverse input =
    input
    |> Seq.rev
    |> Seq.toArray
    |> String

let compare input input2 = 
   if input = input2 then
       true
   else
       false

[<EntryPoint>]
let main argv = 
    printf "Podaj slowo.............. "
    let slowo = Console.ReadLine()
    let lowercase = slowo.ToLower()

    let reversed = reverse lowercase
    let isPalindrome = compare lowercase reversed
    printfn "Wynik %b" isPalindrome

    0