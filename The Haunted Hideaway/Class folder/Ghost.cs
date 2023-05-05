using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Haunted_Hideaway;

public class Ghost
{
    private Texture2D texture;
    private Vector2 position;
    private Vector2 direction;
    private float speed;
    private float radius;
    private int damage;
    
    public Ghost(Texture2D texture, Vector2 position, float speed, float radius, int damage)
    {
        this.texture = texture;
        this.position = position;
        this.speed = speed;
        this.radius = radius;
        this.damage = damage;
    }
    
    public void Update(GameTime gameTime, Vector2 playerPosition)
    {
        var directionToPlayer = playerPosition - position;
        var distanceToPlayer = directionToPlayer.Length();
        if (distanceToPlayer < radius && !Hero.HideInShadow())
        {
            direction = Vector2.Normalize(directionToPlayer);
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        if (IsIntersect(playerPosition))
        {
            
        }
    }

    public bool IsIntersect(Vector2 playerPosition )
    {
        var ghost = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        var player = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 30, 30);
        return ghost.Intersects(player);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}