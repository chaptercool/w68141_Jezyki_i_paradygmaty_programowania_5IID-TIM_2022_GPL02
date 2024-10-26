from functools import reduce

# listy skladane
numbers = [1,2,3,4,5,6,7,8,9]
result = [x ** 2 for x in numbers if x % 2 == 0]
print(result)

# funkcja mapowania
result1 = list(map(lambda x: x * 2, numbers))
print(result1)

# filtrowanie
result2 = list(filter(lambda x: x % 2 == 0, numbers))
print(result2)

#sumowanie z par (czyli 1+2, 3+4 itd i pary zsumowane)
total = reduce(lambda x, y: x + y, numbers)
print(total)

code = '''def witaj(name):
    return "Hello, " + name

wynik = witaj("JA")'''

exec(code)
print(wynik)

input = "1 + 2 + 3 * 23"
out = eval(input)
print(out)