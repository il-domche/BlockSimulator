using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Indiv0.BlockSimulator.Cameras
{
    class Camera : DrawableGameComponent
    {
        #region Private Fields
        private static Camera _activeCamera = null;

        // Matricies used to display the world.
        private Matrix _world = Matrix.Identity;
        private Matrix _view = Matrix.Identity;
        private Matrix _projection = Matrix.Identity;

        private Vector3 _position = new Vector3(0, 0, 0);
        private Vector3 _angle = new Vector3();
        private float _speed = 50f;
        private float _turnSpeed = 60f;
        #endregion

        #region Properties
        public static Camera ActiveCamera
        {
            get { return _activeCamera; }
            set { _activeCamera = value; }
        }

        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Matrix World
        {
            get { return _world; }
            set { _world = value; }
        }

        public Matrix View
        {
            get { return _view; }
        }

        public Matrix Projection
        {
            get { return _projection; }
        }
        #endregion

        #region Initialization
        public Camera(Game game)
            : base(game)
        {
            if (_activeCamera == null)
                _activeCamera = this;

            int centerX = Game.Window.ClientBounds.Width / 2;
            int centerY = Game.Window.ClientBounds.Height / 2;

            Mouse.SetPosition(centerX, centerY);

            float ratio = Game.GraphicsDevice.Viewport.AspectRatio;
            _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ratio, 1.0f, 1000.0f);
        }
        #endregion

        #region Public Methods
        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            
            _view = Matrix.Identity;
            _view *= Matrix.CreateTranslation(-_position);
            _view *= Matrix.CreateRotationZ(_angle.Z);
            _view *= Matrix.CreateRotationY(_angle.Y);
            _view *= Matrix.CreateRotationX(_angle.X);
            _view *= _world;

            base.Update(gameTime);
        }
        #endregion

        #region Private Methods
        private void HandleInput(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            int centerX = Game.Window.ClientBounds.Width / 2;
            int centerY = Game.Window.ClientBounds.Height / 2;

            Mouse.SetPosition(centerX, centerY);

            _angle.X += MathHelper.ToRadians((mouse.Y - centerY) * _turnSpeed * 0.01f); // pitch
            _angle.Y += MathHelper.ToRadians((mouse.X - centerX) * _turnSpeed * 0.01f); // yaw

            Vector3 forward = Vector3.Normalize(new Vector3((float)Math.Sin(-_angle.Y), (float)Math.Sin(_angle.X), (float)Math.Cos(-_angle.Y)));
            Vector3 left = Vector3.Normalize(new Vector3((float)Math.Cos(_angle.Y), 0f, (float)Math.Sin(_angle.Y)));

            if (keyboard.IsKeyDown(Keys.Up) | keyboard.IsKeyDown(Keys.W))
                _position -= forward * _speed * delta;

            if (keyboard.IsKeyDown(Keys.Down) | keyboard.IsKeyDown(Keys.S))
                _position += forward * _speed * delta;

            if (keyboard.IsKeyDown(Keys.Left) | keyboard.IsKeyDown(Keys.A))
                _position -= left * _speed * delta;

            if (keyboard.IsKeyDown(Keys.Right) | keyboard.IsKeyDown(Keys.D))
                _position += left * _speed * delta;

            if (keyboard.IsKeyDown(Keys.PageUp) | keyboard.IsKeyDown(Keys.Q))
                _position += Vector3.Up * _speed * delta;

            if (keyboard.IsKeyDown(Keys.PageDown) | keyboard.IsKeyDown(Keys.E))
                _position += Vector3.Down * _speed * delta;
        }
        #endregion
    }
}
