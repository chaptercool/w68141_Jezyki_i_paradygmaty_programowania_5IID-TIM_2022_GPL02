//pętla for
for i = 1 to 5 do
    printfn "i = %d" i

//odwrotnie
for i = 5 downto 1 do
    printfn "i = %d" i

//while
let mutable x = 1
while x < 5 do
    printfn "x = %d" x
    x <- x + 1

//rekurencja
let rec countdown n =
    if n <= 0 then
        printfn "bam.................................."
    else
        printfn "%d..." n
        countdown (n - 1)

countdown 5

let sumaTailRec n =
    let rec aux n acc = //aux pomocnicza, acc akumulator (przechowuje sumę)
        if n <= 0 then acc
        else aux (n - 1) (acc + n)
    aux n 0

let wynik = sumaTailRec 5
printfn "Wynik: %d" wynik

//funkcje iteracyjne

let numbers = [1; 2; 3; 4; 5]
let nuumbrs1 = List.iter (fun x -> printf "element: %d, " x)
let nuumbrs2 = List.map(fun x -> x * 2) numbers

