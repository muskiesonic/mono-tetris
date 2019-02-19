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
                {0, 0, 0},
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

        private float staggeredYPosition = 0f;
        private float staggeredXPosition = 0f;
        private float staggeredRotate = 0f;

        public Tetromino(Texture2D texture, Vector2 position, short[,] blocks)
        {
            Texture = texture;
            Position = position;
            Blocks = blocks;
        }

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public short[,] Blocks { get; set; }

        public void MoveDown(float amount)
        {
            staggeredYPosition += amount;
            if (staggeredYPosition > HEIGHT)
            {
                int blocksMoved = (int)(staggeredYPosition / HEIGHT);
                staggeredYPosition = staggeredYPosition % HEIGHT;
                Position = new Vector2(Position.X, Position.Y + (blocksMoved * HEIGHT));
            }
        }

        public void ShiftLeft(int amount)
        {
            Position = new Vector2(Position.X - (amount * WIDTH), Position.Y);
        }

        public void ShiftRight(int amount)
        {
            Position = new Vector2(Position.X + (amount * WIDTH), Position.Y);
        }

        public void MoveLeft(float amount)
        {
            staggeredXPosition += amount;
            if (staggeredXPosition > WIDTH)
            {
                int blocksMoved = (int)(staggeredXPosition / WIDTH);
                staggeredXPosition = staggeredXPosition % WIDTH;
                Position = new Vector2(Position.X - (blocksMoved * WIDTH), Position.Y);
            }
        }

        public void MoveRight(float amount)
        {
            staggeredXPosition += amount;
            if (staggeredXPosition > WIDTH)
            {
                int blocksMoved = (int)(staggeredXPosition / WIDTH);
                staggeredXPosition = staggeredXPosition % WIDTH;
                Position = new Vector2(Position.X + (blocksMoved * WIDTH), Position.Y);
            }
        }

        public void RotateRight(int amount)
        {
            var rows = Blocks.GetLength(0) - 1;
            var cols = Blocks.GetLength(1) - 1;
            var newPositions = new short[Blocks.GetLength(0), Blocks.GetLength(1)];
            for (var i = 0; i < Blocks.GetLength(0); i++)
            {
                for (var j = 0; j < Blocks.GetLength(1); j++)
                {
                    newPositions[j, cols - i] = Blocks[i, j];
                }
            }
            Blocks = newPositions;
        }

        public void RotateRight(float amount)
        {
            staggeredRotate += amount;
            if (staggeredRotate > WIDTH)
            {
                int rotations = (int)(staggeredRotate / WIDTH);
                staggeredRotate = staggeredRotate % WIDTH;

                var rows = Blocks.GetLength(0) - 1;
                var cols = Blocks.GetLength(1) - 1;
                var newPositions = new short[Blocks.GetLength(0), Blocks.GetLength(1)];
                for (var i = 0; i < Blocks.GetLength(0); i++)
                {
                    for (var j = 0; j < Blocks.GetLength(1); j++)
                    {
                        newPositions[j, cols - i] = Blocks[i, j];
                    }
                }
                Blocks = newPositions;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Blocks.GetLength(0); i++)
                for (var j = 0; j < Blocks.GetLength(1); j++)
                    if (Blocks[i, j] == 1)
                        spriteBatch.Draw(Texture, new Vector2(Position.X + (j * WIDTH), Position.Y + (i * HEIGHT)), Color.White);
        }
    }
}
