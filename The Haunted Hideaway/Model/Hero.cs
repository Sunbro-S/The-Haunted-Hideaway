using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Haunted_Hideaway.Map;

namespace The_Haunted_Hideaway;

public class Hero
{
     private Texture2D Texture { get; }
     private static Direction Direction { get; set; }
     private Rectangle rectangle;
     public Vector2 Velocity;
     public Camera Camera { get; set; }
     private static int _health;

     public Hero(Texture2D texture, Rectangle rectangle, int health)
     {
          Texture = texture;
          this.rectangle = rectangle;
          _health = health;
     }

     public Vector2 position()
     {
          return new Vector2(rectangle.X, rectangle.Y);
     }
     


     public void Draw(SpriteBatch spriteBatch)
     {
          spriteBatch.Draw(Texture, rectangle, Color.White);
     }

     public void Update(GameTime gameTime)
     {
          
     }
     
     
     public void MoveDirection(int speed, Map.Map map)
     {
          var newPosition = rectangle.Location;

          switch (Direction)
          {
               case Direction.Left:
                    newPosition.X -= speed;
                    break;
               case Direction.Right:
                    newPosition.X += speed;
                    break;
               case Direction.Up:
                    newPosition.Y -= speed;
                    break;
               case Direction.Down:
                    newPosition.Y += speed;
                    break;
          }
          
          var tempRectangle = new Rectangle(newPosition, rectangle.Size);
          var collisionDetected = false;

          foreach (var tile in map.CollisionTilesList)
          {
               Camera.Update();
               if (tempRectangle.Intersects(tile.Rectangle) && tile.Type != 1)
               {
                    collisionDetected = true;
                    break;
               }
               
          }
          if (!collisionDetected)
               rectangle.Location = newPosition;
          
     }

     public void Move(int speed, Map.Map map)
     {
          if (Keyboard.GetState().IsKeyDown(Keys.A))
               {
                    Direction = Direction.Left;
                    Velocity = new Vector2(-speed, Velocity.Y);
                    MoveDirection(2, map);
               }

               if (Keyboard.GetState().IsKeyDown(Keys.D))
               {
                    Direction = Direction.Right;
                    Velocity = new Vector2(speed, Velocity.Y);
                    MoveDirection(2, map);
               }

               if (Keyboard.GetState().IsKeyDown(Keys.W))
               {
                    Direction = Direction.Up;
                    Velocity = new Vector2(Velocity.X, -speed);
                    MoveDirection(2, map);
               }

               if (Keyboard.GetState().IsKeyDown(Keys.S))
               {
                    Direction = Direction.Down;
                    Velocity = new Vector2(Velocity.X, speed);
                    MoveDirection(2, map);
               }
               else Velocity = Vector2.Zero;
     }

     public bool InBounds(int width, int height)
     {
          if (rectangle.X > 0 && rectangle.X < width && rectangle.Y > 0 && rectangle.Y < height)
               return true;
          return false;
     }
     
     public void TakeDamage(int amount)
     {
          _health -= amount;
          var flag = true;
          if (_health <= 0)
          {
               //TODO: Что то после смерти
          }
     }

     public static bool HideInShadow()
     {
          if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
          {
               return true;
          }

          return false;
     }

     public static void RegenerateHealth()
     {
          _health += 25;
     }
}