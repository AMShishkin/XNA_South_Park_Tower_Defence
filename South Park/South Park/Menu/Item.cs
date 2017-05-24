using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace South_Park
{
    class Item
    {
        public event EventHandler Click;

        private Vector2 location;

        public Item(string name, Vector2 position, bool active)
        {
            this.Name = name;
            location = position;
            this.Active = active;
        }

        public Texture2D Texture { get; set; }

        public float X
        {
            get { return location.X; }
            set { location.X = value; }
        }
        public float Y { get; set; }

        public Vector2 Location
        {
            get { return location; }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
                location = value;

            }
        }

        public string Name { get; set; }

        public bool Active { get; set; }




        public void OnClick()
        {
            if (Click != null) this.Click(this, null);
        }
    }
}