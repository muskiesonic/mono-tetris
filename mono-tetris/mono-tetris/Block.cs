using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_tetris.Desktop
{
    public class Block
    {
        public Block(Tetromino parent, Texture2D texture, Vector2 position)
        {
            Parent = parent;
            Texture = texture;
            Position = position;
        }

        public Texture2D Texture { get; private set; }

        public Vector2 Position { get; set; }

        public Vector2 GamePosition
        {
            get
            {
                return new Vector2(Position.X + Parent.GamePosition.X, Position.Y + Parent.GamePosition.Y);
            }
        }

        public Tetromino Parent { get; private set; }
    }
}
