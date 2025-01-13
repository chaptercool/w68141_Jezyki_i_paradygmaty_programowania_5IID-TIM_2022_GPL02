def palindrom(n):
    reversed = n [::-1]
    if n == reversed:
        return True
    else: return False

slowo = "ksiomi"
slowo2 = "szalazs"
print("Wynik", palindrom(slowo))
print("Wynik", palindrom(slowo2))