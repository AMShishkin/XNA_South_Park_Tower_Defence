using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;


namespace South_Park
{
    class ManagerContentMenuCost
    {
        private Dictionary<string, Texture2D> sprites;
        private Dictionary<string, Rectangle> bounds;

        public ManagerContentMenuCost(ContentManager contentManager)
        {
            sprites = new Dictionary<string, Texture2D>()
            {
                { "MenuCost", contentManager.Load<Texture2D>("MenuCost/MenuCost") },
                { "ElectricTower", contentManager.Load<Texture2D>("MenuCost/Towers/ElectricTower") },

                { "1", contentManager.Load<Texture2D>("MenuCost/1") },
                { "2", contentManager.Load<Texture2D>("MenuCost/2") },
                { "3", contentManager.Load<Texture2D>("MenuCost/3") },

            };

            bounds = new Dictionary<string, Rectangle>()
            {
                { "MenuCost", new Rectangle(0, 0, 240, 120) },
                { "Tower", new Rectangle(0, 0, 80, 100) },
            };
        }


        public Texture2D Texture(string key)
        {
            return sprites[key];
        }

        public Rectangle Bounds(string key)
        {
            return bounds[key];
        }

    } 
}