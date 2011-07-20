using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Indiv0.BlockSimulator.Blocks
{
    abstract class Block
    {
        #region Constants
        private const int NUM_TRIANGLES = 12;
        private const int NUM_VERTICES = 36;
        #endregion

        #region Private Fields
        private Vector3 _size;
        private Vector3 _position;
        private Vector3 _texture;

        // Array of vertex information - contains position, normal and texture data
        private VertexPositionNormalTexture[] _vertices;

        // The vertex buffer where we load the vertices before drawing the shape
        private VertexBuffer _shapeBuffer;

        // Lets us check if the data has been constructed or not to improve performance
        private bool _isConstructed = false;
        #endregion

        #region Properties
        public Vector3 Size {
            get { return _size; }
            set { _size = value; } 
        }
        public Vector3 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        public Vector3 Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the size and position parameters for this cube.
        /// </summary>
        /// <param name="size">A Vector3 object representing the size in each dimension</param>
        /// <param name="position">A Vector3 object representing the position</param>
        public Block(Vector3 size, Vector3 position)
        {
            _size = size;
            _position = position;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Writes our list of vertices to the vertex buffer, 
        /// then draws triangles to the device
        /// </summary>
        /// <param name="device"></param>
        public void RenderToDevice(GraphicsDevice graphics)
        {
            // Build the cube, setting up the _vertices array
            if (_isConstructed == false)
                ConstructCube();
            
            // Create the shape buffer and dispose of it to prevent out of memory
            using (VertexBuffer buffer = new VertexBuffer(
                graphics,
                VertexPositionNormalTexture.VertexDeclaration,
                NUM_VERTICES,
                BufferUsage.WriteOnly))
            {
                // Load the buffer
                buffer.SetData(_vertices);

                // Send the vertex buffer to the device
                graphics.SetVertexBuffer(buffer);
            }

            // Draw the primitives from the vertex buffer to the device as triangles
            graphics.DrawPrimitives(PrimitiveType.TriangleList, 0, NUM_TRIANGLES);   
        }
        #endregion

        #region Private Methods
        private void ConstructCube()
        {
            _vertices = new VertexPositionNormalTexture[NUM_VERTICES];

            // Calculate the position of the vertices on the top face.
            Vector3 topLeftFront = _position + new Vector3(-1.0f, 1.0f, -1.0f) * _size;
            Vector3 topLeftBack = _position + new Vector3(-1.0f, 1.0f, 1.0f) * _size;
            Vector3 topRightFront = _position + new Vector3(1.0f, 1.0f, -1.0f) * _size;
            Vector3 topRightBack = _position + new Vector3(1.0f, 1.0f, 1.0f) * _size;

            // Calculate the position of the vertices on the bottom face.
            Vector3 btmLeftFront = _position + new Vector3(-1.0f, -1.0f, -1.0f) * _size;
            Vector3 btmLeftBack = _position + new Vector3(-1.0f, -1.0f, 1.0f) * _size;
            Vector3 btmRightFront = _position + new Vector3(1.0f, -1.0f, -1.0f) * _size;
            Vector3 btmRightBack = _position + new Vector3(1.0f, -1.0f, 1.0f) * _size;

            // Normal vectors for each face (needed for lighting / display)
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f) * _size;
            Vector3 normalBack = new Vector3(0.0f, 0.0f, -1.0f) * _size;
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f) * _size;
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f) * _size;
            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f) * _size;
            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f) * _size;

            // UV texture coordinates
            Vector2 textureTopLeft = new Vector2(1.0f * _size.X, 0.0f * _size.Y);
            Vector2 textureTopRight = new Vector2(0.0f * _size.X, 0.0f * _size.Y);
            Vector2 textureBottomLeft = new Vector2(1.0f * _size.X, 1.0f * _size.Y);
            Vector2 textureBottomRight = new Vector2(0.0f * _size.X, 1.0f * _size.Y);

            // Add the vertices for the FRONT face.
            _vertices[0] = new VertexPositionNormalTexture(topLeftFront, normalFront, textureTopLeft);
            _vertices[1] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            _vertices[2] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);
            _vertices[3] = new VertexPositionNormalTexture(btmLeftFront, normalFront, textureBottomLeft);
            _vertices[4] = new VertexPositionNormalTexture(btmRightFront, normalFront, textureBottomRight);
            _vertices[5] = new VertexPositionNormalTexture(topRightFront, normalFront, textureTopRight);

            // Add the vertices for the BACK face.
            _vertices[6] = new VertexPositionNormalTexture(topLeftBack, normalBack, textureTopRight);
            _vertices[7] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[8] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[9] = new VertexPositionNormalTexture(btmLeftBack, normalBack, textureBottomRight);
            _vertices[10] = new VertexPositionNormalTexture(topRightBack, normalBack, textureTopLeft);
            _vertices[11] = new VertexPositionNormalTexture(btmRightBack, normalBack, textureBottomLeft);

            // Add the vertices for the TOP face.
            _vertices[12] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            _vertices[13] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);
            _vertices[14] = new VertexPositionNormalTexture(topLeftBack, normalTop, textureTopLeft);
            _vertices[15] = new VertexPositionNormalTexture(topLeftFront, normalTop, textureBottomLeft);
            _vertices[16] = new VertexPositionNormalTexture(topRightFront, normalTop, textureBottomRight);
            _vertices[17] = new VertexPositionNormalTexture(topRightBack, normalTop, textureTopRight);

            // Add the vertices for the BOTTOM face. 
            _vertices[18] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            _vertices[19] = new VertexPositionNormalTexture(btmLeftBack, normalBottom, textureBottomLeft);
            _vertices[20] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            _vertices[21] = new VertexPositionNormalTexture(btmLeftFront, normalBottom, textureTopLeft);
            _vertices[22] = new VertexPositionNormalTexture(btmRightBack, normalBottom, textureBottomRight);
            _vertices[23] = new VertexPositionNormalTexture(btmRightFront, normalBottom, textureTopRight);

            // Add the vertices for the LEFT face.
            _vertices[24] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);
            _vertices[25] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            _vertices[26] = new VertexPositionNormalTexture(btmLeftFront, normalLeft, textureBottomRight);
            _vertices[27] = new VertexPositionNormalTexture(topLeftBack, normalLeft, textureTopLeft);
            _vertices[28] = new VertexPositionNormalTexture(btmLeftBack, normalLeft, textureBottomLeft);
            _vertices[29] = new VertexPositionNormalTexture(topLeftFront, normalLeft, textureTopRight);

            // Add the vertices for the RIGHT face. 
            _vertices[30] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            _vertices[31] = new VertexPositionNormalTexture(btmRightFront, normalRight, textureBottomLeft);
            _vertices[32] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);
            _vertices[33] = new VertexPositionNormalTexture(topRightBack, normalRight, textureTopRight);
            _vertices[34] = new VertexPositionNormalTexture(topRightFront, normalRight, textureTopLeft);
            _vertices[35] = new VertexPositionNormalTexture(btmRightBack, normalRight, textureBottomRight);

            _isConstructed = true;
        }
        #endregion
    }
}