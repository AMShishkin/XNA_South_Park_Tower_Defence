using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class Star
    {
        private Rectangle bounds;
        private int size;

        public Star(Vector2 location)
        {
            size = Randomize.Random.Next(1, 5);

            bounds.X = (int)location.X;
            bounds.Y = (int)location.Y;
            bounds.Width = bounds.Height = size;

            this.Location = bounds.Location;
        }

        public Point Location
        {
            get { return bounds.Location; }
            set { bounds.Location = value; }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, Texture2D sprite)
        {
            SPHalper.SpriteBatch.Draw(sprite, bounds, Color.White);
            if (SPHalper.EditorState) SPHalper.SpriteBatch.DrawRectangle(bounds, Color.Lime);
        }
    }
}
