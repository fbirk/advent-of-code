
namespace AoC2024._07
{
    public class Day07
    {
        public static long Part1()
        {
            long result = 0;
            var lines = File.ReadAllLines(@"07\data07.txt");
            var equations = new List<(long result, long[] numbers)>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var split = line.Split(':');
                var res = long.Parse(split[0]);
                long[] vals = split[1].Trim().Split(' ').ToList().Select(long.Parse).ToArray();

                equations.Add((res, vals));
            }

            foreach (var equation in equations)
            {
                result += EvaluateEquation(equation);
            }

            return result;
        }

        private static long EvaluateEquation((long result, long[] numbers) equation)
        {
            var (result, numbers) = equation;
            var root = new NTree<long>(numbers[0], numbers[0]);

            for (int i = 1; i < numbers.Length; i++)
            {
                var leafs = Hierarchy.GetLeafs(root, node => node.Children);
                foreach (var leaf in leafs)
                {
                    leaf.AddChild(leaf.Data + numbers[i], numbers[i]);
                    leaf.AddChild(leaf.Data * numbers[i], numbers[i]);
                }
            }

            var leafNodes = Hierarchy.GetLeafs(root, node => node.Children).ToList();

            return leafNodes.Any(x => x.Data == result) ? result : 0;
        }
    }

    public class NTree<T>
    {
        public T Val { get; }
        public T Data { get; }
        public LinkedList<NTree<T>> Children { get; }

        public NTree(T data, T val)
        {
            Data = data;
            Val = val;
            Children = new LinkedList<NTree<T>>();
        }

        public void AddChild(T data, T val)
        {
            Children.AddFirst(new NTree<T>(data, val));
        }
    }

    public static class Hierarchy
    {
        /// <summary>
        /// Gets the collection of leafs (items that have no children) from a hierarchical collection
        /// </summary>
        /// <typeparam name="T">The collection type</typeparam>
        /// <param name="source">The sourceitem of the collection</param>
        /// <param name="getChildren">A method that returns the children of an item</param>
        /// <returns>The collection of leafs</returns>
        public static IEnumerable<T> GetLeafs<T>(T source, Func<T, IEnumerable<T>> getChildren)
        {
            if (!getChildren(source).Any())
            {
                yield return source;
            }
            else
            {
                foreach (var child in getChildren(source))
                {
                    foreach (var subchild in GetLeafs(child, getChildren))
                    {
                        yield return subchild;
                    }
                }
            }
        }
    }
}
