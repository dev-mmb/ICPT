using ConsoleApp1;

/* stack */
var stack = new ConsoleApp1.Stack<int>
{
    Data = new List<int>()
};
stack = Stack.Push(ref stack, 8);
stack = Stack.Push(ref stack, 1);
stack = Stack.Push(ref stack, 3);
Console.WriteLine($"Peek at top of the stack: {Stack.Peek(ref stack)}");
Console.WriteLine($"Pop from top of the stack: {Stack.Pop(ref stack)}");
Console.WriteLine($"Pop from top of the stack: {Stack.Pop(ref stack)}");
Console.WriteLine();

/* queue */
var queue = new ConsoleApp1.Queue<int>
{
    Data = new List<int>()
};
queue = Queue.Push(ref queue, 8);
queue = Queue.Push(ref queue, 1);
queue = Queue.Push(ref queue, 3);
Console.WriteLine($"Peek at front of the queue: {Queue.Peek(ref queue)}");
Console.WriteLine($"Pop from front of the queue: {Queue.Pop(ref queue)}");
Console.WriteLine($"Pop from front of the queue: {Queue.Pop(ref queue)}");
Console.WriteLine();

/* BINARY TREE */
// creating and adding to the tree
var tree = BinarySearchTree<int>.Construct(5);
tree = BinarySearchTree<int>.Add(ref tree, 3);
tree = BinarySearchTree<int>.Add(ref tree, 4);
tree = BinarySearchTree<int>.Add(ref tree, 900);
tree = BinarySearchTree<int>.Add(ref tree, 10);
tree = BinarySearchTree<int>.Add(ref tree, 9);
BinarySearchTree<int>.Print(ref tree);

// deleting from the tree
Console.WriteLine("\n");
tree = BinarySearchTree<int>.Delete(ref tree, 3);
BinarySearchTree<int>.Print(ref tree);

// finding in the tree
Console.WriteLine("\n");
var index = BinarySearchTree<int>.Find(ref tree, 4);
Console.WriteLine(index.HasValue ? $"Value found at index {index.Value.Data}" : "Value not found!");

int? highestValue = BinarySearchTree<int>.Highest(ref tree);
Console.WriteLine(highestValue.HasValue ? $"Highest value: {highestValue.Value}" : "Value not found!");


