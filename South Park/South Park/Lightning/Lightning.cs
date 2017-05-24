using Microsoft.Xna.Framework;

namespace South_Park
{
    class Lightning : IGameObject
    {
        private Animation animation;
        private short timer = 0;
        

        public Lightning(Game game)
        {
            this.Rotation = 0f;
            this.Enabled = true;
            this.Type = ShellType.ElectricTowerLevel1;

            animation = new Animation(game)
            {
                Frames = 6,
                SpriteBounds = new Rectangle(0, 0, 200, 40),
                Texture = ManagerGameContent.Texture("Lightning"),
                Location = Vector2.Zero,
                Size = new System.Drawing.Size(120, 40),
                Rotation = this.Rotation = 150,
                Rotate = true,
                CenterRotate = new Vector2(0, 20),
            };
        }

        public ShellType Type { get; set; }


        public bool Enabled { get; set; }

        public Vector2 Location { get; set; }

        public float LayerDepth { get; set; }

        public float Rotation { get; set; }

        public Rectangle GetBounds()
        {
            return animation.SpriteBounds;
        }

        public System.Drawing.Size Size()
        {
            return animation.Size;
        }



        public bool CheckCollision(Rectangle mask)
        {
            return true;
        }

        public void Update(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                if (timer >= 40)
                {
                    timer = 0;
                    this.Enabled = true;

                }
                else
                {
                    timer++;
                    animation.Construct(this.Location, this.LayerDepth, this.Rotation);
                    animation.Update(gameTime);
                }
            }
        }



        public void Draw(GameTime gameTime)
        {
            if (!this.Enabled) animation.Draw(gameTime);
        }
    }
}
