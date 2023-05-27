using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Haunted_Hideaway.Map;

namespace The_Haunted_Hideaway;

public class Hero
{
     private Texture2D Texture { get; }
     private static Direction Direction { get; set; }
     private Rectangle rectangle;
     private readonly int speed = 5; 
     
     public Camera Camera { get; set; }
     private static int _health;
     
     private AnimationManager animationManager = new AnimationManager();
     private Vector2 Velocity;
     private Animation animationDown;
     private Animation animationUp;
     private Animation animationLeft;
     private Animation animationRight;
     private SoundEffect stepSound;
     private SoundEffectInstance stepSoundInstance;
     private bool isMoving = false;

     public Hero(Texture2D down, Texture2D up,Texture2D left,Texture2D right,SoundEffect step,Rectangle rectangle, int health)
     {
          Texture = down;
          this.rectangle = rectangle;
          _health = health;
          stepSound = step;
          stepSoundInstance = stepSound.CreateInstance();
          stepSoundInstance.IsLooped = true;
          animationDown = new Animation(down, 4, 0.2f);
          animationUp = new Animation(up, 4, 0.2f);
          animationRight = new Animation(right, 4, 0.2f);
          animationLeft = new Animation(left, 4, 0.2f);
     }

     public Vector2 position() => new Vector2(rectangle.X, rectangle.Y);



     public void Draw(SpriteBatch spriteBatch)
     {
          animationManager.Draw(spriteBatch, position());
          if (isMoving)
          {
               if (Velocity.X > 0 && Math.Abs(Velocity.Y) < Math.Abs(Velocity.X))
                    animationManager.PlayAnimation(animationRight);
               else if (Velocity.X < 0 && Math.Abs(Velocity.Y) < Math.Abs(Velocity.X))
                    animationManager.PlayAnimation(animationLeft);
               else if (Velocity.Y < 0 && Math.Abs(Velocity.X) < Math.Abs(Velocity.Y))
                    animationManager.PlayAnimation(animationUp);
               else if (Velocity.Y > 0 && Math.Abs(Velocity.X) < Math.Abs(Velocity.Y))
                    animationManager.PlayAnimation(animationDown);
          }
         
     }

     public void Update(GameTime gameTime)
     {
          
     }
     
     
     public void MoveDirection( Map.Map map)
     {
          var newPosition = rectangle.Location;
          Velocity = new Vector2(0,0);

          switch (Direction)
          {
               case Direction.Left:
                    
                    newPosition.X -= speed;
                    Velocity = new Vector2(-speed, Velocity.Y);
                    break;
               case Direction.Right:
                    newPosition.X += speed;
                    Velocity = new Vector2(speed, Velocity.Y);
                    break;
               case Direction.Up:
                    newPosition.Y -= speed;
                    Velocity = new Vector2(Velocity.X, -speed);
                    break;
               case Direction.Down:
                    newPosition.Y += speed;
                    Velocity = new Vector2(Velocity.X, speed);
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
         isMoving = false;
          
          if (Keyboard.GetState().IsKeyDown(Keys.A))
          {
               Direction = Direction.Left;
               MoveDirection(map);
               isMoving = true;
          }
          else if (Keyboard.GetState().IsKeyDown(Keys.D))
          {
               Direction = Direction.Right;
               MoveDirection(map);
               isMoving = true;
          }

          if (Keyboard.GetState().IsKeyDown(Keys.W))
          {
               Direction = Direction.Up;
               isMoving = true;
               MoveDirection(map);
          }
          else if (Keyboard.GetState().IsKeyDown(Keys.S))
          {
               Direction = Direction.Down;
               MoveDirection(map);
               isMoving = true;
          }

          
          if (isMoving)
          {
               
               stepSoundInstance.Play();
          }
          
          else
          {
               animationManager.Stop();
               stepSoundInstance.Stop();
          }

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