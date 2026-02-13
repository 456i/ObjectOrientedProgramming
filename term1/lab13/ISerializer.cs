public interface ISerializer
{
    // === РАБОТА С ОДИНОЧНЫМИ ОБЪЕКТАМИ ===

    string Serialize<T>(T obj);

    T Deserialize<T>(string data);

    void SerializeToFile<T>(T obj, string filename);

    T DeserializeFromFile<T>(string filename);

    // === РАБОТА С КОЛЛЕКЦИЯМИ ОБЪЕКТОВ ===

    string SerializeCollection<T>(IEnumerable<T> collection);

    IEnumerable<T> DeserializeCollection<T>(string data);

    void SerializeCollectionToFile<T>(IEnumerable<T> collection, string filename);

    IEnumerable<T> DeserializeCollectionFromFile<T>(string filename);
}
