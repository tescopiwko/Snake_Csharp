using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Snake_C_
{
    internal class Food : Renderable
    {
        public Point Position { get; private set; }
        private Random _random;

        public Food(int cellSize)
        {
            _random = new Random();
        }

        public void Spawn(int maxX, int maxY)
        {
            Position = new Point(_random.Next(0, maxX), _random.Next(0, maxY));
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel, int cellSize)
        {
            spriteBatch.Draw(pixel, new Rectangle(Position.X * cellSize, Position.Y * cellSize, cellSize, cellSize), Color.Red);
        }
    }
}
