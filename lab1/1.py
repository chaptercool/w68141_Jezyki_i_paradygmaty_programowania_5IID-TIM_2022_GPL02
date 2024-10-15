def podzielPaczki(wagi, max_waga):
    for waga in wagi:
        if waga > max_waga:
            raise ValueError(f"Paczka o wadze {waga} przekracza maksymalną dozwoloną wage, która wynosi {max_waga}kg!")
    wagi_sorted = sorted(wagi, reverse = True)
    kursy = []

    for waga in wagi_sorted:
        dodano = False
        for kurs in kursy:
            if sum(kurs) + waga <= max_waga:
                kurs.append(waga)
                dodano = True
                break
        if not dodano:
            kursy.append([waga])

    return len(kursy), kursy


wagi = [10 ,15, 7, 20, 5, 8, 10]
max_waga = 25

# print(podzielPaczki(wagi, max_waga))
liczba_kursow, kursy = podzielPaczki(wagi, max_waga)

for i, kurs in enumerate(kursy, 1):
    print(f"Kurs {i}: {kurs} - suma wag: {sum(kurs)}kg.")