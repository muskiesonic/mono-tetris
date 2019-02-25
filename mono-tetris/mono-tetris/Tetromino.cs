using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_tetris.Desktop
{
    public enum SpinState
    {
        Zero,
        Ninety,
        OneEighty,
        TwoSixty
    };

    public abstract class Tetromino
    {
        public static short HEIGHT = 16;
        public static short WIDTH = 16;

        public static Tetromino T(Gameboard parent, Texture2D texture)
        {
            return new TTetrimino(parent, texture);
        }

        protected Vector2[] wk_zeroNinety = new Vector2[]
        { 
            new Vector2(-1,  0),
            new Vector2(-1,  1),
            new Vector2( 0, -1),
            new Vector2(-1, -1)
        };

        protected Vector2[] wk_ninetyOneEighty = new Vector2[]
        {
            new Vector2(1,  0),
            new Vector2(1, -1),
            new Vector2(0,  1),
            new Vector2(1,  1)
        };

        protected Vector2[] wk_oneEightyTwoSixty = new Vector2[]
        {
            new Vector2(1,  0),
            new Vector2(1,  1),
            new Vector2(0, -1),
            new Vector2(1, -1)
        };

        protected Vector2[] wk_twoSixtyZero = new Vector2[]
        {
            new Vector2(-1,  0),
            new Vector2(-1, -1),
            new Vector2( 0,  1),
            new Vector2(-1,  1)
        };

        private float staggeredYPosition = 0f;
        private float staggeredXPosition = 0f;
        private float staggeredRotate = 0f;

        public Tetromino(Gameboard parent)
        {
            Parent = parent;
            Position = Vector2.Zero;
            Blocks = new List<Block>();
            SpinState = SpinState.Zero;
        }

        public Gameboard Parent { get; set; }

        protected SpinState SpinState { get; private set; }

        public Vector2 Position { get; private set; }

        public Vector2 GamePosition
        {
            get
            {
                return Position;
            }
        }

        public List<Block> Blocks { get; protected set; }

        public Vector2 Size { get; protected set; }

        protected void Move(int x = 0, int y = 0)
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
            var rotSpinState = (SpinState)(((int)SpinState + 1) % 4);
            staggeredRotate = 0;

            List<Vector2> prevPos = new List<Vector2>();
            foreach (var block in Blocks)
            {
                prevPos.Add(block.Position);
                block.Position = new Vector2(Size.Y - block.Position.Y, block.Position.X);
            }

            var collision = Parent.CheckCollision(this);

            if (collision != CollisionType.None)
            {
                var wallkick = TryWallKick(rotSpinState);
                if (wallkick)
                {
                    collision = CollisionType.None;
                }
            }

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

            if (collision == CollisionType.None)
            {
                SpinState = rotSpinState;
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

        protected virtual bool TryWallKick(SpinState rotSpinState)
        {
            Vector2[] wallKicks = null;

            if (SpinState == SpinState.Zero && rotSpinState == SpinState.Ninety)
                wallKicks = wk_zeroNinety;

            if (SpinState == SpinState.Ninety && rotSpinState == SpinState.OneEighty)
                wallKicks = wk_ninetyOneEighty;

            if (SpinState == SpinState.OneEighty && rotSpinState == SpinState.TwoSixty)
                wallKicks = wk_oneEightyTwoSixty;

            if (SpinState == SpinState.TwoSixty && rotSpinState == SpinState.Zero)
                wallKicks = wk_twoSixtyZero;

            foreach (var attempt in wallKicks)
            {
                var prevPos = Position;
                Position = new Vector2(Position.X + attempt.X, Position.Y + attempt.Y);

                var collision = Parent.CheckCollision(this);
                if (collision == CollisionType.None)
                    return true;

                Position = prevPos;
            }

            return false;
        }
    }
}
