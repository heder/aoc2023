using MathNet.Numerics;

class Program
{
    class Node
    {
        public string Name;
        public Node Left;
        public Node Right;
    }

    static Dictionary<string, Node> nodes = new Dictionary<string, Node>();

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();
        string move = lines[0];

        for (int i = 2; i < lines.Length; i++)
        {
            var line = lines[i];

            var split1 = line.Split('=');
            var nodename = split1[0].Trim();

            var split2 = split1[1].Split(',');

            var leftname = split2[0].Trim(' ', '(');
            var rightname = split2[1].Trim(' ', ')');

            Node node;
            Node leftnode;
            Node rightnode;

            if (nodes.TryGetValue(nodename, out node) == false)
            {
                node = new Node() { Name = nodename };
                nodes.Add(nodename, node);
            }

            if (nodes.TryGetValue(leftname, out leftnode) == false)
            {
                leftnode = new Node() { Name = leftname };
                nodes.Add(leftname, leftnode);
            }

            if (nodes.TryGetValue(rightname, out rightnode) == false)
            {
                rightnode = new Node() { Name = rightname };
                nodes.Add(rightname, rightnode);
            }

            node.Left = leftnode;
            node.Right = rightnode;
        }

        List<Node> currentNodes = nodes.Values.Where(f => f.Name.EndsWith('A')).ToList();
        long n = 0;

        // Upphittade cyklerna i varje path från findings i loopen nedan.
        Console.WriteLine(Euclid.LeastCommonMultiple(14999, 16697, 17263, 20093, 20659, 22357));
        Console.ReadKey();

        while (true)
        {
            for (int i = 0; i < move.Length; i++)
            {
                for (int j = 0; j < currentNodes.Count; j++)
                {
                    if (move[i] == 'L')
                    {
                        currentNodes[j] = currentNodes[j].Left;
                    }
                    else
                    {
                        currentNodes[j] = currentNodes[j].Right;
                    }

                    if (currentNodes[j].Name.EndsWith('Z')) // Hitta cykler i graferna
                    {
                        Console.WriteLine(j + ": is at " + currentNodes[j].Name + "after " + (n + 1) + " iterations");
                        Console.ReadKey();

                        // Varje path roterar till samma nod efter 14999 16697 17263 20093 20659 22357 iterationer
                        // Beräkna LCM av alla cykler för att hitta när de alignar samtidigt.
                    }
                }

                n++;

                if (n % 10000000 == 0) Console.WriteLine(n);

                if (currentNodes.All(f => f.Name.EndsWith('Z')))
                {
                    Console.WriteLine(n);
                    Console.ReadKey();
                }
            }
        }

    }

}