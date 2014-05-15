using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AR_Geometric.GameObjects.Rules.Hole
{
    public class PieceHole 
    {
        private static string CIRCLE_TEXTURE = @"texture\circle";
        private static string TRIANGLE_TEXTURE = @"texture\triangle";
        private static string SQUARE_TEXTURE = @"texture\square";

        private Texture2D texture;
        
        private Matrix transformation;
        public Matrix Transformation
        {
            set { transformation = value; }
            get { return transformation; }
        }

        private Matrix view;
        public Matrix View
        {
            set { transformation = value; }
            get { return view; }
        }



    }
}
