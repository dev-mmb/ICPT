namespace ConsoleApp1;

public static class BinarySearchTree<T> where T : IComparable<T>
{
    public static BinarySearchTreeData<T> Construct(T root)
    {
        var tree = new BinarySearchTreeData<T>
        {
            Data = new List<(bool, T)?> { (true, root) }
        };
        return tree;
    }

    public static ref BinarySearchTreeData<T> Add(ref BinarySearchTreeData<T> tree, T value)
    {
        AddToNode(ref tree, value, 0);
        return ref tree;
    }

    public static BinarySearchTreeNode? Find(ref BinarySearchTreeData<T> tree, T value)
    {
        var index = FindOfChildren(ref tree, value, 0);
        if (!index.HasValue) return null;
        var v = new BinarySearchTreeNode
        {
            Data = index.Value 
        };
        return v;
    }

    public static ref BinarySearchTreeData<T> Delete(ref BinarySearchTreeData<T> tree, T value)
    {
        var values = tree.Items.Where(item => item.CompareTo(value) != 0).ToArray();
        tree = BinarySearchTree<T>.Construct(values[0]);
        for (int i = 1; i < values.Length; i++)
        {
            BinarySearchTree<T>.Add(ref tree, values[i]);
        }

        return ref tree;
    }

    public static T? Highest(ref BinarySearchTreeData<T> tree)
    {
        var right = BinarySearchTree<T>.HighestValueIndex(ref tree, 0);
        return tree.Data[right!.Value]!.Value.Item2;
    }

    private static int? HighestValueIndex(ref BinarySearchTreeData<T> tree, int root)
    {
        var right = GetRightIndex(root);
        if (right >= tree.Data.Count) return root;
        var item = tree.Data[right];
        if (item is { Item1: true })
        {
            var child = HighestValueIndex(ref tree, right);
            // if the child has a value returned, return that
            if (child.HasValue) return child;
            // return the parent 
            return right;
        }
        return null;
    }
    private static int? FindOfChildren(ref BinarySearchTreeData<T> tree, T value, int index)
    {
        var root = tree.Data[index];
        if (root is null or { Item1: false }) return null;
        int comp = value.CompareTo(root.Value.Item2);
        if (comp == 0) return index;
        if (comp > 0)
        {
            return FindOfChildren(ref tree, value, GetRightIndex(index));
        }
        else return FindOfChildren(ref tree, value, GetLeftIndex(index));
    }

    private static void AddToNode(ref BinarySearchTreeData<T> tree, T value, int index)
    {
        // grow the data array if needed
        if (tree.Data.Count <= index) 
            AddRange(ref tree, index - tree.Data.Count + 1);
        (bool, T)? node = tree.Data[index];
        if (!node.HasValue|| node.Value.Item1 is false)
        {
            tree.Data[index] = (true, value);
            return;
        }

        var compared = value.CompareTo(node.Value.Item2);
        // is greater
        if (compared > 0)
        {
            AddToNode(ref tree, value, GetRightIndex(index)); 
            return;
        }
        // is less or equal
        AddToNode(ref tree, value, GetLeftIndex(index)); 
        return;
    }

    private static void AddRange(ref BinarySearchTreeData<T> tree, int len)
    {
        int start = tree.Data.Count;
        int end = start + len;
        tree.Data.AddRange(new (bool, T)?[len]);
        for (int i = start; i < end; i++)
        {
            tree.Data[i] = (false, default(T))!;
        }
    }

    private static int GetLeftIndex(int index)
    {
        return (index * 2) + 1;
    }
 
    private static int GetRightIndex(int index)
    {
        return (index * 2) + 2;
    }
    
    public static void Print(ref BinarySearchTreeData<T> tree)
    {
        for (int i = 0; i < tree.Data.Count; i++) {
            if (tree.Data[i].HasValue && tree.Data[i]!.Value.Item1)
            {
                var value = tree.Data[i]!.Value;
                Console.Write(value.Item2 + ", ");
            }               
            else
                Console.Write("-, ");
        }
    }
}

public struct BinarySearchTreeNode
{
    private int _data;

    public int Data
    {
        get => _data;
        set => _data = value;
    }
}
public struct BinarySearchTreeData<T> where T : IComparable<T>
{
    private List<(bool, T)?> _data;

    public List<(bool, T)?> Data
    {
        get => _data;
        set => _data = value;
    }

    public T Root => _data.First()!.Value.Item2;

    public T[] Items => _data
        .Where(item => item is { Item1: true })
        .Select(item => item.Value.Item2)
        .ToArray();
} 