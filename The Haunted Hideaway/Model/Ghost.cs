using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Haunted_Hideaway;

public class Ghost
{
    private Texture2D Down;
    private Texture2D Up;
    private Texture2D Left;
    private Texture2D Right;
    private Vector2 position;
    private Vector2 direction;
    private float speed;
    private float radius;
    private int damage;
    
    public Ghost(Texture2D down,Texture2D up, Texture2D left,Texture2D right,Vector2 position, float speed, float radius, int damage)
    {
        Down = down;
        Up = up;
        Left = left;
        Right = right;
        this.position = position;
        this.speed = speed;
        this.radius = radius;
        this.damage = damage;
    }
    
    public void Update(GameTime gameTime, Hero hero)
    {
        var directionToPlayer = hero.position() - position;
        var distanceToPlayer = directionToPlayer.Length();
        if (distanceToPlayer < radius && !Hero.HideInShadow())
        {
            direction = Vector2.Normalize(directionToPlayer);
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (IsIntersect(hero.position()))
        {
            hero.TakeDamage(25);
        }
    }

    public bool IsIntersect(Vector2 playerPosition )
    {
        var ghost = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        var player = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 50, 50);
        return ghost.Intersects(player);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Down, position, Color.White);
        if(direction.X!=0 && direction.Y!=0)
        {
            if (direction.X > 0 && direction.Y > direction.X)
                spriteBatch.Draw(Right, position, Color.White);
            else if (direction.X < 0 && direction.Y > direction.X)
                spriteBatch.Draw(Left, position, Color.White);
            else if (direction.Y < 0)
                spriteBatch.Draw(Up, position, Color.White);
            else if (direction.Y > 0)
                spriteBatch.Draw(Down, position, Color.White);
        }
    }
}