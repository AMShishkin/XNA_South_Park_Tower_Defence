using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class PauseMenu : DrawableGameComponent
    {
        private Texture2D BackGround;
        private Rectangle TextureBox;

        public PauseMenu(Game game)
            : base(game)
        {
            TextureBox = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);

            this.LoadContent();

        }


        protected override void LoadContent()
        {
          //  BackGround = Game.Content.Load<Texture2D>("Menu/Pause/Background");
            base.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {


            //SPHalper.SpriteBatch.Draw(BackGround, TextureBox, Color.White);
            base.Draw(gameTime);
        }


    }
}
