using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class ManagerHero
    {
        private System.Drawing.Size size;
        private Rectangle bounds;

        public ManagerHero()
        {
            size = new System.Drawing.Size(80, 80);
            bounds = new Rectangle(0, 0, 40, 10);
        }

        public Vector2 Movement(MovementDirectionCondition direction, Vector2 location, float step)
        {
            switch (direction)
            {
                case MovementDirectionCondition.MovingLeft: location.X -= step; return location;
                case MovementDirectionCondition.MovingLeftUp: location.X -= step; location.Y -= step; return location;
                case MovementDirectionCondition.MovingUp: location.Y -= step; return location;
                case MovementDirectionCondition.MovingRightUp: location.X += step; location.Y -= step; return location;
                case MovementDirectionCondition.MovingRight: location.X += step; return location;
                case MovementDirectionCondition.MovingRightDown: location.X += step; location.Y += step; return location;
                case MovementDirectionCondition.MovingDown: location.Y += step; return location;
                case MovementDirectionCondition.MovingLeftDown: location.X -= step; location.Y += step; return location;
                default: return Vector2.Zero;
            }
        }

        public System.Drawing.Size Size()
        {
            return size;
        }

        public Rectangle Bounds(Vector2 location)
        {
            bounds.X = (int)location.X + 20;
            bounds.Y = (int)location.Y + 70;
            return bounds;
        }

        public void LoadContent(ContentManager contentManager, ref Dictionary<String, Texture2D> texture)
        {
            texture.Add("EMGDown", contentManager.Load<Texture2D>("Eric/Movement/Down/EMGDown"));
            texture.Add("EMNDown", contentManager.Load<Texture2D>("Eric/Movement/Down/EMNDown"));
            texture.Add("EMBDown", contentManager.Load<Texture2D>("Eric/Movement/Down/EMBDown"));

            texture.Add("EMUp", contentManager.Load<Texture2D>("Eric/Movement/EMUp"));
            texture.Add("EMLeftRight", contentManager.Load<Texture2D>("Eric/Movement/EMLeftRight"));

            texture.Add("ESGood", contentManager.Load<Texture2D>("Eric/Stopped/ESGood"));
            texture.Add("ESNormal", contentManager.Load<Texture2D>("Eric/Stopped/ESNormal"));
            texture.Add("ESBad", contentManager.Load<Texture2D>("Eric/Stopped/ESBad"));

            texture.Add("CMDown", contentManager.Load<Texture2D>("Eric/Coon/Movement/CMDown"));
            texture.Add("CMUp", contentManager.Load<Texture2D>("Eric/Coon/Movement/CMUp"));
            texture.Add("CMLeftRight", contentManager.Load<Texture2D>("Eric/Coon/Movement/CMLeftRight"));

            texture.Add("CSDown", contentManager.Load<Texture2D>("Eric/Coon/Stopped/CSDown"));
        }
    }
}