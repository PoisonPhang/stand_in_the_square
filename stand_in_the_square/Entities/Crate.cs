﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using stand_in_the_square.Camera;

namespace stand_in_the_square.Entities
{
    /// <summary>
    /// The type of crate to create
    /// </summary>
    public enum CrateType
    {
        Slats = 0,
        Cross,
        DarkCross,
    }

    public class Crate
    {
        // The game this crate belongs to
        private Game _game;

        // The VertexBuffer of crate vertices
        private VertexBuffer _vertexBuffer;

        // The IndexBuffer defining the Crate's triangles
        private IndexBuffer _indexBuffer;

        // The effect to render the crate with
        private BasicEffect _effect;

        // The texture to apply to the crate
        private Texture2D _texture;

        /// <summary>
        /// Creates a new crate instance
        /// </summary>
        /// <param name="game">The game this crate belongs to</param>
        /// <param name="type">The type of crate to use</param>
        /// <param name="world">The position and orientation of the crate in the world</param>
        public Crate(Game game, CrateType type, Matrix world)
        {
            _game = game;
            _texture = game.Content.Load<Texture2D>($"crate{(int)type}_diffuse");
            InitializeVertices();
            InitializeIndices();
            InitializeEffect();
            _effect.World = world;
        }

        /// <summary>
        /// Initializes the vertex of the cube
        /// </summary>
        public void InitializeVertices()
        {
            var vertexData = new VertexPositionNormalTexture[] { 
                // Front Face
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Forward },
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Forward },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Forward },

                // Back Face
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Backward },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Forward },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Forward },

                // Top Face
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, 1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Up },
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Up },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Up },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Up },

                // Bottom Face
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Down },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Down },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Down },
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Down },

                // Left Face
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Left },
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Left },
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Left },
                new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Left },

                // Right Face
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Right },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Right },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Right },
                new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Right },
            };

            _vertexBuffer = new VertexBuffer(_game.GraphicsDevice, typeof(VertexPositionNormalTexture), vertexData.Length, BufferUsage.None);
            _vertexBuffer.SetData<VertexPositionNormalTexture>(vertexData);
        }

        /// <summary>
        /// Initializes the Index Buffer
        /// </summary>
        public void InitializeIndices()
        {
            var indexData = new short[]
            {
                // Front face
                0, 2, 1,
                0, 3, 2,

                // Back face 
                4, 6, 5,
                4, 7, 6,

                // Top face
                8, 10, 9,
                8, 11, 10,

                // Bottom face 
                12, 14, 13,
                12, 15, 14,

                // Left face 
                16, 18, 17,
                16, 19, 18,

                // Right face 
                20, 22, 21,
                20, 23, 22
            };

            _indexBuffer = new IndexBuffer(_game.GraphicsDevice, IndexElementSize.SixteenBits, indexData.Length, BufferUsage.None);
            _indexBuffer.SetData<short>(indexData);
        }

        /// <summary>
        /// Initializes the BasicEffect to render our crate
        /// </summary>
        void InitializeEffect()
        {
            _effect = new BasicEffect(_game.GraphicsDevice);
            _effect.World = Matrix.CreateScale(1.0f);
            _effect.View = Matrix.CreateLookAt(
                new Vector3(8, 9, 12), // The camera position
                new Vector3(0, 0, 0), // The camera target,
                Vector3.Up            // The camera up vector
            );
            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,                         // The field-of-view 
                _game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
                0.1f, // The near plane distance 
                100.0f // The far plane distance
            );
            _effect.TextureEnabled = true;
            _effect.Texture = _texture;

            // Turn on lighting
            _effect.LightingEnabled = true;
            // Set up light 0
            _effect.DirectionalLight0.Enabled = true;
            _effect.DirectionalLight0.Direction = new Vector3(1f, 0, 1f);
            _effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.8f, 0.8f);
            _effect.DirectionalLight0.SpecularColor = new Vector3(0.4f, 0.4f, 0.4f);

            _effect.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.3f);
        }

        public void UpdatePosition(Matrix world)
        {
            _effect.World = world;
        }

        public void SetGreen(bool green)
        {
            if (green)
                _effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 2f, 0.8f);
            else
                _effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.8f, 0.8f);
        }

        /// <summary>
        /// Draws the crate
        /// </summary>
        /// <param name="camera">The camera to use to draw the crate</param>
        public void Draw(ICamera camera)
        {
            // set the view and projection matrices
            _effect.View = camera.View;
            _effect.Projection = camera.Projection;

            // apply the effect 
            _effect.CurrentTechnique.Passes[0].Apply();

            // set the vertex buffer
            _game.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
            // set the index buffer
            _game.GraphicsDevice.Indices = _indexBuffer;
            // Draw the triangles
            _game.GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList, // Tye type to draw
                0,                          // The first vertex to use
                0,                          // The first index to use
                12                          // the number of triangles to draw
            );

        }
    }
}
