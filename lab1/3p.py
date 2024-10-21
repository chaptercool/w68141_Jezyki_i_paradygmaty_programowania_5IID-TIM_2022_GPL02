# Proceduralnie
def minimizator(zadania):
    zadania.sort(key = lambda x: x[1])
    suma_czas = 0
    czas_w_toku = 0
    optym_kolejka = []

    for zadanie in zadania:
        czas_w_toku += zadanie[1]
        suma_czas += czas_w_toku
        optym_kolejka.append(zadanie)

    return optym_kolejka, suma_czas

#legenda: nazwa, czas (min), nagroda (zl)
Zadania = [
    ("Sprzątanie", 20, 30),
    ("Szkolenie", 90, 200),
    ("Jazda", 40, 50),
    ("Odpoczynek", 120, 0),
    ("Sen", 480, 0),
    ("Jedzenie", 30, 2)
]

opt_kolej, czas_oczekiw = minimizator(Zadania)

print("Optymalna kolejność to: ", opt_kolej, f"\n Czas oczekiwania wynosi {czas_oczekiw}")