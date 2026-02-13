public class Node<T> : where cl
{
    public T value;
    public Node<T> next;

    public Node(T value, Node<T> next = null)
    {
        this.value = value;
        this.next = next;
    }
}

Node<string> node = new Node<string>("0");
Node<string> dummy = node;

int i = 1;

while (i < 5)
{
    node.next = new Node<string>(i.ToString());
    node = node.next;
    i++;
}

while (dummy != null)
{
    Console.WriteLine(dummy.value);
    dummy = dummy.next;
}
