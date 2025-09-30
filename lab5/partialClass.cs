abstract partial class PrintedEdition
{
    public virtual string PrintInfo()
    {
        return $"Title: {Title}, Pages: {Pages}, Publisher: {Publisher.Name}";
    }

    public abstract bool DoClone();

    public override string ToString()
    {
        return $"Type: {GetType().Name}, Title: {Title}, Pages: {Pages}, Publisher: {Publisher.Name}";
    }
}
