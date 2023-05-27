using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using The_Haunted_Hideaway.Map;

namespace The_Haunted_Hideaway;

public class Jumpscare
{
    private Rectangle jumpscareRectangle;
    private Color jumpscareColor;
    private float jumpscareDuration;
    private static float jumpscareTimer;
    private static float jumpscareAlpha;
    private float jumpscareMaxAlpha;
    
    public static bool IsActive { get; private set; }
    
    public Jumpscare(GraphicsDevice graphicsDevice, float duration, float maxAlpha, Viewport viewport, Camera camera)
    {
        jumpscareRectangle = new Rectangle(viewport.Width / 2, viewport.Height / 2,1280, 720);
        jumpscareColor = Color.White;
        jumpscareDuration = duration;
        jumpscareTimer = 0f;
        jumpscareAlpha = 1f;
        jumpscareMaxAlpha = maxAlpha;
        IsActive = false;
    }
    
    public static void Activate()
    {
        IsActive = true;
        jumpscareTimer = 0f;
        jumpscareAlpha = 1f;
    }
    
    public void Update(GameTime gameTime, Matrix transformMatrix)
    {
        var scale = new Vector2(transformMatrix.M11, transformMatrix.M22);
        var translation = new Vector2(transformMatrix.M41, transformMatrix.M42);

        var transformedRectangle = new Rectangle(
            (int)(jumpscareRectangle.X * scale.X + translation.X),
            (int)(jumpscareRectangle.Y * scale.Y + translation.Y),
            1280,
            720
        );
        jumpscareRectangle = transformedRectangle;
        if (IsActive)
        {
            jumpscareTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            jumpscareAlpha = MathHelper.Lerp(jumpscareMaxAlpha, 0f, jumpscareTimer / jumpscareDuration);
            
            if (jumpscareTimer >= jumpscareDuration)
            {
                IsActive = false;
            }
        }
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsActive)
        {
            jumpscareColor.A = (byte)(jumpscareAlpha * 255);
            spriteBatch.Draw(Globals.Content.Load<Texture2D>("blankTexture"), jumpscareRectangle, jumpscareColor);
        }
    }
}