class Program
{
    class Tile
    {
        public char Character { get; set; }
        public int GalaxyId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    class GalaxyPair
    {
        public Tile GalaxyA { get; set; }
        public Tile GalaxyB { get; set; }
        public int Distance { get; set; }
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
                }

                row.Add(t);
                id++;
            }

            world.Add(row);
        }

        for (int y = 0; y < yMax; y++) // Expand rows
        {
            if (world[y].All(f => f.Character == '.'))
            {
                var newRow = new List<Tile>();
                for (int i = 0; i < xMax; i++)
                {
                    newRow.Add(new Tile() { Character = '.' });
                }

                world.Insert(y, newRow);
                yMax++;
            }
        }

        for (int x = 0; x < xMax; x++) // Expand columns
        {
            if (world.All(f => f[x].Character == '.'))
            {
                for (int i = 0; i < yMax; i++)
                {
                    world[i].Insert(x, new Tile() { Character = '.' });
                    xMax++;
                }
            }
        }

        List<Tile> galaxies = new List<Tile>();

        // Recalc positions of galaxies
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (world[x][y].Character == '#')
                {
                    world[x][y].X = x;
                    world[x][y].Y = y;
                    galaxies.Add(world[x][y]);
                }
            }
        }

        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = 0; j < galaxies.Count; j++)
            {
                if (i != j)
                {
                    if (galaxypairs.Any(f => f.GalaxyB.GalaxyId == galaxies[i].GalaxyId && f.GalaxyA.GalaxyId == galaxies[i].GalaxyId) == false)
                    {
                        galaxypairs.Add(new GalaxyPair() { GalaxyA = galaxies[i], GalaxyB = galaxies[j] });
                    }
                }
            }
        }

        foreach (var pair in galaxypairs)
        {
            var xdist = Math.Abs(pair.GalaxyA.X - pair.GalaxyB.X);
            var ydist = Math.Abs(pair.GalaxyA.Y - pair.GalaxyB.Y);

            pair.Distance = xdist + ydist;
        }

        int sum = galaxypairs.Sum(f => f.Distance);
        Console.WriteLine(sum);
        Console.ReadKey();
    }
}