using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerBonus
    {
        private Speed speed;
      

        public ManagerBonus(Game game)
        {
            speed = new Speed(game);
            
        }

        public void CreateBonus(TypeBonus typeBonus, Vector2 location, ref List<IGameObject> list)
        {
            if (typeBonus == TypeBonus.Speed && speed.Enabled)
            {
                speed.Location = location;
                speed.Enabled = false;
                list.Add(speed);
            }
        }
    }
}
