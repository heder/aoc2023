
class Rule
{
    public char Variable { get; set; }
    public char Operator { get; set; }
    public int Operand { get; set; }
    public string Destination { get; set; }

}



class Part
{
    public Dictionary<char, int> Ratings { get; set; }
}

class Program
{
    static Dictionary<string, List<Rule>> rules = [];
    static List<Part> parts = [];

    static List<Part> accepted = [];
    static List<Part> rejected = [];


    static void Main()
    {
        var lines = File.ReadAllLines("in.txt");

        int i = 0;
        while (lines[i] != "")
        {
            var line = lines[i];

            var split1 = line.Split('{');
            var name = split1[0]; //

            var split2 = split1[1].Trim('}').Split(',');
            for (int j = 0; j < split2.Length; j++)
            {
                var split3 = split2[j].Split(':');

                if (split3.Length == 2)
                {
                    if (split3[0].Contains('<'))
                    {
                        var split4 = split3[0].Split('<');

                        var p = new Rule
                        {
                            Variable = split4[0][0],
                            Operand = Convert.ToInt32(split4[1]),
                            Operator = '<',
                            Destination = split3[1]
                        };

                        if (rules.TryGetValue(name, out List<Rule>? value))
                        {
                            value.Add(p);
                        }
                        else
                        {
                            rules.Add(name, new List<Rule>() { p });
                        }
                    }

                    if (split3[0].Contains('>'))
                    {
                        var split4 = split3[0].Split('>');

                        var p = new Rule
                        {
                            Variable = split4[0][0],
                            Operand = Convert.ToInt32(split4[1]),
                            Operator = '>',
                            Destination = split3[1]
                        };

                        if (rules.TryGetValue(name, out List<Rule>? value))
                        {
                            value.Add(p);
                        }
                        else
                        {
                            rules.Add(name, new List<Rule>() { p });
                        }
                    }


                }
                else if (split3.Length == 1)
                {
                    if (split3[0] == "A")
                    {
                        var p = new Rule
                        {
                            Operator = 'A'
                        };

                        if (rules.TryGetValue(name, out List<Rule>? value))
                        {
                            value.Add(p);
                        }
                        else
                        {
                            rules.Add(name, new List<Rule>() { p });
                        }
                    }
                    else if (split3[0] == "R")
                    {
                        var p = new Rule
                        {
                            Operator = 'R'
                        };

                        if (rules.TryGetValue(name, out List<Rule>? value))
                        {
                            value.Add(p);
                        }
                        else
                        {
                            rules.Add(name, new List<Rule>() { p });
                        }
                    }
                    else
                    {
                        var p = new Rule
                        {
                            Operator = 'J',
                            Destination = split3[0]
                        };

                        if (rules.TryGetValue(name, out List<Rule>? value))
                        {
                            value.Add(p);
                        }
                        else
                        {
                            rules.Add(name, new List<Rule>() { p });
                        }
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            i++;
        }

        i++;

        while (i < lines.Length)
        {
            var line = lines[i];
            var x = line.Trim('{', '}').Split(',').Select(f => f.Split('='));

            Dictionary<char, int> r = [];
            foreach (var item in x)
            {
                r.Add(item[0][0], Convert.ToInt32(item[1]));
            }

            parts.Add(new Part() { Ratings = r });

            i++;
        }






        foreach (var part in parts)
        {
            var currentRules = rules["in"];
            Console.Write("in -> ");
            while (true)
            {
                for (int r = 0; r < currentRules.Count; r++)
                {
                    var rule = currentRules[r];

                    switch (rule.Operator)
                    {
                        case '>':
                            if (part.Ratings[rule.Variable] > rule.Operand)
                            {
                                if (rule.Destination == "A")
                                {
                                    Console.Write("A");
                                    accepted.Add(part);
                                    goto nextnext;
                                }
                                else if (rule.Destination == "R")
                                {
                                    Console.Write("R");
                                    rejected.Add(part);
                                    goto nextnext;
                                }
                                else
                                {
                                    Console.Write($"{rule.Destination} ->");
                                    currentRules = rules[rule.Destination];
                                    goto next;
                                }
                            }
                            break;

                        case '<':
                            if (part.Ratings[rule.Variable] < rule.Operand)
                            {
                                if (rule.Destination == "A")
                                {
                                    Console.Write("A");
                                    accepted.Add(part);
                                    goto nextnext;
                                }
                                else if (rule.Destination == "R")
                                {
                                    Console.Write("R");
                                    rejected.Add(part);
                                    goto nextnext;
                                }
                                else
                                {
                                    Console.Write($"{rule.Destination} ->");
                                    currentRules = rules[rule.Destination];
                                    goto next;
                                }

                            }
                            break;

                        case 'J':
                            Console.Write($"{rule.Destination} ->");
                            currentRules = rules[rule.Destination];
                            goto next;

                        case 'A':
                            Console.Write("A");
                            accepted.Add(part);
                            goto nextnext;

                        case 'R':
                            Console.Write("R");
                            rejected.Add(part);
                            goto nextnext;
                    }
                }

            next: { }
            }

        nextnext: { }
            Console.WriteLine();
        }

        int sum = 0;
        foreach (var item in accepted)
        {
            sum += item.Ratings.Sum(f => f.Value);
        }

        Console.WriteLine(sum);
        Console.ReadKey();
    }
}




