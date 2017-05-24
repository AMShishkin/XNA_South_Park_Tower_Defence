using Microsoft.Xna.Framework;

namespace South_Park
{
    class Blood : IGameObject
    {
        private Animation animation;

        private int timer;

        public Blood(Game game)
        {
            this.Enabled = true;
            this.LayerDepth = 0;

            animation = new Animation(game)
            {
                Size = new System.Drawing.Size(80, 80),
                SpriteBounds = ManagerGameContent.Rectangle("Blood"),
                Frames = 7,
                Texture = ManagerGameContent.Texture("Blood"),
                Frequency = 4,
                TimeFrame = 2,
            };
        }

        public bool Enabled { get; set; }

        public Vector2 Location { get; set; }

        public float LayerDepth { get; set; }

        public Rectangle GetBounds()
        {
            return ManagerGameContent.Rectangle("Blood");
        }

        public System.Drawing.Size Size()
        {
            return new System.Drawing.Size();
        }

        public bool CheckCollision(Rectangle mask)
        {
            return ManagerGameContent.Rectangle("Blood").Intersects(mask);
        }

        public void Update(GameTime gameTime)
        {
            if (!this.Enabled)
            {
                if (timer > 450)
                {
                    animation.TimeFrame = 0.09f;

                    if (animation.CurrentFrame == 6)
                    {
                        timer = 0;
                        animation.TimeFrame = 0;
                        animation.CurrentFrame = 0;
                        this.Enabled = true;
                        
                    }

                    animation.Update(gameTime);
                }
              

                
                
                animation.Construct(this.Location, this.LayerDepth);
                

                timer++;
            }




        }

        public void Draw(GameTime gameTime)
        {
            if (!this.Enabled) animation.Draw(gameTime);
        }
    }
}
    

