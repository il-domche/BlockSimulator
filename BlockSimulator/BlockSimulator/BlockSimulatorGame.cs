using System.Collections.Generic;
using Indiv0.BlockSimulator.Blocks;
using Indiv0.BlockSimulator.Cameras;
using Indiv0.BlockSimulator.Common;
using Indiv0.BlockSimulator.GUI;
using Indiv0.BlockSimulator.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Indiv0.BlockSimulator
{
    public class BlockSimulatorGame : Microsoft.Xna.Framework.Game
    {
        #region Private Fields
        private IList<Manager> _managers;

        // Graphics related variables
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Keyboard input related variables
        private KeyboardState _previousKeyboardState;
        private KeyboardState _currentKeyboardState;
        #endregion

        #region Initialization
        public BlockSimulatorGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            this.Components.Add(new Camera(this));

            InitializeManagers();

            _currentKeyboardState = Keyboard.GetState();
            _previousKeyboardState = _currentKeyboardState;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //InitializeManagers();
        }
        #endregion

        #region Protected Methods
        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            //_utilityManager.Update(gameTime, this);
            foreach (Manager manager in _managers)
            {
                manager.Update(gameTime);
            }

            _previousKeyboardState = _currentKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            foreach (Manager manager in _managers)
            {
                manager.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion

        #region Private Methods
        private void InitializeManagers()
        {
            _managers = new List<Manager>();
            _managers.Add(new UtilityManager(this));
            _managers.Add(new FontManager(this));
            _managers.Add(new BlockManager(this));
        }

        private void HandleInput()
        {
            _currentKeyboardState = Keyboard.GetState();

            if (_currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (_currentKeyboardState != _previousKeyboardState)
            {
                if (_currentKeyboardState.IsKeyDown(Keys.K))
                {
                    foreach (Manager manager in _managers)
                    {
                        if (manager is BlockManager)
                        {
                            BlockManager blockManager = (BlockManager)manager;
                            Camera.ActiveCamera.Position = blockManager.Blocks[0].Position;
                        }
                    }
                    //var manager = _managers.
                    //BlockManager manager = _managers.Contains(<BlockManager);
                    //Camera.ActiveCamera.Position = _managers.Find(BlockManager manager)
                }
            }
        }
        #endregion
    }
}
