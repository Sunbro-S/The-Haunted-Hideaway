using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Haunted_Hideaway;

public class LoaderContent
{
    
    public static void Update(GameTime gameTime, Hero hero, List<Ghost> ghosts, GameState state)
    {
        hero.Move(2);
        GhostsManager.Update(ghosts,gameTime,hero);
        switch (state)
        {
            case GameState.Menu:
                SplashScreen.Update();
                if (Keyboard.GetState().IsKeyDown(Keys.Enter)) state = GameState.Game;
                break;
            case GameState.Game:
                SplashScreen.Update();
                if (Keyboard.GetState().IsKeyDown(Keys.Escape)) state = GameState.Game;
                if (Keyboard.GetState().IsKeyDown(Keys.P)) state = GameState.Pause;
                break;
            case GameState.Pause:
                if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.P))
                    state = GameState.Game;
                break;
        }
    }

    public static void Draw(SpriteBatch spriteBatch, Hero hero, List<Ghost> ghosts)
    {
        SplashScreen.Draw(spriteBatch);
        hero.Draw(spriteBatch);
        GhostsManager.Draw(ghosts,hero,spriteBatch);
    }
}