#r "nuget: System.Runtime"

using System;

// partial класс (2 части)
public partial class Book
{
    private static readonly int id;
    private string title;
    private string author;
    private string publisher;
    private int year;
    private int pages;
    private double price;
    private string coverType;

    private const string category = "Literature";
    private static int count;

    public int Id => id;

    public string Title { get => title; set => title = value; }
    public string Author { get => author; set => author = value; }
    public string Publisher { get => publisher; set => publisher = value; }
    public int Year { get => year; set => year = value; }
    public int Pages { get => pages; set => pages = value; }
    public double Price { get => price; private set => price = value; }
    public string CoverType { get => coverType; set => coverType = value; }

    public Book()
    {
        id = GetNextId();
        count++;
    }

    public Book(string title, string author, string publisher, int year, int pages, double price, string coverType)
    {
        id = GetNextId();
        this.title = title;
        this.author = author;
        this.publisher = publisher;
        this.year = year;
        this.pages = pages;
        this.price = price;
        this.coverType = coverType;
        count++;
    }

    public Book(string title = "Unknown", string author = "Unknown")
    {
        id = GetNextId();
        this.title = title;
        this.author = author;
        count++;
    }

    private Book(int id)
    {
        this.id = id;
        count++;
    }

    public static Book CreateWithId(int customId) => new Book(customId);

    static Book()
    {
        count = 0;
    }

    public void UpdatePrice(ref double newPrice, out double oldPrice)
    {
        oldPrice = price;
        price = newPrice;
    }

    public static void PrintClassInfo()
    {
        Console.WriteLine($"Class Book: total objects created = {count}, category = {category}");
    }

    private static int GetNextId() => Guid.NewGuid().GetHashCode();

    public override bool Equals(object obj) => obj is Book other && this.id == other.id;
    public override int GetHashCode() => id.GetHashCode();
    public override string ToString() => $"Book: {title}, {author}, {publisher}, {year}, {pages} pages, {price}$, cover: {coverType}, id: {id}";
}

// вторая часть partial
public partial class Book { }

// ======= Код выполнения =======

// несколько объектов
var b1 = new Book();
var b2 = new Book("Title1", "Author1", "Publisher1", 2001, 300, 15.5, "Hard");
var b3 = new Book("Title2", "Author2");
var b4 = Book.CreateWithId(12345);

double newPrice = 20, oldPrice;
b2.UpdatePrice(ref newPrice, out oldPrice);
Console.WriteLine($"Old price: {oldPrice}, New price: {b2.Price}");

Console.WriteLine(b2.Equals(b3));
Console.WriteLine(b2.GetType());

// массив книг
Book[] books =
{
    new Book("War and Peace", "Tolstoy", "PublisherA", 1869, 1200, 30, "Hard"),
    new Book("Anna Karenina", "Tolstoy", "PublisherB", 1877, 900, 25, "Soft"),
    new Book("The Hobbit", "Tolkien", "PublisherC", 1937, 300, 20, "Hard")
};

string searchAuthor = "Tolstoy";
Console.WriteLine($"\nBooks by {searchAuthor}:");
foreach (var book in books)
    if (book.Author == searchAuthor)
        Console.WriteLine(book);

int year = 1900;
Console.WriteLine($"\nBooks after {year}:");
foreach (var book in books)
    if (book.Year > year)
        Console.WriteLine(book);

// анонимный тип
var anon = new { Title = "Sample", Author = "Somebody", Year = 2024 };
Console.WriteLine($"\nAnonymous: {anon.Title}, {anon.Author}, {anon.Year}");

Book.PrintClassInfo();
