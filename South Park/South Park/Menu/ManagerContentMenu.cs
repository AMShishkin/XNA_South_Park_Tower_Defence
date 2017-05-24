using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace South_Park
{
    sealed class ManagerContentMenu
    {
        ManagerTextures mTextures;
        ManagerBounds mBounds;
        ManagerLocations mLocations;
        

        public ManagerContentMenu(ContentManager contentManager)
        {
            mTextures = new ManagerTextures(contentManager);
            mBounds = new ManagerBounds();
            mLocations = new ManagerLocations();
        }

        public ManagerTextures Textures
        {
            get { return mTextures; }
        }

        public ManagerBounds Bounds
        {
            get { return mBounds; }     
        }

        public ManagerLocations Locations
        {
            get { return mLocations; }
        }

    }


    sealed class ManagerTextures
    {
        private Dictionary<string, Texture2D> sprites;

        public ManagerTextures(ContentManager contentManager)
        {

            if (SPGame.graphics.GraphicsDevice != null)
            {

                sprites = new Dictionary<string, Texture2D>()
            {
                { "Background", contentManager.Load<Texture2D>("Menu/MainMenu/Background") },
                { "Moon", contentManager.Load<Texture2D>("Menu/MainMenu/Moon") },
                { "Light", contentManager.Load<Texture2D>("Menu/MainMenu/Light") },
                { "Coon", contentManager.Load<Texture2D>("Menu/MainMenu/Coon") },
                { "Mysterion", contentManager.Load<Texture2D>("Menu/MainMenu/Mysterion") },
                { "SPLogo", contentManager.Load<Texture2D>("Menu/MainMenu/Logo") },
                { "Star", contentManager.Load<Texture2D>("Star/Star") },
                { "DarkEffectUp", contentManager.Load<Texture2D>("Menu/MainMenu/DarkEffectUp") },
                { "DarkEffectDown", contentManager.Load<Texture2D>("Menu/MainMenu/DarkEffectDown") },
               // { "DarkFone", contentManager.Load<Texture2D>("LEvelInscriptions/DarkFone") },
            };
            }
        }

        public Texture2D this[string name]
        {
            get {return sprites[name];}
        }
    }

    sealed class ManagerBounds
    {
        private Dictionary<string, Rectangle> bounds;

        public ManagerBounds()
        {
            if (bounds != null)
            {
                bounds = new Dictionary<string, Rectangle>()
            {
                { "Background", new Rectangle(0, 0, SPHalper.RenderTarget.Bounds.Width, SPHalper.RenderTarget.Bounds.Height) },
                { "Moon", new Rectangle(0, 0, 400, 400) },
                { "Light", new Rectangle(0, 0, 640, 640) },
                { "Coon", new Rectangle(0, 0, 680, 420) },
                { "Mysterion", new Rectangle(0, 0, 700, 400) },
                { "SPLogo", new Rectangle(0, 0, 266, 146) },
                { "DarkEffect", new Rectangle(0, 0, SPHalper.RenderTarget.Width, 32) },


            };
            }
        }

        public Rectangle this[string name]
        { 
            get { return bounds[name]; }
        }

    }

    sealed class ManagerLocations
    {
        private Dictionary<string, Vector2> locations;

        public ManagerLocations()
        {
            if (locations != null)
            locations = new Dictionary<string, Vector2>()
            {
                { "Background", Vector2.Zero },
                { "Moon", new Vector2(SPHalper.RenderTarget.Bounds.Center.X - 200, 0) },
                { "Light", new Vector2(SPHalper.RenderTarget.Bounds.Center.X - 320, -120) },
                { "Coon", new Vector2(0, SPHalper.RenderTarget.Bounds.Height - 400) },
                { "Mysterion", new Vector2(SPHalper.RenderTarget.Bounds.Width, SPHalper.RenderTarget.Bounds.Height - 400) },
                { "SPLogo", new Vector2(30, 30) },
                { "DarkEffectUp", new Vector2(0, 0) },
                { "DarkEffectDown", new Vector2(0, SPHalper.RenderTarget.Height - 32) },
            };
        }

        public Vector2 this[string name]
        { 
            get { return locations[name]; }
            set { locations[name] = value; }
        }
    }
}