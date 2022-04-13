using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace stand_in_the_square.Entities
{
    public enum ShapeType
    {
        Square = 0,
        Circle,
        Rectangle,
    }

    public class Shape
    {
        private const int UNIT_WIDTH = 576;
        private const int UNIT_HEIGHT = 576;

        public ShapeType ShapeType { get; private set; }

        public Vector3 Position { get; private set; }

        private VertexPositionTexture[] _vertices;
        private short[] _indices;
        private BasicEffect _effect;
        private Game _game;

        public Shape(Game game, Vector3 position)
        {

        }
    }
}
