class Program
{
    class Tile
    {
        public char Character { get; set; }
        public int GalaxyId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int xs { get; set; } = 1;
        public int ys { get; set; } = 1;
    }

    class GalaxyPair
    {
        public Tile GalaxyA { get; set; }
        public Tile GalaxyB { get; set; }
        public long Distance { get; set; }
    }

    static List<GalaxyPair> galaxypairs = [];
    static int yMax;
    static int xMax;
    static List<List<Tile>> world = [];

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        int id = 1;

        for (int y = 0; y < yMax; y++)
        {
            var row = new List<Tile>();

            for (int x = 0; x < xMax; x++)
            {
                var t = new Tile();
                t.Character = lines[y][x];

                if (t.Character == '#')
                {
                    t.GalaxyId = id;
                    id++;
                }

                row.Add(t);
            }

            world.Add(row);
        }




        for (int y = 0; y < yMax; y++) // Expand rows
        {
            if (world[y].All(f => f.Character == '.'))
            {
                for (int i = 0; i < xMax; i++)
                {
                    world[y][i].ys = 1000000;
                }
            }
        }

        for (int x = 0; x < xMax; x++) // Expand columns
        {
            if (world.All(f => f[x].Character == '.'))
            {
                for (int i = 0; i < yMax; i++)
                {
                    world[i][x].xs = 1000000;
                }
            }
        }

        List<Tile> galaxies = [];

        // Recalc positions of galaxies
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (world[y][x].Character == '#')
                {
                    world[y][x].X = x;
                    world[y][x].Y = y;
                    galaxies.Add(world[y][x]);
                }
            }
        }

        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = 0; j < galaxies.Count; j++)
            {
                if (i != j)
                {
                    if (galaxypairs.Any(f => f.GalaxyB.GalaxyId == galaxies[i].GalaxyId && f.GalaxyA.GalaxyId == galaxies[j].GalaxyId) == false)
                    {
                        galaxypairs.Add(new GalaxyPair() { GalaxyA = galaxies[i], GalaxyB = galaxies[j] });
                    }
                }
            }
        }

        foreach (var pair in galaxypairs)
        {
            long xSum = 0;
            for (int x = Math.Min(pair.GalaxyA.X, pair.GalaxyB.X) + 1; x <= Math.Max(pair.GalaxyA.X, pair.GalaxyB.X); x++)
            {
                xSum += world[pair.GalaxyA.Y][x].xs;
            }

            long ySum = 0;
            for (int y = Math.Min(pair.GalaxyA.Y, pair.GalaxyB.Y) + 1; y <= Math.Max(pair.GalaxyA.Y, pair.GalaxyB.Y); y++)
            {
                ySum += world[y][pair.GalaxyA.X].ys;
            }

            pair.Distance = xSum + ySum;
        }

        long sum = galaxypairs.Sum(f => f.Distance);
        Console.WriteLine(sum);
        Console.ReadKey();
    }
}