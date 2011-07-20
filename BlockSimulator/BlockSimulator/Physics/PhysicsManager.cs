using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JigLibX.Physics;
using JigLibX.Collision;
using Microsoft.Xna.Framework;
using Indiv0.BlockSimulator.Common;

namespace Indiv0.BlockSimulator.Physics
{
    class PhysicsManager : Manager
    {
        #region Private Fields
        private PhysicsSystem _physicsSystem;
        private bool _updatePhysics = true;
        #endregion

        #region Initialization
        public PhysicsManager(BlockSimulatorGame game)
            : base(game)
        {
            _physicsSystem = new PhysicsSystem();
            _physicsSystem.CollisionSystem = new CollisionSystemSAP();
        }
        #endregion

        #region Public Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Update the physics system
            if (_updatePhysics)
            {
                float timeStep = (float)gameTime.ElapsedGameTime.Ticks / TimeSpan.TicksPerSecond;
                PhysicsSystem.CurrentPhysicsSystem.Integrate(timeStep);
            }
        }
        #endregion
    }
}
