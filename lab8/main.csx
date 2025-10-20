public class Player
{
    public string Name;
    public int Health;

    public Player(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void ShowStatus()
    {
        Console.WriteLine($"{Name}: {Health} HP");
    }
}

public class Game
{
    public delegate void AttackHandler(int damage);
    public delegate void HealHandler(int amount);

    public event AttackHandler Attack;
    public event HealHandler Heal;

    public void OnAttack(int damage)
    {
        Attack?.Invoke(damage);
    }

    public void OnHeal(int amount)
    {
        Heal?.Invoke(amount);
    }
}

static class StringProcessor
{
    public static string RemovePunctuation(string s)
    {
        char[] punct = new[] { '.', ',', '!', '?', ';', ':' };
        foreach (var c in punct)
            s = s.Replace(c.ToString(), "");
        return s;
    }

    public static string ToUpperCase(string s) => s.ToUpper();

    public static string AddStars(string s) => $"***{s}***";

    public static string RemoveExtraSpaces(string s) =>
        string.Join(" ", s.Split(" ", StringSplitOptions.RemoveEmptyEntries));

    public static string ReplaceIWith1(string s) => s.Replace('i', '1');
}

// ---- Task 1 ------

Game game = new Game();

Player p1 = new Player("Игрок1", 100);
Player p2 = new Player("Игрок2", 100);
Player p3 = new Player("Игрок3", 100);

game.Attack += dmg =>
{
    p1.Health -= dmg;
    Console.WriteLine($"{p1.Name} атакован на {dmg} HP");
};
game.Attack += dmg =>
{
    p2.Health -= dmg / 2;
    Console.WriteLine($"{p2.Name} атакован на {dmg / 2} HP");
};
game.Heal += amount =>
{
    p1.Health += amount;
    Console.WriteLine($"{p1.Name} лечится на {amount} HP");
};
game.Heal += amount =>
{
    p3.Health += amount / 2;
    Console.WriteLine($"{p3.Name} лечится на {amount / 2} HP");
};

Console.WriteLine("\n--- События: Атака и Лечение ---");
game.OnAttack(20);
game.OnHeal(10);

Console.WriteLine("\n--- Статус игроков после событий ---");
p1.ShowStatus();
p2.ShowStatus();
p3.ShowStatus();

// Повторный запуск событий
Console.WriteLine("\n--- Повторная атака ---");
game.OnAttack(30);
p1.ShowStatus();
p2.ShowStatus();
p3.ShowStatus();

// ---- Task 2 ------

string text = "  hi,   world!   Welcome to C#   lab...  ";

Func<string, string> process;
process += StringProcessor.RemovePunctuation;
process += StringProcessor.ToUpperCase;
process += StringProcessor.AddStars;
process += StringProcessor.RemoveExtraSpaces;
process += StringProcessor.ReplaceIWith1;

string result = text;
foreach (Func<string, string> f in process.GetInvocationList())
{
    result = f(result);
    Console.WriteLine(result);
}

Action<string> print = s => Console.WriteLine(s);

Console.WriteLine("\n--- Исходный текст ---");
print(text);

Console.WriteLine("\n--- Обработанный текст ---");
print(result);

Predicate<string> containsWorld = s => s.Contains("WORLD");

Console.WriteLine("\n--- Проверка Predicate ---");
Console.WriteLine($"Содержит 'WORLD': {containsWorld(result)}");
