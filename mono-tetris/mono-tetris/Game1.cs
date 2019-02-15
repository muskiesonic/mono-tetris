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
        Texture2D redBlockTexture;
        List<Tetromino> pieces;

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

            pieces = new List<Tetromino>();
            pieces.Add(Tetromino.Square(redBlockTexture, new Vector2(2 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT)));
            pieces.Add(Tetromino.I(redBlockTexture, new Vector2(7 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT)));
            pieces.Add(Tetromino.Z(redBlockTexture, new Vector2(10 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT)));
            pieces.Add(Tetromino.S(redBlockTexture, new Vector2(16 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT)));
            pieces.Add(Tetromino.T(redBlockTexture, new Vector2(21 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT)));
            pieces.Add(Tetromino.L(redBlockTexture, new Vector2(27 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT)));
            pieces.Add(Tetromino.BackwardsL(redBlockTexture, new Vector2(31 * Tetromino.WIDTH, 2 * Tetromino.HEIGHT)));
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

            // TODO: Add your update logic here

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
            foreach (var piece in pieces)
                piece.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
