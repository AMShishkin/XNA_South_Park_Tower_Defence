using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace South_Park
{
    class ManagerGameInscription
    {
        private static int timer;
        private static Texture2D[] texture;
        private static Point LocationUp, LocationDown;
        private static Rectangle RectangleBoxUp, RectangleBoxDown, rText, rCoonPower;
        private static Dictionary<String, Action<GameTime>> inscription;


        static ManagerGameInscription()

        {
            timer = 200;
            texture = new Texture2D[4];
            IsActive = true;
            LocationUp = new Point(-2, -2);
            LocationDown = new Point(-2, SPHalper.RenderTarget.Bounds.Height - 79);
            RectangleBoxUp = new Rectangle(LocationUp.X, LocationUp.Y, SPHalper.RenderTarget.Bounds.Width + 5, 80);
            RectangleBoxDown = new Rectangle(LocationDown.X, LocationDown.Y, SPHalper.RenderTarget.Bounds.Width + 5, 80);
            rText = new Rectangle(SPHalper.RenderTarget.Bounds.Center.X - 140, SPHalper.RenderTarget.Bounds.Center.Y - 80, 280, 160);
            rCoonPower = new Rectangle(SPHalper.RenderTarget.Bounds.Center.X - 240, SPHalper.RenderTarget.Bounds.Center.Y - 92, 480, 184);
            InscriptionCondition = "LevelStart";
            inscription = new Dictionary<String, Action<GameTime>>
            {
                { "LevelStart", DrawLevelStartInscription },
                { "CoonPower" , DrawCoonPowerInscription },
            };


        }

        public static Vector2 Location { get; set; }

        public static bool IsActive { get; set; }

        public static string InscriptionCondition { get; set; }

        public static void LevelStartInscription(Vector2 location)
        {
            timer = 0;
            texture[0] = SPHalper.ContentManager.Load<Texture2D>("LevelInscriptions/LevelStart");
            texture[1] = SPHalper.ContentManager.Load<Texture2D>("LevelInscriptions/DarkFone");
            

            Location = location;
        }

        public static void CPowerInscription(Vector2 location)
        {
            timer = 0;
            texture[2] = SPHalper.ContentManager.Load<Texture2D>("LevelInscriptions/CoonPower");
            Location = location;
        }


        public static void Update(GameTime gameTime)
        {
            if (timer != 200) timer++;
            else IsActive = false;

            if (timer >= 180)
            {
                LocationUp.Y -= 8;
                LocationDown.Y += 8;
                RectangleBoxUp.Location = LocationUp;
                RectangleBoxDown.Location = LocationDown;
            }
        }



        private static void DrawLevelStartInscription(GameTime gameTime)
        {
            SPHalper.SpriteBatch.Draw(texture[0], rText, Color.White);
        }


        private static void DrawCoonPowerInscription(GameTime gameTime)
        {
            SPHalper.SpriteBatch.Draw(texture[2], rCoonPower, Color.White);
        }


        public static void Draw(GameTime gameTime)
        {
            if (timer != 200)
            {
                SPHalper.SpriteBatch.Draw(texture[1], RectangleBoxUp, Color.White);
                inscription[InscriptionCondition](gameTime);
                SPHalper.SpriteBatch.Draw(texture[1], RectangleBoxDown, Color.White);
            }

        }
    }
}
