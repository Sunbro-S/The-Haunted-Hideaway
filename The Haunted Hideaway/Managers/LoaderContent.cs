using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using The_Haunted_Hideaway.Map;

namespace The_Haunted_Hideaway;


public class LoaderContent
{
    enum GameState
    {
        MainMenu,
        Map,
        Tutorial,
        Fin
    }

    private static GameState currentState = GameState.MainMenu;
    private static CButton buttonPlay;
    private static CButton buttonExit;
    private static CButton buttonTutorial;
    private static CButton buttonBack;
    private static Hero hero;
    private static Container container;
    private static Ghost ghost;
    private static Ghost ghost2;
    private static Ghost ghost3;
    private static List<Ghost> ghosts;
    private static Texture2D player;
    private static Camera Camera;
    private static Jumpscare jumpscareEffect;

    public static Viewport Viewport;
    public static Map.Map Map { get; set; }
    
    public static void Initialize()
    {
        buttonPlay = new CButton(LoadTexture("PlayButton"));
        buttonTutorial = new CButton(LoadTexture("PlayButton"));
        buttonExit = new CButton(LoadTexture("PlayButton"));
        buttonPlay.SetPosition(new Vector2(300,350));
        buttonTutorial.SetPosition(new Vector2(Globals.Graphics.PreferredBackBufferWidth/2,650));
        buttonPlay.SetPosition(new Vector2(Globals.Graphics.PreferredBackBufferWidth/2,800));
    }

    public static void LoadContent()
    {
        SplashScreen.Background = Globals.Content.Load<Texture2D>("background");
        hero = new Hero(LoadTexture("heroDown"),LoadTexture("heroUp"),LoadTexture("heroLeft"), LoadTexture("heroRight"),
            Globals.Content.Load<SoundEffect>("stepSound"),
            new Rectangle(128,257,50,50),100);
        ghost = new Ghost(LoadTexture("ghostDown"),LoadTexture("ghostUp"),LoadTexture("ghostLeft"),LoadTexture("ghostRight"),
            Globals.Content.Load<SoundEffect>("screamer"), 
            new Vector2(1600, 256), 100, 300, 30, new List<Vector2>(){new Vector2(1600,1500), new Vector2(1600, 256)});
        ghost2 = new Ghost(LoadTexture("ghostDown"),LoadTexture("ghostUp"),LoadTexture("ghostLeft"),LoadTexture("ghostRight"),
            Globals.Content.Load<SoundEffect>("screamer"), 
            new Vector2(2368, 1090), 100, 300, 30, new List<Vector2>(){new Vector2(2368, 1090)});
        ghost3 = new Ghost(LoadTexture("ghostDown"),LoadTexture("ghostUp"),LoadTexture("ghostLeft"),LoadTexture("ghostRight"),
            Globals.Content.Load<SoundEffect>("screamer"), 
            new Vector2(3648, 1530), 100, 300, 30, new List<Vector2>(){new Vector2(1500,1530), new Vector2(3648, 1530)});
        ghosts = new List<Ghost>();
        ghosts.Add(ghost);
        ghosts.Add(ghost2);
        ghosts.Add(ghost3);
        Camera = new Camera(Viewport, hero);
        hero.Camera = Camera;
        jumpscareEffect = new Jumpscare(Globals.Graphics.GraphicsDevice, 0.5f, 0.8f, Viewport, Camera);
        Map.GetFirstMap();
        
    }

    private static Texture2D LoadTexture(string name) => Globals.Content.Load<Texture2D>(name);
    
    public static void Update(GameTime gameTime)
    {
        var mouse = Mouse.GetState();
        switch (currentState)
        {
            case GameState.MainMenu:
                if (buttonPlay.isClicked) currentState = GameState.Map;
                buttonPlay.Update(mouse);
                break;
            case GameState.Map:
                hero.Move( Map,gameTime);
                hero.Update(gameTime);
                jumpscareEffect.Update(gameTime, Camera.GetTransformMatrix());
                GhostsManager.Update(ghosts,gameTime,hero);
                break;
                
        }
        
        
    }

    public static void Draw()
    {
        Globals.SpriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend,
            null,null,null,null,
            Camera.Transform);
        switch (currentState)
        {
            case GameState.MainMenu:
                Globals.SpriteBatch.Draw(LoadTexture("background"), new Rectangle(0, 0, 1280, 720), Color.White);
                buttonPlay.Draw(Globals.SpriteBatch);
                buttonTutorial.Draw(Globals.SpriteBatch);
                buttonExit.Draw(Globals.SpriteBatch);
                break;
            case GameState.Map:
                Map.Draw(Globals.SpriteBatch);
                hero.Draw(Globals.SpriteBatch);
                GhostsManager.Draw(ghosts,hero,Globals.SpriteBatch);
                jumpscareEffect.Draw(Globals.SpriteBatch);
                break;
                
        }
        
        
        
        Globals.SpriteBatch.End();
    }

   
}