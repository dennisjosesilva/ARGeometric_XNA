using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects.Piece.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AR_Geometric.GameObjects.Core;

namespace AR_Geometric.GameObjects.Animation
{
    public class AnimPiece
    {
        private static string PRISM_MODEL = "prism";
        private static string CUBE_MODEL = "cube";
        private static string SPHERE_MODEL = "sphere";
        
        private EPieceType myType;
        private Model model;
        private float aspectRatio;
        private bool isAnimation;
        public bool IsAnimation
        {
            get { return isAnimation; }
        }

        private Matrix transfomation;
        private Matrix initialTransformation;
        private int animPass;
        private int animNumberOfPasses;

        public AnimPiece(Matrix initialTransform, EPieceType type, float aspectRatio)
        {
            this.initialTransformation = this.transfomation = initialTransform;
            this.myType = type;
            this.aspectRatio = aspectRatio;

            load();
            resetObject();
        }

        protected void load()
        {
            
            switch(myType)
            {
                case EPieceType.Prism:
                    model = ContentSingleton.Singleton.Content.Load<Model>(PRISM_MODEL);
                    break;
                case EPieceType.Cube:
                    model = ContentSingleton.Singleton.Content.Load<Model>(CUBE_MODEL);
                    break;
                case EPieceType.Sphere:
                    model = ContentSingleton.Singleton.Content.Load<Model>(SPHERE_MODEL);
                    break;
            }

            isAnimation = false;
            animPass = 0;
            animNumberOfPasses = 150;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = initialTransformation;
                    effect.View = Camera.Singleton.ViewMatrix;
                    effect.Projection = Camera.Singleton.PerspectiveMatrix;
                    effect.EnableDefaultLighting();
                }
                
            }

        }

        public void Animate()
        {
            resetObject();
            isAnimation = true;
        }


        public void Update(GameTime gameTime)
        {
            if (isAnimation)
            {
                
                animPass++;
                
                if (animPass > animNumberOfPasses)
                    resetObject();
            }
        }

        protected void resetObject()
        {
            isAnimation = false;
            transfomation = Matrix.CreateTranslation(new Vector3(0, 3, 0)); ;
            animPass = 0;
        }

        public void Draw(GameTime gameTime)
        {
            if (isAnimation)
            {
                Matrix[] transfoms = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(transfoms);
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.World *=  transfomation;
                    }
                    mesh.Draw();
                }
            }
        }
    }
}
