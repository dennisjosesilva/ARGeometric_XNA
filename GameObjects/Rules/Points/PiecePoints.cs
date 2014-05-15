using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AR_Geometric.GameObjects.Rules.Points
{
    public class PiecePoints
    {
        private float deltaT;
        private int nm;
        private float dp;
        
        private float pp;
        public float PP
        {
            get { return pp; }
            set { pp = value; }
        }

        public PiecePoints()
        {
            pp = 0.0f;
            dp = 150.0f;
            nm = 0;
            deltaT = 0.0f;
        }

        public PiecePoints(float defaultPiecePoints):this()
        {
            dp = defaultPiecePoints;
        }

        public void Uptade(GameTime gameTime)
        {
        }
    }
}
