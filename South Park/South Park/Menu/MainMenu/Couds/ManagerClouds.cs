using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class ManagerClouds
    {
        private List<Cloud> clouds;
        private Dictionary<sbyte, String> type;
        private Dictionary<String, Texture2D> texture;
        private Dictionary<String, Rectangle> bounds;

        public ManagerClouds(Game game)
        {
            sbyte _type;

            type = new Dictionary<sbyte, String>()
            {
                { 1, "One" },
                { 2, "Two" },
                { 3, "Three" },
                { 4, "Four" },
                { 5, "Five" },
                { 6, "Six" },
            };


           

                texture = new Dictionary<String, Texture2D>()
            {
                { "One", SPHalper.ContentManager.Load<Texture2D>("Menu/MainMenu/Cloud/Cloud01") },
                { "Two", SPHalper.ContentManager.Load<Texture2D>("Menu/MainMenu/Cloud/Cloud02") },
                { "Three", SPHalper.ContentManager.Load<Texture2D>("Menu/MainMenu/Cloud/Cloud03") },
                { "Four", SPHalper.ContentManager.Load<Texture2D>("Menu/MainMenu/Cloud/Cloud04") },
                { "Five" , SPHalper.ContentManager.Load<Texture2D>("Menu/MainMenu/Cloud/Cloud05") },
                { "Six", SPHalper.ContentManager.Load<Texture2D>("Menu/MainMenu/Cloud/Cloud06") },

            };
            

            bounds = new Dictionary<String, Rectangle>()
            {
                { "One", new Rectangle(0, 0, 400, 200) },
                { "Two", new Rectangle(0, 0, 640, 240) },
                { "Three", new Rectangle(0, 0, 480, 320) },
                { "Four", new Rectangle(0, 0, 640, 220) },
                { "Five", new Rectangle(0, 0, 488, 280) },
                { "Six", new Rectangle(0, 0, 524, 280) },
            };

            clouds = new List<Cloud>();

            for (int i = 0; i < 10; i++)
            {
                _type = (sbyte)Randomize.Random.Next(1, 6);
        
                clouds.Add(new Cloud(game, type[_type], texture[type[_type]], bounds[type[_type]], new Vector2(Randomize.Random.Next(0, 1600), Randomize.Random.Next(0, 400))));

                clouds[i].RandomStep();
            }
            clouds.TrimExcess();
        }

        public int Count
        {
            get { return clouds.Count; }
        }

        public Cloud this[int i]
        {
            get { return clouds[i]; }
            set { clouds[i] = value; }
        }

        public void Update(GameTime gameTime)
        {
            for (short i = 0; i < clouds.Count; i++) clouds[i].Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            for (short i = 0; i < clouds.Count; i++) clouds[i].Draw(gameTime);   
        }
    }
}
