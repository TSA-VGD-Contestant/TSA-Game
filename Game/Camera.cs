using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Camera
{
    private Vector2 offset;
    public Camera()
    {

    }

    public Vector2 GetOffset()
    {
        return offset;
    }

    public void SetOffset(Vector2 offset)
    {
        this.offset = offset;
    }
}

