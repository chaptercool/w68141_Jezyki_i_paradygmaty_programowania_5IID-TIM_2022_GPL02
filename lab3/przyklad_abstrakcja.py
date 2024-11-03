from abc import ABC, abstractmethod
#abstract base class, definicje abstrakcyjnych metod

class Zwierze(ABC):
    @abstractmethod
    def dzwiek(self): pass

class Kotek(Zwierze):
    def dzwiek(self):
        return "Miau."

class Piesek(Zwierze):
    def dzwiek(self):
        return "Hau."

piesek = Piesek()
print(piesek.dzwiek())

class Kalk():
    @staticmethod
    def dodac(a, b): return a + b

    @staticmethod
    def odejmowac(a, b): return a - b

print(Kalk.dodac(1, 2))
print(Kalk.odejmowac(1, 2))