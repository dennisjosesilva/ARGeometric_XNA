using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameObjects.Piece.Util;
using AR_Geometric.GameObjects.Rules.Points;
using SpikeCS.ARUtil;
using SpikeCS.Transformation;
using AR_Geometric.GameObjects.Core;

namespace AR_Geometric.GameObjects.Piece
{
    public class ARPiece
    {
        #region Attributes
        private static  string PRISM_MODEL = "prism";
        private static  string CUBE_MODEL = "cube";
        private static  string SPHERE_MODEL = "sphere";

        private static string PRISM_PATTERN = "prism.patt";
        private static string CUBE_PATTERN = "cube.patt";
        private static string SPHERE_PATTERN = "sphere.patt";

        private Matrix world;
        public Matrix World
        {
            get { return world; }
            set { world = value; }
        }

        private Matrix view;
        public Matrix View
        {
            get { return view; }
            set { view = value; }
        }

        private Model m;
        private EPieceType type;
        public EPieceType Type
        {
            get { return type; }
        }
        
        private PiecePoints points;

        public double currentPoints
        {
            get { return points.PP; }
        }

        private ARPattern pattern;
        public ARPattern Pattern
        {
            get { return pattern; }
            set { pattern = value; }
        }

        private int isInTheScreen;
        public int IsInTheScreen
        {
            get { return isInTheScreen; }
            set { isInTheScreen = value; }
        }

        private float aspectRatio;

        private bool isActive;
        public bool IsActive
        {
            get 
            { 
                
                return isActive; 
            }
            set 
            {
                if (value == true)
                    configWorldMatrix();
                isActive = value; 
            }
        }

        #endregion

        public ARPiece(EPieceType type, Matrix world, float aspectRatio)
        {
            this.isInTheScreen = 0;
            this.type = type;
            this.world = world;
            this.aspectRatio = aspectRatio;
            this.view = Matrix.CreateLookAt(new Vector3(55, 69, 500), Vector3.Zero, Vector3.Up); ;
            switch (this.type)
            {
                case EPieceType.Cube:
                    this.m = ContentSingleton.Singleton.Content.Load<Model>(ARPiece.CUBE_MODEL);
                    this.pattern = new ARPattern(ARPiece.CUBE_PATTERN, 80.0, 100, Vector2.Zero);
                    break;

                case EPieceType.Prism:
                    this.m = ContentSingleton.Singleton.Content.Load<Model>(ARPiece.PRISM_MODEL);
                    this.pattern = new ARPattern(ARPiece.PRISM_PATTERN, 80.0, 100, Vector2.Zero);
                    break;

                case EPieceType.Sphere:
                    this.m = ContentSingleton.Singleton.Content.Load<Model>(ARPiece.SPHERE_MODEL);
                    this.pattern = new ARPattern(ARPiece.SPHERE_PATTERN, 80.0, 100, Vector2.Zero);
                    break;

                default:
                    this.m = null;
                    this.pattern = null;
                    break;
            }

            isActive = true;
            configWorldMatrix();
        }

        private void configWorldMatrix()
        {
            Matrix[] transforms = new Matrix[m.Bones.Count];
            m.CopyAbsoluteBoneTransformsTo(transforms);


            foreach (ModelMesh mesh in m.Meshes)
            {

                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = transforms[mesh.ParentBone.Index] * world;
                    effect.View = view;
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60.0f), aspectRatio, 1.0f, 10000.0f);
                    effect.EnableDefaultLighting();
                }
            }
        }

        public void Update(GameTime gameTime)
        {}

        public void Draw(GameTime gameTime)
        {

            if (isActive)
            {
                if (isInTheScreen < 10)
                {
                    foreach (ModelMesh mesh in this.m.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.View = this.view;
                            effect.World = this.world;
                        }
                        mesh.Draw();
                    }
                }
            }
        }
    }
}