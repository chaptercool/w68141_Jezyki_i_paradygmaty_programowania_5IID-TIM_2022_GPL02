class Pojazd:
    def __init__(self, nazwa):
        self.nazwa = nazwa

    def printnazwa(self):
        print("To jest ", self.nazwa)

class Samochod(Pojazd):
    def beep(self):
        print("Beep!")

autko = Samochod("ksiomi")
autko.beep()
autko.printnazwa()