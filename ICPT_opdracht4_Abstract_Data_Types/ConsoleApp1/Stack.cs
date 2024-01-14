namespace ConsoleApp1;

public static class Stack
{
    public static ref Stack<T> Push<T>(ref Stack<T> stack, T value)
    {
        stack.Data.Add(value);
        return ref stack;
    }

    public static T Pop<T>(ref Stack<T> stack)
    {
        if (stack.Length == 0) 
            throw new InvalidOperationException("Cannot pop on empty stack");
        T value = stack.Data.Last();
        stack.Data.RemoveAt(stack.Length - 1);
        return value;
    }

    public static T Peek<T>(ref Stack<T> stack)
    {
        if (stack.Length == 0) 
            throw new InvalidOperationException("Cannot peek on empty stack");
        return stack.Data.Last();
    }
}

public struct Stack<T>
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