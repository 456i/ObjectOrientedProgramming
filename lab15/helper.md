# Ответы на вопросы по TPL

## 1. Что такое TPL? Как и для чего используется тип Task

**Краткий ответ:**  
TPL (Task Parallel Library) - библиотека .NET для параллельного и асинхронного программирования. Task представляет асинхронную операцию.

**Детальный ответ:**  
TPL - это набор API в пространстве имен `System.Threading.Tasks`, который предоставляет абстракции для параллельного выполнения кода. Основные компоненты:
- **Task** - представляет асинхронную операцию
- **Parallel** - методы для параллельных циклов
- **PLINQ** - параллельные LINQ-запросы

**Task используется для:**
- Выполнения CPU-bound операций без блокировки UI
- Асинхронных I/O операций
- Параллельной обработки данных
- Композиции асинхронных операций

```csharp
// Создание и использование Task
Task task = Task.Run(() => HeavyCalculation());
await task; // Асинхронное ожидание
```

## 2. Почему эффект от распараллеливания наблюдается на большом количестве элементов?

**Краткий ответ:**  
Из-за накладных расходов на создание потоков и синхронизацию.

**Детальный ответ:**  
Параллелизация имеет overhead:
- Создание и управление потоками
- Синхронизация между потоками
- Распределение работы
- Сбор результатов

**Пороговые значения:**
- < 10,000 элементов: обычный цикл быстрее
- 10,000-100,000: зависит от сложности операции  
- > 100,000: параллелизация дает выигрыш

```csharp
// Маленький объем - параллелизация проигрывает
for (int i = 0; i < 1000; i++) { } // Быстрее
Parallel.For(0, 1000, i => { });    // Медленнее (overhead)

// Большой объем - параллелизация выигрывает
for (int i = 0; i < 1000000; i++) { } // Медленнее  
Parallel.For(0, 1000000, i => { });    // Быстрее
```

## 3. Основные достоинства работы с задачами по сравнению с потоками

**Краткий ответ:**  
Эффективное использование ресурсов, отмена, продолжения, обработка исключений.

**Детальный ответ:**

**Потоки (Thread):**
- Дорогое создание (~1MB памяти на поток)
- Сложное управление жизненным циклом
- Нет встроенной отмены
- Сложная композиция операций

**Задачи (Task):**
- Используют пул потоков (экономят ресурсы)
- Автоматическое управление жизненным циклом  
- Встроенная отмена через CancellationToken
- Легкая композиция (ContinueWith, WhenAll, WhenAny)
- Удобная обработка исключений

```csharp
// Потоки - сложно
var thread = new Thread(() => Work());
thread.Start();
thread.Join(); // Блокировка

// Задачи - проще
var task = Task.Run(() => Work());
await task; // Неблокирующее ожидание
```

## 4. Три способа создания и/или запуска Task

**Краткий ответ:**  
`Task.Run()`, `Task.Factory.StartNew()`, `new Task().Start()`

**Детальный ответ:**

```csharp
// 1. Task.Run (рекомендуется)
Task task1 = Task.Run(() => Console.WriteLine("Task.Run"));

// 2. Task.Factory.StartNew (больше настроек)
Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Factory"), 
    CancellationToken.None, 
    TaskCreationOptions.DenyChildAttach, 
    TaskScheduler.Default);

// 3. new Task + Start (явное создание)
Task task3 = new Task(() => Console.WriteLine("new Task"));
task3.Start();
```

## 5. Методы Wait(), WaitAll() и WaitAny()

**Краткий ответ:**  
Методы для ожидания завершения задач.

**Детальный ответ:**

- **Wait()** - ожидание одной задачи
- **WaitAll()** - ожидание всех задач в массиве  
- **WaitAny()** - ожидание любой задачи из массива

```csharp
var tasks = new[]
{
    Task.Run(() => Thread.Sleep(1000)),
    Task.Run(() => Thread.Sleep(2000)),
    Task.Run(() => Thread.Sleep(1500))
};

tasks[0].Wait();                    // ⏳ Ждет первую задачу
Task.WaitAll(tasks);                // ⏳ Ждет ВСЕ задачи  
int index = Task.WaitAny(tasks);    // ⏳ Ждет ЛЮБУЮ задачу
```

## 6. Пример синхронного запуска Task

**Краткий ответ:**  
`task.RunSynchronously()`

**Детальный ответ:**
```csharp
Task task = new Task(() => 
{
    Console.WriteLine("Выполняется в текущем потоке");
});
task.RunSynchronously(); // ⚡ Выполняется в текущем потоке, а не в пуле
```

## 7. Создание задачи с возвратом результата

**Краткий ответ:**  
Использовать `Task<T>` вместо `Task`.

**Детальный ответ:**
```csharp
// Задача возвращает int
Task<int> task = Task.Run(() => 
{
    Thread.Sleep(1000);
    return 42;
});

int result = await task; // или task.Result
Console.WriteLine($"Результат: {result}");
```

## 8. Обработка исключений в Task

**Краткий ответ:**  
Оборачивать в try-catch при await/Wait/Result.

**Детальный ответ:**
```csharp
Task task = Task.Run(() => throw new InvalidOperationException("Ошибка!"));

try
{
    await task; // или task.Wait() или var r = task.Result
}
catch (AggregateException ex) // При task.Wait()/Result
{
    Console.WriteLine($"AggregateException: {ex.InnerException.Message}");
}
catch (Exception ex) // При await
{
    Console.WriteLine($"Exception: {ex.Message}");
}
```

## 9. CancellationToken и отмена задач

**Краткий ответ:**  
Механизм для безопасной отмены операций.

**Детальный ответ:**
```csharp
using var cts = new CancellationTokenSource();

Task task = Task.Run(() =>
{
    for (int i = 0; i < 100; i++)
    {
        cts.Token.ThrowIfCancellationRequested(); // ✅ Проверка отмены
        Thread.Sleep(100);
    }
}, cts.Token);

cts.CancelAfter(500); // Отмена через 500ms

try { await task; }
catch (OperationCanceledException) 
{ 
    Console.WriteLine("Задача отменена!"); 
}
```

## 10. Организация задачи продолжения

**Краткий ответ:**  
`ContinueWith()` или `await`.

**Детальный ответ:**
```csharp
// Способ 1: ContinueWith
Task<int> firstTask = Task.Run(() => 10);
Task<string> continuation = firstTask.ContinueWith(t => 
    $"Результат: {t.Result * 2}");

// Способ 2: await (рекомендуется)
int result = await Task.Run(() => 10);
string message = $"Результат: {result * 2}";
```

## 11. Объект ожидания в задачах продолжения

**Краткий ответ:**  
`GetAwaiter()` для ручного управления await.

**Детальный ответ:**
```csharp
Task<int> task = Task.Run(() => 42);
var awaiter = task.GetAwaiter();

awaiter.OnCompleted(() => 
{
    // Выполнится когда задача завершится
    int result = awaiter.GetResult();
    Console.WriteLine($"Результат: {result}");
});
```

## 12. Назначение класса System.Threading.Tasks.Parallel

**Краткий ответ:**  
Для параллельного выполнения циклов и операций.

**Детальный ответ:**
`Parallel` предоставляет методы для Data Parallelism:
- `Parallel.For` - параллельный for
- `Parallel.ForEach` - параллельный foreach  
- `Parallel.Invoke` - параллельное выполнение действий

```csharp
// Автоматически распараллеливает работу
Parallel.For(0, 1000000, i => 
{
    // Выполняется в нескольких потоках
});
```

## 13. Пример Parallel.For

**Краткий ответ:**  
`Parallel.For(0, 100, i => { /* работа */ });`

**Детальный ответ:**
```csharp
int[] numbers = new int[1000000];

Parallel.For(0, numbers.Length, i =>
{
    numbers[i] = i * i; // Вычисление квадрата
});

// С настройками
var options = new ParallelOptions { MaxDegreeOfParallelism = 4 };
Parallel.For(0, 1000000, options, i => 
{
    // Максимум 4 потока
});
```

## 14. Пример Parallel.ForEach

**Краткий ответ:**  
`Parallel.ForEach(collection, item => { /* обработка */ });`

**Детальный ответ:**
```csharp
List<string> files = Directory.GetFiles(".", "*.txt").ToList();

Parallel.ForEach(files, file =>
{
    string content = File.ReadAllText(file);
    Console.WriteLine($"Обработан: {file}");
});

// С возвратом результата
List<string> results = new List<string>();
Parallel.ForEach(files, () => new List<string>(), (file, loopState, localList) =>
{
    localList.Add(File.ReadAllText(file));
    return localList;
}, localList => lock(results) results.AddRange(localList));
```

## 15. Пример Parallel.Invoke()

**Краткий ответ:**  
`Parallel.Invoke(action1, action2, action3);`

**Детальный ответ:**
```csharp
Parallel.Invoke(
    () => {
        Console.WriteLine($"Action1 в потоке {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(1000);
    },
    () => {
        Console.WriteLine($"Action2 в потоке {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(1500);
    },
    () => {
        Console.WriteLine($"Action3 в потоке {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(800);
    }
);
// Блокирует выполнение пока ВСЕ действия не завершатся
```

## 16. Отмена параллельных операций с CancellationToken

**Краткий ответ:**  
Через `ParallelOptions` с `CancellationToken`.

**Детальный ответ:**
```csharp
using var cts = new CancellationTokenSource();
var options = new ParallelOptions 
{ 
    CancellationToken = cts.Token 
};

try
{
    Parallel.For(0, 1000000, options, i =>
    {
        options.CancellationToken.ThrowIfCancellationRequested();
        // Работа
        Thread.Sleep(1);
    });
}
catch (OperationCanceledException)
{
    Console.WriteLine("Параллельная операция отменена");
}

cts.CancelAfter(500); // Отмена через 500ms
```

## 17. BlockingCollection<T> - назначение и особенности

**Краткий ответ:**  
Потокобезопасная коллекция для producer-consumer pattern.

**Детальный ответ:**

**Особенности:**
- Автоматическая блокировка при чтении из пустой коллекции
- Ограничение емкости
- Уведомление о завершении добавления

```csharp
var collection = new BlockingCollection<string>(boundedCapacity: 5);

// Producer
Task.Run(() =>
{
    for (int i = 0; i < 10; i++)
    {
        collection.Add($"Item {i}");
    }
    collection.CompleteAdding(); // ✅ Больше нельзя добавлять
});

// Consumer  
Task.Run(() =>
{
    foreach (var item in collection.GetConsumingEnumerable())
    {
        Console.WriteLine($"Обработано: {item}");
    }
    // Цикл завершится когда CompleteAdding И коллекция пуста
});
```

## 18. Организация асинхронного выполнения с async и await

**Краткий ответ:**  
Пометить метод `async` и использовать `await`.

**Детальный ответ:**
```csharp
// 1. Асинхронный метод
static async Task<int> GetDataAsync()
{
    await Task.Delay(1000); // ⏸️ Неблокирующая задержка
    return 42;
}

// 2. Использование
static async Task Main()
{
    Console.WriteLine("Начало");
    int result = await GetDataAsync(); // ⏸️ Ожидание без блокировки
    Console.WriteLine($"Результат: {result}");
}

// 3. Параллельное выполнение
static async Task ProcessMultiple()
{
    Task<int> task1 = GetDataAsync();
    Task<string> task2 = ProcessTextAsync();
    
    await Task.WhenAll(task1, task2); // ⏳ Ожидание всех задач
    
    Console.WriteLine($"{task1.Result}, {task2.Result}");
}
```