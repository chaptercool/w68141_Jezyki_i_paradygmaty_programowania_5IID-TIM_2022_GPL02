//type Person(name: string, age: int) =
//    let mutable _name = name
//    let mutable _age = age

//    //właściwości - zapis kompaktowy
//    member this.Name
//        with get() = _name //zwracanie
//        and set(value) = _name <- value //ustawianie wart którą przekażemy

//    //zapis rozszerzony
//    //member this.Name with get()
//    //member this.Name with set(value) = _name <- value

//    member this.Age
//        with get() = _age
//        and set(value) = _age <- value

//    //metoda
//    member this.View() =
//        printfn "Witaj %s, %d" _name _age

//let person = Person("Jan", 30)
//person.View()

////dziedziczenie
//type Pracownik(name: string, age: int, stanowisko: string) =
//    inherit Person(name, age)
//    member this.Stanowisko = stanowisko

//    override this.View() =
//        printfn "Witaj %s, %d, twoje stanowisko: %s" this.Name this.Age this.Stanowisko

//let pracownik = Pracownik("Jan", 30, "Programista")
//pracownik.View()

//interfejs
//type Chodzacy =
//    abstract member Chodz : unit -> unit

//type Person(name: string) =
//    //inny zapis get set
//    member val name = name with get, set

//    interface Chodzacy with
//       member this.Chodz() =
//           printfn "%s chodzi....................................." this.name

//let person = Person("Jan")
//(person :> Chodzacy).Chodz() //rzutowanie na interfejs

//polimorfizm
type Animal() =
   static member Speak() = printfn "Zwirze wydaje glos........"

type Dog() =
    inherit Animal()
    override this.Speak() = printfn "Hau Hau"

type Cat() =
    inherit Animal()
    override this.Speak() = printfn "Miau Miau"