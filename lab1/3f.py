#Funkcyjnie
from functools import reduce

def optymalizator(sum, zadanie):
    sum['czas_w_toku'] += zadanie[1]
    sum['czas_oczekiw'] += sum['czas_w_toku']
    sum['optym_kolejka'].append(zadanie)
    return sum

#legenda: nazwa, czas (min), nagroda (zl)
Zadania = [
    ("Sprzątanie", 20, 30),
    ("Szkolenie", 90, 200),
    ("Jazda", 40, 50),
    ("Odpoczynek", 120, 0),
    ("Sen", 480, 0),
    ("Jedzenie", 30, 2)
]

posortowane = sorted(Zadania, key = lambda x: x[1])

poczatkowe_wartosci = {
    'czas_w_toku': 0,
    'czas_oczekiw': 0,
    'optym_kolejka': []
}

wynik = reduce(optymalizator, posortowane, poczatkowe_wartosci)

print(f"Optymalna kolejnosc: {wynik['optym_kolejka']} \n Czas oczekiwania wynosi {wynik['czas_oczekiw']}")