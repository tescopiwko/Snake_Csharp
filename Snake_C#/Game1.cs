using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake_C_;
using System;
using System.Collections.Generic;

namespace Snake_C_
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D pixel;
        SpriteFont _font;

        Snake1 snake1;
        Snake2 snake2;
        Food food;

        int cellSize = 20;
        int updateSpeed = 150;
        double timer = 0;

        private int score1 = 0;
        private int score2 = 0; 

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

            try
            {
                _font = Content.Load<SpriteFont>("ScoreFont");
            }
            catch (Exception)
            {
                _font = null;
            }

            snake1 = new Snake1();
            snake2 = new Snake2();
            food = new Food(cellSize);

            SpawnFood();

            UpdateWindowTitle();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var k  = Keyboard.GetState();

            if (k.IsKeyDown(Keys.Up) && snake1.Direction != new Point(0,1))
                snake1.Direction = new Point(0, -1);

            if (k.IsKeyDown(Keys.Down) && snake1.Direction != new Point(0,-1))
                snake1.Direction = new Point(0, 1);

            if (k.IsKeyDown(Keys.Left) && snake1.Direction != new Point(1, 0))
                snake1.Direction = new Point(-1, 0);

            if (k.IsKeyDown(Keys.Right) && snake1.Direction != new Point(-1, 0))
                snake1.Direction = new Point(1, 0);


            if (k.IsKeyDown(Keys.W) && snake2.Direction != new Point(0, 1))
                snake2.Direction = new Point(0, -1);

            if (k.IsKeyDown(Keys.S) && snake2.Direction != new Point(0, -1))
                snake2.Direction = new Point(0, 1);

            if (k.IsKeyDown(Keys.A) && snake2.Direction != new Point(1, 0))
                snake2.Direction = new Point(-1, 0);

            if (k.IsKeyDown(Keys.D) && snake2.Direction != new Point(-1, 0))
                snake2.Direction = new Point(1, 0);

            timer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= updateSpeed)
            {
                timer = 0;

                snake1.Move();
                snake2.Move();

                int maxX = _graphics.PreferredBackBufferWidth / cellSize;
                int maxY = _graphics.PreferredBackBufferHeight / cellSize;

                if (snake1.Body[0] == food.Position)
                {
                    snake1.Grow();
                    SpawnFood();
                }

                if (snake2.Body[0] == food.Position)
                {
                    snake2.Grow();
                    SpawnFood();
                }

                if (snake1.CollidesWithWalls(maxX, maxY) || snake1.CollidesWithSelf())
                {
                    AddPointToSnake2();
                    ResetRound();
                }

                if (snake2.CollidesWithWalls(maxX, maxY) || snake2.CollidesWithSelf())
                {
                    AddPointToSnake1();
                    ResetRound();
                }

                CollidesWithAnother();

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (var part in snake1.Body)
            {
                _spriteBatch.Draw(pixel, new Rectangle(part.X * cellSize, part.Y * cellSize, cellSize, cellSize), Color.Orange);
            }

            foreach (var part in snake2.Body)
            {
                _spriteBatch.Draw(pixel, new Rectangle(part.X * cellSize, part.Y * cellSize, cellSize, cellSize), Color.Green);
            }

            food.Draw(_spriteBatch, pixel, cellSize);


            if (_font != null)
            {
                _spriteBatch.DrawString(_font, $"Orange: {score1}", new Vector2(10, 10), Color.Black);

                string scoreSnake2 = $"Green: {score2}";
                Vector2 size = _font.MeasureString(scoreSnake2);
                float x = _graphics.PreferredBackBufferWidth - size.X - 10;
                _spriteBatch.DrawString(_font, scoreSnake2, new Vector2(x, 10), Color.Black);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void SpawnFood()
        {
            int maxX = _graphics.PreferredBackBufferWidth / cellSize;
            int maxY = _graphics.PreferredBackBufferHeight / cellSize;

            food.Spawn(maxX, maxY);
        }

        void CollidesWithAnother()
        {
            Point head1 = snake1.Body[0];
            Point head2 = snake2.Body[0];

            // kolize hlavy prvniho hada se telem druheho hada 
            for (int i = 1; i < snake2.Body.Count; i++)
            {
                if (head1 == snake2.Body[i])
                {
                    AddPointToSnake2();
                    ResetRound();
                    return;
                }
            }

            // opak - kolize hlavy druheho hada s telem prvniho hada
            for (int i =1; i < snake1.Body.Count; i++)
            {
                if (head2 == snake1.Body[i])
                {
                    AddPointToSnake1();
                    ResetRound();
                    return;
                }
            }
            if (head1 == head2)
            {
                // Obě hlavy na stejném políčku
                ResetRound();
            }
        }
        private void AddPointToSnake1()
        {
            score1++;
            UpdateWindowTitle();
        }
        private void AddPointToSnake2()
        {
            score2++;
            UpdateWindowTitle();
        }
        private void ResetRound()
        {
            snake1.Reset();
            snake2.Reset();
            SpawnFood();
        }
        private void UpdateWindowTitle()
        {
            Window.Title = $"Orange: {score1} - Green: {score2}"; 
        }

    }
}
        

