using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace South_Park
{
    sealed class EnemyManager
    {
        private Dictionary<string, EnemyType> rTypeEnemy;



        private static System.Drawing.Size sSize;
        private static Rectangle rBounds;
        
        public EnemyManager()
        {
            rTypeEnemy = new Dictionary<string, EnemyType>()
            {
                { "1", EnemyType.One },
                { "2", EnemyType.Two },
                { "3", EnemyType.Three },
                { "4", EnemyType.Four },
                { "5", EnemyType.Five },
                { "6", EnemyType.Six },
                { "7", EnemyType.Seven },
                { "8", EnemyType.Eight },
                { "9", EnemyType.Nine },
                { "10", EnemyType.Ten },
            };

            sSize = new System.Drawing.Size();
            rBounds = new Rectangle(0, 0, 0, 0);
        }

 
        static public Vector2 Movement(sbyte direction, Vector2 location, float step)
        {
            switch (direction)
            {
                case 0: location.X -= step; return location;
                case 1: location.X -= step; location.Y -= step; return location;
                case 2: location.Y -= step; return location;
                case 3: location.X += step; location.Y -= step; return location;
                case 4: location.X += step; return location;
                case 5: location.X += step; location.Y += step; return location;
                case 6: location.Y += step; return location;
                case 7: location.X -= step; location.Y += step; return location;
                default: return Vector2.Zero;
            }
        }
   
        static public Vector2 Stop(int direction, Vector2 location, float step)
        {
            switch (direction)
            {
                case 0: return new Vector2(location.X + step, location.Y); ;
                case 1: return new Vector2(location.X + step, location.Y + step); ;
                case 2: return new Vector2(location.X, location.Y - step); ;
                case 3: return new Vector2(location.X - step, location.Y + step);
                case 4: return new Vector2(location.X - step, location.Y);
                case 5: return new Vector2(location.X - step, location.Y - step);
                case 6: return new Vector2(location.X, location.Y - step);
                case 7: return new Vector2(location.X + step, location.Y - step);
                default: return Vector2.Zero;
            }
        }



        // +++
        private static Rectangle BoundsFuturePerson(ref string type, ref Vector2 location)
        {
            rBounds.Width = 80;
            rBounds.Height = 20;
            rBounds.X = (int)location.X;
            rBounds.Y = (int)location.Y + 120;

            switch (type)
            {
                case "FuturePersonOne": return rBounds;
                case "FuturePersonTwo": return rBounds;
                case "FuturePersonThree": return rBounds;
                case "FuturePersonFour": return rBounds;
                case "FuturePersonFive": return rBounds;
                default:
                    rBounds.Height = rBounds.Width = 0;
                    return rBounds;
            }     
        }
        private static Rectangle BoundsGingers(ref string type, ref Vector2 location)
        {
            rBounds.Width = 60;
            rBounds.Height = 20;
            rBounds.X = (int)location.X;
            rBounds.Y = (int)location.Y + 40;

            switch (type)
            {
                case "GingersOne":
                    rBounds.Y = (int)location.Y + 60;
                    return rBounds;
                case "GingersTwo": return rBounds;
                case "GingersThree": return rBounds;
                case "GingersFour":
                    rBounds.Y = (int)location.Y + 60;
                    return rBounds;
                case "GingersFive": return rBounds;
                default:
                    rBounds.Height = rBounds.Width = 0;
                    return rBounds;
            }
        }
        private static Rectangle BoundsCanadian(ref string type, ref Vector2 location)
        {
            rBounds.Width = 80;
            rBounds.Height = 140;
            rBounds.X = (int)location.X;
            rBounds.Y = (int)location.Y;

            switch (type)
            {
                case "CanadianOne":
                    rBounds.Y = (int)location.Y;
                    return rBounds;
                case "CanadianTwo": return rBounds;
                case "CanadianThree": return rBounds;
                case "CanadianFour":
                    return rBounds;
                case "CanadianFive": return rBounds;
                default:
                    rBounds.Height = rBounds.Width = 0;
                    return rBounds;
            }
        }














        // ++
        public static Rectangle CollisionBounds(string type, Vector2 location)
        {
            if (type == "FuturePersonOne" || type == "FuturePersonTwo" || type == "FuturePersonThree"
                || type == "FuturePersonFour" || type == "FuturePersonFive") return BoundsFuturePerson(ref type, ref location);
            else if (type == "GingersOne" || type == "GingersTwo" || type == "GingersThree"
                     || type == "GingersFour" || type == "GingersFive") return BoundsGingers(ref type, ref location);
            else if (type == "CanadianOne" || type == "CanadianTwo" || type == "CanadianThree"
                 || type == "CanadianFour" || type == "CanadianFive") return BoundsCanadian(ref type, ref location);
            else
            {
                rBounds.Height = rBounds.Width = 0;
                return rBounds;
            }
               
        }






        // +++
        private static System.Drawing.Size SizeFuturePerson(ref string type)
        {
            sSize.Width = 80;
            sSize.Height = 140;

            switch (type)
            {
                case "FuturePersonOne": return sSize;
                case "FuturePersonTwo": return sSize;
                case "FuturePersonThree": return sSize;
                case "FuturePersonFour": return sSize;
                case "FuturePersonFive": return sSize;
                default:
                    sSize.Width = 0;
                    sSize.Height = 0;
                    return sSize;
            }
        }
        private static System.Drawing.Size SizeGingers(ref string type)
        {
            sSize.Width = 60;
            sSize.Height = 80;

            switch (type)
            {
                case "GingersOne": return sSize;
                case "GingersTwo":
                    sSize.Height = 60;
                    return sSize;
                case "GingersThree":
                    sSize.Height = 60;
                    return sSize;
                case "GingersFour": return sSize;
                case "GingersFive":
                    sSize.Height = 60;
                    return sSize;
                default: 
                    sSize.Width = 0;
                    sSize.Height = 0;
                    return sSize;
            }
        }
        private static System.Drawing.Size SizeCanadian(ref string type)
        {
            sSize.Width = 80;
            sSize.Height = 140;

            switch (type)
            {
                case "CanadianOne": return sSize;
                case "CanadianTwo": return sSize;
                case "CanadianThree": return sSize;
                case "CanadianFour": return sSize;
                case "CanadianFive":
                    sSize.Height = 60;
                    return sSize;
                default:
                    sSize.Width = 0;
                    sSize.Height = 0;
                    return sSize;
            }
        }






        // ++
        public static System.Drawing.Size Size(string type)
        {
            if (type == "FuturePersonOne" || type == "FuturePersonTwo" || type == "FuturePersonThree"
                || type == "FuturePersonFoure" || type == "FuturePersonFive") return SizeFuturePerson(ref type);
            else if (type == "GingersOne" || type == "GingersTwo" || type == "GingersThree"
                     || type == "GingersFour" || type == "GingersFive") return SizeGingers(ref type);
            else if (type == "CanadianOne" || type == "CanadianTwo" || type == "CanadianThree"
                 || type == "CanadianFour" || type == "CanadianFive") return SizeCanadian(ref type);

            else
            {
                sSize.Width = 0;
                sSize.Height = 0;
                return sSize;
            }

        }
    


        //// ++
        //public IGameObject Construct(Game game, string type)
        //{
        //    //switch (type)
        //    //{
        //    //    case "GingersOne": return new Enemy(game, EnemyType.GingersOne, 1);
        //    //    case "GingersTwo": return new Enemy(game, EnemyType.GingersOne, 1);
        //    //    case "GingersThree": return new Enemy(game, EnemyType.GingersOne, 1);
        //    //    case "GingersFour": return new Enemy(game, EnemyType.GingersOne, 1);
        //    //    case "GingersFive": return new Enemy(game, EnemyType.GingersOne, 1);



        //    //    case "FuturePersonOne": return new Enemy(game, EnemyType.FuturePersonOne, 1);
        //    //    case "FuturePersonTwo": return new Enemy(game, EnemyType.FuturePersonTwo, 1);
        //    //    default: return null;
        //    //}
        //}
    }
}