import numpy as np

def wymiar_dodawanie(m1, m2):
    return m1.shape == m2.shape


def wymiar_mnozenie(m1, m2):
    return m1.shape[1] == m2.shape[0]


def operacja(oper_name, macierzy):
    try:
        for nazwa, macierz in macierzy.items():
            exec(f"{nazwa} = macierz", {}, {nazwa: macierz})

        wynik = eval(oper_name)

        if isinstance(wynik, np.ndarray):
            return wynik
        else:
            raise ValueError("Operacja nie zwróciła macierzy.")
    except Exception as e:
        print(f"Błąd wykonania operacji: {e}")
        return None


def main():
    A = np.array([[1, 2], [3, 4]])
    B = np.array([[5, 6], [7, 8]])
    C = np.array([[1], [2]])

    macierzy = {'A': A, 'B': B, 'C': C}

    # Przykładowe operacje
    operacje = [
        "A + B",
        "A @ B",
        "A.T",
        "A @ C",
        "A + C",
    ]

    for op in operacje:
        if "+" in op:
            m1, m2 = op.split(" + ")
            if wymiar_dodawanie(macierzy[m1], macierzy[m2]):
                wynik = operacja(op, macierzy)
                if wynik is not None:
                    print(f"Wynik operacji '{op}':\n{wynik}\n")
                else:
                    print(f"Operacja '{op}' jest niepoprawna.")
            else:
                print(f"Operacja '{op}' jest niepoprawna - wymiary macierzy nie zgadzają się.")

        elif "@" in op:
            m1, m2 = op.split(" @ ")
            if wymiar_mnozenie(macierzy[m1], macierzy[m2]):
                wynik = operacja(op, macierzy)
                if wynik is not None:
                    print(f"Wynik operacji '{op}':\n{wynik}\n")
                else:
                    print(f"Operacja '{op}' jest niepoprawna.")
            else:
                print(f"Operacja '{op}' jest niepoprawna - wymiary macierzy nie zgadzają się.")

        elif ".T" in op:
            wynik = operacja(op, macierzy)
            if wynik is not None:
                print(f"Wynik operacji '{op}':\n{wynik}\n")
            else:
                print(f"Operacja '{op}' jest niepoprawna.")
        else:
            print(f"Operacja '{op}' jest nieznana.")


if __name__ == "__main__":
    main()
