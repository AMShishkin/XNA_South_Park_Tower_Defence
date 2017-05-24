using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    class ManagerGameInterface 
    {
        private Dictionary<String, Vector2> location;
        private Dictionary<String, StringBuilder> indicator;


    

        private SpriteFont SpriteFontAll, SpriteFontHealth, SpriteFontCityHealth;

        //private Texture2D textureAlert;

        public ManagerGameInterface(Game game)
        {
            this.LoadContent(game);
            this.Initialize(game);
        }

        // +++
        private void Initialize(Game game)
        {
            location = new Dictionary<String, Vector2>
            {
                { "Money", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 285, ManagerGameContent.Rectangle("Panel").Location.Y + 45) },
                { "Towers", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 680, ManagerGameContent.Rectangle("Panel").Location.Y + 45) },
                { "CityHealth", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 838, ManagerGameContent.Rectangle("Panel").Location.Y + 30) },
                { "EnemiesCount", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 60, ManagerGameContent.Rectangle("Panel").Location.Y + 45) },
                { "EnemiesName", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 60, ManagerGameContent.Rectangle("Panel").Location.Y + 25) },
                { "ExcellentHealth", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 476, ManagerGameContent.Rectangle("Panel").Location.Y + 23) },
                { "NormalHealth", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 483, ManagerGameContent.Rectangle("Panel").Location.Y + 23) },
                { "BadHealth", new Vector2(ManagerGameContent.Rectangle("Panel").Location.X + 490, ManagerGameContent.Rectangle("Panel").Location.Y + 23) },

            };

            indicator = new Dictionary<String, StringBuilder>
            {
                { "Health", new StringBuilder("100", 3) },
                { "Money", new StringBuilder("0000", 6) },
                { "Continnum", new StringBuilder(5) },
                { "Towers", new StringBuilder("000", 3) },
                { "CityHealth", new StringBuilder(4) },
                { "EnemiesCount", new StringBuilder("000", 4) },
            };
        }

        private void LoadContent(Game game)
        {
            SpriteFontAll = game.Content.Load<SpriteFont>("Fonts/SFAll");
            SpriteFontHealth = game.Content.Load<SpriteFont>("Fonts/SFHealth");
            SpriteFontCityHealth = game.Content.Load<SpriteFont>("Fonts/SFCityHealth");
        }

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



        // +++
        public void DataConstruct(short hCount, Int16 mCount, Int16 continnum, int tCount, Int16 cityHealth, int eCount, string eName)
        {
            this.HealthDataConstruct(hCount);
            this.MoneyDataConstruct(mCount);
            this.EnemiesCountDataConstruct(eCount);
            this.TowersDataConstruct(tCount);




            //indicator["Health"] = (indicator["Health"] != health) ? health : indicator["Health"];
            //indicator["Money"] = (indicator["Money"] != money) ? money : indicator["Money"];
            //indicator["Continnum"] = (indicator["Continnum"] != continnum) ? continnum : indicator["Continnum"];
            //indicator["Towers"] = (indicator["Towers"] != towers) ? towers : indicator["Towers"];
            //indicator["CityHealth"] = (indicator["CityHealth"] != cityHealth) ? cityHealth : indicator["CityHealth"];
            //indicator["EnemiesCount"] = (indicator["EnemiesCount"] != enemiesCount) ? enemiesCount : indicator["EnemiesCount"];
            //indicator["EnemiesName"] = (indicator["EnemiesName"] != enemiesName) ? enemiesName : indicator["EnemiesName"];

           


        }

        private void MoneyDataConstruct(Int16 mCount)
        {
            if (mCount >= 1000 && mCount <= 9999)
            {
                indicator["Money"][0] = this.DataConverter((short)(mCount / 1000));
                indicator["Money"][1] = this.DataConverter((short)((mCount % 1000) / 100));
                indicator["Money"][2] = this.DataConverter((short)(((mCount % 1000) % 100) / 10));
                indicator["Money"][3] = this.DataConverter((short)(((mCount % 1000) % 100) % 10));
            }
            else if (mCount >= 100 && mCount <= 999)
            {
                indicator["Money"][0] = this.DataConverter((short)(mCount / 100));
                indicator["Money"][1] = this.DataConverter((short)((mCount % 100) / 10));
                indicator["Money"][2] = this.DataConverter((short)(((mCount % 100) % 10) % 10));
                indicator["Money"][3] = ' ';
            }
            else if (mCount >= 10 && mCount <= 99)
            {
                indicator["Money"][0] = this.DataConverter((short)(mCount / 10));
                indicator["Money"][1] = this.DataConverter((short)(mCount % 10));
                indicator["Money"][2] = ' ';
                indicator["Money"][3] = ' ';
            }
            else if (mCount >= 0 && mCount <= 9)
            {
                indicator["Money"][0] = this.DataConverter(mCount);
                indicator["Money"][1] = ' ';
                indicator["Money"][2] = ' ';
                indicator["Money"][3] = ' ';
            }
        }

        private void HealthDataConstruct(short hCount)
        {
            if (hCount >= 100)
            {
                indicator["Health"][0] = '1';
                indicator["Health"][1] = '0';
                indicator["Health"][2] = '0';
            }
            else if (hCount >= 10 && hCount <= 99)
            {
                indicator["Health"][0] = this.DataConverter((short)(hCount / 10));
                indicator["Health"][1] = this.DataConverter((short)(hCount % 10));
                indicator["Health"][2] = ' ';
            }
            else if (hCount >= 0 && hCount <= 10)
            {
                indicator["Health"][0] = this.DataConverter(hCount);
                indicator["Health"][1] = ' ';
                indicator["Health"][2] = ' ';
            }
        }

        private void EnemiesCountDataConstruct(int eCount)
        {
            if (eCount >= 100 && eCount <= 999)
            {
                indicator["EnemiesCount"][0] = this.DataConverter((short)(eCount / 100));
                indicator["EnemiesCount"][1] = this.DataConverter((short)((eCount % 100) / 10));
                indicator["EnemiesCount"][2] = this.DataConverter((short)(((eCount % 100) % 10) / 10));
            }
            else if (eCount >= 10 && eCount <= 99)
            {
                indicator["EnemiesCount"][0] = this.DataConverter((short)(eCount / 10));
                indicator["EnemiesCount"][1] = this.DataConverter((short)(eCount % 10));
                indicator["EnemiesCount"][2] = ' ';
            }
            else if (eCount >= 0 && eCount <= 10)
            {
                indicator["EnemiesCount"][0] = this.DataConverter(eCount);
                indicator["EnemiesCount"][1] = ' ';
                indicator["EnemiesCount"][2] = ' ';
            }
        }

        private void TowersDataConstruct(int tCount)
        {
            if (tCount >= 10 && tCount <= 99)
            {
                indicator["Towers"][0] = this.DataConverter((short)(tCount / 10));
                indicator["Towers"][1] = this.DataConverter((short)(tCount % 10));
                indicator["Towers"][2] = ' ';
            }
            else if (tCount >= 0 && tCount <= 10)
            {
                indicator["Towers"][0] = this.DataConverter(tCount);
                indicator["Towers"][1] = ' ';
                indicator["Towers"][2] = ' ';
            }
        }








        public void Draw(GameTime gameTime)
        {
            SPHalper.SpriteBatch.Draw(ManagerGameContent.Texture("Panel"), ManagerGameContent.Rectangle("Panel"), Color.White); // Панель
            SPHalper.SpriteBatch.DrawString(SpriteFontAll, "x " + indicator["Money"], location["Money"], Color.White); // Монеты
            SPHalper.SpriteBatch.DrawString(SpriteFontAll, indicator["Towers"] + " x", location["Towers"], Color.White); // Башни
            //SPHalper.SpriteBatch.DrawString(SpriteFontCityHealth, "" + indicator["CityHealth"], location["CityHealth"], Color.Lime); // Жизни города
            SPHalper.SpriteBatch.DrawString(SpriteFontAll, "x " + indicator["EnemiesCount"], location["EnemiesCount"], Color.White); // Кол-во врагов
            SPHalper.SpriteBatch.DrawString(SpriteFontAll, SPDataHalper.SPEnemyName, location["EnemiesName"], Color.White); // Название врага
            //SpriteBatch.DrawString(SpriteFont, "x " + this.CartmanContinnumIndicator, new Vector2(690, 700), Microsoft.Xna.Framework.Color.White);

            //if (indicator["Health"] >= 100) 
            SPHalper.SpriteBatch.DrawString(SpriteFontHealth, indicator["Health"], location["ExcellentHealth"], Color.Lime);
            //else if (indicator["Health"] < 100 && indicator["Health"] >= 10)
            //{
            //    if (indicator["Health"] >= 80) SPHalper.SpriteBatch.DrawString(SpriteFontHealth, "" + indicator["Health"], location["NormalHealth"], Color.Lime);
            //    else if (indicator["Health"] >= 40 && indicator["Health"] < 80) SPHalper.SpriteBatch.DrawString(SpriteFontHealth, "" + indicator["Health"], location["NormalHealth"], Color.Orange);
            //    else if (indicator["Health"] <= 40)
            //    {
            //        SPHalper.SpriteBatch.DrawString(SpriteFontHealth, "" + indicator["Health"], location["NormalHealth"], Color.Red);
            //        //  ManagerSpiteBatch.SpriteBatch.Draw(textureAlert, new Rectangle(0, 0, 1320, 720), Color.White);
            //    }
            //}
            //else if (indicator["Health"] < 10) SPHalper.SpriteBatch.DrawString(SpriteFontHealth, "" + indicator["Health"], location["BadHealth"], Color.Red);

            

        }
    }
}