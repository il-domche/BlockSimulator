using JigLibX.Collision;
using JigLibX.Geometry;
using JigLibX.Physics;
using Microsoft.Xna.Framework;
using JigLibX.Math;
using Microsoft.Xna.Framework.Graphics;
using Indiv0.BlockSimulator.Cameras;

namespace Indiv0.BlockSimulator.Physics
{
    public abstract class PhysicsObject
    {
        #region Private Fields
        private Vector3 _position, _scale;
        private Model _model;
        private Body _body;
        private CollisionSkin _skin;
        #endregion

        #region Public Properties
        public Body Body {
            get { return _body; }
            set { _body = value; }
        }
        public CollisionSkin Skin
        {
            get { return _skin; }
            set { _skin = value; }
        }
        public Model Model {
            get { return _model; }
            set { _model = value; }
        }
        public Vector3 Position {
            get { return _position; }
            set { _position = value; }
        }
        public Vector3 Scale {
            get { return _scale; }
            set { _scale = value; }
        }
        #endregion

        #region Initialization
        public PhysicsObject(BlockSimulatorGame game, Vector3 position, Vector3 scale, string modelDir)
        {
            _position = position;
            _scale = scale;

            Body = new Body();
            Skin = new CollisionSkin(Body);

            Body.CollisionSkin = Skin;

            //Vector3 com = SetMass(1.0f);

            //Body.MoveTo(_position, Matrix.Identity);

            //Skin.ApplyLocalTransform(new Transform(-com, Matrix.Identity));
            //Body.EnableBody();
        }

        protected virtual void LoadContent(BlockSimulatorGame game, string modelDir)
        {
            _model = game.Content.Load<Model>(modelDir);
        }
        #endregion

        #region Public Methods
        public void Draw(GameTime gameTime)
        {
            Matrix[] transforms = new Matrix[_model.Bones.Count];

            _model.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix worldMatrix = this.GetWorldMatrix();

            foreach (ModelMesh mesh in _model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;
                    effect.World = transforms[mesh.ParentBone.Index] * worldMatrix;
                    //effect.View = game.View;
                    //effect.Projection = game.Projection;
                    effect.View = Camera.ActiveCamera.View;
                    //effect.World = Camera.ActiveCamera.World;
                    //effect.World = transforms[mesh.ParentBone.Index] * Camera.ActiveCamera.World;
                    effect.Projection = Camera.ActiveCamera.Projection;
                }
                mesh.Draw();
            }
        }
        #endregion

        #region Protected Methods
        protected Vector3 SetMass(float mass)
        {
            PrimitiveProperties primitiveProperties = new PrimitiveProperties(
                PrimitiveProperties.MassDistributionEnum.Solid,
                PrimitiveProperties.MassTypeEnum.Mass,
                mass);

            float junk;
            Vector3 com;
            Matrix it, itCom;

            Skin.GetMassProperties(primitiveProperties, out junk, out com, out it, out itCom);

            Body.BodyInertia = itCom;
            Body.Mass = junk;

            return com;
        }
        #endregion

        #region Private Methods
        private Matrix GetWorldMatrix()
        {
            return Matrix.CreateScale(_scale) * this.Skin.GetPrimitiveLocal(0).Transform.Orientation * this.Body.Orientation * Matrix.CreateTranslation(this.Body.Position);
        }
        #endregion
    }
}
