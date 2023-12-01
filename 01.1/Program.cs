class Program
{
    static void Main()
    {
        var lines = File.ReadLines("in.txt").ToArray();

        int sum = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            var f = lines[i].First(f => char.IsNumber(f));
            var l = lines[i].Last(f => char.IsNumber(f));

            string value = $"{f}{l}";

            sum += Convert.ToInt32(value);

        }

        Console.WriteLine(sum);
        Console.ReadKey();
    }
}

