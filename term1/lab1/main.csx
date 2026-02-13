void BuildInTypesDemo()
{
    int a = 10;
    double b = 5.5;
    float c = 3.3f;
    char d = 'x';
    bool e = true;
    string f = "Hello";
    byte g = 255;
    short h = -32000;
    long i = 1000000;
    decimal j = 100.5m;

    Console.WriteLine(a);
    a = int.Parse(Console.ReadLine());
    Console.WriteLine(b);
    b = double.Parse(Console.ReadLine());
    Console.WriteLine(c);
    c = float.Parse(Console.ReadLine());
    Console.WriteLine(d);
    d = char.Parse(Console.ReadLine());
    Console.WriteLine(e);
    e = Convert.ToBoolean(int.Parse(Console.ReadLine()));
    Console.WriteLine(f);
    f = Console.ReadLine();
    Console.WriteLine(g);
    g = byte.Parse(Console.ReadLine());
    Console.WriteLine(h);
    h = short.Parse(Console.ReadLine());
    Console.WriteLine(i);
    i = long.Parse(Console.ReadLine());
    Console.WriteLine(j);
    j = decimal.Parse(Console.ReadLine());
}

void TypeConversions()
{
    int intX = 10;
    double doubleY = intX;
    float floatF = intX;
    long longL = intX;
    decimal dec = intX;
    double doubleZ = 5.5;
    int xi = (int)doubleZ;
    byte byteB = (byte)intX;
    short shortS = (short)intX;
    float fx = (float)doubleZ;

    Console.WriteLine(Convert.ToInt32("123"));
    Console.WriteLine(Convert.ToDouble("45.6"));
    Console.WriteLine(Convert.ToString(789));
    Console.WriteLine(Convert.ToBoolean(1));
    Console.WriteLine(Convert.ToByte(200));

    // Boxing и unboxing
    int boxA = 42;
    object obj = boxA;
    int unboxB = (int)obj;
    Console.WriteLine(obj);
    Console.WriteLine(unboxB);

    // Implicitly typed variable
    var implicitX = 5;
    Console.WriteLine(implicitX);

    // Nullable types
    int? nullableA = null;
    if (nullableA.HasValue)
        Console.WriteLine(nullableA.Value);
    else
        Console.WriteLine("null");
    nullableA = 10;
    Console.WriteLine(nullableA);

    // Var example
    var varX = 5;
    Console.WriteLine(varX);
    // varX = "string"; // Ошибка: var тип фиксируется при инициализации
}

void StringsDemo()
{
    string s1 = "Hello";
    string s2 = "World";
    string s3 = "Hello";

    Console.WriteLine(s1 == s2);
    Console.WriteLine(s1 == s3);

    string a = "Hello";
    string b = "C#";
    string c = "World";

    string concat = a + " " + b + " " + c;
    string copy = string.Copy(a);
    string substring = concat.Substring(0, 5);
    string[] words = concat.Split(' ');
    string insert = concat.Insert(6, "Insert ");
    string remove = concat.Remove(6, 2);
    string interpol = $"Combined: {a} {b} {c}";

    Console.WriteLine(concat);
    Console.WriteLine(copy);
    Console.WriteLine(substring);
    Console.WriteLine(string.Join(", ", words));
    Console.WriteLine(insert);
    Console.WriteLine(remove);
    Console.WriteLine(interpol);

    string empty = "";
    string nullStr = null;
    Console.WriteLine(string.IsNullOrEmpty(empty));
    Console.WriteLine(string.IsNullOrEmpty(nullStr));

    StringBuilder sb = new StringBuilder("StringBuilderDemo");
    sb.Remove(0, 6);
    sb.Insert(0, "Start-");
    sb.Append("-End");
    Console.WriteLine(sb);
}

void ArraysDemo()
{
    int[,] matrix =
    {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 },
    };
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        for (int j = 0; j < matrix.GetLength(1); j++)
            Console.Write(matrix[i, j] + " ");
        Console.WriteLine();
    }

    string[] strArr = { "one", "two", "three" };
    Console.WriteLine($"Length: {strArr.Length}");
    Console.WriteLine(string.Join(", ", strArr));
    Console.Write("Change index: ");
    int idx = int.Parse(Console.ReadLine());
    Console.Write("New value: ");
    strArr[idx] = Console.ReadLine();
    Console.WriteLine(string.Join(", ", strArr));

    double[][] jagged = new double[3][];
    jagged[0] = new double[2];
    jagged[1] = new double[3];
    jagged[2] = new double[4];

    for (int i = 0; i < 3; i++)
    for (int j = 0; j < jagged[i].Length; j++)
    {
        Console.Write($"Element [{i},{j}]: ");
        jagged[i][j] = double.Parse(Console.ReadLine());
    }

    var implicitArr = new[] { 1, 2, 3 };
    var implicitStr = "Implicit string";
    Console.WriteLine(string.Join(", ", implicitArr));
    Console.WriteLine(implicitStr);
}

void TuplesDemo()
{
    var tuple = (10, "Hello", 'A', "World", 100UL);
    Console.WriteLine(tuple);
    Console.WriteLine($"{tuple.Item1}, {tuple.Item3}, {tuple.Item4}");

    var (i, s1, c, s2, ul) = tuple;
    Console.WriteLine(i);
    Console.WriteLine(s1);
    Console.WriteLine(c);

    var (x, _, _, _, y) = tuple;
    Console.WriteLine(x + ", " + y);

    var tuple2 = (10, "Hello", 'A', "World", 100UL);
    Console.WriteLine(tuple == tuple2);
}

void LocalFunctionDemo()
{
    int[] arr = { 1, 2, 3, 4, 5 };
    string str = "Hello";

    (int max, int min, int sum, char first) LocalFunction(int[] array, string s)
    {
        int max = array[0],
            min = array[0],
            sum = 0;
        foreach (var val in array)
        {
            if (val > max)
                max = val;
            if (val < min)
                min = val;
            sum += val;
        }
        return (max, min, sum, s[0]);
    }

    var result = LocalFunction(arr, str);
    Console.WriteLine(result);
}

void CheckedUncheckedDemo()
{
    void CheckedFunc()
    {
        checked
        {
            int max = int.MaxValue;
            try
            {
                max += 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Checked overflow: " + ex.Message);
            }
        }
    }

    void UncheckedFunc()
    {
        unchecked
        {
            int max = int.MaxValue;
            max += 1;
            Console.WriteLine("Unchecked result: " + max);
        }
    }

    CheckedFunc();
    UncheckedFunc();
}

while (true)
{
    Console.WriteLine("\nВыберите задачу:");
    Console.WriteLine("1 - Примитивные типы");
    Console.WriteLine("2 - Строки");
    Console.WriteLine("3 - Массивы");
    Console.WriteLine("4 - Кортежи");
    Console.WriteLine("5 - Локальная функция");
    Console.WriteLine("6 - Checked/Unchecked");
    Console.WriteLine("0 - Выход");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            BuildInTypesDemo();
            break;
        case "2":
            StringsDemo();
            break;
        case "3":
            ArraysDemo();
            break;
        case "4":
            TuplesDemo();
            break;
        case "5":
            LocalFunctionDemo();
            break;
        case "6":
            CheckedUncheckedDemo();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Неверный выбор");
            break;
    }
}
