using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake_Game;
using System;
using System.Collections.Generic;

namespace Snake_C_
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D pixel;
        Snake snake;
        Food food;

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

            snake = new Snake();
            food = new Food(cellSize);

            SpawnFood();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var k  = Keyboard.GetState();

            if (k.IsKeyDown(Keys.Up) && snake.Direction != new Point(0,1))
                snake.Direction = new Point(0, -1);

            if (k.IsKeyDown(Keys.Down) && snake.Direction != new Point(0,-1))
                snake.Direction = new Point(0, 1);

            if (k.IsKeyDown(Keys.Left) && snake.Direction != new Point(1, 0))
                snake.Direction = new Point(-1, 0);

            if (k.IsKeyDown(Keys.Right) && snake.Direction != new Point(-1, 0))
                snake.Direction = new Point(1, 0);

            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= updateSpeed)
            {
                timer = 0;

                snake.Move();

                int maxX = _graphics.PreferredBackBufferWidth / cellSize;
                int maxY = _graphics.PreferredBackBufferHeight / cellSize;

                if (snake.Body[0] == food.Position)
                {
                    snake.Grow();
                    SpawnFood();
                }
                
                if (snake.CollidesWithBody(maxX, maxY) || snake.CollidesWithSelf())
                {
                    snake.Reset();
                    SpawnFood();
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (var part in snake.Body)
            {
                _spriteBatch.Draw(pixel, new Rectangle(part.X * cellSize, part.Y * cellSize, cellSize, cellSize), Color.Green);
            }
            
            food.Draw(_spriteBatch, pixel);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void SpawnFood()
        {
            int maxX = _graphics.PreferredBackBufferWidth / cellSize;
            int maxY = _graphics.PreferredBackBufferHeight / cellSize;

            food.Spawn(maxX, maxY);
        }
    }
}
        

