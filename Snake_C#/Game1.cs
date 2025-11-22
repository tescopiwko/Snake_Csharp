using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Snake_C_
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D pixel;

        List<Point> snake = new List<Point>();
        Point direction = new Point(1, 0);

        Point food;

        int cellSize = 20;
        int updateSpeed = 60;
        double timer = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            snake.Clear();
            snake.Add(new Point(10, 10));
            snake.Add(new Point(9, 10));
            snake.Add(new Point(8, 10));

            SpawnFood();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var k  = Keyboard.GetState();

            if (k.IsKeyDown(Keys.Up) && direction != new Point(0,1))
                direction = new Point(0, -1);

            if (k.IsKeyDown(Keys.Down) && direction != new Point(0,-1))
                direction = new Point(0, 1);

            if (k.IsKeyDown(Keys.Left) && direction != new Point(1, 0))
                direction = new Point(-1, 0);

            if (k.IsKeyDown(Keys.Right) && direction != new Point(-1, 0))
                direction = new Point(1, 0);

            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= updateSpeed)
            {
                timer = 0;

                Point newHead = new Point(snake[0].X + direction.X, snake[0].Y + direction.Y);

                snake.Insert(0, newHead);

                if (newHead == food)
                {
                    SpawnFood();
                }
                else
                {
                    snake.RemoveAt(snake.Count - 1);
                }

                CheckCollisions();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (var part in snake)
            {
                _spriteBatch.Draw(pixel, new Rectangle(part.X * cellSize, part.Y * cellSize, cellSize, cellSize), Color.Green);
            }
            _spriteBatch.Draw(pixel, new Rectangle(food.X * cellSize, food.Y * cellSize, cellSize, cellSize), Color.Red);
            base.Draw(gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void SpawnFood()
        {
            Random rnd = new Random();
            int maxX = _graphics.PreferredBackBufferWidth / cellSize;
            int maxY = _graphics.PreferredBackBufferHeight / cellSize;

            food = new Point(rnd.Next(0, maxX), rnd.Next(0, maxY));
        }
        void CheckCollisions()
        {
            int maxX = _graphics.PreferredBackBufferWidth / cellSize;
            int maxY = _graphics.PreferredBackBufferHeight / cellSize;

            if (snake[0].X < 0 || snake[0].X >= maxX ||
                snake[0].Y < 0 || snake[0].Y >= maxY)
            {
                ResetGame();
                return;
            }
        }

        void ResetGame()
        {
            snake.Clear();
            snake.Add(new Point(10, 10));
            snake.Add(new Point(9, 10));
            snake.Add(new Point(8, 10));
            direction = new Point(1, 0);
            SpawnFood();
        }
    }
}
        

