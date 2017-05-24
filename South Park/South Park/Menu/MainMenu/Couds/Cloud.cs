using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace South_Park
{
    class Cloud : DrawableGameComponent
    {
       
        private Vector2 location;
        private Rectangle bCollision;

        public Cloud(Game game, String cloudType, Texture2D texture, Rectangle bound, Vector2 location)
            : base(game)
        {
           

            this.Texture = texture;
            this.Location = location;
            this.BoundDraw = bound;
            this.Step = 0f;
            this.Type = cloudType;
            this.IsCollision = false;
            this.BoundCollision = new Rectangle(0, 0, 300, 125);
        }

        public string Type { get; set; }

        public Texture2D Texture { get; set; }

        public Vector2 Location
        {
            get { return location; }
            set { location = value; }
        }

        public bool IsCollision { get; set; }

        public Rectangle BoundCollision
        {
            get { return bCollision; }
            set { bCollision = value; }
        }

        public Rectangle BoundDraw { get; set; }

        public float Step { get; set; }

        public bool CheckCollision(Rectangle mask)
        {
            return this.BoundCollision.Intersects(mask);
        }

        public void RandomStep()
        {
            switch (Randomize.Random.Next(0, 10))
            {
                case 0: this.Step = 0.04f; break;
                case 1: this.Step = 0.05f; break;
                case 2: this.Step = 0.06f; break;
                case 3: this.Step = 0.07f; break;
                case 4: this.Step = 0.08f; break;
                case 5: this.Step = 0.09f; break;
                case 6: this.Step = 0.12f; break;
                case 7: this.Step = 0.15f; break;
                case 8: this.Step = 0.16f; break;
                case 9: this.Step = 0.13f; break;
                case 10: this.Step = 0.11f; break;
                default: this.Step = 0.06f; break;
            }  
        }

        private void DrawOnlineEditingMode(GameTime gameTime)
        {
            SPHalper.SpriteBatch.Draw(this.Texture, location, this.BoundDraw, Color.White);
           // SPHalper.SpriteBatch.DrawRectangle(bCollision, Color.Lime);
        }

        private void DrawOfflineEditorMode(GameTime gameTime)
        {
            SPHalper.SpriteBatch.Draw(this.Texture, location, this.BoundDraw, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            bCollision.X = ((int)location.X + this.BoundDraw.Width / 2) - bCollision.Width / 2;
            bCollision.Y = ((int)location.Y + this.BoundDraw.Height  / 2) - bCollision.Height / 2;

            if (location.X >= (int)SPHalper.RenderTarget.Bounds.Width + 80)
            {
                location.X = -700;
                location.Y = Randomize.Random.Next(0, 400);
                this.RandomStep();           
            }

            location.X += this.Step;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (SPHalper.EditorState) this.DrawOnlineEditingMode(gameTime);
            else this.DrawOfflineEditorMode(gameTime);
    
            base.Draw(gameTime);
        }
    }
}
