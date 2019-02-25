using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_tetris.Desktop
{
    public class Game1 : Game
    {
        static int WINDOW_WIDTH = 800;
        static int WINDOW_HEIGHT = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousKeyState;
        Texture2D redBlockTexture;
        Tetromino currentPiece;
        Gameboard gameboard;
        Texture2D gameboardTexture;

        float fallSpeed = Tetromino.HEIGHT;
        float moveSpeed = Tetromino.WIDTH * 5;
        float rotateSpeed = Tetromino.WIDTH * 2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            redBlockTexture = new Texture2D(GraphicsDevice, Tetromino.WIDTH, Tetromino.HEIGHT);
            Color[] colorData = new Color[Tetromino.HEIGHT * Tetromino.WIDTH];
            for (var i = 0; i < Tetromino.HEIGHT * Tetromino.WIDTH; i++)
                colorData[i] = Color.Red;
            redBlockTexture.SetData<Color>(colorData);

            gameboardTexture = new Texture2D(GraphicsDevice, Tetromino.WIDTH * 19, Tetromino.HEIGHT * 25);
            colorData = new Color[Tetromino.WIDTH * 19 * Tetromino.HEIGHT * 25];
            for (var i = 0; i < Tetromino.WIDTH * 19 * Tetromino.HEIGHT * 25; i++)
                colorData[i] = Color.DimGray;
            gameboardTexture.SetData<Color>(colorData);

            previousKeyState = Keyboard.GetState();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameboard = new Gameboard(gameboardTexture, new Vector2(WINDOW_WIDTH / 2 - 19 / 2 * Tetromino.WIDTH, WINDOW_HEIGHT / 2 - 25 / 2 * Tetromino.HEIGHT), new Vector2(19, 25));
            currentPiece = Tetromino.T(gameboard, redBlockTexture);
            gameboard.SetCurrentPiece(currentPiece);
        }

        protected override void UnloadContent()
        {
            redBlockTexture.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            // User Interaction
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Left))
            {
                if (!previousKeyState.IsKeyDown(Keys.Left))
                    currentPiece.ShiftLeft(1);
                else
                    currentPiece.MoveLeft((float)(gameTime.ElapsedGameTime.TotalSeconds * moveSpeed));
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                if (!previousKeyState.IsKeyDown(Keys.Right))
                    currentPiece.ShiftRight(1);
                else
                    currentPiece.MoveRight((float)(gameTime.ElapsedGameTime.TotalSeconds * moveSpeed));
            }
            else if (keyState.IsKeyDown(Keys.Space))
            {
                if (!previousKeyState.IsKeyDown(Keys.Space))
                    currentPiece.RotateRight(1);
                else
                    currentPiece.RotateRight((float)(gameTime.ElapsedGameTime.TotalSeconds * rotateSpeed));
            }

            previousKeyState = keyState;

            // Update Game State
            currentPiece.MoveDown((float)(gameTime.ElapsedGameTime.TotalSeconds * fallSpeed));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(gameboard.Texture, gameboard.Position, Color.White);

            foreach (var block in gameboard.CurrentPiece.Blocks)
            {
                var posX = gameboard.Position.X + gameboard.CurrentPiece.Position.X * Tetromino.WIDTH + block.Position.X * Tetromino.WIDTH;
                var posY = gameboard.Position.Y + gameboard.CurrentPiece.Position.Y * Tetromino.HEIGHT + block.Position.Y * Tetromino.HEIGHT;
                spriteBatch.Draw(block.Texture, new Vector2(posX, posY), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
