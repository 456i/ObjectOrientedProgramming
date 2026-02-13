public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return $"{Title} — {Author}, {Year}, {Pages} стр., {Price} руб.";
    }
}

// ----TASK1----
string[] months =
{
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
};

int n = 5;

var lengthN = from m in months where m.Length == n select m;

string[] summerWinter = { "December", "January", "February", "June", "July", "August" };

var summerWinterMonths = months.Where(m => summerWinter.Contains(m));

var alphabetOrder = months.OrderBy(m => m);

var containsU = months.Where(m => m.Contains('u') && m.Length >= 4);

Console.WriteLine("Месяцы длиной " + n + ": " + string.Join(", ", lengthN));
Console.WriteLine("Летние и зимние: " + string.Join(", ", summerWinterMonths));
Console.WriteLine("Алфавитный порядок: " + string.Join(", ", alphabetOrder));
Console.WriteLine("Содержат 'u' и длиной ≥ 4: " + string.Join(", ", containsU));

// ----TASK2----
List<Book> books = new List<Book>
{
    new Book
    {
        Title = "Clean Code",
        Author = "Robert Martin",
        Publisher = "Prentice Hall",
        Year = 2008,
        Pages = 464,
        Price = 45,
    },
    new Book
    {
        Title = "C# in Depth",
        Author = "Jon Skeet",
        Publisher = "Manning",
        Year = 2019,
        Pages = 900,
        Price = 60,
    },
    new Book
    {
        Title = "The Pragmatic Programmer",
        Author = "Andrew Hunt",
        Publisher = "Addison-Wesley",
        Year = 1999,
        Pages = 352,
        Price = 40,
    },
    new Book
    {
        Title = "Code Complete",
        Author = "Steve McConnell",
        Publisher = "Microsoft Press",
        Year = 2004,
        Pages = 960,
        Price = 55,
    },
    new Book
    {
        Title = "Design Patterns",
        Author = "Erich Gamma",
        Publisher = "Addison-Wesley",
        Year = 1994,
        Pages = 395,
        Price = 50,
    },
    new Book
    {
        Title = "Refactoring",
        Author = "Martin Fowler",
        Publisher = "Addison-Wesley",
        Year = 2018,
        Pages = 448,
        Price = 70,
    },
    new Book
    {
        Title = "Effective C#",
        Author = "Bill Wagner",
        Publisher = "Pearson",
        Year = 2020,
        Pages = 320,
        Price = 35,
    },
    new Book
    {
        Title = "Introduction to Algorithms",
        Author = "Thomas Cormen",
        Publisher = "MIT Press",
        Year = 2009,
        Pages = 1312,
        Price = 90,
    },
    new Book
    {
        Title = "Head First Design Patterns",
        Author = "Eric Freeman",
        Publisher = "O'Reilly",
        Year = 2020,
        Pages = 694,
        Price = 65,
    },
    new Book
    {
        Title = "Pro C# 10",
        Author = "Andrew Troelsen",
        Publisher = "Apress",
        Year = 2022,
        Pages = 1500,
        Price = 80,
    },
};

// Пример запроса LINQ (синтаксис LINQ)
var expensiveBooks = from b in books where b.Price > 60 select b;

// Пример через методы расширения
var recentBooks = books
    .Where(b => b.Year >= 2015)
    .OrderByDescending(b => b.Year)
    .Select(b => b.Title);

Console.WriteLine("Дорогие книги (>60):");
foreach (var b in expensiveBooks)
    Console.WriteLine(b);

Console.WriteLine("\nСовременные книги:");
Console.WriteLine(string.Join(", ", recentBooks));

// ----TASK3----
var byAuthor = books.Where(b => b.Author.Contains("Martin"));
var byPublisher = books.Where(b => b.Publisher == "Addison-Wesley");
var afterYear = books.Where(b => b.Year > 2010);
var sortedByPrice = books.OrderBy(b => b.Price);
var maxPages = books.Max(b => b.Pages);
var largestBooks = books.Where(b => b.Pages == maxPages);

// ----TASK4----
var complexQuery = books
    .Where(b => b.Year > 2000 && b.Price > 40)
    .GroupBy(b => b.Publisher)
    .Select(g => new
    {
        Publisher = g.Key,
        AvgPrice = g.Average(b => b.Price),
        Count = g.Count(),
        HasThickBook = g.Any(b => b.Pages > 1000),
    })
    .OrderByDescending(g => g.AvgPrice)
    .Take(3);

Console.WriteLine("\nТоп издательств по средней цене книг:");
foreach (var item in complexQuery)
    Console.WriteLine(
        $"{item.Publisher}: {item.AvgPrice} руб., {item.Count} книг, толстая книга: {item.HasThickBook}"
    );

// ----TASK5----
var authors = new List<(string Name, string Country)>
{
    ("Robert Martin", "USA"),
    ("Jon Skeet", "UK"),
    ("Andrew Hunt", "USA"),
    ("Steve McConnell", "USA"),
    ("Erich Gamma", "Switzerland"),
    ("Martin Fowler", "UK"),
    ("Bill Wagner", "USA"),
    ("Thomas Cormen", "USA"),
    ("Eric Freeman", "USA"),
    ("Andrew Troelsen", "Denmark"),
};
var joinQuery =
    from b in books
    join a in authors on b.Author equals a.Name
    select new
    {
        Book = b.Title,
        Author = b.Author,
        a.Country,
        b.Price,
    };

Console.WriteLine("\nКниги и страны авторов (Join):");
foreach (var item in joinQuery)
    Console.WriteLine($"{item.Book} — {item.Author} ({item.Country}), {item.Price} руб.");

var Query = books.Where(b => (b.Year <= 2000)).Where(b => (b.Year >= 1990)).Select(b => b.Author);

Console.WriteLine("extra task");
foreach (var b in Query)
{
    Console.WriteLine(b);
}
