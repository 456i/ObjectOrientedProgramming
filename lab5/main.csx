#load "partialClass.csx"

// --- Перечисление ---
enum Genre
{
    Fiction,
    Science,
    Education,
    Technology,
    History,
}

// --- Структура ---
struct ReleaseDate
{
    public int Year { get; set; }
    public int Month { get; set; }

    public ReleaseDate(int year, int month)
    {
        Year = year;
        Month = month;
    }

    public override string ToString() => $"{Month:D2}/{Year}";
}

// --- Интерфейс ---
interface ICloneableInfo
{
    bool DoClone();
}

// --- Абстрактный класс (partial в другом файле) ---
abstract partial class PrintedEdition
{
    public string Title { get; set; }
    public int Pages { get; set; }
    public Publisher Publisher { get; set; }
    public Genre Genre { get; set; }
    public ReleaseDate ReleaseDate { get; set; }

    public PrintedEdition(
        string title,
        int pages,
        Publisher publisher,
        Genre genre,
        ReleaseDate release
    )
    {
        Title = title;
        Pages = pages;
        Publisher = publisher;
        Genre = genre;
        ReleaseDate = release;
    }
}

// --- Книга ---
class Book : PrintedEdition
{
    public Author Author { get; set; }

    public Book(
        string title,
        int pages,
        Publisher publisher,
        Author author,
        Genre genre,
        ReleaseDate release
    )
        : base(title, pages, publisher, genre, release)
    {
        Author = author;
    }

    public override string PrintInfo()
    {
        return $"{base.PrintInfo()}, Author: {Author.Name}, Genre: {Genre}, Release: {ReleaseDate}";
    }

    public override bool DoClone()
    {
        Console.WriteLine("Book clone method");
        return true;
    }

    public override string ToString() => PrintInfo();
}

// --- Учебник ---
class Textbook : Book
{
    public string Subject { get; set; }
    public int ClassLevel { get; set; }

    public Textbook(
        string title,
        int pages,
        Publisher publisher,
        Author author,
        string subject,
        int classLevel,
        Genre genre,
        ReleaseDate release
    )
        : base(title, pages, publisher, author, genre, release)
    {
        Subject = subject;
        ClassLevel = classLevel;
    }

    public override string PrintInfo()
    {
        return $"{base.PrintInfo()}, Subject: {Subject}, Class: {ClassLevel}";
    }

    public override bool DoClone()
    {
        Console.WriteLine("Textbook clone method");
        return true;
    }

    public override string ToString() => PrintInfo();
}

// --- Журнал (sealed) ---
sealed class Journal : PrintedEdition
{
    public int IssueNumber { get; set; }
    public string MonthName { get; set; }

    public Journal(
        string title,
        int pages,
        Publisher publisher,
        int issueNumber,
        string month,
        Genre genre,
        ReleaseDate release
    )
        : base(title, pages, publisher, genre, release)
    {
        IssueNumber = issueNumber;
        MonthName = month;
    }

    public override string PrintInfo()
    {
        return $"{base.PrintInfo()}, Issue: {IssueNumber}, Month: {MonthName}";
    }

    public override bool DoClone()
    {
        Console.WriteLine("Journal clone method");
        return true;
    }

    public override string ToString() => PrintInfo();
}

// --- Персона ---
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString() => $"Person: {Name}, Age: {Age}";
}

// --- Автор ---
class Author : Person, ICloneableInfo
{
    public string Biography { get; set; }

    public Author(string name, int age, string bio)
        : base(name, age)
    {
        Biography = bio;
    }

    public string GetInfo() => $"Author: {Name}, Age: {Age}, Bio: {Biography}";

    // Интерфейсная реализация
    public bool DoClone()
    {
        Console.WriteLine("Author clone via interface");
        return true;
    }

    public override string ToString() => GetInfo();
}

// --- Издательство ---
class Publisher
{
    public string Name { get; set; }
    public string Address { get; set; }

    public Publisher(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public override string ToString() => $"Publisher: {Name}, Address: {Address}";
}

// --- Контейнер ---
class PrintedEditionContainer
{
    private List<PrintedEdition> items = new List<PrintedEdition>();

    public void Add(PrintedEdition item) => items.Add(item);

    public void Remove(PrintedEdition item) => items.Remove(item);

    public PrintedEdition Get(int index) => items[index];

    public void Set(int index, PrintedEdition item) => items[index] = item;

    public void PrintAll()
    {
        Console.WriteLine("=== Container Contents ===");
        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }

    public List<PrintedEdition> GetAll() => items;
}

// --- Контроллер ---
class PrintedEditionController
{
    private PrintedEditionContainer container;

    public PrintedEditionController(PrintedEditionContainer c)
    {
        container = c;
    }

    // Пример запроса: отбор по жанру
    public void PrintByGenre(Genre genre)
    {
        Console.WriteLine($"=== Filtered by Genre: {genre} ===");
        foreach (var item in container.GetAll())
        {
            if (item.Genre == genre)
                Console.WriteLine(item);
        }
    }

    // Пример запроса: поиск по количеству страниц
    public void PrintByPages(int minPages)
    {
        Console.WriteLine($"=== Filtered by Pages > {minPages} ===");
        foreach (var item in container.GetAll())
        {
            if (item.Pages > minPages)
                Console.WriteLine(item);
        }
    }
}

// --- Демонстрация ---

Publisher pub = new Publisher("Tech Pub", "Main St. 1");
Author author = new Author("Ivan Ivanov", 45, "Famous author");

Book book = new Book("Book One", 300, pub, author, Genre.Fiction, new ReleaseDate(2020, 5));
Textbook textbook = new Textbook(
    "Math Basics",
    200,
    pub,
    author,
    "Math",
    7,
    Genre.Education,
    new ReleaseDate(2021, 9)
);
Journal journal = new Journal(
    "Tech Journal",
    50,
    pub,
    5,
    "September",
    Genre.Technology,
    new ReleaseDate(2022, 9)
);

// Контейнер
PrintedEditionContainer container = new PrintedEditionContainer();
container.Add(book);
container.Add(textbook);
container.Add(journal);

container.PrintAll();

// Контроллер
PrintedEditionController controller = new PrintedEditionController(container);
controller.PrintByGenre(Genre.Education);
controller.PrintByPages(100);

// Интерфейс
ICloneableInfo cloneAuthor = author;
cloneAuthor.DoClone();
