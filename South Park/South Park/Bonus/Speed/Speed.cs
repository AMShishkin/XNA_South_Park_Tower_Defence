using C3.XNA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    // Нарисовать анимаю аптечки (летящие +)


    class Speed : IGameObject
    {
        private Animation animation;
        private int timer;
        private Rectangle bounds;


        public Speed(Game game)
        {
            this.Enabled = true;
            this.Location = Vector2.Zero;
            this.LayerDepth = 1;
            this.Rotation = new Vector2(Randomize.Random.Next(-5, 5), -8);
            this.Gravity = new Vector2(0, 0.2f);


            bounds = ManagerGameContent.Rectangle("GoldenMonet");


            animation = new Animation(game, SPHalper.ContentManager.Load<Texture2D>("Bonus/Speed/S"), new System.Drawing.Size(32, 32),
                                      this.Location, 8, 1);
            timer = 0;
        }

        public float LayerDepth { get; set; }

        public bool Enabled { get; set; }

        public Vector2 Location { get; set; }

        public Vector2 Gravity { get; set; }

        public Vector2 Rotation { get; set; }





        public System.Drawing.Size Size()
        {
            return animation.Size;
        }



        public bool CheckCollision(Rectangle mask)
        {
            return bounds.Intersects(mask);
        }


        public Rectangle GetBounds()
        {
            return bounds;
        }


        int rd = 44;


        public void Update(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                if (timer < 850)
                {
                    timer++;


                    if (timer < rd)
                    {

                        this.Rotation += this.Gravity / 0.5f;
                        this.Location += this.Rotation;
                    }


                    bounds.X = (int)this.Location.X;
                    bounds.Y = (int)this.Location.Y;


                    this.LayerDepth = (this.Location.Y + 31) / SPHalper.RenderTarget.Bounds.Height;

                    animation.Construct(this.Location, this.LayerDepth);
                    animation.Update(gameTime);
                }
                else
                {
                    rd = Randomize.Random.Next(44, 48);
                    this.Rotation = new Vector2(Randomize.Random.Next(-5, 5), -8);
                    timer = 0;
                    this.Enabled = true;
                }
            }
        }





        public void Draw(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                animation.Draw(gameTime);
                if (SPHalper.EditorState) SPHalper.SpriteBatch.DrawRectangle(bounds, Color.Lime);
            }
        }
    }
}

