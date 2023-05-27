using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Haunted_Hideaway;

public class ExitLab
{
    private Texture2D _texture2D;
    private Rectangle _rectangle;

    public ExitLab(Texture2D texture2D, Rectangle rectangle)
    {
        _rectangle = rectangle;
        _texture2D = texture2D;
    }

    public Rectangle Rectangle => _rectangle;

    public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture2D, _rectangle, Color.White);
}