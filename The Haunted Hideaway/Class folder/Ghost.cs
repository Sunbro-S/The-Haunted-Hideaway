using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Haunted_Hideaway;

public class Ghost
{
    // Fields
    private Texture2D texture;
    private Vector2 position;
    private Vector2 direction;
    private float speed;
    private float radius;
    private int damage;

    // Constructor
    public Ghost(Texture2D texture, Vector2 position, float speed, float radius, int damage)
    {
        this.texture = texture;
        this.position = position;
        this.speed = speed;
        this.radius = radius;
        this.damage = damage;
    }

    // Update the ghost position
    public void Update(GameTime gameTime, Vector2 playerPosition)
    {
        var directionToPlayer = playerPosition - position;
        var distanceToPlayer = directionToPlayer.Length();
        var ghostRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        var playerRect = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 30, 30);
        if (distanceToPlayer < radius)
        {
            direction = Vector2.Normalize(directionToPlayer);
            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (ghostRect.Intersects(playerRect))
        {
            // Inflict damage to the player
            Hero.TakeDamage(damage);

            // Destroy the ghost
            Dispose(ghostRect);
        }
    }

    public void Dispose(Ghost ghost)
    {
        _ghosts.Remove(ghost);
    }

    // Draw the ghost
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(texture, position, Color.White);
    }
}