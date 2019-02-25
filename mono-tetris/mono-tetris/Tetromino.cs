using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_tetris.Desktop
{
    public class Tetromino
    {
        public static short HEIGHT = 16;
        public static short WIDTH = 16;

        //public static Tetromino Square(Texture2D texture)
        //{
        //    var blocks = new short[,]
        //    {
        //        {1, 1},
        //        {1, 1}
        //    };
        //    return new Tetromino(texture, blocks);
        //}

        //public static Tetromino I(Texture2D texture)
        //{
        //    var blocks = new short[,]
        //    {
        //        {1},
        //        {1},
        //        {1},
        //        {1}
        //    };
        //    return new Tetromino(texture, blocks);
        //}

        //public static Tetromino Z(Texture2D texture)
        //{
        //    var blocks = new short[,]
        //    {
        //        {1, 1, 0},
        //        {0, 1, 1}
        //    };
        //    return new Tetromino(texture, blocks);
        //}

        //public static Tetromino S(Texture2D texture)
        //{
        //    var blocks = new short[,]
        //    {
        //        {0, 1, 1},
        //        {1, 1, 0}
        //    };
        //    return new Tetromino(texture, blocks);
        //}

        public static Tetromino T(Gameboard parent, Texture2D texture)
        {
            var piece = new Tetromino(parent, new Vector2(2, 2));
            piece.Blocks.Add(new Block(piece, texture, new Vector2(0, 1)));
            piece.Blocks.Add(new Block(piece, texture, new Vector2(1, 1)));
            piece.Blocks.Add(new Block(piece, texture, new Vector2(2, 1)));
            piece.Blocks.Add(new Block(piece, texture, new Vector2(1, 2)));
            return piece;
        }

        //public static Tetromino L(Texture2D texture)
        //{
        //    var blocks = new short[,]
        //    {
        //        {1, 0},
        //        {1, 0},
        //        {1, 1}
        //    };
        //    return new Tetromino(texture, blocks);
        //}

        //public static Tetromino BackwardsL(Texture2D texture)
        //{
        //    var blocks = new short[,]
        //    {
        //        {0, 1},
        //        {0, 1},
        //        {1, 1}
        //    };
        //    return new Tetromino(texture, blocks);
        //}

        private float staggeredYPosition = 0f;
        private float staggeredXPosition = 0f;
        private float staggeredRotate = 0f;

        public Tetromino(Gameboard parent, Vector2 size)
        {
            Parent = parent;
            Position = Vector2.Zero;
            Blocks = new List<Block>();
            Size = size;
        }

        public Gameboard Parent { get; set; }

        public Vector2 Position { get; private set; }

        public Vector2 GamePosition
        {
            get
            {
                return Position;
            }
        }

        public List<Block> Blocks { get; set; }

        public Vector2 Size { get; set; }

        private void Move(int x = 0, int y = 0)
        {
            var prevPos = Position;
            Position = new Vector2(Position.X + x, Position.Y - y);

            var collision = Parent.CheckCollision(this);
            if (collision == CollisionType.GameboardWall)
            {
                Position = prevPos;
            }
            if (collision == CollisionType.GameboardFloor)
            {
                Position = prevPos;
            }
        }

        public void MoveDown(float amount)
        {
            staggeredYPosition += amount;
            if (staggeredYPosition > HEIGHT)
            {
                int blocksMoved = (int)(staggeredYPosition / HEIGHT);
                staggeredYPosition = staggeredYPosition % HEIGHT;
                Move(y: -blocksMoved);
            }
        }

        public void ShiftLeft(int amount)
        {
            Move(x: -amount);
        }

        public void ShiftRight(int amount)
        {
            Move(x: amount);
        }

        public void MoveLeft(float amount)
        {
            staggeredXPosition += amount;
            if (staggeredXPosition > WIDTH)
            {
                int blocksMoved = (int)(staggeredXPosition / WIDTH);
                staggeredXPosition = staggeredXPosition % WIDTH;
                Move(x: -blocksMoved);
            }
        }

        public void MoveRight(float amount)
        {
            staggeredXPosition += amount;
            if (staggeredXPosition > WIDTH)
            {
                int blocksMoved = (int)(staggeredXPosition / WIDTH);
                staggeredXPosition = staggeredXPosition % WIDTH;
                Move(x: blocksMoved);

            }
        }

        public void RotateRight(int amount)
        {
            staggeredRotate = 0;
            List<Vector2> prevPos = new List<Vector2>();
            foreach (var block in Blocks)
            {
                prevPos.Add(block.Position);
                block.Position = new Vector2(Size.Y - block.Position.Y, block.Position.X);
            }

            var collision = Parent.CheckCollision(this);

            if (collision == CollisionType.GameboardWall)
            {
                for (var i = 0; i < Blocks.Count; i++)
                    Blocks[i].Position = prevPos[i];
            }
            if (collision == CollisionType.GameboardFloor)
            {
                for (var i = 0; i < Blocks.Count; i++)
                    Blocks[i].Position = prevPos[i];
            }
        }

        public void RotateRight(float amount)
        {
            staggeredRotate += amount;
            if (staggeredRotate > WIDTH)
            {
                int rotations = (int)(staggeredRotate / WIDTH);
                staggeredRotate = staggeredRotate % WIDTH;
                RotateRight(rotations);
            }
        }
    }
}
