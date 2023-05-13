using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace The_Haunted_Hideaway;

public enum GameState
{   
    Menu,
    Game,
    Pause
}
public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Texture2D player;
    private GameState state = GameState.Menu;
    private Hero hero;
    private Container container;
    private Ghost ghost;
    private List<Ghost> ghosts;
    

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;
        container = new Container(new Line(10, graphics.PreferredBackBufferWidth - 10),
            new Line(10, graphics.PreferredBackBufferHeight - 10));
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        SplashScreen.Background = Content.Load<Texture2D>("background");
        hero = new Hero(Content.Load<Texture2D>("playerDemo"), new Rectangle(30,container.Height.X2/2,30,30),100);
        ghost = new Ghost(Content.Load<Texture2D>("ghost"),
        new Vector2(graphics.PreferredBackBufferWidth - 100, container.Height.X2 / 2), 50, 300, 30);
        ghosts = new List<Ghost>();
        ghosts.Add(ghost);
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
       LoaderContent.Update(gameTime, hero, ghosts, state);
        // TODO: Add your update logic here
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin();
        LoaderContent.Draw(spriteBatch,hero,ghosts);
        spriteBatch.End(); 

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}