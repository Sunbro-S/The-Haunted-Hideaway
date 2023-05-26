using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace The_Haunted_Hideaway;

public class InputManager
{
    private static Vector2 direction;
    public static Vector2 Direction => direction;
    public static bool Moving => direction != Vector2.Zero;

    public void Update()
    {
        direction = Vector2.Zero;
        var keyboardState = Keyboard.GetState();

        if (keyboardState.GetPressedKeyCount() > 0)
        {
            if (keyboardState.IsKeyDown(Keys.A)) direction.X--;
            if (keyboardState.IsKeyDown(Keys.D)) direction.X++;
            if (keyboardState.IsKeyDown(Keys.W)) direction.Y--;
            if (keyboardState.IsKeyDown(Keys.S)) direction.Y++;
        }
    }
}