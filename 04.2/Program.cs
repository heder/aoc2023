class Program
{
    class Card
    {
        public int CardId { get; set; }
        public List<int> WinningNumbers { get; set; } = [];
        public List<int> DrawnNumbers { get; set; } = [];
        public int Instances { get; set; } = 1;
    }

    static void Main()
    {
        var cards = new List<Card>();
        var lines = File.ReadLines("in.txt");

        foreach (var line in lines)
        {
            var c = new Card();

            var split1 = line.Split(':');
            var split2 = split1[0].Split(' ').Where(f => f.Trim() != "").ToArray();

            c.CardId = Convert.ToInt32(split2[1]);

            var split3 = split1[1].Split('|');

            var split4 = split3[0];
            var split5 = split3[1];

            var split6 = split4.Trim().Split(' ').Where(f => f.Trim() != "").Select(f => Convert.ToInt32(f));
            var split7 = split5.Trim().Split(' ').Where(f => f.Trim() != "").Select(f => Convert.ToInt32(f));

            foreach (var item in split6)
            {
                c.WinningNumbers.Add(Convert.ToInt32(item));
            }

            foreach (var item in split7)
            {
                c.DrawnNumbers.Add(Convert.ToInt32(item));
            }

            cards.Add(c);
        }

        for (int i = 0; i < cards.Count; i++)
        {
            var card = cards[i];
            var wins = card.WinningNumbers.Intersect(card.DrawnNumbers).Count();

            for (int j = i + 1; j < i + 1 + wins; j++)
            {
                cards[j].Instances += card.Instances;
            }
        }

        Console.WriteLine(cards.Sum(f => f.Instances));
        Console.ReadKey();
    }
}
