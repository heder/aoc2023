class Program
{
    static int yMax;
    static int xMax;
    static bool[,] world;

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        int startline;
        int endline;

        int sum = 0;

        int i = 0;
        while (i < lines.Length)
        {
            startline = i;
            while (lines[i].Length > 0 && i < lines.Length - 1)
            {
                i++;
            }
            endline = i - 1;

            xMax = lines[startline].Length - 1;
            yMax = endline - startline;
            world = new bool[xMax + 1, yMax + 1];

            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    world[x, y] = lines[startline + y][x] == '#';
                }
            }

            // Find vertical mirror
            for (int x = 0; x <= xMax - 1; x++)
            {
                int scanleft = x;
                int scanright = x + 1;

                while (scanleft >= 0 && scanright <= xMax)
                {
                    for (int y = 0; y <= yMax; y++)
                    {
                        if (world[scanleft, y] == world[scanright, y])
                        {
                            // We're good
                        }
                        else
                        {
                            goto bailout;
                        }
                    }

                    scanleft--; scanright++;
                }

                // If here, mirror found at x/x+1
                Console.WriteLine($"Mirror found at {x + 1}/{x + 2}. Lines to the left {x + 1}");
                sum += (x + 1);

            bailout: { }

            }

            // Find horizontal mirror
            for (int y = 0; y <= yMax - 1; y++)
            {
                int scanup = y;
                int scandown = y + 1;

                while (scanup >= 0 && scandown <= yMax)
                {
                    for (int x = 0; x <= xMax; x++)
                    {
                        if (world[x, scanup] == world[x, scandown])
                        {
                            // We're good
                        }
                        else
                        {
                            goto bailout;
                        }
                    }

                    scanup--; scandown++;
                }

                // If here, mirror found at x/x+1
                Console.WriteLine($"Mirror found at {y + 1}/{y + 2}. Rows above {y + 1}");
                sum += (100 * (y + 1));

            bailout: { }

            }

            i++;
        }

        Console.WriteLine(sum);
        Console.ReadKey();
    }
}