namespace ConsoleApp1;

public static class Queue
{
    public static ref Queue<T> Push<T>(ref Queue<T> queue, T value)
    {
        queue.Data.Add(value);
        return ref queue;
    }

    public static T Pop<T>(ref Queue<T> queue)
    {
        if (queue.Length == 0) 
            throw new InvalidOperationException("Cannot pop on empty stack");
        T value = queue.Data.First();
        queue.Data.RemoveAt(0);
        return value;
    }

    public static T Peek<T>(ref Queue<T> queue)
    {
        if (queue.Length == 0) 
            throw new InvalidOperationException("Cannot peek on empty stack");
        return queue.Data.First();
    }
}

public struct Queue<T>
{
    private List<T> _data;

    public List<T> Data
    {
        get => _data;
        set
        {
            _data = value;
        }
    }

    public int Length => _data.Count;
}

struct Test
{
    private Test left;
    private Test test;
}