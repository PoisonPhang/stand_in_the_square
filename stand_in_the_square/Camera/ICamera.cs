using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace stand_in_the_square.Camera
{
    /// <summary>
    /// A 3D view camera
    /// </summary>
    public interface ICamera
    {
        /// <summary>
        /// The view matrix
        /// </summary>
        Matrix View { get; }

        /// <summary>
        /// The projection matrix
        /// </summary>
        Matrix Projection { get; }
    }
}
