using System.Text.Json;

// ======== 1. ОБОБЩЁННЫЙ ИНТЕРФЕЙС ========
interface IMyCollection<T>
{
    void Add(T item);
    void Remove(T item);
    void View();
}

// ======== 2. ОБОБЩЁННЫЙ КЛАСС ========
class CollectionType<T> : IMyCollection<T>
    where T : class
{
    private List<T> items = [];

    public void Add(T item)
    {
        try
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Элемент не может быть null");
            items.Add(item);
            Console.WriteLine($"Добавлен элемент: {item}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Метод Add завершён.\n");
        }
    }

    public void Remove(T item)
    {
        try
        {
            if (!items.Remove(item))
                throw new KeyNotFoundException("Элемент не найден для удаления");
            Console.WriteLine($"Удалён элемент: {item}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Метод Remove завершён.\n");
        }
    }

    public void View()
    {
        Console.WriteLine("Содержимое коллекции:");
        foreach (var i in items)
            Console.WriteLine(i);
        Console.WriteLine();
    }

    public T Find(Predicate<T> predicate)
    {
        try
        {
            var result = items.Find(predicate);
            if (result == null)
                throw new Exception("Элемент по условию не найден.");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка поиска: {ex.Message}");
            return null;
        }
        finally
        {
            Console.WriteLine("Метод Find завершён.\n");
        }
    }

    // ======== Сохранение и загрузка ========

    public void SaveToFile(string filename)
    {
        try
        {
            string json = JsonSerializer.Serialize(
                items,
                new JsonSerializerOptions { WriteIndented = true }
            );
            File.WriteAllText(filename, json);
            Console.WriteLine($"Коллекция сохранена в файл {filename}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
        }
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("Файл не найден.");

            string json = File.ReadAllText(filename);
            items = JsonSerializer.Deserialize<List<T>>(json);
            Console.WriteLine($"Коллекция загружена из файла {filename}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
        }
    }
}

// ======== 3. КЛАССЫ ИЗ ЛАБОРАТОРНОЙ №4 ========

class Publisher
{
    public string Name { get; set; }
    public string Address { get; set; }

    public Publisher(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public override string ToString() => $"{Name} ({Address})";
}

class Author
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Bio { get; set; }

    public Author(string name, int age, string bio)
    {
        Name = name;
        Age = age;
        Bio = bio;
    }

    public override string ToString() => $"{Name}, {Age} лет — {Bio}";
}

abstract class PrintedEdition
{
    public string Title { get; set; }
    public int Pages { get; set; }
    public Publisher Publisher { get; set; }

    protected PrintedEdition(string title, int pages, Publisher publisher)
    {
        Title = title;
        Pages = pages;
        Publisher = publisher;
    }

    public virtual string PrintInfo() => $"Название: {Title}, стр: {Pages}, изд: {Publisher.Name}";
}

class Book : PrintedEdition
{
    public Author Author { get; set; }

    public Book(string title, int pages, Publisher publisher, Author author)
        : base(title, pages, publisher)
    {
        Author = author;
    }

    public override string ToString() => $"{PrintInfo()}, Автор: {Author.Name}";
}

// ======== 4. ДЕМОНСТРАЦИЯ РАБОТЫ ========

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("=== Пример 1: стандартные типы ===");
var intCollection = new CollectionType<string>();
intCollection.Add("яблоко");
intCollection.Add("банан");
intCollection.View();
intCollection.Remove("яблоко");
intCollection.View();

Console.WriteLine("=== Пример 2: пользовательский тип (Book) ===");
var pub = new Publisher("TechBooks", "Минск, пр. Победителей 1");
var author = new Author("Иван Иванов", 40, "писатель-фантаст");

var books = new CollectionType<Book>();
books.Add(new Book("Галактический Путь", 320, pub, author));
books.Add(new Book("Сингулярность", 280, pub, author));

books.View();

// Поиск по предикату
var found = books.Find(b => b.Pages > 300);
Console.WriteLine("Найденная книга: " + found + "\n");

// Сохранение и загрузка
books.SaveToFile("books.json");
var loadedBooks = new CollectionType<Book>();
loadedBooks.LoadFromFile("books.json");
loadedBooks.View();

Console.WriteLine("Работа завершена.");
