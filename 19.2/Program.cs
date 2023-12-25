
class Rule
{
    public char Variable { get; set; }
    public char Operator { get; set; }
    public int Operand { get; set; }
    public string Destination { get; set; }
    public Rule PrevRule { get; set; } = null;
    public string WorkFlow { get; set; }

}

class ValidInterval
{
    public char Variable { get; set; }

    public int Low { get; set; }
    public int High { get; set; }
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

                Rule p = null;

                if (split3.Length == 2)
                {
                    if (split3[0].Contains('<'))
                    {
                        var split4 = split3[0].Split('<');

                        p = new Rule
                        {
                            Variable = split4[0][0],
                            Operand = Convert.ToInt32(split4[1]),
                            Operator = '<',
                            Destination = split3[1],
                            PrevRule = p,
                            WorkFlow = name
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

                        p = new Rule
                        {
                            Variable = split4[0][0],
                            Operand = Convert.ToInt32(split4[1]),
                            Operator = '>',
                            Destination = split3[1],
                            PrevRule = p,
                            WorkFlow = name
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
                        p = new Rule
                        {
                            Operator = 'A',
                            PrevRule = p,
                            WorkFlow = name
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
                        p = new Rule
                        {
                            Operator = 'R',
                            PrevRule = p,
                            WorkFlow = name
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
                        p = new Rule
                        {
                            Operator = 'J',
                            Destination = split3[0],
                            PrevRule = p,
                            WorkFlow = name
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

        // Backtrack rules from 'A'

        var acceptingRules = rules.SelectMany(f => f.Value).Where(f => f.Operator == 'A');
        foreach (var acceptingRule in acceptingRules)
        {
            BackTrack(acceptingRule, new List<ValidInterval>());
        }



        void BackTrack(Rule rule, List<ValidInterval> v)
        {
            if (rule.PrevRule == null && rule.WorkFlow == "in")
            {
                // At root
            }

            List<Rule> backPath;
            if (rule.PrevRule == null)
            {
                // Find rules that lead us here
                backPath = rules.SelectMany(f => f.Value).Where(f => f.Destination == rule.WorkFlow).ToList();
            }
            else
            {
                backPath = new List<Rule>() { rule.PrevRule };
            }

            List<ValidInterval> vi = new List<ValidInterval>(v);

            if (rule.Operator == 'A')
            {

            }

            if (rule.Operator == '<')
            {
                vi.Add(new ValidInterval() { Variable = rule.Variable, Low = rule.Operand, High = 4000 });
            }

            if (rule.Operator == '>')
            {
                vi.Add(new ValidInterval() { Variable = rule.Variable, Low = rule.Operand, High = 4000 });
            }


            foreach (var r in backPath)
            {
                BackTrack(r, vi);
            }
        }


        //foreach (var part in parts)
        //{
        //    var currentRules = rules["in"];
        //    Console.Write("in -> ");
        //    while (true)
        //    {
        //        for (int r = 0; r < currentRules.Count; r++)
        //        {
        //            var rule = currentRules[r];

        //            switch (rule.Operator)
        //            {
        //                case '>':
        //                    if (part.Ratings[rule.Variable] > rule.Operand)
        //                    {
        //                        if (rule.Destination == "A")
        //                        {
        //                            Console.Write("A");
        //                            accepted.Add(part);
        //                            goto nextnext;
        //                        }
        //                        else if (rule.Destination == "R")
        //                        {
        //                            Console.Write("R");
        //                            rejected.Add(part);
        //                            goto nextnext;
        //                        }
        //                        else
        //                        {
        //                            Console.Write($"{rule.Destination} ->");
        //                            currentRules = rules[rule.Destination];
        //                            goto next;
        //                        }
        //                    }
        //                    break;

        //                case '<':
        //                    if (part.Ratings[rule.Variable] < rule.Operand)
        //                    {
        //                        if (rule.Destination == "A")
        //                        {
        //                            Console.Write("A");
        //                            accepted.Add(part);
        //                            goto nextnext;
        //                        }
        //                        else if (rule.Destination == "R")
        //                        {
        //                            Console.Write("R");
        //                            rejected.Add(part);
        //                            goto nextnext;
        //                        }
        //                        else
        //                        {
        //                            Console.Write($"{rule.Destination} ->");
        //                            currentRules = rules[rule.Destination];
        //                            goto next;
        //                        }

        //                    }
        //                    break;

        //                case 'J':
        //                    Console.Write($"{rule.Destination} ->");
        //                    currentRules = rules[rule.Destination];
        //                    goto next;

        //                case 'A':
        //                    Console.Write("A");
        //                    accepted.Add(part);
        //                    goto nextnext;

        //                case 'R':
        //                    Console.Write("R");
        //                    rejected.Add(part);
        //                    goto nextnext;
        //            }
        //        }

        //    next: { }
        //    }

        //nextnext: { }
        //    Console.WriteLine();
        //}

        //int sum = 0;
        //foreach (var item in accepted)
        //{
        //    sum += item.Ratings.Sum(f => f.Value);
        //}

        Console.WriteLine();
        Console.ReadKey();
    }
}




