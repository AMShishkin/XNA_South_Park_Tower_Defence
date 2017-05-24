using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerView
    {
        private string vName; // Название волны врагов
        private EnemyManager MEnemies;
    //    private List<IGameObject> globalList, enemyList;



        private Vector2 location;

        private List<Cell> sPoints;


        public ManagerView(Game game)
        {
            MEnemies = new EnemyManager();
            sPoints = new List<Cell>();

            for (sbyte i = 0; i < 11; i++)
                for (sbyte j = 0; j < 21; j++) if (Map.WorldMatrix[i, j].CellCondition == CellsCondition.Start) sPoints.Add(Map.WorldMatrix[i, j]);

            this.State = true;
        }






        public bool State { get; set; } // Состояние true - можно создать новую волну false - запрещено

 


        
        
        



        public void Create(Game game, string viewName, int count, ref List<IGameObject> eList, ref List<IGameObject> gList)
        {
            this.State = false; // волна создана 
            vName = viewName;

            SPDataHalper.SPEnemyName = viewName;

            for (int i = 0; i < count; i++)
            {
                gList.Remove(eList[i]);
                eList[i].Enabled = false;
                

                ((Enemy)eList[i]).Type = viewName;
                ((Enemy)eList[i]).Construct(SPHalper.ContentManager);
                ((Enemy)eList[i]).Condition = EnemyCondition.Moving;
                ((Enemy)eList[i]).Step = 2;

                sbyte ind = (sbyte)Randomize.Random.Next(0, sPoints.Count);

                ((Enemy)eList[i]).RowIndex = sPoints[ind].i;
                ((Enemy)eList[i]).ColumnIndex = sPoints[ind].j;

                location.X = (0 - (i - 1) * Randomize.Random.Next(60, 65));

                if (((Enemy)eList[i]).Type == "GingersOne" || ((Enemy)eList[i]).Type == "GingersTwo" || ((Enemy)eList[i]).Type == "GingersThree" 
                    || ((Enemy)eList[i]).Type == "GingersFour" || ((Enemy)eList[i]).Type == "GingersFive") location.Y = Randomize.Random.Next((int)sPoints[ind].Location.Y - 15, (int)sPoints[ind].Location.Y);

                else location.Y = sPoints[ind].Location.Y;

                eList[i].Location = location;
                gList.Add(eList[i]);
                
            }



            //for (int i = 0; i < count; i++)
            //{
            //    eList.Add(MEnemies.Construct(game, vName));

            //    ((Gingers)eList[eList.Count - 1]).RowIndex = 4;
            //    ((Gingers)eList[eList.Count - 1]).ColumnIndex = 0;

            //    if (((Gingers)eList[eList.Count - 1]).Type == TypeEnemy.One || ((Gingers)eList[eList.Count - 1]).Type == TypeEnemy.Four
            //          || ((Gingers)eList[eList.Count - 1]).Type == TypeEnemy.Six)
            //    {
            //        location.X = (0 - (eList.Count - 1) * Randomize.Random.Next(60, 65));
            //        location.Y = Randomize.Random.Next(280, 315);

            //        (eList[eList.Count - 1]).Location = location;
            //    }
            //    else
            //    {
            //        location.X = (0 - (eList.Count - 1) * Randomize.Random.Next(60, 65));
            //        location.Y = Randomize.Random.Next(290, 325);

            //        (eList[eList.Count - 1]).Location = location;
            //    }

            //}


           // enemyList = eList;
            

           // globalList = gList;

            //for (int i = 0; i < enemyList.Count; i++)
            //{
            //    globalList.Add(enemyList[i]);
            //    gList.Add(enemyList[i]);
            //}

            //for (int i = 0; i < eList.Count; i++)
            //{
            //   if (!eList[i].Enabled) gList.Add(eList[i]);
            //}
            
        }
        
   
        






        public void Update(GameTime gameTime)
        {


                    if (!this.State)
                    {
                        this.State = true;
                    }
        }
    }
}
