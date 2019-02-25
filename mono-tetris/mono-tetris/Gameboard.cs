using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace mono_tetris.Desktop
{
    public enum CollisionType
    {
        None,
        GameboardWall,
        GameboardFloor,
        Block
    }

    public class Gameboard
    {
        public Gameboard(Texture2D texture, Vector2 position, Vector2 boardSize)
        {
            Texture = texture;
            Position = position;
            BoardSize = boardSize;
        }

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 BoardSize { get; set; }

        public Tetromino CurrentPiece { get; set; }

        public void SetCurrentPiece(Tetromino piece)
        {
            CurrentPiece = piece;
        }

        public CollisionType CheckCollision(Tetromino piece)
        {
            foreach (var block in piece.Blocks)
            {
                if (block.GamePosition.X > BoardSize.X - 1 || block.GamePosition.X < 0)
                {
                    return CollisionType.GameboardWall;
                }

                if (block.GamePosition.Y > BoardSize.Y - 1)
                {
                    return CollisionType.GameboardFloor;
                }
            }

            return CollisionType.None;
        }
    }
}
