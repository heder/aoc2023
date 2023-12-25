class Program
{
    class WorldTile
    {
        public int Distance { get; set; } = int.MinValue;
        public char Character { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int LastVisitedBy { get; set; } = 0;
        public List<Path> VisitedBy { get; set; } = [];

        public bool IsVisitedBy(Path p)
        {
            var directvisit = VisitedBy.Contains(p);
            if (directvisit)
                return true;

            // Is visited by parent
            var looper = p;
            do
            {
                if (VisitedBy.Contains(looper)) return true;
                looper = looper.ParentPath;
            } while (looper != null);

            return false;
        }
    }

    static int pathid = 0;

    class Path
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int PathDistance { get; set; }
        public Path ParentPath { get; set; }
    }


    static int yMax;
    static int xMax;
    static WorldTile[,] world;
    //static List<int> partNos = [];

    static List<Path> trails = [];

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        world = new WorldTile[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                world[x, y] = new WorldTile();
                world[x, y].Character = lines[y][x];
                world[x, y].X = x;
                world[x, y].Y = y;

            }
        }

        List<int> distances = [];

        //Dump();

        var p1 = new Path() { X = 1, Y = 0, PathDistance = 0, Id = ++pathid, ParentPath = null };
        trails.Add(p1);
        world[1, 0].Distance = 0;
        world[1, 0].VisitedBy.Add(p1);


        int i = 0;
        while (true)
        {
            //Dump();

            var tc = trails.Count;
            for (int p = 0; p < tc; p++)
            {
                MoveAndBranch(trails[p]);
            }

            List<Path> toRemove = [];

            for (int p = 0; p < trails.Count; p++)
            {
                var p2 = trails[p];
                world[p2.X, p2.Y].Distance = p2.PathDistance;
                world[p2.X, p2.Y].VisitedBy.Add(p2);
                world[p2.X, p2.Y].LastVisitedBy = p2.Id;

                if (p2.X == xMax - 2 && p2.Y == yMax - 1)
                {
                    distances.Add(p2.PathDistance);
                    toRemove.Add(p2);
                }
            }


            for (int rr = 0; rr < toRemove.Count; rr++)
            {
                trails.Remove(toRemove[rr]);
            }


            if (trails.Count == 0) break;
        }


        //int sum = partNos.Sum();

        Console.WriteLine(distances.Max());
        Console.ReadKey();
    }


    static void MoveAndBranch(Path p)
    {
        int x = p.X; int y = p.Y;
        bool currentUsed = false;
        var retval = new List<Path>();
        var paths = 0;

        List<Path> branches = [];

        //if (world[x - 1, y - 1].Character != '#' && world[x - 1, y - 1].IsVisitedBy(p) == false) retval.Add(new Path() { X = x - 1, Y = y - 1 });

        if (y > 0 && world[x, y - 1].Character != '#' && world[x, y - 1].Character != 'v' && world[x, y - 1].IsVisitedBy(p) == false)
        {
            paths++;
            branches.Add(new Path() { X = x, Y = y - 1, PathDistance = p.PathDistance + 1, ParentPath = p }); ;
        }

        //if (world[x + 1, y - 1].Character != '#' && world[x + 1, y - 1].IsVisitedBy(p) == false) retval.Add(new Path() { X = x + 1, Y = y - 1 });

        if (x > 0 && world[x - 1, y].Character != '#' && world[x - 1, y].Character != '>' && world[x - 1, y].IsVisitedBy(p) == false)
        {
            paths++;
            branches.Add(new Path() { X = x - 1, Y = y, PathDistance = p.PathDistance + 1, ParentPath = p });
        }

        if (x < xMax - 1 && world[x + 1, y].Character != '#' && world[x + 1, y].Character != '<' && world[x + 1, y].IsVisitedBy(p) == false)
        {
            paths++;
            branches.Add(new Path() { X = x + 1, Y = y, PathDistance = p.PathDistance + 1, ParentPath = p });
        }


        //if (world[x - 1, y + 1].Character != '#' && world[x - 1, y + 1].IsVisitedBy(p) == false) { SpoolPartNo(x - 1, y + 1); }

        if (y < yMax - 1 && world[x, y + 1].Character != '#' && world[x, y + 1].Character != '^' && world[x, y + 1].IsVisitedBy(p) == false)
        {
            paths++;
            branches.Add(new Path() { X = x, Y = y + 1, PathDistance = p.PathDistance + 1, ParentPath = p });
        }


        if (paths == 1) // Use existing and discard
        {
            var b = branches.First();
            p.X = b.X; p.Y = b.Y; p.PathDistance = b.PathDistance;
        }
        else
        {
            foreach (var item in branches)
            {
                item.Id = ++pathid;
            }

            trails.AddRange(branches);
            trails.Remove(p);
        }


        //if (world[x + 1, y + 1].Character != '#' && world[x + 1, y + 1].IsVisitedBy(p) == false) { SpoolPartNo(x + 1, y + 1); }
    }


    static void Dump()
    {
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                string s = world[x, y].Distance == int.MinValue ? world[x, y].Character.ToString() : $"{world[x, y].LastVisitedBy}";


                Console.Write(s.PadLeft(4));
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }


    //static void SpoolPartNo(int x, int y)
    //{
    //    while (x > 0 && char.IsDigit(world[x - 1, y].Character) == true)
    //    {
    //        x--;
    //    }

    //    string partNo = "";
    //    while (x < xMax && char.IsDigit(world[x, y].Character) == true)
    //    {
    //        partNo += world[x, y].Character;
    //        world[x, y].Handled = true;
    //        x++;
    //    }

    //    partNos.Add(Convert.ToInt32(partNo));
    //}
}