using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class MenuCost
    {
        private ManagerContentMenuCost mContentMenuCost;
        private Animation animation;

        private List<MenuCostItem> lItems;

        private SpriteFont font;

        private Color costColor;

        private StringBuilder cost;


        public MenuCost(Game game)
        {
            this.Enabled = false;
            this.Location = Vector2.Zero;
            this.Index = 0;


            mContentMenuCost = new ManagerContentMenuCost(SPHalper.ContentManager);

            lItems = new List<MenuCostItem>();

          //  for (int i = 0; i < 10; i++)
           // {
               // lItems.Add(new MenuCostItem(game, true, mContentMenuCost.Texture("1")));
              //  lItems.Add(new MenuCostItem(game, true, mContentMenuCost.Texture("2")));
               // lItems.Add(new MenuCostItem(game, true, mContentMenuCost.Texture("3")));
               lItems.Add(new MenuCostItem(game, true, mContentMenuCost.Texture("1"), TowerType.None));
               lItems.Add(new MenuCostItem(game, true, mContentMenuCost.Texture("2"), TowerType.None));
               lItems.Add(new MenuCostItem(game, true, mContentMenuCost.Texture("3"), TowerType.None));
               lItems.Add(new MenuCostItem(game, true, mContentMenuCost.Texture("ElectricTower"), TowerType.ElectricTowerOne));
          //  }



            animation = new Animation(game)
            {
                Texture = mContentMenuCost.Texture("MenuCost"),
                SpriteBounds = mContentMenuCost.Bounds("MenuCost"),
                Size = new System.Drawing.Size(240, 120),
                Frames = 1,
            };



            font = SPHalper.ContentManager.Load<SpriteFont>("Fonts/LogoSmall");
            costColor = Color.OrangeRed;
            cost = new StringBuilder("0000", 4);
        }


        public bool Enabled { get; set; }

        public Vector2 Location { get; set; }

        public int Index { get; set; }



        private char DataConverter(int number)
        {
            switch (number)
            {
                case 0: return '0';
                case 1: return '1';
                case 2: return '2';
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                default: return '~';
            }
        }



        public void Update(GameTime gameTime)
        {
            if (this.Enabled)
            {
                animation.Construct(this.Location, 1);
                animation.Update(gameTime);


                if (this.Index < 0) this.Index = lItems.Count - 1;
                if (this.Index > lItems.Count - 1) this.Index = 0;



                for (int i = 0; i < lItems.Count; i++)
                {
                    if (i == this.Index)
                    {
                        if (i - 1 < 0)
                        {
                            lItems[lItems.Count - 1].Location = this.Location;
                        }
                        else lItems[i - 1].Location = this.Location;

                        lItems[i].Location = new Vector2(this.Location.X + 80, this.Location.Y);

                        if (i + 1 > lItems.Count - 1)
                        {
                            lItems[0].Location = new Vector2(this.Location.X + 160, this.Location.Y);
                        }
                        else lItems[i + 1].Location = new Vector2(this.Location.X + 160, this.Location.Y);
                    }
                    lItems[i].Update(gameTime);
                }

                if (SPDataHalper.SPMoney < lItems[this.Index].Cost) costColor = Color.Red;
                else costColor = Color.Lime;


                MoneyData(lItems[this.Index].Cost);

            }
        }

        private void MoneyData(int number)
        {
            if (number >= 1000 && number <= 9999)
            {
                cost[0] = this.DataConverter(lItems[this.Index].Cost / 1000);
                cost[1] = this.DataConverter((lItems[this.Index].Cost % 1000) / 100);
                cost[2] = this.DataConverter(((lItems[this.Index].Cost % 1000) % 100) / 10);
                cost[3] = this.DataConverter(((lItems[this.Index].Cost % 1000) % 100) % 10);
            }
            else if (number >= 100 && number <= 999)
            {
                cost[0] = this.DataConverter(lItems[this.Index].Cost / 100);
                cost[1] = this.DataConverter((lItems[this.Index].Cost % 100) / 10);
                cost[2] = this.DataConverter(((lItems[this.Index].Cost % 100) % 10) % 10);
                cost[3] = ' ';
            }
            else if (number >= 10 && number <= 99)
            {
                cost[0] = this.DataConverter(lItems[this.Index].Cost / 10);
                cost[1] = this.DataConverter(lItems[this.Index].Cost % 10);
                cost[2] = ' ';
                cost[3] = ' ';
            }
            else if (number >= 0 && number <= 9)
            {
                cost[0] = this.DataConverter(lItems[this.Index].Cost);
                cost[1] = ' ';
                cost[2] = ' ';
                cost[3] = ' ';
            }
        }


        public void Draw(GameTime gameTime)
        {
            if (this.Enabled)
            {
                animation.Draw(gameTime);

                if (this.Index - 1 < 0) lItems[lItems.Count - 1].Draw(gameTime);
                else lItems[this.Index - 1].Draw(gameTime);

                lItems[this.Index].Draw(gameTime);

                if (this.Index + 1 > lItems.Count - 1) lItems[0].Draw(gameTime);
                else lItems[this.Index + 1].Draw(gameTime);

             
            
                // GC

                SPHalper.SpriteBatch.DrawString(font, lItems[this.Index].Name, new Vector2((this.Location.X + 120) - (lItems[this.Index].Name.Length * 18) / 2, this.Location.Y - 20), Color.Orange);

                

                SPHalper.SpriteBatch.DrawString(font, cost, new Vector2((this.Location.X + 120) - (cost.Length * 12) / 2, this.Location.Y + 95), costColor);
            }
        }

    }
}
