using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_tetris.Desktop
{
    public class Tetromino
    {
        public static short HEIGHT = 16;
        public static short WIDTH = 16;

        public static Tetromino Square(Texture2D texture, Vector2 postion)
        {
            var blocks = new short[,]
            {
                {1, 1},
                {1, 1}
            };
            return new Tetromino(texture, postion, blocks);
        }

        public static Tetromino I(Texture2D texture, Vector2 postion)
        {
            var blocks = new short[,]
            {
                {1},
                {1},
                {1},
                {1}
            };
            return new Tetromino(texture, postion, blocks);
        }

        public static Tetromino Z(Texture2D texture, Vector2 postion)
        {
            var blocks = new short[,]
            {
                {1, 1, 0},
                {0, 1, 1}
            };
            return new Tetromino(texture, postion, blocks);
        }

        public static Tetromino S(Texture2D texture, Vector2 postion)
        {
            var blocks = new short[,]
            {
                {0, 1, 1},
                {1, 1, 0}
            };
            return new Tetromino(texture, postion, blocks);
        }

        public static Tetromino T(Texture2D texture, Vector2 postion)
        {
            var blocks = new short[,]
            {
                {1, 1, 1},
                {0, 1, 0}
            };
            return new Tetromino(texture, postion, blocks);
        }

        public static Tetromino L(Texture2D texture, Vector2 postion)
        {
            var blocks = new short[,]
            {
                {1, 0},
                {1, 0},
                {1, 1}
            };
            return new Tetromino(texture, postion, blocks);
        }

        public static Tetromino BackwardsL(Texture2D texture, Vector2 postion)
        {
            var blocks = new short[,]
            {
                {0, 1},
                {0, 1},
                {1, 1}
            };
            return new Tetromino(texture, postion, blocks);
        }

        public Tetromino(Texture2D texture, Vector2 position, short[,] blocks)
        {
            Texture = texture;
            Position = position;
            Blocks = blocks;
        }

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public short[,] Blocks { get; }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Blocks.GetLength(0); i++)
                for (var j = 0; j < Blocks.GetLength(1); j++)
                    if (Blocks[i, j] == 1)
                        spriteBatch.Draw(Texture, new Vector2(Position.X + (j * WIDTH), Position.Y + (i * HEIGHT)), Color.White);
        }
    }
}
