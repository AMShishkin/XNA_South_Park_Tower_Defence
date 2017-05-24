using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class MenuCostItem 
    {
        private Animation animation;


        public MenuCostItem(Game game, bool enabled, Texture2D texture = null, TowerType type = TowerType.None)
        {
            this.Enabled = enabled;
            this.Texture = texture;
            this.Type = type;
            this.LayerDepth = 1f;
            this.Name = "None";
            this.Cost = 0;

            this.DataConstruct(type);

            animation = new Animation(game)
            {
                Texture = this.Texture,
                Size = new System.Drawing.Size(80, 100),
                Frames = 1,
                SpriteBounds = new Rectangle(0, 0, 80, 100),
            };

        }


        public bool Enabled { get; set; }

        public TowerType Type { get; set; }

        public string Name { get; private set; }

        public int Cost { get; private set; }

        public Texture2D Texture { get; set; }

        public Vector2 Location { get; set; }

        public float LayerDepth { get; set; }

       

        private void DataConstruct(TowerType type)
        {
            switch (type)
            {
                case TowerType.None: 
                    this.Name = "None";
                    this.Cost = 0;
                    break;
                case TowerType.CameraTowerOne: 
                    this.Name = "Фотокамера";
                    this.Cost = 100;
                    break;

                case TowerType.ElectricTowerOne: 
                    this.Name = "Башня тесла";
                    this.Cost = 80;
                    break;

                default:break;
            }
        }




        public Rectangle GetBounds()
        {
            return new Rectangle(0, 0, 10, 10);
        }




        public bool CheckCollision(Rectangle mask)
        {
            return true;
        }









        public void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                animation.Construct(this.Location, this.LayerDepth);
                animation.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (this.Enabled) animation.Draw(gameTime);

        }

    }
}
