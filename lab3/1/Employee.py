class Employee:
    def __init__(self, imie, wiek, zarobki):
        self.imie = imie
        self.wiek = wiek
        self.zarobki = zarobki

    # definicja obiektu w czytelnej postaci
    def __str__(self):
        return f"Pracownik: {self.imie}, {self.wiek} lat(a)\n{self.zarobki} zl"

    # dostarczenie reprezentacji do debugowania
    def __repr__(self):
        return f"Pracownik(imie = {self.imie}, wiek = {self.wiek}, zarobki = {self.zarobki})"

pracownik = Employee("Jan Kowalski", 72, 1000)
# print(pracownik, "\n")
# print(repr(pracownik))