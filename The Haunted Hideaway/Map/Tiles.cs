using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace The_Haunted_Hideaway;

public class Tiles
{
    protected Texture2D texture;
    public int Type { get; protected set; }
    public Rectangle Rectangle { get; protected set; }
    public static ContentManager Content { get; set; }

    public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(texture, Rectangle, Color.White);
}

public class CollisionTiles : Tiles
{
    public CollisionTiles(int i, Rectangle newRectangle)
    {
        texture = Content.Load<Texture2D>("Tile" + i);
        this.Rectangle = newRectangle;
        Type = i;
    }
}