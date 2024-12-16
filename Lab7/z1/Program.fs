open System
open System.Collections.Generic

type Book(title: string, author: string, pages: int) =
    member this.Title = title
    member this.Author = author
    member this.Pages = pages

    member this.GetInfo() =
        sprintf "Title: %s, Autor: %s, stron: %d" this.Title this.Author this.Pages

type User(name: string) =
    member this.Name = name
    member this.BorrowedBooks = new List<Book>()

    member this.BorrowBook(book: Book) =
        this.BorrowedBooks.Add(book)
        printfn "To co sie nazywa %s wypozyczylo %s" this.Name book.Title

    member this.ReturnBook(book: Book) =
        if this.BorrowedBooks.Remove(book: Book) then
            printfn "%s zwrocil %s" this.Name book.Title
        else printfn "%s nie uczestniczyl w kradziezy %s" this.Name book.Title

type Library() = 
    let books = new List<Book>()

    member this.AddBook(book: Book) =
        books.Add(book)
        printfn "Dodano ksiazke %s" book.Title

    member this.RemoveBook(book: Book) =
        if books.Remove(book) then
            printfn "Wyrzucono %s" book.Title
        else printfn "Co to za ksiazka %s ???????????????" book.Title

    member this.ListBooks() =
        printfn "Lista ksiazek: "
        for book in books do
            printfn " - %s" (book.GetInfo())

//dopisac status dostepnosci!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
[<EntryPoint>]
let main argv =
    let library = Library()

    let book1 = Book("ksz1", "a1", 128)
    let book2 = Book("ksz2", "a2", 284)
    let book3 = Book("ksz3", "a3", 412)

    library.AddBook(book1)
    library.AddBook(book2)
    library.AddBook(book3)
    library.ListBooks()

    let user1 = User("Jan Nowak")
    user1.BorrowBook(book1)
    user1.BorrowBook(book2)

    user1.ReturnBook(book3)

    library.ListBooks()
    
    0