using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace The_Haunted_Hideaway.Map;

public class Camera
{
    public Matrix Transform { get; set; }
    private Viewport Viewport;
    private Hero player;

    public Camera(Viewport viewport, Hero player)
    {
        Viewport = viewport;
        this.player = player;
    }

    public void Update()
    {
        var position = player.position();

        var cameraPosition = new Vector2(Viewport.Width / 2, Viewport.Height / 2);
        var playerOffset = position - cameraPosition;

        Transform = Matrix.CreateTranslation(-playerOffset.X, -playerOffset.Y, 0);
    }
    
    public Matrix GetTransformMatrix() => Matrix.CreateTranslation(-player.position().X, -player.position().Y, 0);

}