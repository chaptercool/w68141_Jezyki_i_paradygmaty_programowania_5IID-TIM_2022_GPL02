def dodawanie(a, b):
    return a + b

def odejmowanie(a, b):
    return a - b

def mnozenie(a, b):
    return a * b

def dzielenie(a, b):
    if b != 0:
        return a / b
    else:
        return "Dzielenie przez zero."

inpA = int(input("Podaj pierwszą liczbę: "))
inpB = int(input("Podaj drugą liczbę: "))
command = input("Wybierz operację: \n 1. Dodawanie\n2.Odejmowanie\n3.Mnożenie\n4.Dzielenie\n")
match command:
    case "1":
        print("Wynik: ", dodawanie(inpA, inpB))
    case "2":
        print("Wynik: ", odejmowanie(inpA, inpB))
    case "3":
        print("Wynik: ", mnozenie(inpA, inpB))
    case "4":
        print("Wynik: ", dzielenie(inpA, inpB))
    case _:
        print("Nieznana opcja.")