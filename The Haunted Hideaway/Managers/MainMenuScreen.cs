using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;

public abstract class MainMenuScreen : GameScreen
{
    private SpriteFont font;
    private Texture2D backgroundTexture;
    private Rectangle startButtonRect;
    private Rectangle quitButtonRect;

    public MainMenuScreen(Game game) : base(game)
    {
    }

    public override void LoadContent()
    {
        SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
        font = Content.Load<SpriteFont>("Font"); // Замените "Font" на имя вашего шрифта
        backgroundTexture = Content.Load<Texture2D>("Background"); // Замените "Background" на имя вашей текстуры заднего фона

        int screenWidth = GraphicsDevice.Viewport.Width;
        int screenHeight = GraphicsDevice.Viewport.Height;

        int buttonWidth = 200;
        int buttonHeight = 50;
        int buttonSpacing = 20;
        int buttonX = (screenWidth - buttonWidth) / 2;
        int buttonY = (screenHeight - (2 * buttonHeight + buttonSpacing)) / 2;

        startButtonRect = new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight);
        quitButtonRect = new Rectangle(buttonX, buttonY + buttonHeight + buttonSpacing, buttonWidth, buttonHeight);
    }

    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            Game.Exit();
        }

        MouseState mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            if (startButtonRect.Contains(mousePosition))
            {
                // Обработка нажатия кнопки "Start"
                // Здесь вы можете запустить новую игру или переключиться на другой экран
            }
            else if (quitButtonRect.Contains(mousePosition))
            {
                // Обработка нажатия кнопки "Quit"
                Game.Exit();
            }
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
        spriteBatch.DrawString(font, "Main Menu", new Vector2(50, 50), Color.White);
        spriteBatch.DrawRectangle(startButtonRect, Color.White);
        spriteBatch.DrawRectangle(quitButtonRect, Color.White);
        spriteBatch.DrawString(font, "Start", new Vector2(startButtonRect.X + 50, startButtonRect.Y + 10), Color.White);
        spriteBatch.DrawString(font, "Quit", new Vector2(quitButtonRect.X + 50, quitButtonRect.Y + 10), Color.White);
        spriteBatch.End();
    }
}