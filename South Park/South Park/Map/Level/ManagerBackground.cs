using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerBackground
    {
        public static Texture2D GetBackground(string levelName)
        {
            switch (levelName)
            {

                case "EricHouse": return SPHalper.ContentManager.Load<Texture2D>("Levels/EricHouse/Background");
                default: return null;
            }
        }


    }
}
