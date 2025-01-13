from functools import reduce

parzyste = lambda x: x % 2 == 0

lista = [2, 2, 3, 1, 5, 8, 17, 39, 40, 65]

p_liczby = list(filter(parzyste, lista))

suma = reduce(lambda x, y: x + y, p_liczby)

print("Parzyste: ", p_liczby)
print("Ich suma: ", suma)