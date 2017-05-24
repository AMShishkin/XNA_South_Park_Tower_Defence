using Microsoft.Xna.Framework;

namespace South_Park
{
    class Bang : IGameObject
    {
        private Animation animation;

        private int timer;

        public Bang(Game game)
        {
            this.Enabled = true;
            this.LayerDepth = 1;

            animation = new Animation(game)
            {
                Size = new System.Drawing.Size(48, 48),
                SpriteBounds = ManagerGameContent.Rectangle("Bang01"),
                Frames = 1,
                Texture = ManagerGameContent.Texture("Bang0" + Randomize.Random.Next(1, 5)),
                Frequency = 4,
            };
        }

        public bool Enabled { get; set; }

        public Vector2 Location { get; set; }

        public float LayerDepth { get; set; }

        public Rectangle GetBounds()
        {
            return ManagerGameContent.Rectangle("Bang01");
        }

        public System.Drawing.Size Size()
        {
            return new System.Drawing.Size();
        }

        public bool CheckCollision(Rectangle mask)
        {
            return ManagerGameContent.Rectangle("Bang01").Intersects(mask);
        }

        public void Update(GameTime gameTime)
        {
            if (!this.Enabled) 
            {
                if (timer <= 35)
                {
                    animation.Construct(this.Location, this.LayerDepth);
                    animation.Update(gameTime);
                }
                else 
                {
                    timer = 0;
                    this.Enabled = true;
                    animation.Texture = ManagerGameContent.Texture("Bang0" + Randomize.Random.Next(1, 5));
                }
                timer++;
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (!this.Enabled) animation.Draw(gameTime);
        }
    }
}

    

