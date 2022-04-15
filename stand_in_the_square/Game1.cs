using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using stand_in_the_square.Camera;
using stand_in_the_square.Entities;

namespace stand_in_the_square
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private StationaryCamera _camera;
        private Crate _crate;
        private Shape[] _shapes;
        private PlayerController _controller;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _crate = new Crate(this, CrateType.Cross, Matrix.Identity);
            _shapes = new Shape[]
            {
                new Shape(this, ShapeType.Square, new Vector3(3, 0, 3)),
                new Shape(this, ShapeType.Circle, new Vector3(-3, 0, -3)),
                new Shape(this, ShapeType.Rectangle, new Vector3(3, 0, -3))
            };

            _camera = new StationaryCamera(this, new Vector3(0, 3, 10), Vector3.Zero);
            _controller = new PlayerController(0.1f, 1);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _controller.Update(gameTime);
            _crate.UpdatePosition(Matrix.CreateTranslation(_controller.Velocity.X, _controller.Velocity.Y, _controller.Velocity.Z));

            _crate.SetGreen(_controller.Velocity.X > 1.5f && _controller.Velocity.Z > 2.5f && _controller.Velocity.X < 3.5 && _controller.Velocity.Z < 4.5f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _crate.Draw(_camera);

            foreach (Shape shape in _shapes)
            {
                shape.Draw(_camera);
            }

            base.Draw(gameTime);
        }
    }
}
