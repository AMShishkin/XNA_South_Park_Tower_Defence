using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System;

namespace South_Park
{
    static class ManagerTower
    {
        private static Dictionary<String, Texture2D> textureScaning, textureFire;
        private static System.Drawing.Size size;
        private static Rectangle bounds;

        public static void Initialize()
        {
            textureScaning = new Dictionary<String, Texture2D>
            {
                { "None", null },
                { "ElectricTowerOne", SPHalper.ContentManager.Load<Texture2D>("Towers/ElectricTower/ETowerL1") },
                { "ElectricTowerTwo", SPHalper.ContentManager.Load<Texture2D>("Towers/ElectricTower/ETowerL2") },
                { "ElectricTowerThree", SPHalper.ContentManager.Load<Texture2D>("Towers/ElectricTower/ETowerL1") },
            };

            textureFire = new Dictionary<String, Texture2D>
            {
                { "None", null },

                { "ElectricTowerOne", SPHalper.ContentManager.Load<Texture2D>("Towers/ElectricTower/eTowerL1[Fire]") },
            };


        }

        

        public static System.Drawing.Size Size(String towerType)
        {
            size.Width = 80;
            size.Height = 100;

            if (towerType == "ElectricTowerOne" || towerType == "ElectricTowerTwo" || towerType == "ElectricTowerThree") return size;
            else
            {
                size.Width = size.Height = 0;
                return size;
            }
        }

        public static Texture2D Texture(String towerType, TowerCondition towerCondition)
        {
            if (towerCondition == TowerCondition.Scaning) return textureScaning[towerType];
            else return textureFire[towerType];
        }

        public static Rectangle CollisionBounds(String towerType, Vector2 location)
        {
            bounds.Width = 80;
            bounds.Height = 2;
            bounds.X = (int)location.X;
            bounds.Y = (int)location.Y + 100;

            if (towerType == "ElectricTowerOne" || towerType == "ElectricTowerTwo" || towerType == "ElectricTowerThree") return bounds;
            else
            {
                bounds.Width = bounds.Height = 0;
                bounds.X = bounds.Y = 0;
                return bounds;
            }
        }
    }
}