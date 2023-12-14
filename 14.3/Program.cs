class Program
{
    // Stupid cycle validation routine because of brain meltdown
    static void Main()
    {
        int c = 92;
        for (int i = 155; i <= 1000000000; i++)
        {
            if (c == 155)
            {
                c = 92;
            }

            if (i == 155)
            {
                Console.WriteLine($"{i} {c}");
            }

            if (i == 218)
            {
                Console.WriteLine($"{i} {c}");
            }

            if (i == 281)
            {
                Console.WriteLine($"{i} {c}");
            }

            if (i == 344)
            {
                Console.WriteLine($"{i} {c}");
            }

            if (i == 407)
            {
                Console.WriteLine($"{i} {c}");
            }

            if (i == 1000000000)
            {
                Console.WriteLine($"{i} {c}");
            }

            {
                c++;
            }
        }
    }
}
