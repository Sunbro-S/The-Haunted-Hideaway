using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using The_Haunted_Hideaway.Map;

namespace The_Haunted_Hideaway;

public class LoaderContent
{
    private static Hero hero;
    private static Container container;
    private static Ghost ghost;
    private static List<Ghost> ghosts;
    private static Texture2D player;
    private static Camera Camera;

    public static Viewport Viewport;
    public static Map.Map Map { get; set; }

    public static void LoadContent()
    {
        SplashScreen.Background = Globals.Content.Load<Texture2D>("background");
        hero = new Hero(LoadTexture("heroDown"),LoadTexture("heroUp"),LoadTexture("heroLeft"), LoadTexture("heroRight"),
            new Rectangle(128,257,50,50),100);
        ghost = new Ghost(LoadTexture("ghostDown"),LoadTexture("ghostUp"),LoadTexture("ghostLeft"),LoadTexture("ghostRight"),
            new Vector2(Globals.Graphics.PreferredBackBufferWidth - 100, Globals.Container.Height.X2 / 2), 100, 300, 30);
        ghosts = new List<Ghost>();
        ghosts.Add(ghost);
        Camera = new Camera(Viewport, hero);
        hero.Camera = Camera;
        Map.Generate(new int[,]
        {
            {7,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,9},
            {6,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,10},
            {5,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,14,11},
            {4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,12},
            {4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,12},
            {4,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,12},
            {3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,17,1,1,1,1,16,2,13},
            
        },64);
    }

    private static Texture2D LoadTexture(string name) => Globals.Content.Load<Texture2D>(name);
    
    public static void Update(GameTime gameTime, GameState state)
    {
        hero.Move( Map,gameTime);
        hero.Update(gameTime);

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
        Globals.SpriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,
            null,null,null,null,
            Camera.Transform);
        Map.Draw(Globals.SpriteBatch);
        hero.Draw(Globals.SpriteBatch);
        GhostsManager.Draw(ghosts,hero,Globals.SpriteBatch);
        Globals.SpriteBatch.End();
    }
}