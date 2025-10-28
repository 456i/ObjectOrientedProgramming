// IEnumerator<T> — это объект для итерации по одному элементу

// IEnumerable<T> — это объект, который может создавать новый IEnumerator каждый раз, когда нужен цикл foreach

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

public static class Reflector
{
    public static string output_path = "output.json";

    public static string GetAssemblyName(Type t) => t.Assembly.FullName;

    public static bool HasPublicConstructors(Type t) => t.GetConstructors().Length > 0;

    public static IEnumerable<string> GetPublicMethods(Type t)
    {
        foreach (
            var m in t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
        )
            yield return m.Name;
    }

    public static IEnumerable<string> GetFieldsAndProperties(Type t)
    {
        foreach (
            var f in t.GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance
            )
        )
            yield return $"Field: {f.Name}";

        foreach (var p in t.GetProperties())
            yield return $"Property: {p.Name}";
    }

    public static IEnumerable<string> GetInterfaces(Type t)
    {
        foreach (var i in t.GetInterfaces())
            yield return i.Name;
    }

    public static IEnumerable<string> GetMethodsWithParameterType(Type t, Type paramType)
    {
        foreach (var m in t.GetMethods())
        {
            foreach (var p in m.GetParameters())
            {
                if (p.ParameterType == paramType)
                {
                    yield return m.Name;
                    break;
                }
            }
        }
    }

    public static void Invoke(Type t, string methodName, string paramFile = "param.txt")
    {
        object obj = Activator.CreateInstance(t);
        MethodInfo method = t.GetMethod(methodName);

        if (method == null)
        {
            Console.WriteLine($"Метод {methodName} не найден в {t.Name}");
            return;
        }

        object[] parameters = ReadParametersFromFile(paramFile, method);

        Console.WriteLine($"Вызов {methodName} у {t.Name}:");
        var result = method.Invoke(obj, parameters);
        Console.WriteLine($"Результат: {result}");
    }

    public static object[] ReadParametersFromFile(string file_path, MethodInfo method)
    {
        string[] lines = File.Exists(file_path)
            ? File.ReadAllLines(file_path)
            : Array.Empty<string>();
        ParameterInfo[] param_info = method.GetParameters();

        List<object> result = new();

        for (int i = 0; i < param_info.Length; i++)
        {
            string value = i < lines.Length ? lines[i] : "";
            Type paramType = param_info[i].ParameterType;
            result.Add(Convert.ChangeType(value, paramType));
        }
        return result.ToArray();
    }

    private static object[] GenerateParameters(MethodInfo method)
    {
        Random rnd = new();
        List<object> result = new();

        foreach (var p in method.GetParameters())
        {
            if (p.ParameterType == typeof(int))
                result.Add(rnd.Next(1, 100));
            else if (p.ParameterType == typeof(double))
                result.Add(rnd.NextDouble() * 10);
            else if (p.ParameterType == typeof(string))
                result.Add("GeneratedValue");
            else
                result.Add(null);
        }

        return result.ToArray();
    }

    public static void WriteToFile(object data)
    {
        string json = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(output_path, json);
        Console.WriteLine($"Информация записана в {output_path}");
    }

    public static T Create<T>()
        where T : new()
    {
        return new T();
    }
}

interface IComputer
{
    string Name { get; set; }
    string CPU { get; set; }
    int RAM { get; set; }

    void ShowInfo();
}

class Computer : IComputer
{
    public string Name { get; set; }
    public string CPU { get; set; }
    public int RAM { get; set; }

    public Computer() { } // добавлен конструктор без параметров для Invoke

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

    public override bool Equals(object obj)
    {
        return obj is Computer c && c.Name == Name;
    }

    public override int GetHashCode() => Name.GetHashCode();
}

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

// ======= Программа =======
class Program
{
    static void Main()
    {
        Type compType = typeof(Computer);
        Type bookType = typeof(Book);

        Console.WriteLine("=== Исследование Computer ===");
        var info = new
        {
            Assembly = Reflector.GetAssemblyName(compType),
            HasConstructors = Reflector.HasPublicConstructors(compType),
            Methods = Reflector.GetPublicMethods(compType),
            FieldsAndProps = Reflector.GetFieldsAndProperties(compType),
            Interfaces = Reflector.GetInterfaces(compType),
        };

        Reflector.WriteToFile(info);

        Console.WriteLine("\nМетоды с параметром string:");
        foreach (var m in Reflector.GetMethodsWithParameterType(compType, typeof(string)))
            Console.WriteLine($"- {m}");

        // Вызов метода ShowInfo без параметров
        Reflector.Invoke(compType, "ShowInfo");

        // Демонстрация Create<T>
        var newBook = Reflector.Create<Book>();
        Console.WriteLine($"\nСоздан объект типа: {newBook.GetType().Name}");
    }
}
