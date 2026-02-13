public static class SerializerFactory
{
    public static ISerializer CreateSerializer(string format) =>
        format.ToLower() switch
        {
            "binary" => new BinarySerializer(),
            "soap" => new SoapSerializer(),
            "xml" => new XmlCustomSerializer(), // Изменили на XmlCustomSerializer
            "json" => new JsonSerializerImpl(),
            _ => throw new ArgumentException($"Unknown format: {format}"),
        };
}
