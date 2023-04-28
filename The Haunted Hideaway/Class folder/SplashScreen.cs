using System.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Haunted_Hideaway;

public class SplashScreen
{
    public static Texture2D Background { get; set; }
    static Color color;
    private static int counter = 0;

     public static void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Background, Vector2.Zero, color);
    }

    public static void Update()
    {
        color = Color.FromNonPremultiplied(255, 255, 255, 255);
        counter++;
    }
}
