using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace South_Park
{
    class ManagerGameContent
    {
        private static Dictionary<String, Texture2D> texture;
        private static Dictionary<String, Rectangle> rBound;

        

        static public void Initialize()
        {
            texture = new Dictionary<String, Texture2D>
            {
                { "Panel", SPHalper.ContentManager.Load<Texture2D>("GameInterface/Panel") },
                { "Arrow", SPHalper.ContentManager.Load<Texture2D>("Arrow/Arrow") },
                { "HealthStripGood", SPHalper.ContentManager.Load<Texture2D>("HealthStrip/HSGood/HSGood") },
                { "HealthStripNormal", SPHalper.ContentManager.Load<Texture2D>("HealthStrip/HSNormal/HSNormal") },
                { "HealthStripBad", SPHalper.ContentManager.Load<Texture2D>("HealthStrip/HSBad/HSBad") },
                { "HealthStripBorder", SPHalper.ContentManager.Load<Texture2D>("HealthStrip/HSBorder/HSBorder") },
                { "Blood", SPHalper.ContentManager.Load<Texture2D>("Blood/Blood1") },
                { "FiringRadius", SPHalper.ContentManager.Load<Texture2D>("FiringRadius/FiringRadius") },
                { "CircleFireArea", SPHalper.ContentManager.Load<Texture2D>("FiringRadius/FireArea") },

                { "Bang01", SPHalper.ContentManager.Load<Texture2D>("Bangs/Bang01") },
                { "Bang02", SPHalper.ContentManager.Load<Texture2D>("Bangs/Bang02") },
                { "Bang03", SPHalper.ContentManager.Load<Texture2D>("Bangs/Bang03") },
                { "Bang04", SPHalper.ContentManager.Load<Texture2D>("Bangs/Bang04") },
                { "Bang05", SPHalper.ContentManager.Load<Texture2D>("Bangs/Bang05") },

               { "Lightning", SPHalper.ContentManager.Load<Texture2D>("Lightning/Lig") },
               { "MenuCost", SPHalper.ContentManager.Load<Texture2D>("MenuCost/MenuCost") },
                
                
            };

            rBound = new Dictionary<String, Rectangle>
            {
                { "Panel", new  Rectangle(SPHalper.RenderTarget.Width / 2 - Texture("Panel").Width / 2,
                                          SPHalper.RenderTarget.Height - Texture("Panel").Height, Texture("Panel").Width, Texture("Panel").Height) },
                { "Arrow", new Rectangle(0, 0, 40, 100) }, 
                { "Bang01", new Rectangle(0, 0, 32, 32) },   
                { "Blood", new Rectangle(0, 0, 80, 80) },  
                { "Lightning", new Rectangle(0, 0, 120, 20) }, 
                { "FiringRadius", new Rectangle(0, 0, 240, 240) },
                { "GoldenMonet", new Rectangle(0, 0, 32, 32) },
                { "MenuCost", new Rectangle(0, 0, 200, 100) },
            };




        }

        static public Texture2D Texture(string name)
        {
            return texture[name]; 
        }


        public static Rectangle Rectangle(string name)
        {
            return rBound[name];
        }
    }
}
