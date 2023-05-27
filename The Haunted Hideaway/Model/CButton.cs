using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Haunted_Hideaway;

public class CButton
{
    private Texture2D _texture2D;
    private Vector2 position;
    private Rectangle rectangle;

    private Color Color = new Color(255, 255, 255, 255);
    public Vector2 size;

    public CButton(Texture2D newTexture)
    {
        _texture2D = newTexture;
        size = new Vector2(Globals.Graphics.GraphicsDevice.Viewport.Width / 8,
            Globals.Graphics.GraphicsDevice.Viewport.Width / 30);
    }

    private bool down;
    public bool isClicked;

    public void Update(MouseState mouse)
    {
        rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        var mouseRectangle = new Rectangle(mouse.X, mouse.Y,1,1);
        if (mouseRectangle.Intersects(rectangle))
        {
            if (Color.A == 255) down = false;
            if (Color.A == 0) down = false;
            if (down) Color.A += 3;
            else Color.A -= 3;
            if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
        }
        else if (Color.A < 255)
        {
            Color.A += 3;
            isClicked = false;
        }
    }

    public void SetPosition(Vector2 newPosition) => position = newPosition;
    

    public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture2D,rectangle,Color);
}