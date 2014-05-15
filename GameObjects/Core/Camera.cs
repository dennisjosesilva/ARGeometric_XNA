using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AR_Geometric.GameObjects.Core
{
    public class Camera
    {
        private Matrix perspectiveMatrix;
        public Matrix PerspectiveMatrix
        {
            get { return perspectiveMatrix; }
        }

        private Matrix viewMatrix;
        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
        }

        private static Camera singleton;
        public static Camera Singleton
        {
            get 
            {
                if (singleton == null)
                    singleton = new Camera();
                return Camera.singleton; 
            }
        }

        public Camera()
        {
        }

        public void Init(float aspectRatio)
        {
            this.perspectiveMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 1.0f, 10000.0f);
            this.viewMatrix = Matrix.CreateLookAt(new Vector3(0, 300, 1000), Vector3.Zero, Vector3.Up);
        }
    }
}
