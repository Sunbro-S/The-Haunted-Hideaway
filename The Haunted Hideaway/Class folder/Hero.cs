using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Haunted_Hideaway;

public class Hero
{
     private Texture2D Texture { get; }
     private static Direction Direction { get; set; }
     private Rectangle _rectangle;
     private Rectangle Rectangle { get; set; }
     private static int _health;

     public Hero(Texture2D texture, Rectangle rectangle, int health)
     {
          Texture = texture;
          _rectangle = rectangle;
          _health = health;
     }

     public Vector2 position()
     {
          return new Vector2(_rectangle.X, _rectangle.Y);
     }

     public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
     {
          spriteBatch.Draw(Texture, _rectangle, Color.White);
     }

     public void MoveDirection(int speed)
     {
          switch (Direction)
          {
               case Direction.Left:
                    _rectangle.X -= speed;
                    break;
               case Direction.Right:
                    _rectangle.X += speed;
                    break;
               case Direction.Up:
                    _rectangle.Y -= speed;
                    break;
               case Direction.Down:
                    _rectangle.Y += speed;
                    break;
          }
     }

     public void Move(int speed)
     {
          if (Keyboard.GetState().IsKeyDown(Keys.A))
               {
                    Direction = Direction.Left;
                    MoveDirection(2);
               }

               if (Keyboard.GetState().IsKeyDown(Keys.D))
               {
                    Direction = Direction.Right;
                    MoveDirection(2);
               }

               if (Keyboard.GetState().IsKeyDown(Keys.W))
               {
                    Direction = Direction.Up;
                    MoveDirection(2);
               }

               if (Keyboard.GetState().IsKeyDown(Keys.S))
               {
                    Direction = Direction.Down;
                    MoveDirection(2);
               }
     }

     public bool InBounds(int width, int height)
     {
          if (_rectangle.X > 0 && _rectangle.X < width && _rectangle.Y > 0 && _rectangle.Y < height)
               return true;
          return false;
     }
     
     public static void TakeDamage(int amount)
     {
          _health -= amount;

          if (_health <= 0)
          {
               // Player is dead
               // TODO: Add code to handle player death
          }
     }
}