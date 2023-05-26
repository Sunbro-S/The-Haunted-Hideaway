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
     private readonly int speed = 2; 
     public Camera Camera { get; set; }
     private static int _health;
     
     private AnimationManager animationManager = new AnimationManager();
     private Animation animationDown;
     private Animation animationUp;
     private Animation animationLeft;
     private Animation animationRight;

     public Hero(Texture2D down, Texture2D up,Texture2D left,Texture2D right,Rectangle rectangle, int health)
     {
          Texture = down;
          this.rectangle = rectangle;
          _health = health;
          animationDown = new Animation(down, 4, 0.2f);
          animationUp = new Animation(up, 4, 0.2f);
          animationRight = new Animation(right, 4, 0.2f);
          animationLeft = new Animation(left, 4, 0.2f);
     }

     public Vector2 position() => new Vector2(rectangle.X, rectangle.Y);



     public void Draw(SpriteBatch spriteBatch)
     {
          animationManager.Draw(spriteBatch, position());
     }

     public void Update(GameTime gameTime)
     {
          
     }
     
     
     public void MoveDirection( Map.Map map)
     {
          var newPosition = rectangle.Location;

          switch (Direction)
          {
               case Direction.Left:
                    
                    newPosition.X -= speed;
                    animationManager.PlayAnimation(animationLeft);
                    break;
               case Direction.Right:
                    
                    newPosition.X += speed;
                    animationManager.PlayAnimation(animationRight);
                    break;
               case Direction.Up:
                    
                    newPosition.Y -= speed;
                    animationManager.PlayAnimation(animationUp);
                    break;
               case Direction.Down:
                    
                    newPosition.Y += speed;
                    animationManager.PlayAnimation(animationDown);
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

     public void Move(Map.Map map,GameTime gameTime)
     {
          var isMoving = false;
          
          if (Keyboard.GetState().IsKeyDown(Keys.A))
          {
               Direction = Direction.Left;
               isMoving = true;
          }
          else if (Keyboard.GetState().IsKeyDown(Keys.D))
          {
               Direction = Direction.Right;
               isMoving = true;
          }

          if (Keyboard.GetState().IsKeyDown(Keys.W))
          {
               Direction = Direction.Up;
               isMoving = true;
          }
          else if (Keyboard.GetState().IsKeyDown(Keys.S))
          {
               Direction = Direction.Down;
               isMoving = true;
          }

          
          if (isMoving)
               MoveDirection(map);
          
          else
               animationManager.Stop();

          animationManager.Update(gameTime);
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