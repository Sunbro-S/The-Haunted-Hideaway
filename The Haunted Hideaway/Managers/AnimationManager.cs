using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Haunted_Hideaway;

public class AnimationManager
{
    private Animation currentAnimation;
    private bool isPlaying;

    public void PlayAnimation(Animation animation)
    {
        currentAnimation = animation;
    }

    public void Stop()
    {
        if (currentAnimation != null)
            currentAnimation.Reset();
    }

    public void Update(GameTime gameTime)
    {
        if (currentAnimation != null)
            currentAnimation.Update(gameTime);
        
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        if (currentAnimation != null)
            currentAnimation.Draw(spriteBatch, position);
        
    }
    
}