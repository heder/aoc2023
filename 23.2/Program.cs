class Program
{
    static void Main() { }
}


//    //class WorldTile
//    //{
//    //    public int Distance { get; set; } = int.MinValue;
//    //    public char Character { get; set; }
//    //    public int X { get; set; }
//    //    public int Y { get; set; }
//    //    public int LastVisitedBy { get; set; } = 0;
//    //    public HashSet<int> VisitedBy { get; set; } = [];

//    //    public bool IsVisitedBy(Path p)
//    //    {
//    //        var directvisit = VisitedBy.Contains(p.Id);
//    //        if (directvisit)
//    //            return true;

//    //        // Is visited by parent
//    //        var looper = p;
//    //        do
//    //        {
//    //            if (VisitedBy.Contains(looper.Id)) return true;
//    //            looper = looper.ParentPath;
//    //        } while (looper != null);

//    //        return false;
//    //    }
//    //}



//    class WorldTile
//    {
//        //public int Distance { get; set; } = int.MinValue;
//        public char Character { get; set; }
//        public int X { get; set; }
//        public int Y { get; set; }
//        public bool Visited { get; set; } = false;        
//    }

//    static int pathid = 0;

//    class Path
//    {
//        public Path ComingFrom { get; set; }
//        public int Id { get; set; }
//        public int X { get; set; }
//        public int Y { get; set; }
//        public int PathDistance { get; set; }
//        public Path ParentPath { get; set; }
//    }



//    class Node
//    {
//        public string Name { get; set; }
//        public int X { get; set; }
//        public int Y { get; set; }
//        public List<Edge> Edges { get; set; }
//        public bool IsStart { get; set; }
//        public bool IsEnd { get; set; }
//    }


//    class Edge
//    {
//        public int Length { get; set; }
//        public Node NodeA { get; set; }
//        public Node NodeB { get; set; }
//    }

//    static List<Node> Nodes = new List<Node>();
//    static List<Edge> Edges = new List<Edge>();



//    static int yMax;
//    static int xMax;
//    static WorldTile[,] world;

//    static List<Path> trails = [];

//    static void Main()
//    {
//        string[] lines = File.ReadAllLines("in.txt").ToArray();

//        yMax = lines.Length;
//        xMax = lines[0].Length;
//        world = new WorldTile[xMax, yMax];

//        for (int y = 0; y < yMax; y++)
//        {
//            for (int x = 0; x < xMax; x++)
//            {
//                world[x, y] = new WorldTile();
//                world[x, y].Character = lines[y][x];
//                world[x, y].X = x;
//                world[x, y].Y = y;

//            }
//        }

//        List<int> distances = [];

//        //Dump();

//        var p1 = new Path() { X = 1, Y = 1, PathDistance = 1, Id = ++pathid, ParentPath = null };
//        trails.Add(p1);
//        //world[1, 1].Distance = 1;
//        //world[1, 1].VisitedBy.Add(p1.Id);
//        world[1,1].Visited = true;

//        var n = new Node() { Name = $"{1},{1}", X = 1, Y = 1, IsStart = true };
//        Nodes.Add(n);

//        world[1, 1].

//        //int i = 0;

//        List<Path> toRemove0 = [];
//        List<Path> toRemove = [];

//        while (true)
//        {
//            //Dump();

//            toRemove0.Clear();

//            var tc = trails.Count;
//            for (int p = 0; p < tc; p++)
//            {
//                var x = MoveAndBranch(trails[p]);
//                if (x != null) toRemove0.Add(x);
//            }


//            for (int rr = 0; rr < toRemove0.Count; rr++)
//            {
//                trails.Remove(toRemove0[rr]);
//            }


//            toRemove.Clear();

//            for (int p = 0; p < trails.Count; p++)
//            {
//                var p2 = trails[p];
//                world[p2.X, p2.Y].Distance = p2.PathDistance;
//                world[p2.X, p2.Y].VisitedBy.Add(p2.Id);
//                world[p2.X, p2.Y].LastVisitedBy = p2.Id;

//                if (p2.X == xMax - 2 && p2.Y == yMax - 1)
//                {
//                    Console.WriteLine($"Found path {p2.PathDistance}. Paths: evaluating: {trails.Count}");
//                    distances.Add(p2.PathDistance);
//                    toRemove.Add(p2);
//                }
//            }

//            for (int rr = 0; rr < toRemove.Count; rr++)
//            {
//                trails.Remove(toRemove[rr]);
//            }

//            if (trails.Count == 0) break;
//        }


//        //int sum = partNos.Sum();

//        Console.WriteLine(distances.Max());
//        Console.ReadKey();
//    }


//    static Path MoveAndBranch(Path p)
//    {
//        int x = p.X; int y = p.Y;
//        //bool currentUsed = false;
//        //var retval = new List<Path>();
//        var paths = 0;

//        List<Path> branches = [];

//        //if (world[x - 1, y - 1].Character != '#' && world[x - 1, y - 1].IsVisitedBy(p) == false) retval.Add(new Path() { X = x - 1, Y = y - 1 });

//        //        if (y > 0 && world[x, y - 1].Character != '#' && world[x, y - 1].Character != 'v' && world[x, y - 1].IsVisitedBy(p) == false)
//        if (world[x, y - 1].Character != '#' && world[x, y - 1].Visited == false)
//        {
//            paths++;
//            branches.Add(new Path() { X = x, Y = y - 1, PathDistance = 1, ParentPath = p }); ;
//        }

//        //if (world[x + 1, y - 1].Character != '#' && world[x + 1, y - 1].IsVisitedBy(p) == false) retval.Add(new Path() { X = x + 1, Y = y - 1 });

//        //if (x > 0 && world[x - 1, y].Character != '#' && world[x - 1, y].Character != '>' && world[x - 1, y].IsVisitedBy(p) == false)
//        if (world[x - 1, y].Character != '#' && world[x - 1, y].Visited == false)
//        {
//            paths++;
//            branches.Add(new Path() { X = x - 1, Y = y, PathDistance = 1, ParentPath = p });
//        }

//        //if (x < xMax - 1 && world[x + 1, y].Character != '#' && world[x + 1, y].Character != '<' && world[x + 1, y].IsVisitedBy(p) == false)
//        if (world[x + 1, y].Character != '#' && world[x + 1, y].Visited == false)
//        {
//            paths++;
//            branches.Add(new Path() { X = x + 1, Y = y, PathDistance = 1, ParentPath = p });
//        }


//        //if (world[x - 1, y + 1].Character != '#' && world[x - 1, y + 1].IsVisitedBy(p) == false) { SpoolPartNo(x - 1, y + 1); }

//        //if (y < yMax - 1 && world[x, y + 1].Character != '#' && world[x, y + 1].Character != '^' && world[x, y + 1].IsVisitedBy(p) == false)
//        if (world[x, y + 1].Character != '#' && world[x, y + 1].Visited == false)
//        {
//            paths++;
//            branches.Add(new Path() { X = x, Y = y + 1, PathDistance = 1, ParentPath = p });
//        }

//        if (paths == 0)
//        {
//            return p;
//        }
//        if (paths == 1) // Use existing and discard
//        {
//            var b = branches.First();
//            p.X = b.X; p.Y = b.Y; p.PathDistance = p.PathDistance + 1;
//            return null;
//        }
//        else
//        {
//            Nodes.Add(new Node() { X = x, Y = y, Name = $"{x},{y}" });

//            foreach (var item in branches)
//            {
//                item.Id = ++pathid;
//            }

//            trails.AddRange(branches);
//            return p;
//        }




//        //if (world[x + 1, y + 1].Character != '#' && world[x + 1, y + 1].IsVisitedBy(p) == false) { SpoolPartNo(x + 1, y + 1); }
//    }


//    static void Dump()
//    {
//        Console.Clear();

//        for (int y = 0; y < yMax; y++)
//        {
//            for (int x = 0; x < xMax; x++)
//            {
//                string s = world[x, y].Distance == int.MinValue ? world[x, y].Character.ToString() : $"{world[x, y].LastVisitedBy}";


//                Console.Write(s.PadLeft(3));
//            }

//            Console.WriteLine();
//        }

//        Thread.Sleep(5);

//        Console.WriteLine();
//    }


//    //static void SpoolPartNo(int x, int y)
//    //{
//    //    while (x > 0 && char.IsDigit(world[x - 1, y].Character) == true)
//    //    {
//    //        x--;
//    //    }

//    //    string partNo = "";
//    //    while (x < xMax && char.IsDigit(world[x, y].Character) == true)
//    //    {
//    //        partNo += world[x, y].Character;
//    //        world[x, y].Handled = true;
//    //        x++;
//    //    }

//    //    partNos.Add(Convert.ToInt32(partNo));
//    //}
//}