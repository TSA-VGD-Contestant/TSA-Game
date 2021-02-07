using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

abstract class Screen
{
    public Screen()
    {

    }

    /// <summary>
    /// Update the screen every frame.
    /// </summary>
    public abstract void Update();
}