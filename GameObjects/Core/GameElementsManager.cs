using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using AR_Geometric.GameObjects.Piece;
using SpikeCS.ARUtil;
using Spike;
using Microsoft.Xna.Framework.Input;

namespace AR_Geometric.GameObjects.Core
{

    public class CameraNotConfigExecption : Exception
    {
        CameraNotConfigExecption()
            : base("Camera is not configured, Config files are not configured or there is a config error.")
        { }
    }

    class GameElementsManager : DrawableGameComponent
    {
        private List<ARPiece> pieces;
        public List<ARPiece> Pieces
        {
            get { return pieces; }
        }

        private ARCamera camera;
        private StringManager strToPrint;

        public GameElementsManager(Game game, ARCamera camera)
            : base(game)
        {
            pieces = new List<ARPiece>();
            this.camera = camera;
            strToPrint = new StringManager(game, string.Empty);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.camera.InitCameraParam();
             
            this.camera.VideoCapStart();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            camera.VideoCapNext();
            AdpCameraFrame frame = camera.VideoGetImage();

            foreach (ARPiece piece in pieces)
            {
                ARPatternInfo pattInfo = piece.Pattern.GetInfoFromFrame(frame);

                if (pattInfo != null)
                {     
                    piece.View = pattInfo.GetView(piece.Pattern.Center, piece.Pattern.Width);
                    piece.IsInTheScreen = 0;
                    


                    //Posição Esfera
                    if (piece.Type == global::GameObjects.Piece.Util.EPieceType.Sphere)
                    {
                        Vector3 trans = piece.View.Translation;
                        strToPrint.StrToPrint = trans.ToString();
                        StaticObject mesa = (StaticObject)Game.Services.GetService(typeof(StaticObject));
                          

                        if (trans.Z < -620 && trans.Z > -680 &&
                           trans.Y > -50 && trans.Y < 50 &&
                           trans.X > -42 && trans.X < 25)
                        {
                            MouseState mState = Mouse.GetState();
                           
                            if (mState.LeftButton == ButtonState.Pressed)
                            {
                                piece.IsActive = false;
                                mesa.animPieces[0].Animate();
                            }

                          
                        }

                        if (!mesa.animPieces[0].IsAnimation)
                            piece.IsActive = true;
                    }
                    else if (piece.Type == global::GameObjects.Piece.Util.EPieceType.Prism)
                    {
                        Vector3 trans = piece.View.Translation;
                        strToPrint.StrToPrint = trans.ToString();
                        StaticObject mesa = (StaticObject)Game.Services.GetService(typeof(StaticObject));


                        if (trans.Z < -620 && trans.Z > -680 &&
                           trans.Y > -50 && trans.Y < 50 &&
                           trans.X > -200 && trans.X < -100)
                        {
                            MouseState mState = Mouse.GetState();

                            if (mState.LeftButton == ButtonState.Pressed)
                            {
                                piece.IsActive = false;
                                mesa.animPieces[1].Animate();
                            }
                        }

                        if (!mesa.animPieces[0].IsAnimation)
                            piece.IsActive = true;
                    }

                }
                else
                    piece.IsInTheScreen++;

                piece.Update(gameTime);
            }            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (ARPiece piece in pieces)
                piece.Draw(gameTime);

            strToPrint.Draw(gameTime);
        }
    }
}