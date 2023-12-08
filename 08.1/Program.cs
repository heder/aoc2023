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

        Node currentNode = nodes["AAA"];
        int n = 0;
        while (true)
        {
            for (int i = 0; i < move.Length; i++)
            {
                if (move[i] == 'L')
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }

                n++;

                if (currentNode.Name == "ZZZ")
                {
                    Console.WriteLine(n);
                    Console.ReadKey();
                }
            }
        }

    }

}