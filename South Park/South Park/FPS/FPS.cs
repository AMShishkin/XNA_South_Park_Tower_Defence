using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class FPS
    {

        private byte frames; // Количество кадров
        private double seconds; // Количество кадров в секунду




        public byte Count { get; set; }

        public void Update(GameTime gameTime)
        {
            seconds += gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds >= 1)
            {
                this.Count = frames;
                seconds = 0;
                frames = 0;
            }

        }

        public void Draw(GameTime gameTime)
        {
            frames++;

        }

    }
}
