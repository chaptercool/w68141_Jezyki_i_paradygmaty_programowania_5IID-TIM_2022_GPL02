from Employee import Employee

class EmployeeManager:
    def __init__(self):
        self.employees = []

    def dodac(self, employee: Employee):
        self.employees.append(employee)
        print(f"Dodano pomyślnie pracownika {employee}")

    def lista(self):
        if not self.employees:
            print(False)
        else:
            for i in self.employees:
                print(i)

    def usunac_wiek(self, min: int, max: int):
        curr = len(self.employees)
        self.employees = [
            i for i in self.employees if not (min <= i.age <= max)
        ]
        usunieto = curr - len(self.employees)
        print(f"Usunieto {usunieto} pracownikow w zakresie {min}-{max} lat.")

    def szukaj(self, imie: str):
        for i in self.employees:
            if i.imie == imie:
                return i
        return None

    def nowe_zarobki(self, imie: str, zarobki: float):
        employee = self.szukaj(imie)
        if employee:
            print(f"Zaktualizowano zarobki dla {employee.imie}. Nowe zarobki to: {zarobki}")
            employee.zarobki = zarobki
        else:
            print(f"Nie znaleziono pracownika {imie}")