class Program
{
    class WorldTile
    {
        public char Character { get; set; }
        public bool Handled { get; set; }
        public int Id { get; set; }
    }


    static Dictionary<int, List<int>> gears = new Dictionary<int, List<int>>();
    static List<int> ratios = new List<int>();

    static int yMax;
    static int xMax;
    static WorldTile[,] world;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        int id = 0;

        world = new WorldTile[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                world[x, y] = new WorldTile();
                world[x, y].Character = lines[y][x];
                world[x, y].Handled = false;
                world[x, y].Id = id;
                id++;
            }
        }


        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (world[x, y].Character == '*')
                {
                    FindPartNo(x, y, world[x,y].Id);
                }
            }
        }

        int sum = ratios.Sum();

        Console.WriteLine(sum);
        Console.ReadKey();
    }


    static void FindPartNo(int x, int y, int id)
    {
        if (char.IsDigit(world[x - 1, y - 1].Character) && world[x - 1, y - 1].Handled == false) { SpoolPartNo(x - 1, y - 1, id); }
        if (char.IsDigit(world[x, y - 1].Character) && world[x, y - 1].Handled == false) { SpoolPartNo(x, y - 1, id); }
        if (char.IsDigit(world[x + 1, y - 1].Character) && world[x + 1, y - 1].Handled == false) { SpoolPartNo(x + 1, y - 1, id); }
        if (char.IsDigit(world[x - 1, y].Character) && world[x - 1, y].Handled == false) { SpoolPartNo(x - 1, y, id); }
        if (char.IsDigit(world[x + 1, y].Character) && world[x + 1, y].Handled == false) { SpoolPartNo(x + 1, y, id); }
        if (char.IsDigit(world[x - 1, y + 1].Character) && world[x - 1, y + 1].Handled == false) { SpoolPartNo(x - 1, y + 1, id); }
        if (char.IsDigit(world[x, y + 1].Character) && world[x, y + 1].Handled == false) { SpoolPartNo(x, y + 1, id); }
        if (char.IsDigit(world[x + 1, y + 1].Character) && world[x + 1, y + 1].Handled == false) { SpoolPartNo(x + 1, y + 1, id); }
    }


    static void SpoolPartNo(int x, int y, int id)
    {
        while (x > 0 && char.IsDigit(world[x - 1, y].Character) == true)
        {
            x--;
        }

        string partNo = "";
        while (x < xMax && char.IsDigit(world[x, y].Character) == true)
        {
            partNo += world[x, y].Character;
            world[x, y].Handled = true;
            x++;
        }

        if (gears.ContainsKey(id))
        {
            gears[id].Add(Convert.ToInt32(partNo));
        }
        else
        {
            gears.Add(id, [Convert.ToInt32(partNo)]);
        }

        if (gears[id].Count == 2)
        {
            ratios.Add(gears[id][0] * gears[id][1]);
        }
    }
}