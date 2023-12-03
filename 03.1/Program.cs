class Program
{
    class WorldTile
    {
        public char Character { get; set; }
        public bool Handled { get; set; }
    }

    static int yMax;
    static int xMax;
    static WorldTile[,] world;
    static List<int> partNos = [];

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
                world[x,y] = new WorldTile();   
                world[x, y].Character = lines[y][x];
                world[x, y].Handled = false;

            }
        }


        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (char.IsDigit(world[x, y].Character) == false && world[x, y].Character != '.')
                {
                    FindPartNo(x, y);
                }
            }
        }

        int sum = partNos.Sum();

        Console.WriteLine(sum);
        Console.ReadKey();
    }


    static void FindPartNo(int x, int y)
    {
        if (char.IsDigit(world[x - 1, y - 1].Character) && world[x - 1, y - 1].Handled == false) { SpoolPartNo(x - 1, y - 1);}
        if (char.IsDigit(world[x, y - 1].Character) && world[x, y - 1].Handled == false) { SpoolPartNo(x, y - 1); }
        if (char.IsDigit(world[x + 1, y - 1].Character) && world[x + 1, y - 1].Handled == false) { SpoolPartNo(x + 1, y - 1); }
        if (char.IsDigit(world[x - 1, y].Character) && world[x - 1, y].Handled == false) { SpoolPartNo(x - 1, y);  }
        if (char.IsDigit(world[x + 1, y].Character) && world[x + 1, y].Handled == false) { SpoolPartNo(x + 1, y); }
        if (char.IsDigit(world[x - 1, y + 1].Character) && world[x - 1, y + 1].Handled == false) { SpoolPartNo(x - 1, y + 1); }
        if (char.IsDigit(world[x, y + 1].Character) && world[x, y + 1].Handled == false) { SpoolPartNo(x, y + 1); }
        if (char.IsDigit(world[x + 1, y + 1].Character) && world[x + 1, y + 1].Handled == false) { SpoolPartNo(x + 1, y + 1); }
    }


    static void SpoolPartNo(int x, int y)
    {
        while (x > 0 && char.IsDigit(world[x - 1, y].Character) == true)
        {
            x--;
        }

        string partNo = "";
        while (x < xMax && char.IsDigit(world[x, y].Character) == true)
        {
            partNo += world[x, y].Character;
            world[x,y].Handled = true;
            x++;
        }

        partNos.Add(Convert.ToInt32(partNo));
    }
}