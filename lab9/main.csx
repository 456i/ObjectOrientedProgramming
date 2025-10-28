using System.Collections.ObjectModel; // Для ObservableCollection<T>
using System.Collections.Specialized; // Для NotifyCollectionChangedEventArgs

// ---- Task 1 ------

// Интерфейс для демонстрации
interface IComputer
{
    string Name { get; set; }
    string CPU { get; set; }
    int RAM { get; set; }

    void ShowInfo();
}

// Класс по варианту
class Computer : IComputer
{
    public string Name { get; set; }
    public string CPU { get; set; }
    public int RAM { get; set; }

    public Computer(string name, string cpu, int ram)
    {
        Name = name;
        CPU = cpu;
        RAM = ram;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Name: {Name}, CPU: {CPU}, RAM: {RAM}GB");
    }

    // Для HashSet уникальность по имени
    public override bool Equals(object obj)
    {
        return obj is Computer c && c.Name == Name;
    }

    public override int GetHashCode() => Name.GetHashCode();
}

// Класс с вложенной коллекцией
class ComputerCollection
{
    public ISet<Computer> Computers { get; } = new HashSet<Computer>();

    public void Add(Computer c) => Computers.Add(c);

    public void Remove(Computer c) => Computers.Remove(c);

    public Computer Find(string name) => new List<Computer>(Computers).Find(c => c.Name == name);

    public void ShowAll()
    {
        foreach (var c in Computers)
            c.ShowInfo();
    }
}

// Демонстрация
var compCollection = new ComputerCollection();

compCollection.Add(new Computer("PC1", "Intel i5", 16));
compCollection.Add(new Computer("PC2", "AMD Ryzen 7", 32));
compCollection.Add(new Computer("PC3", "Intel i7", 16));

Console.WriteLine("--- Все компьютеры ---");
compCollection.ShowAll();

Console.WriteLine("\n--- Поиск PC2 ---");
compCollection.Find("PC2")?.ShowInfo();

Console.WriteLine("\n--- Удаление PC1 ---");
compCollection.Remove(new Computer("PC1", "", 0));
compCollection.ShowAll();

// ---- Task 2 ------

// Создаём коллекцию HashSet<int>
ISet<int> numbers = new HashSet<int> { 1, 2, 3, 4, 5, 6, 7 };

Console.WriteLine("\n--- Исходная коллекция ---");
Console.WriteLine(string.Join(", ", numbers));

// Удаляем n последовательных элементов
int n = 3;
int count = 0;
foreach (var num in new List<int>(numbers))
{
    if (count++ < n)
        numbers.Remove(num);
}

Console.WriteLine($"\n--- После удаления {n} элементов ---");
Console.WriteLine(string.Join(", ", numbers));

// Добавляем новые элементы (все методы)
numbers.Add(10);
numbers.UnionWith(new[] { 11, 12 });

Console.WriteLine("\n--- После добавления новых элементов ---");
Console.WriteLine(string.Join(", ", numbers));

// Создаём вторую коллекцию (List<int>) и копируем из первой
List<int> numberList = new List<int>(numbers);

Console.WriteLine("\n--- Вторая коллекция ---");
Console.WriteLine(string.Join(", ", numberList));

// Находим заданное значение
int searchValue = 11;
Console.WriteLine($"\nСодержит {searchValue}: {numberList.Contains(searchValue)}");

// ---- Task 3 ------

// Наблюдаемая коллекция компьютеров
ObservableCollection<Computer> observableComputers = new ObservableCollection<Computer>();

// Метод для события CollectionChanged
void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
{
    Console.WriteLine("\n--- Событие изменения коллекции ---");
    Console.WriteLine($"Action: {e.Action}");
    if (e.NewItems != null)
        foreach (Computer c in e.NewItems)
            Console.WriteLine($"Добавлен: {c.Name}");
    if (e.OldItems != null)
        foreach (Computer c in e.OldItems)
            Console.WriteLine($"Удалён: {c.Name}");
}

// Подписка на событие
observableComputers.CollectionChanged += OnCollectionChanged;

// Демонстрация добавления и удаления
observableComputers.Add(new Computer("PC4", "Intel i9", 64));
observableComputers.Add(new Computer("PC5", "AMD Ryzen 5", 16));
observableComputers.Remove(observableComputers[0]);
