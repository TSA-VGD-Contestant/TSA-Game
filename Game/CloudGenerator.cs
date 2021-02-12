using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class CloudGenerator
{
    private List<Cloud> Clouds;
    private readonly Random rand = new Random();

    private readonly int NumberOfclouds;

    public CloudGenerator()
    {
        Clouds = new List<Cloud>();
        NumberOfclouds = rand.Next(5, 10);
        for(int i = 0; i < NumberOfclouds; i++)
        { 
            Clouds.Add(new Cloud(new Bounds2(rand.Next(- GrassBiome.LEVELONEWIDTH / 2, GrassBiome.LEVELONEWIDTH / 2), rand.Next(0, 100), rand.Next(50, 100), rand.Next(50, 100)), rand.Next(0, 10) - 5));
        }
        Console.WriteLine(Clouds[0].GetBounds());
    }

    public void Update(float offset)
    {
        foreach(Cloud c in Clouds)
        {
            c.Update(offset);
        }
    }
}
