using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class ElementLightning
    {
        private Vector2 position;


        



        public ElementLightning(Game game, Vector2 location)
        {
            this.Rotation = 0f;
            this.LocationBegin = location;



            position = this.LocationBegin;
        }

        


        public Vector2 LocationBegin { get; set; }
        public Vector2 LocationEnd { get; set; }

        public float Rotation { get; set; }

        public void Update(GameTime gameTime)
        {



        }


        public void Draw(GameTime gameTime)
        {
           // SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("ElementLightning"), position, ManagerGameContent.Rectangle("ElementLightning"), Color.Lime);
            SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("ElementLightning"), LocationBegin, ManagerGameContent.Rectangle("ElementLightning"), Color.Red, this.Rotation, Vector2.Zero, 1, SpriteEffects.None, 1);
        }


    }
}
