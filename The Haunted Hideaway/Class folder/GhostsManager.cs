using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Haunted_Hideaway;

public class GhostsManager
{
    public static void Draw(List<Ghost> ghosts, Hero hero, SpriteBatch spriteBatch)
    {
        var ghostsToRemove = new List<Ghost>();
        foreach (var ghost in ghosts)
        {
            if (!ghost.IsIntersect(hero.position()))
            {
                ghost.Draw(spriteBatch);
            }
            else
            {
                ghostsToRemove.Add(ghost);
            }
        }
        foreach (var ghostToRemove in ghostsToRemove)
        {
            ghosts.Remove(ghostToRemove);
        }
    }

    public static void Update(List<Ghost> ghosts, GameTime gameTime, Vector2 playerPosition)
    {
        foreach (var ghost in ghosts)
        {
            ghost.Update(gameTime,playerPosition);
        }
    }
}