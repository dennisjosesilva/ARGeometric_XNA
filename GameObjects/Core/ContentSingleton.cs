using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;

namespace AR_Geometric.GameObjects.Core
{
    public class ContentSingleton
    {
        public class HaveNoContentManagerException : Exception
        {
            public HaveNoContentManagerException()
                : base("This singlenton Have No Content Manager")
            { }
        }

        private static ContentSingleton singleton;
        public static ContentSingleton Singleton
        {
            get 
            {
                if (singleton == null)
                    singleton = new ContentSingleton();
                return ContentSingleton.singleton; 
            }
        }

        private ContentManager content;
        public ContentManager Content
        {
            get 
            {
                if (content == null)
                    throw new HaveNoContentManagerException();
                return content; 
            }
            set { content = value; }
        }

        public ContentSingleton()
        {

        }

    }
}
