using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Snake_Game
{
    internal class Food
    {
        public Point Position { get; private set; }
        private int _cellSize;
        private Random _random;

        public Food(int cellSize)
        {
            _cellSize = cellSize;
            _random = new Random();
        }

        public void Spawn(int maxX, int maxY)
        {
            Position = new Point(_random.Next(0, maxX), _random.Next(0, maxY));
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            spriteBatch.Draw(pixel, new Rectangle(Position.X * _cellSize, Position.Y * _cellSize, _cellSize, _cellSize), Color.Red);
        }
    }
}
