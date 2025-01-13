class Prostokat:
    def __init__(self, dl, sz):
        self.dl =  dl
        self.sz = sz

    def pole(self):
        print("Pole wynosi: ", self.dl * self.sz)

    def obwod(self):
        print("Obwód wynosi: ", 2 * self.dl + 2 * self.sz)

pr1 = Prostokat(2,4)

pr1.pole()
pr1.obwod()