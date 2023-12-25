﻿using MathNet.Numerics;

abstract class Node
{
    public string name;
    public long timestamp;
    public List<Node> children = [];

    public Queue<bool> outputQueue = new Queue<bool>();

    public virtual void In(bool pulse, string name)
    {
        timestamp = Program.timestamp;
        Program.timestamp++;
    }

    public virtual void Out(long i)
    {
        var send = outputQueue.Dequeue();

        foreach (Node node in children)
        {
            if (send == true) Program.noHigh++;
            if (send == false) Program.noLow++;

            node.In(send, name);
        }
    }
}

class BroadcasterNode : Node
{
    bool state;

    public override void In(bool pulse, string name)
    {
        state = pulse;
        base.In(pulse, name);

        outputQueue.Enqueue(state);
    }
}

class OutputNode : Node
{
    public override void In(bool pulse, string name)
    {
        if (pulse == false)
        {
            Console.WriteLine("Low pulse received");
            Console.ReadKey();
        }

        outputQueue.Enqueue(pulse);
        base.In(pulse, name);
    }
}


class ConjunctionNode : Node
{
    public Dictionary<string, bool> inputs = [];
    public override void In(bool pulse, string name)
    {
        inputs[name] = pulse;
        base.In(pulse, name);

        if (inputs.Values.All(f => f == true))
        {
            outputQueue.Enqueue(false);
        }
        else
        {
            outputQueue.Enqueue(true);
        }
    }

    public override void Out(long i)
    {
        var send = outputQueue.Dequeue();

        foreach (Node node in children)
        {
            if (send == true)
            {
                if (name == "sj" || name == "qq" || name == "bg" || name == "ls")
                {
                    Console.WriteLine($"{name} will output high @ {i}");
                }
            }

            if (send == true) Program.noHigh++;
            if (send == false) Program.noLow++;

            node.In(send, name);
        }
    }
}


class FlipFlopNode : Node
{
    public bool state;

    public override void In(bool pulse, string name)
    {
        if (pulse == true)
        {
            return;
        }

        if (pulse == false)
        {
            state = !state;
        }

        base.In(pulse, name);

        outputQueue.Enqueue(state);
    }
}

class Program
{
    static List<Node> nodes = [];
    public static long timestamp = 0;

    public static long noHigh = 0;
    public static long noLow = 0;

    static void Main()
    {
        var lines = File.ReadAllLines("in.txt");

        for (long i = 0; i < lines.Length; i++)
        {
            var s1 = lines[i].Split("->");
            var node = s1[0].Trim();

            if (node[0] == '%')
                nodes.Add(new FlipFlopNode() { name = node[1..] });
            else if (node[0] == '&')
                nodes.Add(new ConjunctionNode() { name = node[1..] });
            else if (node == "rx")
                nodes.Add(new OutputNode() { name = node });
            else
                nodes.Add(new BroadcasterNode() { name = node });
        }

        for (long i = 0; i < lines.Length; i++)
        {
            var s1 = lines[i].Split("->");
            var node = s1[0].Trim(' ', '%', '&');
            var children = s1[1].Trim().Split(',').Select(f => f.Trim()).ToList();

            if (children[0] == "rx")
            {
                nodes.Add(new OutputNode() { name = "rx" });
            }

            var n = nodes.FirstOrDefault(f => f.name == node);

            if (n != null)
            {
                foreach (var item in children)
                {
                    var c = nodes.First(f => f.name == item);

                    if (c is ConjunctionNode x)
                    {
                        x.inputs.Add(node, false);
                    }

                    n.children.Add(c);
                }
            }
        }

        for (long i = 1; i < 100000; i++)
        {
            noLow++; // Button

            var b = nodes.First(f => f.name == "broadcaster");
            b.In(false, "init");
            while (true)
            {
                var n = nodes.Where(f => f.outputQueue.Count > 0).OrderBy(f => f.timestamp).ToList();
                foreach (var item in n)
                {
                    item.Out(i);
                }

                if (n.Count == 0)
                    break;
            }

            // 3739
            // 3797
            // 3919
            // 4003
        }

        Console.WriteLine(Euclid.LeastCommonMultiple(3739, 3797, 3919, 4003));
        Console.ReadKey();
    }
}