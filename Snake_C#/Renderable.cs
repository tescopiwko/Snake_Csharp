using Microsoft.Xna.Framework.Graphics;

namespace Snake_C_
{
    internal interface Renderable
    {
        void Draw(SpriteBatch spriteBatch, Texture2D pixel, int cellSize);
    }
}
