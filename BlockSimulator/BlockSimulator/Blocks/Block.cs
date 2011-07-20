using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JigLibX.Physics;
using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Math;
using Microsoft.Xna.Framework.Graphics;
using Indiv0.BlockSimulator.Cameras;
using Indiv0.BlockSimulator.Physics;

namespace Indiv0.BlockSimulator.Blocks
{
    public abstract class Block : PhysicsObject
    {
        #region Initialization
        public Block(BlockSimulatorGame game, Vector3 position, Vector3 scale, string modelDir)
            : base(game, position, scale, modelDir)
        {
            Box box = new Box(Position, Matrix.Identity, Scale);
            Skin.AddPrimitive(box, new MaterialProperties(
                0.8f, // elasticity
                0.8f, // static roughness
                0.7f  // dynamic roughness
                ));

            Vector3 com = SetMass(1.0f);
            Body.MoveTo(Position, Matrix.Identity);

            Skin.ApplyLocalTransform(new Transform(-com, Matrix.Identity));
            Body.EnableBody();

            PhysicsSystem.CurrentPhysicsSystem.AddBody(Body);

            LoadContent(game, modelDir);
        }

        protected override void LoadContent(BlockSimulatorGame game, string modelDir)
        {
            base.LoadContent(game, modelDir);
        }
        #endregion
    }
}