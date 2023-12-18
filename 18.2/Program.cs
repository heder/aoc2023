using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Buffer;

class Program
{
    static void Main()
    {
        string[] lines = File.ReadAllLines("in.txt").ToArray();

        int currentX = 10000000; 
        int currentY = 10000000;

        List<Coordinate> coordinates = [new Coordinate(currentX, currentY)];

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var split1 = line.Split(' ');
            string dir = "";
            var color = split1[2].Trim('(', ')', '#');
            var steps = int.Parse(color.Substring(0, 5), System.Globalization.NumberStyles.HexNumber);
            var dircode = int.Parse(color.Substring(5, 1), System.Globalization.NumberStyles.HexNumber);

            switch (dircode)
            {
                case 0: dir = "R"; break;
                case 1: dir = "D"; break;
                case 2: dir = "L"; break;
                case 3: dir = "U"; break;
            }

            switch (dir)
            {
                case "U": currentY -= steps; break;
                case "L": currentX -= steps; break;
                case "R": currentX += steps; break;
                case "D": currentY += steps; break;
            }

            coordinates.Add(new Coordinate(currentX, currentY));
        }

        LinearRing lr = new(coordinates.ToArray());
        Polygon p = new(lr);
        var dilate = p.Buffer(0.5, new BufferParameters() { JoinStyle = JoinStyle.Mitre });

        var zzz = dilate.Area;

        Console.WriteLine(zzz);
        Console.ReadKey();
    }
}
