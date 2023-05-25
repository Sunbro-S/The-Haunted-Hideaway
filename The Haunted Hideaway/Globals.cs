using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace The_Haunted_Hideaway;

public class Globals
{
    public static ContentManager Content { get; set; }
    public static SpriteBatch SpriteBatch { get; set; }
    public static GameWindow Window { get; set; }
    public static GraphicsDeviceManager Graphics { get; set; }
    public static Container Container { get; set; }
    
}