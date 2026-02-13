using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

[Serializable]
public abstract class PrintedEdition
{
    [XmlElement(ElementName = "Title")]
    public string Title { get; set; }

    [XmlIgnore]
    [JsonIgnore]
    [NonSerialized]
    public int Pages; // Запрещенное поле

    [XmlElement(ElementName = "Publisher")]
    public Publisher Publisher { get; set; }

    public PrintedEdition() { }

    public PrintedEdition(string title, int pages, Publisher publisher)
    {
        this.Title = title;
        this.Pages = pages;
        this.Publisher = publisher;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Publisher: {Publisher?.Name}, Pages: {Pages}";
    }
}

[Serializable]
public class Book : PrintedEdition
{
    [XmlElement(ElementName = "Author")]
    public Author Author { get; set; }

    public Book() { }

    public Book(string title, int pages, Publisher publisher, Author author)
        : base(title, pages, publisher)
    {
        this.Author = author;
    }

    public override string ToString()
    {
        return $"Book: {Title}, Author: {Author?.Name}, Publisher: {Publisher?.Name}, Pages: {Pages}";
    }
}

[Serializable]
public class Author
{
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Age")]
    public int Age { get; set; }

    [XmlElement(ElementName = "Biography")]
    public string Biography { get; set; }

    public Author() { }

    public Author(string name, int age, string bio)
    {
        this.Name = name;
        this.Age = age;
        this.Biography = bio;
    }

    public override string ToString()
    {
        return $"Author: {Name}, Age: {Age}";
    }
}

[Serializable]
public class Publisher
{
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "Address")]
    public string Address { get; set; }

    public Publisher() { }

    public Publisher(string name, string address)
    {
        this.Name = name;
        this.Address = address;
    }

    public override string ToString()
    {
        return $"Publisher: {Name}, Address: {Address}";
    }
}
