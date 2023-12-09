class Program
{
    class Report
    {
        public List<List<int>> Lists { get; set; } = [];

        public void GenerateHistory()
        {
            while (true)
            {
                var newList = new List<int>();
                var currentList = Lists.Last();

                for (int j = 0; j < currentList.Count - 1; j++)
                {
                    newList.Add(currentList[j + 1] - currentList[j]);
                }

                Lists.Add(newList);

                if (newList.All(f => f == 0))
                {
                    break;
                }
            }

            Lists.Last().Add(0);

            for (int i = Lists.Count - 1; i > 0; i--)
            {
                int val1 = Lists[i][^1];
                int val2 = Lists[i - 1].Last();

                Lists[i - 1].Add(val1 + val2);
            }
        }
    }

    static List<Report> reports = [];

    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        for (int i = 0; i < lines.Length; i++)
        {
            var s = lines[i].Split(' ').Where(f => f.Trim() != "").Select(f => Convert.ToInt32(f)).ToList();

            var r = new Report();
            r.Lists.Add(s);
            reports.Add(r);

            r.GenerateHistory();
        }

        var sum = reports.Sum(f => f.Lists.First().Last());

        Console.WriteLine(sum);
        Console.ReadKey();
    }
}