class Samochod:
    #pass - pusta definicja
    def __init__(self, marka, model, rok):
        self._marka = marka #protected
        self.__model = model #private
        self.rok = rok

    def wypisz(self):
        return f"{self._marka} {self.__model}, Rok: {self.rok}"

#dziedziczenie
class Osobowy(Samochod):
    def __init__(self, marka, model, rok, typ):
        #przerzucenie elementow z klasy potomnej
        super().__init__(marka, model, rok)
        self.typ = typ

wozik = Samochod("Ford", "Fiesta", 2007)

print(wozik.wypisz())