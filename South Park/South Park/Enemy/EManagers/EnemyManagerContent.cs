using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace South_Park
{
    sealed class EnemyManagerContent
    {
        static public void FuturePerson(ContentManager contentManager, ref Texture2D[] tMovement, ref Texture2D[] tDamage, int index)
        {
            tMovement[0] = tMovement[1] = tMovement[7] = contentManager.Load<Texture2D>("Enemies/FuturePeoples/FuturePeoples[" + index + "]/Movement/FPMLeft");
            tMovement[2] = contentManager.Load<Texture2D>("Enemies/FuturePeoples/FuturePeoples[" + index + "]/Movement/FPMUp");
            tMovement[3] = tMovement[4] = tMovement[5] = contentManager.Load<Texture2D>("Enemies/FuturePeoples/FuturePeoples[" + index + "]/Movement/FPMRight");
            tMovement[6] = contentManager.Load<Texture2D>("Enemies/FuturePeoples/FuturePeoples[" + index + "]/Movement/FPMDown");
        }

        static public void Gingers(ContentManager contentManager, ref Texture2D[] tMovement, ref Texture2D[] tDamage, int index)
        {
            tMovement[0] = tMovement[1] = tMovement[7] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Movement/GMLeft");
            tMovement[2] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Movement/GMUp");
            tMovement[3] = tMovement[4] = tMovement[5] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Movement/GMRight");
            tMovement[6] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Movement/GMDown");

            tDamage[0] = tDamage[1] = tDamage[7] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Damage/GDLeft");
            tDamage[2] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Damage/GDUp");
            tDamage[3] = tDamage[4] = tDamage[5] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Damage/GDRight");
            tDamage[6] = contentManager.Load<Texture2D>("Enemies/Gingers/Ginger[" + index + "]/Damage/GDDown");

        }

        static public void Canadian(ContentManager contentManager, ref Texture2D[] tMovement, ref Texture2D[] tDamage, int index)
        {
            tMovement[0] = tMovement[1] = tMovement[7] = contentManager.Load<Texture2D>("Enemies/Canadian/Canadian[" + index + "]/Movement/CMLeft");
            tMovement[2] = contentManager.Load<Texture2D>("Enemies/Canadian/Canadian[" + index + "]/Movement/CMUp");
            tMovement[3] = tMovement[4] = tMovement[5] = contentManager.Load<Texture2D>("Enemies/Canadian/Canadian[" + index + "]/Movement/CMRight");
            tMovement[6] = contentManager.Load<Texture2D>("Enemies/Canadian/Canadian[" + index + "]/Movement/CMDown");
        }


    }
}
