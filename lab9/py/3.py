import math

def kwadratowe(a, b, c):
    dysk = b ** 2 - 4 * a * c
    if dysk > 0:
        pierw1 = (-b + math.sqrt(dysk)) / (2 * a)
        pierw2 = (-b - math.sqrt(dysk)) / (2 * a)
        return pierw1, pierw2
    elif dysk == 0:
        pierw = -b / (2 * a)
        return pierw
    else:
        return None

a = float(input("Współczynnik a? "))
b = float(input("Współczynnik b? "))
c = float(input("Współczynnik c? "))

wynik = kwadratowe(a, b, c)

if wynik:
    if isinstance(wynik, tuple):
        print("Dwa rozwiązania: x1 = {:.2f}, x2 = {:.2f}".format(wynik[0], wynik[1]))
    else:
        print("Jedno rozwiązanie: x = {:.2f}".format(wynik))
else:
    print("Brak rozwiązań.")