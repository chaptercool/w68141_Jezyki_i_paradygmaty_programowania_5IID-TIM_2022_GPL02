from EmployeesMgr import EmployeeManager
from Employee import Employee

class FrontendManager:
    def __init__(self):
        self.manager = EmployeeManager()

    def panel(self):
        print("Wybierz opcje poprzez wpisywanie jej numeru\n"
              "1. Dodac pracownika\n"
              "2. Lista pracownikow\n"
              "3. Usun pracownika w okreslonym przedziale wiekowym\n"
              "4. Wyszukaj pracownika\n"
              "5. Zaktualizuj zarobki pracownika\n"
              "6. Wyjscie\n")

        opcja = int(input("Wybiez opcje, a potem wcisnij klawisz ENTER: "))
        match opcja:
            case 1:
                imie = input("Wpisz imie pracownika: ")
                wiek = int(input("Wpisz wiek pracownika: "))
                zarobki = float(input("Wpisz zarobki pracownika: "))
                employee = Employee(imie, wiek, zarobki)
                self.manager.dodac(employee)
            case 2:
                self.manager.lista()
            case 3:
                min = int(input("Wpisz minimalny zakres: "))
                max = int(input("Wpisz maksymalny zakres: "))
                self.manager.usunac_wiek(min, max)
            case 4:
                szukane = input("Wpisz pelne imie i nazwisko pracownika: ")
                print(self.manager.szukaj(szukane))
            case 5:
                imie = input("Wpisz imie pracownika aby zaktualizowac jego zarobki: ")
                nowe_zarobki = float(input("Wpisz nowe zarobki: "))
                self.manager.nowe_zarobki(imie, nowe_zarobki)
            case 6:
                print("Koniec dzialania programu.")
            case _:
                print("Taka opcja nie istnieje.")
