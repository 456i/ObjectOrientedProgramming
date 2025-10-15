// --- Node ---
public class Node
{
    public string Value;
    public Node Next;

    public Node(string value)
    {
        Value = value;
        Next = null;
    }

    public void AddNext(string value)
    {
        Node newNode = new Node(value);
        newNode.Next = this.Next;
        this.Next = newNode;
    }

    public void RemoveNext()
    {
        if (Next != null)
            Next = Next.Next;
    }

    public string LongestWord()
    {
        string longest = Value;
        Node current = this;
        while (current != null)
        {
            if (current.Value.Length > longest.Length)
                longest = current.Value;
            current = current.Next;
        }
        return longest;
    }

    public void RemoveLast()
    {
        Node current = this;
        while (current.Next != null && current.Next.Next != null)
            current = current.Next;

        if (current.Next != null)
            current.Next = null;
    }

    public void Print()
    {
        Node current = this;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}

// --- NodeList ---
public class NodeList
{
    private Node head;

    public Node Head => head;
    public Production production;
    public Developer developer;

    public void Add(string value)
    {
        if (head == null)
            head = new Node(value);
        else
        {
            Node current = head;
            while (current.Next != null)
                current = current.Next;
            current.AddNext(value);
        }
    }

    public string LongestWord()
    {
        return head != null ? head.LongestWord() : "";
    }

    public void RemoveLast()
    {
        head?.RemoveLast();
    }

    public void Print()
    {
        head?.Print();
    }

    public static NodeList operator +(NodeList list, (string value, int index) data)
    {
        if (list.head == null || data.index <= 0)
        {
            list.Add(data.value);
            return list;
        }

        Node current = list.head;
        for (int i = 0; i < data.index - 1 && current.Next != null; i++)
            current = current.Next;

        current.AddNext(data.value);
        return list;
    }

    public static NodeList operator >>(NodeList list, int index)
    {
        if (index == 0 && list.head != null)
        {
            list.head = list.head.Next;
            return list;
        }

        Node current = list.head;
        for (int i = 0; i < index - 1 && current.Next != null; i++)
            current = current.Next;

        current.RemoveNext();
        return list;
    }

    public static bool operator !=(NodeList a, NodeList b) => true;

    public static bool operator ==(NodeList a, NodeList b) => false;

    // --- Вложенные классы ---
    public class Production
    {
        public int Id;
        public string Company;

        public Production(int id, string company)
        {
            Id = id;
            Company = company;
        }
    }

    public class Developer
    {
        public string Name;
        public int Level;
        public string Department;

        public Developer(string name, int level, string dept)
        {
            Name = name;
            Level = level;
            Department = dept;
        }
    }
}

// --- Статический класс ---
public static class StatisticOperation
{
    public static int SumLengths(NodeList list)
    {
        int sum = 0;
        for (Node current = list.Head; current != null; current = current.Next)
            sum += current.Value.Length;
        return sum;
    }

    public static int MaxMinDifference(NodeList list)
    {
        int? max = null,
            min = null;
        for (Node current = list.Head; current != null; current = current.Next)
        {
            int len = current.Value.Length;
            if (max == null || len > max)
                max = len;
            if (min == null || len < min)
                min = len;
        }
        return (max ?? 0) - (min ?? 0);
    }

    public static int CountElements(NodeList list)
    {
        int count = 0;
        for (Node current = list.Head; current != null; current = current.Next)
            count++;
        return count;
    }
}

// --- WordCount ---
public static class Extensions
{
    public static int WordCount(string str)
    {
        return str.Split(
            new char[] { ' ', '\t', '\n' },
            StringSplitOptions.RemoveEmptyEntries
        ).Length;
    }
}

NodeList list1 = new NodeList();
list1.Add("apple");
list1.Add("banana");
list1.Add("pear");

NodeList list2 = new NodeList();
list2.Add("apple");
list2.Add("orange");

list1 = list1 + ("grape", 1);
list1 = list1 >> 2;
Console.WriteLine(list1 != list2);

list1.production = new NodeList.Production(101, "MyCompany");
list1.developer = new NodeList.Developer("Ivanov I.I.", 1, "IT");

Console.WriteLine("Longest word: " + list1.LongestWord());
list1.RemoveLast();

Console.WriteLine("Sum of lengths: " + StatisticOperation.SumLengths(list1));
Console.WriteLine("Max-Min length: " + StatisticOperation.MaxMinDifference(list1));
Console.WriteLine("Count: " + StatisticOperation.CountElements(list1));

string test = "Hello world";
Console.WriteLine("Word count: " + Extensions.WordCount(test));
