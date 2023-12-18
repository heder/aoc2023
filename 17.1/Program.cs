class Program
{
    public enum Direction
    {
        North = 1,
        West = 2,
        East = 3,
        South = 4
    }

    class Node
    {
        public char Character { get; set; }
        public int Loss { get; set; }
        public int Distance { get; set; } = int.MaxValue;
        public int X { get; set; }
        public int Y { get; set; }

        public bool Visited { get; set; } = false;

        public Direction VisitedGoingDirection { get; set; }
        public int StepsInCurrentDirection { get; set; }

    }


    static int yMax;
    static int xMax;


    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        var world = new Node[xMax, yMax];
        List<Node> unvisited = new List<Node>();

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                var t = new Node();
                t.Character = lines[y][x];
                t.Loss = Convert.ToInt32(t.Character.ToString());

                t.X = x;
                t.Y = y;

                world[x, y] = t;

                unvisited.Add(t);
            }
        }

        // Dump();

        // List<Beam> Beams = [new Beam() { Direction = Direction.East, X = 0, Y = 0, Steps = 0 }, new Beam() { Direction = Direction.South, X = 0, Y = 0, Steps = 0 }];


        // Set starting node to zero
        world[0, 0].Distance = 0;

        Node destinationNode = world[xMax - 1, yMax - 1]; ;

        Node currentNode = world[0, 0];

        while (true)
        {
            Dump();

            List<Node> unvisitedNeighBors = GetValidNeighbors(currentNode);

            foreach (var item in unvisitedNeighBors)
            {
                if (item.Loss + currentNode.Distance < item.Distance)
                {
                    item.Distance = item.Loss + currentNode.Distance;
                }
            }

            currentNode.Visited = true;

            // Remove from nodestocheck
            unvisited.Remove(currentNode);

            if (destinationNode.Visited == true)
            {
                Console.ReadKey();
            }

            currentNode = unvisited.First();
        }












        List<Node> GetValidNeighbors(Node n)
        {
            List<Node> neighbours = new List<Node>();

            switch (n.VisitedGoingDirection)
            {
                case Direction.North:

                    if (n.Y - 1 >= 0 && n.StepsInCurrentDirection < 3 && world[n.Y - 1, n.X].Visited == false)
                    {
                        neighbours.Add(world[n.Y - 1, n.X]);
                        world[n.Y - 1, n.X].VisitedGoingDirection = Direction.North;
                        world[n.Y - 1, n.X].StepsInCurrentDirection = n.StepsInCurrentDirection + 1;
                    }

                    if (n.X - 1 >= 0 && n.X - 1 <= 0 && world[n.X - 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X - 1, n.Y]);
                        world[n.X - 1, n.Y].VisitedGoingDirection = Direction.West;
                        world[n.X - 1, n.Y].StepsInCurrentDirection = 1;
                    }

                    if (n.X + 1 < xMax && world[n.X + 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X + 1, n.Y]);
                        world[n.X + 1, n.Y].VisitedGoingDirection = Direction.East;
                        world[n.X + 1, n.Y].StepsInCurrentDirection = 1;
                    }


                    break;

                case Direction.West:

                    if (n.X - 1 >= 0 && n.StepsInCurrentDirection < 3 && world[n.X - 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X - 1, n.Y]);
                        world[n.X - 1, n.Y].VisitedGoingDirection = Direction.West;
                        world[n.X - 1, n.Y].StepsInCurrentDirection = n.StepsInCurrentDirection + 1;
                    }

                    if (n.Y - 1 >= 0 && world[n.X, n.Y - 1].Visited == false)
                    {
                        neighbours.Add(world[n.X, n.Y - 1]);
                        world[n.X, n.Y - 1].VisitedGoingDirection = Direction.North;
                        world[n.X, n.Y - 1].StepsInCurrentDirection = 1;
                    }

                    if (n.Y + 1 < yMax && world[n.X, n.Y + 1].Visited == false)
                    {
                        neighbours.Add(world[n.X, n.Y + 1]);
                        world[n.X, n.Y + 1].VisitedGoingDirection = Direction.South;
                        world[n.X, n.Y + 1].StepsInCurrentDirection = 1;
                    }

                    break;

                case Direction.East:

                    if (n.X + 1 < xMax && n.StepsInCurrentDirection < 3 && world[n.X + 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X + 1, n.Y]);
                        world[n.X + 1, n.Y].VisitedGoingDirection = Direction.East;
                        world[n.X + 1, n.Y].StepsInCurrentDirection = n.StepsInCurrentDirection + 1;
                    }

                    if (n.Y - 1 >= 0 && world[n.X + 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X + 1, n.Y]);
                        world[n.X + 1, n.Y].VisitedGoingDirection = Direction.North;
                        world[n.X + 1, n.Y].StepsInCurrentDirection = 1;
                    }

                    if (n.Y + 1 < yMax && world[n.X + 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X + 1, n.Y]);
                        world[n.X + 1, n.Y].VisitedGoingDirection = Direction.South;
                        world[n.X + 1, n.Y].StepsInCurrentDirection = 1;
                    }

                    break;


                case Direction.South:

                    if (n.Y + 1 < yMax && n.StepsInCurrentDirection < 3 && world[n.X, n.Y + 1].Visited == false)
                    {
                        neighbours.Add(world[n.X, n.Y + 1]);
                        world[n.X, n.Y + 1].VisitedGoingDirection = Direction.South;
                        world[n.X, n.Y + 1].StepsInCurrentDirection = n.StepsInCurrentDirection + 1;
                    }

                    if (n.X - 1 >= 0 && world[n.X - 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X - 1, n.Y]);
                        world[n.X - 1, n.Y].VisitedGoingDirection = Direction.West;
                        world[n.X - 1, n.Y].StepsInCurrentDirection = 1;
                    }

                    if (n.X + 1 < xMax && world[n.X + 1, n.Y].Visited == false)
                    {
                        neighbours.Add(world[n.X + 1, n.Y]);
                        world[n.X + 1, n.Y].VisitedGoingDirection = Direction.East;
                        world[n.X + 1, n.Y].StepsInCurrentDirection = 1;
                    }


                    break;

                default: // From init node
                    neighbours.Add(world[n.X, n.Y + 1]);
                    world[n.X, n.Y + 1].VisitedGoingDirection = Direction.South;
                    world[n.X, n.Y + 1].StepsInCurrentDirection = 1;

                    neighbours.Add(world[n.X + 1, n.Y]);
                    world[n.X + 1, n.Y].VisitedGoingDirection = Direction.East;
                    world[n.X + 1, n.Y].StepsInCurrentDirection = 1;
                    break;
            }

            return neighbours;

        }


        void Dump()
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    string z = world[x, y].Distance == int.MaxValue ? "M" : world[x, y].Distance.ToString();
                    string zz = world[x, y].Distance < int.MaxValue ? "*" : " ";

                    Console.Write($"[{world[x, y].Loss}{zz}{z}] ".PadLeft(8));
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

    }
}
