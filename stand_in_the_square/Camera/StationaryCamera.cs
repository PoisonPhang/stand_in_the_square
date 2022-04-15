using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace stand_in_the_square.Camera
{
    public class StationaryCamera : ICamera
    {
        public Matrix View { get; protected set; }

        public Matrix Projection { get; protected set; }

        public StationaryCamera(Game game, Vector3 position, Vector3 lookAt)
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, game.GraphicsDevice.Viewport.AspectRatio, 1, 1000);
            View = Matrix.CreateLookAt(position, lookAt, Vector3.Up);
        }
    }
}
