using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class HealthStrip
    {
        private const float indicate = 0.6f;
        private Rectangle rBounds, rBorderBounds;
        private Vector2 lHStrip, lBorder;
        
        public HealthStrip(Game game, bool enabled = false)
        {
            rBounds = new Rectangle(0, 0, 60, 5);
            rBorderBounds = new Rectangle(0, 0, 62, 7);
            lBorder = lHStrip = Vector2.Zero;

            this.Enabled = enabled;

        }

        public float Health { get; set; }

        public bool Enabled { get; set; }

        public void Construct(float positionX, float positionY, float health)
        {
            if (this.Health >= 100) this.Enabled = false;
            else this.Enabled = true;
            {
                this.Health = health;
                lHStrip.X = positionX;
                lHStrip.Y = positionY;
                lBorder.X = lHStrip.X - 1;
                lBorder.Y = lHStrip.Y - 1;
                rBounds.Width = (int)(indicate * this.Health);
            }      
        }

        public void Draw(GameTime gameTime)
        {
            if (this.Enabled)
            {
                if (this.Health >= 0 && this.Health <= 39)
                    SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("HealthStripBad"), lHStrip, rBounds, Color.Pink, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);
                else if (this.Health >= 40 && this.Health <= 79)
                    SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("HealthStripNormal"), lHStrip, rBounds, Color.Pink, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);
                else
                    SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("HealthStripGood"), lHStrip, rBounds, Color.Pink, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);
                SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("HealthStripBorder"), lBorder, rBorderBounds, Color.Pink, 0, Vector2.Zero, 1, SpriteEffects.None, 0.99f);
            }     
        }
    }
}
