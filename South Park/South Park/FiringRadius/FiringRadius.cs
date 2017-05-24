using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class FiringRadius : IGameObject
    {
        private Animation animation;

        public FiringRadius(Game game)
        {
            this.Enabled = true;
            this.LayerDepth = 1;

            animation = new Animation(game)
            {
                Texture = ManagerGameContent.Texture("FiringRadius"),
                SpriteBounds = ManagerGameContent.Rectangle("FiringRadius"),
                Size = new System.Drawing.Size(240, 240),
                Frames = 4,
                Frequency = 1,
                TimeFrame = 0.1f,
            };


        }



        public bool Enabled { get; set; }
        public Vector2 Location { get; set; }

        public float LayerDepth { get; set; }

        public Rectangle GetBounds()
        {
            return ManagerGameContent.Rectangle("FiringRadius");
        }

        public System.Drawing.Size Size()
        {
            return new System.Drawing.Size();
        }


        public bool CheckCollision(Rectangle mask)
        {
            return ManagerGameContent.Rectangle("FiringRadius").Intersects(mask);
        }

        public void Update(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                animation.Construct(this.Location);
                animation.Update(gameTime);
            }
        }
             
        public void Draw(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                animation.Draw(gameTime);

               // SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("FiringRadius"), this.Location, this.GetBounds(), Color.White);
            }
        }
    }
}

