class Program
{
    static Dictionary<char, char> valuemap = new Dictionary<char, char>()
    {
        { '2', 'A' },
        { '3', 'B' },
        { '4', 'C' },
        { '5', 'D' },
        { '6', 'E' },
        { '7', 'F' },
        { '8', 'G' },
        { '9', 'H' },
        { 'T', 'I' },
        { 'J', 'J' },
        { 'Q', 'K' },
        { 'K', 'L' },
        { 'A', 'M' }
    };

    class Hand
    {
        public string Cards { get; set; }

        public string SortableCards { get; set; }

        public int HandValue { get; set; }
        public int BidValue { get; set; }
        public int Rank { get; set; }

        public void EvaluateHand()
        {
            var g = Cards.GroupBy(x => x).ToDictionary(g => g.Key, g => g.ToList());

            if (g.Count == 1 && g.Values.Where(f => f.Count == 5).Count() == 1) // 5
            {
                HandValue = 7;
            }
            else if (g.Count == 2 && g.Values.Where(f => f.Count == 4).Count() == 1 && g.Values.Where(f => f.Count == 1).Count() == 1)
            {
                HandValue = 6;
            }
            else if (g.Count == 2 && g.Values.Where(f => f.Count == 3).Count() == 1 && g.Values.Where(f => f.Count == 2).Count() == 1)
            {
                HandValue = 5;
            }
            else if (g.Count == 3 && g.Values.Where(f => f.Count == 3).Count() == 1 && g.Values.Where(f => f.Count == 1).Count() == 2)
            {
                HandValue = 4;
            }
            else if (g.Count == 3 && g.Values.Where(f => f.Count == 2).Count() == 2 && g.Values.Where(f => f.Count == 1).Count() == 1)
            {
                HandValue = 3;
            }
            else if (g.Count == 4 && g.Values.Where(f => f.Count == 2).Count() == 1 && g.Values.Where(f => f.Count == 1).Count() == 3)
            {
                HandValue = 2;
            }
            else if (Cards.Distinct().Count() == 5)
            {
                HandValue = 1;
            }
            else
            {
                throw new Exception("Unknown hand");
            }
        }

        public void GenerateSortableCards()
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                SortableCards += valuemap[Cards[i]];
            }
        }
    }



    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt");
        List<Hand> hands = [];

        foreach (string line in lines)
        {
            var split = line.Split(' ');

            var h = new Hand() { Cards = split[0].Trim(), BidValue = Convert.ToInt32(split[1].Trim()) };
            h.GenerateSortableCards();
            h.EvaluateHand();
            hands.Add(h);
        }

        hands = hands.OrderBy(f => f.HandValue).ThenBy(f => f.SortableCards).ToList();

        int rank = 1;
        foreach (var hand in hands)
        {
            hand.Rank = rank++;
        }

        var sum = hands.Sum(f => f.BidValue * f.Rank);
        Console.WriteLine(sum);
        Console.ReadKey();
    }
}