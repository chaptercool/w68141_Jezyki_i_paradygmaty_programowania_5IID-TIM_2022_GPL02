def analizator(data):
    liczby = list(filter(lambda x: isinstance(x, (int, float)), data))
    najwieksza = max(liczby) if liczby else None

    tekst = list(filter(lambda x: isinstance(x, str), data))
    najdluzszy = max(tekst, key=len) if tekst else None

    krotki = list(filter(lambda x: isinstance(x, tuple), data))
    n_krotka = max(krotki, key=len) if krotki else None

    return najwieksza, najdluzszy, n_krotka

data = [42, "witaj", (1, 2, 3), 3.14, "swiat", (4, 5), "komputer", (6, 7, 8, 9), 100]

najw_liczba, najdl_tekst, najw_krotka = analizator(data)

print("Największa liczba:", najw_liczba)
print("Najdłuższy napis:", najdl_tekst)
print("Krotka o największej liczbie elementów:", najw_krotka)
