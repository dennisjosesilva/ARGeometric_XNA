using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AR_Geometric.GameObjects.Animation;

namespace AR_Geometric.GameObjects.Core
{
    public class StaticObject : DrawableGameComponent
    {
        private Model model;
        private Matrix transform;
        private StringManager str;

        public List<AnimPiece> animPieces;

        public StaticObject(Game game, Model model, Matrix transform)
            :base(game)
        {
            this.model = model;
            this.transform = transform;

            animPieces = new List<AnimPiece>();
            animPieces.Add(new AnimPiece(
                Matrix.Identity * Matrix.CreateScale(80) * Matrix.CreateTranslation(0, 30, -70),
                global::GameObjects.Piece.Util.EPieceType.Sphere,
                game.GraphicsDevice.Viewport.AspectRatio));

            animPieces.Add(new AnimPiece(
                Matrix.Identity * Matrix.CreateScale(80) * Matrix.CreateTranslation(-215, 30, -25),
                global::GameObjects.Piece.Util.EPieceType.Prism,
                game.GraphicsDevice.Viewport.AspectRatio));

           animPieces.Add(new AnimPiece(
             Matrix.Identity * Matrix.CreateScale(63) * Matrix.CreateTranslation(210, 30, -100),
             global::GameObjects.Piece.Util.EPieceType.Cube,
             game.GraphicsDevice.Viewport.AspectRatio));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();


            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
                
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = transform;
                    effect.View = Camera.Singleton.ViewMatrix;

                    effect.Projection = Camera.Singleton.PerspectiveMatrix;
                    effect.EnableDefaultLighting();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (AnimPiece ap in animPieces)
                ap.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
          
            
            foreach (ModelMesh mesh in model.Meshes)
                mesh.Draw();


            foreach (AnimPiece ap in animPieces)
                ap.Draw(gameTime);
        }
    }
}
