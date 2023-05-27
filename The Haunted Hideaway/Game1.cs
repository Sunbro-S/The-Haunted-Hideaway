using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using The_Haunted_Hideaway.Map;

namespace The_Haunted_Hideaway;


public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Container container;
    private Map.Map map;


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
        Globals.Content = Content;
        Globals.Window = Window;
        Globals.Graphics = graphics;
        Globals.Container = container;
        Tiles.Content = Content;
        map = new Map.Map();
        LoaderContent.Map = map;
        LoaderContent.Viewport = GraphicsDevice.Viewport;
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.SpriteBatch = spriteBatch;
        LoaderContent.LoadContent();
        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
       LoaderContent.Update(gameTime);
       if(LoaderContent.HeroTouchedExit())
           Exit();
       base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);
        LoaderContent.Draw();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}