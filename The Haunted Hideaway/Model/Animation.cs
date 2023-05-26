using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Haunted_Hideaway;

public class Animation
{
    private Texture2D texture;
    private int frameCount;
    private float frameDuration;
    private int currentFrame;
    private float timer;
    private Rectangle sourceRect;

    public Animation(Texture2D texture, int frameCount, float frameDuration)
    {
        this.texture = texture;
        this.frameCount = frameCount;
        this.frameDuration = frameDuration;
        currentFrame = 0;
        timer = 1f;
        sourceRect = new Rectangle();
    }
    
    public void Reset()
    {
        currentFrame = 0;
        timer = 0f;
    }

    public void Update(GameTime gameTime)
    {
        timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timer >= frameDuration)
        {
            // Переход к следующему кадру
            currentFrame++;
            if (currentFrame >= frameCount)
            {
                currentFrame = 0; // Зацикливание анимации
            }

            // Обновление прямоугольника источника для текущего кадра
            int frameWidth = texture.Width / frameCount;
            sourceRect = new Rectangle(frameWidth * currentFrame, 0, frameWidth, texture.Height);

            timer = 0f; // Сброс таймера
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
        spriteBatch.Draw(texture, position, sourceRect, Color.White);
    }
}