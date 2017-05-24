using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace South_Park
{
    class Shell : IGameObject, IShell
    {

        private Animation animation;

        private Vector2 location;
        private int timer = 0;

        public Shell(Game game)
        {
            this.Enabled = true;
            this.Location = new Vector2(150, 150);
            this.Direction = 0;
            this.Type = "ElectricTowerLevel1";

            this.Damage = ManagerShell.Damage(this.Type);
    
            this.Bounds = ManagerShell.Bounds(this.Type);

          //  this.Rotation = new Vector2(10, -1);
            this.Gravity = new Vector2(0, 0.1f);
            this.LayerDepth = 1;

        }


        public Rectangle Bounds { get; set; }

        public string Type { get; set; }



        public float Damage { get; set; }

        public Vector2 Gravity { get; set; }

        public bool Enabled { get; set; }

        public float LayerDepth { get; set; }

        public float Step { get; set; }

        public Vector2 Location { get; set; }

        //public Vector2 Rotation{ get; set; }
        public float Rotation { get; set; } 

        /// <summary>
        /// Направление
        /// </summary>
        public int Direction { get; set; }

        // +++
        public Rectangle GetBounds()
        {
            return this.Bounds;
        }

        public System.Drawing.Size Size()
        {
            return new System.Drawing.Size();
        }


        // +++
        public bool CheckCollision(Rectangle mask)
        {
            return this.Bounds.Intersects(mask);
        }


        public bool isAt { get; set; }

        public void Update(GameTime gameTime)
        {
            this.LayerDepth = 1;
            if (!this.Enabled)
            {
                if (timer >= 60)
                {
                    this.Enabled = true;
                    timer = 0;
                }
                else timer++;


                location.X = (float)Math.Sin(this.Rotation) * 5;
                location.Y = (float)Math.Cos(this.Rotation) * 5;

                if (isAt) this.Rotation += 0.013f;
                else this.Rotation -= 0.013f;
                

                this.Location += location;



              



            }
        }



        public void Draw(GameTime gameTime)
        {
            if (!this.Enabled)
                SPHalper.SpriteBatch.Draw(ManagerShell.Texture(this.Type), this.Location, ManagerShell.Bounds(this.Type), Color.Red);
        }

    }
    
}
