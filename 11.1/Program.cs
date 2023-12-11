class Program
{
    class Tile
    {
        public char Character { get; set; }
        //public bool Handled { get; set; }
        public int GalaxyId { get; set; }
    }

    class GalaxyPair
    {
        public int GalaxyA { get; set; }
        public int GalaxyB { get; set; }
        public int Distance { get; set; }
    }

    static List<GalaxyPair> ratios = [];

    static int yMax;
    static int xMax;
    //static Tile[,] world;

    static List<List<Tile>> world = new List<List<Tile>>();

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
                t.GalaxyId = id;

                row.Add(t);

                id++;
            }

            world.Add(row);
        }

        for (int y = 0; y < yMax; y++) // Expand rows
        {
            if (world[y].All(f => f.Character == '.'))
            {
                world.Insert(y, new List<Tile>());
            }

            //for (int x = 0; x < xMax; x++)
            //{
            //    world[x, y] = new Tile();
            //    world[x, y].Character = lines[y][x];
            //    world[x, y].GalaxyId = id;
            //    id++;
            //}
        }



        //for (int y = 0; y < yMax; y++)
        //{
        //    for (int x = 0; x < xMax; x++)
        //    {
        //        if (world[x, y].Character == '*')
        //        {
        //            FindPartNo(x, y, world[x, y].Id);
        //        }
        //    }
        //}

        //int sum = ratios.Sum();
        //Console.WriteLine(sum);
        Console.ReadKey();
    }


    //static void FindPartNo(int x, int y, int id)
    //{
    //    if (char.IsDigit(world[x - 1, y - 1].Character) && world[x - 1, y - 1].Handled == false) { SpoolPartNo(x - 1, y - 1, id); }
    //    if (char.IsDigit(world[x, y - 1].Character) && world[x, y - 1].Handled == false) { SpoolPartNo(x, y - 1, id); }
    //    if (char.IsDigit(world[x + 1, y - 1].Character) && world[x + 1, y - 1].Handled == false) { SpoolPartNo(x + 1, y - 1, id); }
    //    if (char.IsDigit(world[x - 1, y].Character) && world[x - 1, y].Handled == false) { SpoolPartNo(x - 1, y, id); }
    //    if (char.IsDigit(world[x + 1, y].Character) && world[x + 1, y].Handled == false) { SpoolPartNo(x + 1, y, id); }
    //    if (char.IsDigit(world[x - 1, y + 1].Character) && world[x - 1, y + 1].Handled == false) { SpoolPartNo(x - 1, y + 1, id); }
    //    if (char.IsDigit(world[x, y + 1].Character) && world[x, y + 1].Handled == false) { SpoolPartNo(x, y + 1, id); }
    //    if (char.IsDigit(world[x + 1, y + 1].Character) && world[x + 1, y + 1].Handled == false) { SpoolPartNo(x + 1, y + 1, id); }
    //}


    //static void SpoolPartNo(int x, int y, int id)
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

    //    if (gears.ContainsKey(id))
    //    {
    //        gears[id].Add(Convert.ToInt32(partNo));
    //    }
    //    else
    //    {
    //        gears.Add(id, [Convert.ToInt32(partNo)]);
    //    }

    //    if (gears[id].Count == 2)
    //    {
    //        ratios.Add(gears[id][0] * gears[id][1]);
    //    }
    //}
}