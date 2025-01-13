def permutacje(lista):
    if len(lista) == 0: # Sprawdzam czy lista jest pista
        return [[]]
    elif len(lista) == 1: #Sprawdzam czy lista ma tylko jeden element
        return [lista]
    else: # Gdy lista ma więcej niż 1 element
        return [
            [x] + p
            for i, x in enumerate(lista)
                for p in permutacje(lista[:i] + lista[i+1:])
        ]
# 3 przykładowe rozwiązania dla 3 warunków
print(permutacje([1]))
print(permutacje([]))
print(permutacje([1, 2,3]))