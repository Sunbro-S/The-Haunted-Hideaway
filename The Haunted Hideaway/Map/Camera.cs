using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace The_Haunted_Hideaway.Map;

public class Camera
{
    private Vector2 _cameraPosition;
    private Vector2 GetMovementDirection()
    {
        var movementDirection = Vector2.Zero;
        var state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.Down))
        {
            movementDirection += Vector2.UnitY;
        }
        if (state.IsKeyDown(Keys.Up))
        {
            movementDirection -= Vector2.UnitY;
        }
        if (state.IsKeyDown(Keys.Left))
        {
            movementDirection -= Vector2.UnitX;
        }
        if (state.IsKeyDown(Keys.Right))
        {
            movementDirection += Vector2.UnitX;
        }
    
        // Can't normalize the zero vector so test for it before normalizing
        if (movementDirection != Vector2.Zero)
        {
            movementDirection.Normalize(); 
        }
    
        return movementDirection;
    }

    private void MoveCamera(GameTime gameTime)
    {
        var speed = 200;
        var seconds = gameTime.GetElapsedSeconds();
        var movementDirection = GetMovementDirection();
        _cameraPosition += speed * movementDirection * seconds;
    }
}