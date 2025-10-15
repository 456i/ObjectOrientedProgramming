// --- 1. Пользовательские исключения ---
class InvalidAgeException : ArgumentOutOfRangeException
{
    public InvalidAgeException(string message)
        : base(message) { }
}

class InvalidPageCountException : Exception
{
    public InvalidPageCountException(string message)
        : base(message) { }
}

class EmptyTitleException : Exception
{
    public EmptyTitleException(string message)
        : base(message) { }
}

// --- 2. Классы предметной области ---
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name), "Имя не может быть пустым");

        if (age < 0 || age > 120)
            throw new InvalidAgeException("Возраст должен быть от 0 до 120");

        Name = name;
        Age = age;
    }
}

abstract class PrintedEdition
{
    public string Title { get; set; }
    public int Pages { get; set; }

    public PrintedEdition(string title, int pages)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new EmptyTitleException("Название не может быть пустым");

        if (pages <= 0)
            throw new InvalidPageCountException("Количество страниц должно быть > 0");

        Title = title;
        Pages = pages;
    }
}

class Book : PrintedEdition
{
    public Book(string title, int pages)
        : base(title, pages) { }
}

// --- 3. Контейнер ---
class PrintedEditionContainer
{
    private PrintedEdition[] items;
    private int count;

    public PrintedEditionContainer(int size)
    {
        if (size <= 0)
            throw new ArgumentOutOfRangeException(nameof(size), "Размер должен быть положительным");
        items = new PrintedEdition[size];
        count = 0;
    }

    public void Add(PrintedEdition edition)
    {
        if (count >= items.Length)
            throw new IndexOutOfRangeException("Контейнер переполнен!");
        items[count++] = edition;
    }
}

// Метод, который пробрасывает исключение
static void TestThrow()
{
    try
    {
        throw new InvalidPageCountException("Ошибка внутри TestThrow()");
    }
    catch (InvalidPageCountException ex)
    {
        Console.WriteLine("Локальная обработка в TestThrow()");
        throw; // проброс выше
    }
}

try
{
    // Исключение 1: неверный возраст
    Person p1 = new Person("Alice", -5);

    // Исключение 2: пустое название книги
    PrintedEdition b1 = new Book("", 100);

    // Исключение 3: нулевое количество страниц
    PrintedEdition b2 = new Book("C# Guide", 0);

    // Исключение 4: выход за пределы массива
    var container = new PrintedEditionContainer(1);
    container.Add(new Book("Book1", 100));
    container.Add(new Book("Book2", 200));

    // Исключение 5: работа с файлом
    using (StreamReader sr = new StreamReader("nofile.txt"))
    {
        Console.WriteLine(sr.ReadToEnd());
    }

    // Исключение 6: деление на ноль
    int x = 10,
        y = 0;
    int z = x / y;

    // Проброс исключения
    TestThrow();
    // int x = 7;
    // int z;
    // z = x /0;
}
catch (InvalidAgeException ex)
{
    Console.WriteLine($"[InvalidAgeException] {ex.Message}");
}
catch (EmptyTitleException ex)
{
    Console.WriteLine($"[EmptyTitleException] {ex.Message}");
}
catch (InvalidPageCountException ex)
{
    Console.WriteLine($"[InvalidPageCountException] {ex.Message}");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"[FileNotFoundException] {ex.Message}");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"[DivideByZeroException] {ex.Message}");
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"[IndexOutOfRangeException] {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"[General Exception] {ex.GetType().Name} — {ex.Message}");
    Console.WriteLine($"StackTrace: {ex.StackTrace}");
}
finally
{
    Console.WriteLine("=== Finally выполнен ===");
}

// --- 7. Assert ---
int testValue = -1;
Debug.Assert(testValue >= 0, "testValue должен быть неотрицательным!");
Console.WriteLine("Программа завершена.");
