using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

#pragma warning disable SYSLIB0011
public class BinarySerializer : ISerializer
{
    // === РАБОТА С ОДИНОЧНЫМИ ОБЪЕКТАМИ ===

    public string Serialize<T>(T obj)
    {
        using (var stream = new MemoryStream())
        {
            new BinaryFormatter().Serialize(stream, obj);
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    public T Deserialize<T>(string data)
    {
        var bytes = Convert.FromBase64String(data);
        using (var stream = new MemoryStream(bytes))
        {
            return (T)new BinaryFormatter().Deserialize(stream);
        }
    }

    public void SerializeToFile<T>(T obj, string filename)
    {
        using (var stream = File.Create(filename))
        {
            new BinaryFormatter().Serialize(stream, obj);
        }
    }

    public T DeserializeFromFile<T>(string filename)
    {
        using (var stream = File.OpenRead(filename))
        {
            return (T)new BinaryFormatter().Deserialize(stream);
        }
    }

    // === РАБОТА С КОЛЛЕКЦИЯМИ ОБЪЕКТОВ ===

    public string SerializeCollection<T>(IEnumerable<T> collection)
    {
        return Serialize(collection);
    }

    public IEnumerable<T> DeserializeCollection<T>(string data)
    {
        return Deserialize<List<T>>(data);
    }

    public void SerializeCollectionToFile<T>(IEnumerable<T> collection, string filename)
    {
        SerializeToFile(collection, filename);
    }

    public IEnumerable<T> DeserializeCollectionFromFile<T>(string filename)
    {
        return DeserializeFromFile<List<T>>(filename);
    }
}
#pragma warning restore SYSLIB0011

/// SOAP сериализатор - создает XML в SOAP-конверте для веб-сервисов
public class SoapSerializer : ISerializer
{
    // === РАБОТА С ОДИНОЧНЫМИ ОБЪЕКТАМИ ===

    public string Serialize<T>(T obj)
    {
        // Используем полное имя System.Xml.Serialization.XmlSerializer чтобы избежать конфликта
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

        using (var stream = new MemoryStream())
        using (var writer = XmlWriter.Create(stream, settings))
        {
            // СОЗДАЕМ SOAP-КОНВЕРТ - специальная обертка для веб-сервисов
            writer.WriteStartDocument();
            writer.WriteStartElement(
                "soap",
                "Envelope",
                "http://schemas.xmlsoap.org/soap/envelope/"
            );
            writer.WriteStartElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");

            // Сериализуем объект внутри SOAP-конверта
            serializer.Serialize(writer, obj);

            writer.WriteEndElement(); // Body
            writer.WriteEndElement(); // Envelope
            writer.WriteEndDocument();
            writer.Flush();

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    public T Deserialize<T>(string data)
    {
        // Используем полное имя System.Xml.Serialization.XmlSerializer
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(data);

        // ИЗВЛЕКАЕМ ДАННЫЕ ИЗ SOAP-КОНВЕРТА - находим тело сообщения
        var bodyNode = xmlDoc.SelectSingleNode("//soap:Body", GetSoapNamespaceManager(xmlDoc));
        if (bodyNode == null)
            throw new InvalidOperationException("SOAP Body not found");

        // Десериализуем объект из тела SOAP
        using (var reader = new StringReader(bodyNode.InnerXml))
        {
            return (T)serializer.Deserialize(reader);
        }
    }

    public void SerializeToFile<T>(T obj, string filename)
    {
        var soapXml = Serialize(obj);
        File.WriteAllText(filename, soapXml);
    }

    public T DeserializeFromFile<T>(string filename)
    {
        var soapXml = File.ReadAllText(filename);
        return Deserialize<T>(soapXml);
    }

    // === РАБОТА С КОЛЛЕКЦИЯМИ ОБЪЕКТОВ ===

    public string SerializeCollection<T>(IEnumerable<T> collection)
    {
        // SOAP-конверт автоматически создается для коллекции
        return Serialize(collection);
    }

    public IEnumerable<T> DeserializeCollection<T>(string data)
    {
        // Десериализуем коллекцию из SOAP
        return Deserialize<List<T>>(data);
    }

    public void SerializeCollectionToFile<T>(IEnumerable<T> collection, string filename)
    {
        SerializeToFile(collection, filename);
    }

    public IEnumerable<T> DeserializeCollectionFromFile<T>(string filename)
    {
        return DeserializeFromFile<List<T>>(filename);
    }

    /// Создает менеджер пространств имен для работы с SOAP
    private XmlNamespaceManager GetSoapNamespaceManager(XmlDocument doc)
    {
        var nsManager = new XmlNamespaceManager(doc.NameTable);
        nsManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        return nsManager;
    }
}

/// JSON сериализатор - современный формат для веб-API и JavaScript
public class JsonSerializerImpl : ISerializer
{
    // === РАБОТА С ОДИНОЧНЫМИ ОБЪЕКТАМИ ===

    public string Serialize<T>(T obj) => System.Text.Json.JsonSerializer.Serialize(obj);

    public T Deserialize<T>(string data) => System.Text.Json.JsonSerializer.Deserialize<T>(data);

    public void SerializeToFile<T>(T obj, string filename) =>
        File.WriteAllText(filename, Serialize(obj));

    public T DeserializeFromFile<T>(string filename) => Deserialize<T>(File.ReadAllText(filename));

    // === РАБОТА С КОЛЛЕКЦИЯМИ ОБЪЕКТОВ ===

    public string SerializeCollection<T>(IEnumerable<T> collection)
    {
        return Serialize(collection);
    }

    public IEnumerable<T> DeserializeCollection<T>(string data)
    {
        return Deserialize<List<T>>(data);
    }

    public void SerializeCollectionToFile<T>(IEnumerable<T> collection, string filename)
    {
        SerializeToFile(collection, filename);
    }

    public IEnumerable<T> DeserializeCollectionFromFile<T>(string filename)
    {
        return DeserializeFromFile<List<T>>(filename);
    }
}

/// XML сериализатор - структурированный формат для конфигураций и данных
public class XmlCustomSerializer : ISerializer
{
    // === РАБОТА С ОДИНОЧНЫМИ ОБЪЕКТАМИ ===

    public string Serialize<T>(T obj)
    {
        // Используем полное имя чтобы избежать конфликта с нашим классом
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using (var writer = new StringWriter())
        {
            serializer.Serialize(writer, obj);
            return writer.ToString();
        }
    }

    public T Deserialize<T>(string data)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using (var reader = new StringReader(data))
        {
            return (T)serializer.Deserialize(reader);
        }
    }

    public void SerializeToFile<T>(T obj, string filename)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using (var writer = new StreamWriter(filename))
        {
            serializer.Serialize(writer, obj);
        }
    }

    public T DeserializeFromFile<T>(string filename)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        using (var reader = new StreamReader(filename))
        {
            return (T)serializer.Deserialize(reader);
        }
    }

    // === РАБОТА С КОЛЛЕКЦИЯМИ ОБЪЕКТОВ ===

    public string SerializeCollection<T>(IEnumerable<T> collection)
    {
        return Serialize(collection);
    }

    public IEnumerable<T> DeserializeCollection<T>(string data)
    {
        return Deserialize<List<T>>(data);
    }

    public void SerializeCollectionToFile<T>(IEnumerable<T> collection, string filename)
    {
        SerializeToFile(collection, filename);
    }

    public IEnumerable<T> DeserializeCollectionFromFile<T>(string filename)
    {
        return DeserializeFromFile<List<T>>(filename);
    }
}
