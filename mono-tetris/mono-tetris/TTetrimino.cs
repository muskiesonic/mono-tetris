using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_tetris.Desktop
{
    public class TTetrimino : Tetromino
    {
        public TTetrimino(Gameboard parent, Texture2D texture)
            : base(parent)
        {
            Size = new Vector2(2, 2);
            Blocks = new List<Block>()
            {
                new Block(this, texture, new Vector2(1, 0)),
                new Block(this, texture, new Vector2(0, 1)),
                new Block(this, texture, new Vector2(1, 1)),
                new Block(this, texture, new Vector2(2, 1))
            };
        }
    }
}
