import re
from collections import Counter


def scissors(text):
    paragraph = text.strip().split('\n')
    parnum = len(paragraph)

    sentence = re.split(r'[.!?]+', text)
    sennum = len([s for s in sentence if s.strip()])

    words = text.split()
    wordsnum = len(words)

    stop_words = {'i', 'o', 'a', 'z', 'w', 'u', '-', ':'}
    filter_words = filter(lambda word: word not in stop_words, words)
    fwnum = Counter(words)

    print(f"Liczba akapitów: {parnum}")
    print(f"Liczba zdań: {sennum}")
    print(f"Liczba słów: {wordsnum}")


string = """
Jak co roku obchody lanego poniedziałku w Bełchatowie wywołują wiele emocji.
Święto to jest, co prawda, uświęcone tradycją, jednakże są ludzie, którzy zdecydowanie przesadzają z hołdowaniem tej jednej tradycji.
"Świństwo, kiedyś to dziewczyny się perfumami lało, a nie tak, jak teraz, wiadrami; tałatajstwo!!!", oburza się starszy pan, zapytany o opinię na temat nadchodzącego poniedziałku.
"""
scissors(string)