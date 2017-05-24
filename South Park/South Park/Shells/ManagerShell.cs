using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace South_Park
{
    class ManagerShell
    {
        private static Dictionary<String, Texture2D> texture;

        private static Dictionary<String, Rectangle> rBounds;
        private static Dictionary<String, float> damage;

        public static void Initialize()
        {
         

            texture = new Dictionary<String, Texture2D>
            {
                { "None", null },
                { "ElectricTowerLevel1", SPHalper.ContentManager.Load<Texture2D>("Star/Star") },
            };

            rBounds = new Dictionary<String, Rectangle>
            {
                { "None", new Rectangle(0, 0, 0, 0) },
                { "ElectricTowerLevel1", new Rectangle(0, 0, 16, 16) },
            };

            damage = new Dictionary<String, float>
            {
                { "None", 0f },
                { "ElectricTowerLevel1", 10f },
            };



        }



        public static Texture2D Texture(string shellType)
        {
            return texture[shellType];
        }

     

        public static Rectangle Bounds(string shellType)
        {
            return rBounds[shellType.ToString()];
        }

        public static float Damage(string shelltype)
        {
            return damage[shelltype];
        }
    }
}
