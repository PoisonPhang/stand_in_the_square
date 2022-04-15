using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace stand_in_the_square
{
    public class PlayerController
    {
        private KeyboardState _curKeyboardState;
        private KeyboardState _oldKeyboardState;
        private float _speed;
        private float _size;

        public Vector3 Velocity { get; private set; }

        public PlayerController(float speed, float size)
        {
            _speed = speed;
            _size = size;
            Velocity = Vector3.Zero;
        }

        public void Update(GameTime time)
        {
            float velocity = _speed * _size;
            _curKeyboardState = Keyboard.GetState();

            if (_curKeyboardState.IsKeyDown(Keys.W))
            {
                Velocity += new Vector3(0, 0, -velocity);
            }

            if (_curKeyboardState.IsKeyDown(Keys.S))
            {
                Velocity += new Vector3(0, 0, velocity);
            }

            if (_curKeyboardState.IsKeyDown(Keys.A))
            {
                Velocity += new Vector3(-velocity, 0, 0);
            }

            if (_curKeyboardState.IsKeyDown(Keys.D))
            {
                Velocity += new Vector3(velocity, 0, 0);
            }
        }
    }
}
