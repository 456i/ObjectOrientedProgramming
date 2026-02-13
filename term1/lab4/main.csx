// --- Интерфейс ---
interface ICloneableInfo
{
    bool DoClone();
}

// --- Абстрактный класс ---
abstract class PrintedEdition
{
    public string Title { get; set; }
    public int Pages { get; set; }
    public Publisher Publisher { get; set; }

    public PrintedEdition(string title, int pages, Publisher publisher)
    {
        Title = title;
        Pages = pages;
        Publisher = publisher;
    }

    public virtual string PrintInfo()
    {
        return $"Title: {Title}, Pages: {Pages}, Publisher: {Publisher.Name}";
    }

    public abstract bool DoClone();

    public override string ToString()
    {
        return $"Type: {GetType().Name}, Title: {Title}, Pages: {Pages}, Publisher: {Publisher.Name}";
    }
}

// --- Книга ---
class Book : PrintedEdition
{
    public Author Author { get; set; }

    public Book(string title, int pages, Publisher publisher, Author author)
        : base(title, pages, publisher)
    {
        Author = author;
    }

    public override string PrintInfo()
    {
        return $"{base.PrintInfo()}, Author: {Author.Name}";
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
        int classLevel
    )
        : base(title, pages, publisher, author)
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
    public string Month { get; set; }

    public Journal(string title, int pages, Publisher publisher, int issueNumber, string month)
        : base(title, pages, publisher)
    {
        IssueNumber = issueNumber;
        Month = month;
    }

    public override string PrintInfo()
    {
        return $"{base.PrintInfo()}, Issue: {IssueNumber}, Month: {Month}";
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

// --- Printer ---
class Printer
{
    public void IAmPrinting(PrintedEdition item)
    {
        Console.WriteLine($"Type: {item.GetType().Name}");
        Console.WriteLine(item.ToString());
        item.DoClone();
    }
}

// --- Демонстрация ---

Publisher pub = new Publisher("Tech Pub", "Main St. 1");
Author author = new Author("Ivan Ivanov", 45, "Famous author");

Book book = new Book("Book One", 300, pub, author);
Textbook textbook = new Textbook("Math Basics", 200, pub, author, "Math", 7);
Journal journal = new Journal("Tech Journal", 50, pub, 5, "September");

PrintedEdition[] editions = { book, textbook, journal };
Printer printer = new Printer();

foreach (var ed in editions)
{
    printer.IAmPrinting(ed);
}

// Работа с интерфейсом
ICloneableInfo cloneAuthor = author;
cloneAuthor.DoClone();
