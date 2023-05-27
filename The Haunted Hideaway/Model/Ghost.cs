using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Haunted_Hideaway;

public class Ghost
{
    private Texture2D Down;
    private Texture2D Up;
    private Texture2D Left;
    private Texture2D Right;
    private SoundEffect screamer;
    private SoundEffectInstance screamerInstance;
    private Vector2 position;
    private Vector2 direction;
    private float speed;
    private float radius;
    private int damage;
    private Vector2 originalPosition;
    private List<Vector2> patrolRoute;
    private int currentPatrolIndex;
    
    public Ghost(Texture2D down, Texture2D up, Texture2D left, Texture2D right, SoundEffect scream, Vector2 position, float speed, float radius, int damage, List<Vector2> patrolRoute)
    {
        Down = down;
        Up = up;
        Left = left;
        Right = right;
        screamer = scream;
        screamerInstance = screamer.CreateInstance();
        screamerInstance.Volume = 0.5f;
        this.position = position;
        this.speed = speed;
        this.radius = radius;
        this.damage = damage;
        this.patrolRoute = patrolRoute;
        this.originalPosition = position;
        this.currentPatrolIndex = 0;
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
        else
        {
            if (position != patrolRoute[currentPatrolIndex])
            {
                var delta = 10f;

                if (Vector2.Distance(position, patrolRoute[currentPatrolIndex]) <= delta)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolRoute.Count;
                }

                var targetPosition = patrolRoute[currentPatrolIndex];
                direction = Vector2.Normalize(targetPosition - position);
                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        if (IsIntersect(hero.position()))
        {
            hero.TakeDamage(25);
            screamerInstance.Play();
            Jumpscare.Activate();
        }
    }

    public bool IsIntersect(Vector2 playerPosition)
    {
        var ghost = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        var player = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 50, 50);
        return ghost.Intersects(player);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Down, position, Color.White);
        
        if (direction.X != 0 && direction.Y != 0)
        {
            if (direction.X > 0 && Math.Abs(direction.Y) < Math.Abs(direction.X))
                spriteBatch.Draw(Right, position, Color.White);
            else if (direction.X < 0 && Math.Abs(direction.Y) < Math.Abs(direction.X))
                spriteBatch.Draw(Left, position, Color.White);
            else if (direction.Y < 0 && Math.Abs(direction.X) < Math.Abs(direction.Y))
                spriteBatch.Draw(Up, position, Color.White);
            else if (direction.Y > 0 && Math.Abs(direction.X) < Math.Abs(direction.Y))
                spriteBatch.Draw(Down, position, Color.White);
        }
    }
}
