using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class Arrow : IGameObject
    {
        private Animation animation;



        public Arrow(Game game)
        {
            this.Enabled = true;
            this.LayerDepth = 1;


            animation = new Animation(game)
            {
                Size = new System.Drawing.Size(40, 100),
                SpriteBounds = ManagerGameContent.Rectangle("Arrow"),
                Frames = 11,
                Texture = ManagerGameContent.Texture("Arrow"),
                Frequency = 10,
            };

            
        }



        public bool Enabled { get; set; }
        public Vector2 Location { get; set; }

        public float LayerDepth { get; set; }

        public Rectangle GetBounds()
        {
            return ManagerGameContent.Rectangle("Arrow");
        }

        public System.Drawing.Size Size()
        {
            return new System.Drawing.Size();
        }


        public bool CheckCollision(Rectangle mask)
        {
            return ManagerGameContent.Rectangle("Arrow").Intersects(mask);
        }

        public void Update(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                animation.Construct(this.Location, this.LayerDepth);
                animation.Update(gameTime);
            }
        }
             
        public void Draw(GameTime gameTime)
        {
            if (!this.Enabled) animation.Draw(gameTime);
        }
    }
}
