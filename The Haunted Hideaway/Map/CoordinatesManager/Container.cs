using SharpDX.Direct3D9;

namespace The_Haunted_Hideaway;

public class Container
{
    public Line Width { get; }
    public Line Height { get; }

    public Container(Line width, Line height)
    {
        Width = width;
        Height = height;
    }
}