class Program
{
    public enum Direction
    {
        North = 1,
        West = 2,
        East = 3,
        South = 4
    }

    class Tile
    {
        public List<int> Distances = [];
        public int Distance { get; set; } = int.MaxValue;
        public char Character { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"{Character} X:{X}, Y{Y}";
        }
    }


    static int yMax;
    static int xMax;
    static Tile[,] world;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        yMax = lines.Length;
        xMax = lines[0].Length;
        world = new Tile[xMax, yMax];

        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                var t = new Tile();
                t.Character = lines[y][x];
                t.X = x;
                t.Y = y;

                world[x, y] = t;
            }
        }

        while (true)
        {
            var moved = false;
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    if (world[x, y].Character == 'O')
                    {
                        if (y > 0 && world[x, y - 1].Character == '.')
                        {
                            world[x, y - 1].Character = 'O';
                            world[x, y].Character = '.';
                            moved = true;

                            // Dump();
                        }
                    }
                }
            }

            if (moved == false)
            {
                break;
            }
        }



        int sum = 0;
        for (int y = 0; y < yMax; y++)
        {
            for (int x = 0; x < xMax; x++)
            {
                if (world[x, y].Character == 'O')
                {
                    var weight = yMax - y;
                    sum += weight;
                }
            }
        }


        Console.WriteLine(sum);
        Console.ReadKey();


        void Dump()
        {
            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    Console.Write(world[x, y].Character);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
