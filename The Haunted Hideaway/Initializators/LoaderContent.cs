using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Haunted_Hideaway;

public class LoaderContent
{
    private static Hero hero;
    private static Container container;
    private static Ghost ghost;
    private static List<Ghost> ghosts;
    private static Texture2D player;

    public static void LoadContent()
    {
        SplashScreen.Background = Globals.Content.Load<Texture2D>("background");
        hero = new Hero(Globals.Content.Load<Texture2D>("playerDemo"), new Rectangle(30,Globals.Container.Height.X2/2,30,30),100);
        ghost = new Ghost(Globals.Content.Load<Texture2D>("ghost"),
            new Vector2(Globals.Graphics.PreferredBackBufferWidth - 100, Globals.Container.Height.X2 / 2), 50, 300, 30);
        ghosts = new List<Ghost>();
        ghosts.Add(ghost);
    }
    public static void Update(GameTime gameTime, GameState state)
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

    public static void Draw()
    {
        Globals.SpriteBatch.Begin();
        SplashScreen.Draw(Globals.SpriteBatch);
        hero.Draw(Globals.SpriteBatch);
        GhostsManager.Draw(ghosts,hero,Globals.SpriteBatch);
        Globals.SpriteBatch.End();
    }
}