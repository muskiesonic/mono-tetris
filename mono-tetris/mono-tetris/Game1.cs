using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace mono_tetris.Desktop
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousKeyState;
        Texture2D redBlockTexture;
        Tetromino currentPiece;
        float fallSpeed = Tetromino.HEIGHT;
        float moveSpeed = Tetromino.WIDTH * 5;
        float rotateSpeed = Tetromino.WIDTH * 2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            redBlockTexture = new Texture2D(GraphicsDevice, Tetromino.HEIGHT, Tetromino.WIDTH);
            Color[] colorData = new Color[Tetromino.HEIGHT * Tetromino.WIDTH];
            for (var i = 0; i < Tetromino.HEIGHT * Tetromino.WIDTH; i++)
                colorData[i] = Color.Red;
            redBlockTexture.SetData<Color>(colorData);

            previousKeyState = Keyboard.GetState();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentPiece = Tetromino.T(redBlockTexture, new Vector2(21 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
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

            currentPiece.MoveDown((float)(gameTime.ElapsedGameTime.TotalSeconds * fallSpeed));

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            currentPiece.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
