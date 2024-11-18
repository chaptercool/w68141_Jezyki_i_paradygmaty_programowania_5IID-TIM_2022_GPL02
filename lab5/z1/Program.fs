let rec Fibonacci n =
    if n <= 1 then n
    else if n = 1 then 1
    else Fibonacci (n - 1) + Fibonacci (n - 2)

let wynik  = Fibonacci 10
printf "Fib 10 = %d" wynik

let fibTailRec n =
    let rec aux n a b =
        if n = 0 then a
        elif n = 1 then b
        else aux (n - 1) b (a + b)
    aux n 0 1

let wynik2 = fibTailRec 10
printf "Fib 10 = %d" wynik2