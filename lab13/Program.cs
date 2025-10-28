using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        // BINARY: Данные хранятся как поток байтов (нечитаемый для человека)
        // Пример: "AAEAAAD/////AQAAAAAAAAAMAgAAAFlTeXN0ZW0u... (base64)"
        // Плюсы: компактно, быстро | Минусы: нечитаем, зависит от платформы
        //
        // SOAP: XML в специальном конверте для веб-сервисов
        // Пример: "<soap:Envelope><soap:Body><Book>...</Book></soap:Body></soap:Envelope>"
        // Плюсы: стандарт для веб-сервисов, читаем | Минусы: многословный
        //
        // XML: Иерархическая структура с тегами
        // Пример: "<Book><Title>C# Programming</Title><Author>...</Author></Book>"
        // Плюсы: читаем, стандартен | Минусы: многословный, большой размер
        //
        // JSON: Легковесный текстовый формат "ключ-значение"
        // Пример: `{"Title":"C# Programming","Author":{"Name":"John Smith"...}}`
        // Плюсы: компактный, читаем, идеален для веб | Минусы: менее строгий чем XML

        // Создаем тестовые данные
        var publisher = new Publisher("Tech Pub", "Main St 1");
        var author = new Author("John Smith", 45, "Famous author");
        var book = new Book("C# Programming", 350, publisher, author);
        var book2 = new Book("ASP.NET Core", 280, publisher, author);

        // ========== ЗАДАНИЕ 1: Сериализация в 4 форматах ==========
        Console.WriteLine("=== ЗАДАНИЕ 1: СЕРИАЛИЗАЦИЯ В 4 ФОРМАТАХ ===");

        var formats = new[] { "binary", "soap", "xml", "json" };

        foreach (var format in formats)
        {
            Console.WriteLine($"\n--- {format.ToUpper()} ---");

            try
            {
                var serializer = SerializerFactory.CreateSerializer(format);

                // Сериализуем
                string serialized = serializer.Serialize(book);
                Console.WriteLine(
                    $"Сериализовано: {serialized.Substring(0, Math.Min(100, serialized.Length))}..."
                );

                // Десериализуем
                var deserialized = serializer.Deserialize<Book>(serialized);
                Console.WriteLine($"Десериализовано: {deserialized.Title}");

                // Проверяем отсутствие поля Pages
                Console.WriteLine(
                    $"Pages после десериализации: {deserialized.Pages} (должно быть 0)"
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        // ========== ЗАДАНИЕ 2: Сериализация коллекции объектов ==========
        Console.WriteLine("\n\n=== ЗАДАНИЕ 2: СЕРИАЛИЗАЦИЯ КОЛЛЕКЦИИ ===");

        var bookList = new List<Book> { book, book2, book };
        var xmlSerializer = SerializerFactory.CreateSerializer("xml");

        // Сериализуем коллекцию
        string collectionXml = xmlSerializer.Serialize(bookList);
        Console.WriteLine($"Коллекция сериализована: {collectionXml.Length} chars");

        // Десериализуем коллекцию
        var deserializedList = xmlSerializer.Deserialize<List<Book>>(collectionXml);
        Console.WriteLine($"Десериализовано книг: {deserializedList.Count}");

        foreach (var b in deserializedList)
        {
            Console.WriteLine($"  - {b.Title}");
        }

        // ========== ЗАДАНИЕ 3: XPath селекторы ==========
        Console.WriteLine("\n\n=== ЗАДАНИЕ 3: XPATH СЕЛЕКТОРЫ ===");

        try
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(collectionXml);

            // Селектор 1: все названия книг
            var titles = xmlDoc.SelectNodes("//Title");
            Console.WriteLine($"Селектор 1 - //Title: найдено {titles?.Count} элементов");
            if (titles != null)
            {
                foreach (XmlNode title in titles)
                {
                    Console.WriteLine($"  - {title.InnerText}");
                }
            }

            // Селектор 2: книги с авторами
            var booksWithAuthors = xmlDoc.SelectNodes("//Book[Author]");
            Console.WriteLine(
                $"\nСелектор 2 - //Book[Author]: найдено {booksWithAuthors?.Count} книг с авторами"
            );

            // Селектор 3: первая книга
            var firstBook = xmlDoc.SelectSingleNode("//Book[1]");
            Console.WriteLine(
                $"\nСелектор 3 - //Book[1]: первая книга - {firstBook?.SelectSingleNode("Title")?.InnerText}"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка XPath: {ex.Message}");
        }

        // ========== ЗАДАНИЕ 4: LINQ to XML ==========
        Console.WriteLine("\n\n=== ЗАДАНИЕ 4: LINQ TO XML ===");

        // Создание XML документа через LINQ to XML
        var xdoc = new XDocument(
            new XElement(
                "Library",
                new XElement(
                    "Books",
                    new XElement(
                        "Book",
                        new XAttribute("id", 1),
                        new XElement("Title", "C# Programming"),
                        new XElement("Author", "John Smith"),
                        new XElement("Year", 2023),
                        new XElement("Price", 49.99)
                    ),
                    new XElement(
                        "Book",
                        new XAttribute("id", 2),
                        new XElement("Title", "ASP.NET Core"),
                        new XElement("Author", "Jane Doe"),
                        new XElement("Year", 2024),
                        new XElement("Price", 59.99)
                    ),
                    new XElement(
                        "Book",
                        new XAttribute("id", 3),
                        new XElement("Title", "Entity Framework"),
                        new XElement("Author", "John Smith"),
                        new XElement("Year", 2023),
                        new XElement("Price", 39.99)
                    )
                ),
                new XElement(
                    "Statistics",
                    new XElement("TotalBooks", 3),
                    new XElement("TotalValue", 149.97)
                )
            )
        );

        Console.WriteLine("Созданный XML документ:");
        Console.WriteLine(xdoc.ToString());

        // LINQ запрос 1: все книги дороже 45
        var expensiveBooks =
            from bookElement in xdoc.Descendants("Book")
            where (double)bookElement.Element("Price") > 45
            select new
            {
                Title = (string)bookElement.Element("Title"),
                Price = (double)bookElement.Element("Price"),
            };

        Console.WriteLine("\nLINQ запрос 1 - книги дороже $45:");
        foreach (var b in expensiveBooks)
        {
            Console.WriteLine($"  - {b.Title}: ${b.Price}");
        }

        // LINQ запрос 2: книги John Smith
        var johnsBooks = xdoc.Descendants("Book")
            .Where(b => (string)b.Element("Author") == "John Smith")
            .Select(b => new { Title = (string)b.Element("Title"), Year = (int)b.Element("Year") });

        Console.WriteLine("\nLINQ запрос 2 - книги John Smith:");
        foreach (var b in johnsBooks)
        {
            Console.WriteLine($"  - {b.Title} ({b.Year})");
        }

        // LINQ запрос 3: группировка по году
        var booksByYear =
            from bookElement in xdoc.Descendants("Book")
            group bookElement by (int)bookElement.Element("Year") into yearGroup
            select new
            {
                Year = yearGroup.Key,
                Count = yearGroup.Count(),
                Titles = yearGroup.Select(b => (string)b.Element("Title")),
            };

        Console.WriteLine("\nLINQ запрос 3 - группировка по году:");
        foreach (var group in booksByYear)
        {
            Console.WriteLine($"  - {group.Year}: {group.Count} книг");
            foreach (var title in group.Titles)
            {
                Console.WriteLine($"    * {title}");
            }
        }

        Console.WriteLine("\n=== ВСЕ ЗАДАНИЯ ВЫПОЛНЕНЫ ===");
    }
}
