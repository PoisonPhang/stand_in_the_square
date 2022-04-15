using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using stand_in_the_square.Camera;

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

        public Shape(Game game, ShapeType shapeType, Vector3 position)
        {
            _game = game;
            ShapeType = shapeType;
            Position = position;

            InitializeVertices();
            InitializeIndices();
            InitializeEffect();
        }

        public void InitializeVertices()
        {
            _vertices = new VertexPositionTexture[4];

            _vertices[0].Position = new Vector3(Position.X - 2, Position.Y - 2, Position.Z - 2);
            _vertices[0].TextureCoordinate = new Vector2(0, -1);

            _vertices[1].Position = new Vector3(Position.X + 2, Position.Y - 2, Position.Z - 2);
            _vertices[1].TextureCoordinate = new Vector2(1, -1);

            _vertices[2].Position = new Vector3(Position.X + 2, Position.Y - 2, Position.Z + 0);
            _vertices[2].TextureCoordinate = new Vector2(1, 0);

            _vertices[3].Position = new Vector3(Position.X - 2, Position.Y - 2, Position.Z + 0);
            _vertices[3].TextureCoordinate = new Vector2(0, 0);
        }

        public void InitializeIndices()
        {
            _indices = new short[6];

            _indices[0] = 0;
            _indices[1] = 1;
            _indices[2] = 2;

            _indices[3] = 2;
            _indices[4] = 3;
            _indices[5] = 0;
        }

        public void InitializeEffect()
        {
            _effect = new BasicEffect(_game.GraphicsDevice);
            _effect.World = Matrix.Identity;
            _effect.View = Matrix.CreateLookAt(
                new Vector3(0, 0, 4),
                new Vector3(0, 0, 0),
                Vector3.Up
                );
            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                _game.GraphicsDevice.Viewport.AspectRatio,
                0.1f,
                100.0f
                );
            _effect.TextureEnabled = true;

            SetShapeTexture();
        }

        public void ChangeShapeType(ShapeType shapeType)
        {
            ShapeType = shapeType;
            SetShapeTexture();
        }

        public void Draw(ICamera camera)
        {
            BlendState old = _game.GraphicsDevice.BlendState;

            _game.GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _effect.View = camera.View;
            _effect.Projection = camera.Projection;
            _effect.CurrentTechnique.Passes[0].Apply();

            _game.GraphicsDevice.DrawUserIndexedPrimitives(
                PrimitiveType.TriangleList,
                _vertices,
                0,
                4,
                _indices,
                0,
                2
                );

            _game.GraphicsDevice.BlendState = old;
        }

        private void SetShapeTexture()
        {
            switch (ShapeType)
            {
                case ShapeType.Square:
                    _effect.Texture = _game.Content.Load<Texture2D>("shape-square");
                    break;
                case ShapeType.Circle:
                    _effect.Texture = _game.Content.Load<Texture2D>("shape-circle");
                    break;
                case ShapeType.Rectangle:
                    _effect.Texture = _game.Content.Load<Texture2D>("shape-rectangle");
                    break;
            }
        }
    }
}
