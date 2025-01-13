def suma(n):
    return sum(int(cyfra) for cyfra in str(n))

inp = input("Liczba do sumy? ")
liczba = inp
print("Suma: ", suma(liczba))