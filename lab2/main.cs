public partial class Book
{
    private readonly int id;
    private string title;
    private string author;
    private string publisher;
    private int year;
    private int pages;
    private double price;
    private string coverType;

    private const string CATEGORY = "Литература";
    private static int count;

    public int Id => id;
    public string Title
    {
        get => title;
        set => title = value;
    }
    public string Author
    {
        get => author;
        set => author = value;
    }
    public string Publisher
    {
        get => publisher;
        set => publisher = value;
    }
    public int Year
    {
        get => year;
        set => year = value;
    }
    public int Pages
    {
        get => pages;
        set => pages = value;
    }
    public double Price
    {
        get => price;
        private set => price = value;
    }
    public string CoverType
    {
        get => coverType;
        set => coverType = value;
    }

    public Book()
        : this("Неизвестно", "Неизвестно", "Неизвестно", DateTime.Now.Year, 0, 0, "Мягкая") { }

    public Book(
        string title,
        string author,
        string publisher,
        int year,
        int pages,
        double price,
        string coverType
    )
    {
        this.id = GetNextId();
        this.title = title;
        this.author = author;
        this.publisher = publisher;
        this.year = year;
        this.pages = pages;
        this.price = price;
        this.coverType = coverType;
        count++;
    }

    public Book(string title = "Название по умолчанию", string author = "Автор по умолчанию")
        : this(title, author, "Издательство по умолчанию", 2024, 100, 10.0, "Мягкая") { }

    private Book(int customId, string customTitle)
    {
        this.id = customId;
        this.title = customTitle;
        this.author = "Приватный конструктор";
        this.publisher = "Неизвестно";
        this.year = 2024;
        this.pages = 0;
        this.price = 0;
        this.coverType = "Мягкая";
        count++;
    }

    static Book()
    {
        count = 0;
        Console.WriteLine("Статический конструктор вызван");
    }

    public static Book CreateFromPrivateConstructor(int id, string title) => new Book(id, title);

    public bool UpdatePriceWithRefOut(ref double newPrice, out double oldPrice, out string status)
    {
        oldPrice = this.price;

        if (newPrice > 0)
        {
            this.price = newPrice;
            status = "Цена успешно обновлена";
            return true;
        }
        else
        {
            status = "Ошибка: цена должна быть положительной";
            return false;
        }
    }

    public static void PrintClassInfo()
    {
        Console.WriteLine($"Информация о классе: Создано объектов: {count}, Категория: {CATEGORY}");
    }

    private static int GetNextId() => Math.Abs(Guid.NewGuid().GetHashCode());

    public override bool Equals(object obj) => obj is Book other && this.id == other.id;

    public override int GetHashCode() => this.id;

    public override string ToString() =>
        $"'{this.Title}' автор {this.Author} ({this.Year}), {this.Pages} стр., {this.Price} руб., Обложка: {this.CoverType}, ID: {this.Id}";
}

public partial class Book
{
    public string GetShortInfo() => $"{this.Title} ({this.Author}, {this.Year})";
}

Console.WriteLine("1. Создание объектов:");
var book1 = new Book();
Console.WriteLine($"   Book1: {book1}");

var book2 = new Book("1984", "Джордж Оруэлл", "Secker & Warburg", 1949, 328, 15.99, "Твердая");
Console.WriteLine($"   Book2: {book2}");

var book3 = new Book("Преступление и наказание", "Достоевский");
Console.WriteLine($"   Book3: {book3}");

var book4 = Book.CreateFromPrivateConstructor(9999, "Секретная книга");
Console.WriteLine($"   Book4: {book4}");

Console.WriteLine("\n2. Работа со свойствами:");
book1.Title = "Новое название";
book1.Author = "Новый автор";
Console.WriteLine($"   Book1 после изменений: {book1}");

Console.WriteLine("\n3. Метод с ref/out параметрами:");
double newPrice = 25.50;
if (book2.UpdatePriceWithRefOut(ref newPrice, out double oldPrice, out string message))
{
    Console.WriteLine($"   {message}");
    Console.WriteLine($"   Старая цена: {oldPrice}, Новая цена: {book2.Price}");
}

Console.WriteLine("\n4. Сравнение объектов:");
Console.WriteLine($"   book1.Equals(book2): {book1.Equals(book2)}");
Console.WriteLine($"   Хэш-коды: book1={book1.GetHashCode()}, book2={book2.GetHashCode()}");

Console.WriteLine($"\n5. Тип объекта: {book1.GetType()}");

Console.WriteLine("\n6. Работа с массивом объектов:");
Book[] library =
{
    new Book("Война и мир", "Толстой", "Русский вестник", 1869, 1225, 30.0, "Твердая"),
    new Book("Анна Каренина", "Толстой", "Русский вестник", 1877, 864, 25.0, "Мягкая"),
    new Book("Мастер и Маргарита", "Булгаков", "Московский рабочий", 1967, 384, 20.0, "Твердая"),
};

string searchAuthor = "Толстой";
Console.WriteLine($"\nКниги автора '{searchAuthor}':");
foreach (var book in library)
{
    if (book.Author.Contains(searchAuthor))
    {
        Console.WriteLine($"   • {book.Title} ({book.Year})");
    }
}

Console.WriteLine($"\nКниги после 1900 года:");
foreach (var book in library)
{
    if (book.Year > 1900)
    {
        Console.WriteLine($"   • {book.Title} ({book.Year})");
    }
}

Console.WriteLine("\n7. Анонимный тип:");
var anonymousBook = new
{
    Title = "Анонимная книга",
    Author = "Неизвестный автор",
    Year = 2024,
    Price = 15.99,
};
Console.WriteLine(
    $"   Анонимный: {anonymousBook.Title}, {anonymousBook.Author}, {anonymousBook.Year}"
);

Console.WriteLine("\n8. Статическая информация:");
Book.PrintClassInfo();
