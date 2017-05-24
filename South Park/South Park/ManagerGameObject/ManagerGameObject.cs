using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace South_Park
{
    class ManagerGameObject : GameComponent
    {
        // Списки объектов
        private List<IGameObject> lGlobal, lTowers, lShells, lEnemies, lBloods, lBangs,
                                  lLightnings;

        // Пулы объектов
        private ObjectPool<IGameObject> OPBloods, OPBangs, OPEnemy;

        private GoldMonet[] aGoldMonet;

        // Менеджеры
        private EnemyManager MEnemies;
        private ManagerGameCollision MGCollision;
        private ManagerView MView;
        private ManagerBonus mBonus;

        // Еденичные объекты
        private Eric eric;
        private Arrow arrow;
        private FiringRadius firingRadius;
        private Vector2 vLocation;





        private bool flag = true;

        private int eCount = 0;





        public ManagerGameObject(Game game)
            : base(game)
        {
            this.Initialize(game);

            for (short i = 0; i < 500; i++)
            {
                lShells.Add(new Shell(game));

                if (i < 100)
                {
                    if (i < 10)
                    {
                        OPBangs.Push(new Bang(game));
                        OPBloods.Push(new Blood(game));
                    }

                    lEnemies.Add(new Enemy(game));
                    lLightnings.Add(new Lightning(game));
                    aGoldMonet[i] = new GoldMonet(game);
                }
            }

          //  MView.Create(Game, "CanadianThree", SPDataHalper.SPEnemyCount = 2, ref lEnemies, ref lGlobal);

            lGlobal.Add(eric);


            lGlobal.Add(arrow);
            lGlobal.Add(firingRadius);


        }

        // Реф +-
        private void Initialize(Game game)
        {
            // Инициализация пулов
            //OPShells = new ObjectPool<IGameObject>();
            OPBloods = new ObjectPool<IGameObject>();
            OPBangs = new ObjectPool<IGameObject>();
            OPEnemy = new ObjectPool<IGameObject>();
            // Инициализация списков
            lGlobal = new List<IGameObject>();
            lGlobal.Capacity = 500;
            lTowers = new List<IGameObject>();
            lTowers.Capacity = 100;
            lShells = new List<IGameObject>();
            lShells.Capacity = 500;
            lEnemies = new List<IGameObject>();
            lEnemies.Capacity = 150;
            lBloods = new List<IGameObject>();
            lBloods.Capacity = 150;
            lBangs = new List<IGameObject>();
            lBangs.Capacity = 500;
            lLightnings = new List<IGameObject>();
            lLightnings.Capacity = 500;

            aGoldMonet = new GoldMonet[100];

            // Инициализация менеджеров
            MEnemies = new EnemyManager();
            MView = new ManagerView(game);
            mBonus = new ManagerBonus(game);
            MGCollision = new ManagerGameCollision
            {
                RowIndexCollision = 4,
                CollumnIndexCollision = 4,
            };
            // Инициализация еденичных классов
            eric = new Eric(game, Map.WorldMatrix[4, 4].Location);
            arrow = new Arrow(game);
            firingRadius = new FiringRadius(game);


            Eric.Health = 100; // !!! убрать глобальность
        }

        // Реф +
        private void CreateTower(BuildingCondition buildingCondition)
        {
            // если что-то строим
            if (buildingCondition != BuildingCondition.Anything)
            {
                MGCollision.CollisionWithCell(eric, 0, 0);
                // если в клетке нет башенки
                if (Map.WorldMatrix[MGCollision.RowIndexCollision, MGCollision.CollumnIndexCollision].CellCondition != CellsCondition.OccupiedByTower)
                {
                    // помечаем что в клетке есть башенка
                    Map.WorldMatrix[MGCollision.RowIndexCollision, MGCollision.CollumnIndexCollision].CellCondition = CellsCondition.OccupiedByTower;
                    // выбираем башенку
                    switch (buildingCondition)
                    {
                        case BuildingCondition.Anything:
                            break;
                        case
                        BuildingCondition.ElectricTowers: lTowers.Add(new Tower(Game, TowerType.ElectricTowerOne));
                            SPDataHalper.SPMoney -= 80;
                            break;
                        default:
                            break;
                    }

                    vLocation.X = MGCollision.LocationCellCollistion.X;
                    vLocation.Y = MGCollision.LocationCellCollistion.Y - 20;
                    lTowers[lTowers.Count - 1].Location = vLocation;
                    lTowers[lTowers.Count - 1].Enabled = false;
                    lTowers[lTowers.Count - 1].LayerDepth = (lTowers[lTowers.Count - 1].Location.Y + lTowers[lTowers.Count - 1].Size().Height) / SPHalper.RenderTarget.Bounds.Height;

                    for (sbyte r = -1; r < 2; r++)
                        for (sbyte c = -1; c < 2; c++)
                            if (r != 0 || c != 0) this.CreateFireArea(ref r, ref c, (ITower)lTowers[lTowers.Count - 1]);

                    lGlobal.Add(lTowers[lTowers.Count - 1]);
                    SPDataHalper.SPTowersCount++;


                }
            }
        }

        // Создает зону поражения башенки
        private void CreateFireArea(ref sbyte rowIndex, ref sbyte columnIndex, ITower tower)
        {
            // Если клетка RI, CI не занята башенкой
            if (Map.WorldMatrix[MGCollision.RowIndexCollision - rowIndex, MGCollision.CollumnIndexCollision - columnIndex].CellCondition != CellsCondition.OccupiedByTower)
            {
                // Помечаем клетку зоной поражения
                Map.WorldMatrix[MGCollision.RowIndexCollision - rowIndex, MGCollision.CollumnIndexCollision - columnIndex].CellCondition = CellsCondition.DefeatZoneTower;
                // Привязываем данную клетку к данной башенки

                Map.WorldMatrix[MGCollision.RowIndexCollision - rowIndex, MGCollision.CollumnIndexCollision - columnIndex].CollisionWithFireZone
                    += new EventHandler(((Tower)tower).FireZone);

                //Map.WorldMatrix[MGCollision.RowIndexCollision - rowIndex, MGCollision.CollumnIndexCollision - columnIndex].CollisionWithFireZone
                //    += new EventHandler(((Tower)tower).FireZone);
            }
        }


        private void DeleteHeroZone()
        {
            //for (int i = 0; i < 13; i++)
            //    for (int j = 0; j < 23; j++)
            //    {

            //        if (!eric.CheckCollision(Map.WorldMatrix[i, j].Rectangle))
            //            if (Map.WorldMatrix[i, j].CellCondition == CellsCondition.DefeatZoneHero)
            //                Map.WorldMatrix[i, j].CellCondition = CellsCondition.Free;



            //    }

        }


        private void TowersLogic()
        {
            if (lTowers.Count > 0)
            {
                for (int i = 0; i < lTowers.Count; i++)
                {
                    if (!((ITower)lTowers[i]).Fire)
                    {
                        if (((ITower)lTowers[i]).Type == "ElectricTowerOne")
                        {
                            for (int j = 0; j < lLightnings.Count; j++)
                            {
                                if (lLightnings[j].Enabled)
                                {
                                    lLightnings[j].Enabled = false;
                                    vLocation.X = lTowers[i].Location.X + 40;
                                    vLocation.Y = lTowers[i].Location.Y + 35;
                                    lLightnings[j].Location = vLocation;
                                    ((Lightning)lLightnings[j]).Rotation = (float)Math.Atan2(((Tower)lTowers[i]).Ene.Location.Y + 40 - lLightnings[j].Location.Y,
                                                                                             ((Tower)lTowers[i]).Ene.Location.X - lLightnings[j].Location.X + 40);


                                    if (((Lightning)lLightnings[j]).Rotation <= 0) lLightnings[j].LayerDepth = lTowers[i].LayerDepth - 0.001f;
                                    else lLightnings[j].LayerDepth = lTowers[i].LayerDepth + 0.001f;

                                    lGlobal.Add(lLightnings[j]);

                                    this.CreateGoldMonet(ref aGoldMonet, 2, ((Tower)lTowers[i]).Ene.Location);

                                    break;
                                }
                                else continue;
                            }
                        }
                    }
                }
            }
        }

        // ПЕРЕПИСАТЬ
        private void CameraZoom(Vector2 location)
        {
            if (location.X <= 600)
            {
                if (location.Y <= 400)
                {
                    if (Camera2D.Zoom < 1.1f) Camera2D.Zoom += 0.0007f;
                    else
                    {
                        if (Camera2D.Zoom > 0.95f) Camera2D.Zoom -= 0.0007f;
                    }
                }
            }
            else
            {
                if (location.Y <= 400)
                {
                    if (Camera2D.Zoom < 1.02f)
                        Camera2D.Zoom += 0.0007f;
                    else
                    {
                        if (Camera2D.Zoom > 0.95f) Camera2D.Zoom -= 0.0007f;
                    }


                    if (Camera2D.Zoom > 1f) Camera2D.Zoom -= 0.0007f;
                }
            }
        }



        // Проверяет коллизии всех врагов
        private void EnemiesCollision()
        {
            for (short i = 0; i < lEnemies.Count; i++)
                if (!lEnemies[i].Enabled)
                {
                    MGCollision.CEnemieWithIndexMove((IEnemy)lEnemies[i]);
                    //if (((Enemy)lEnemies[i]).CreateGold) this.CreateGoldMonet(ref aGoldMonet, 1, lEnemies[i].Location);
                }

            this.EnemiesCollisionWithHero();
        }

        Vector2 rot = new Vector2(5, 1);

        private void EnemiesCollisionWithHero()
        {
            for (short i = 0; i < lEnemies.Count; i++)
            {
                if (!lEnemies[i].Enabled)
                {
                    if (eric.CollisionWithFireZone(lEnemies[i].GetBounds()) && eric.Attack)
                    {
                        for (short j = 0; j < lShells.Count; j++)
                        {
                            if (lShells[j].Enabled)
                            {
                                lShells[j].Enabled = false;
                                lShells[j].Location = eric.Location;

                                // 2 и 3
                                if (lEnemies[i].Location.Y <= eric.Location.Y && lEnemies[i].Location.X <= eric.Location.X
                                    || lEnemies[i].Location.Y >= eric.Location.Y && lEnemies[i].Location.X <= eric.Location.X)
                                {
                                    ((Shell)lShells[j]).isAt = true;
                                }
                                else
                                    // 4 и 1
                                    if (lEnemies[i].Location.Y >= eric.Location.Y && lEnemies[i].Location.X >= eric.Location.X
                                        || lEnemies[i].Location.Y <= eric.Location.Y && lEnemies[i].Location.X >= eric.Location.X)
                                    {
                                        ((Shell)lShells[j]).isAt = false;
                                    }


                                ((Shell)lShells[j]).Rotation = (float)Math.Atan2(lEnemies[i].Location.X - eric.Location.X,
                                                                                 lEnemies[i].Location.Y - 40 - eric.Location.Y);


                                lGlobal.Add(lShells[j]);
                                return;
                            }
                        }
                    }
                }
            }
        }








        // ПЕРЕПИСАТЬ
        private void isFire(ITower tower)
        {
            if (tower.Fire == false)
            {
                if (tower.Type == "ElectricTowerOne")
                {
                    for (int j = 0; j < lLightnings.Count; j++)
                    {
                        if (lLightnings[j].Enabled)
                        {
                            lLightnings[j].Enabled = false;
                            vLocation.X = ((IGameObject)tower).Location.X + 40;
                            vLocation.Y = ((IGameObject)tower).Location.Y + 35; ;
                            lLightnings[j].Location = vLocation;
                            ((Lightning)lLightnings[j]).Rotation = (float)Math.Atan2(((Tower)tower).Ene.Location.Y + 40 - lLightnings[j].Location.Y,
                                                                                     ((Tower)tower).Ene.Location.X - lLightnings[j].Location.X + 40);
                            lGlobal.Add(lLightnings[j]);
                            break;
                        }
                        else continue;
                    }

                    this.OtherObjectCreate(ref lBangs, ref OPBangs, vLocation);
                }
            }
            else
            {

                //if (OPShells.Count > 0)
                //{
                //    lShells.Add(OPShells.Pop());
                //    lShells[lShells.Count - 1].Enabled = false;
                //    lShells[lShells.Count - 1].Location = ((Tower)tower).Location;


                //    ((IShell)lShells[lShells.Count - 1]).Rotation = (float)Math.Atan2(((Tower)tower).Location.X - ((Tower)tower).Ene.Location.X, ((Tower)tower).Location.Y - ((Tower)tower).Ene.Location.Y);



                //    lGlobal.Add(lShells[lShells.Count - 1]);
            }
        }


        // Проверяем вылет снаряда за границы экрана Реф --
        private bool ShellCollisionWithBound(ref short index)
        {
            if (lShells[index].Location.X <= 0 || lShells[index].Location.X >= SPHalper.RenderTarget.Bounds.Width
                || lShells[index].Location.Y <= 0 || lShells[index].Location.Y >= SPHalper.RenderTarget.Bounds.Height)
            {
                lShells[index].Enabled = true;
                //OPShells.Push(lShells[index]);
                // lGlobal.Remove(lShells[index]);
                lShells.Remove(lShells[index]);
                return true;
            }
            else return false;
        }










        // Реф +
        //private void OtherObjectLogic(ref List<IGameObject> list, ref ObjectPool<IGameObject> pool)
        //{
        //    if (list.Count > 0)
        //    {
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            if (list[i].Enabled)
        //            {
        //                pool.Push(list[i]);
        //                lGlobal.Remove(list[i]);
        //                list.Remove(list[i]);
        //            }
        //        }
        //    }
        //}

        // Реф +
        private void OtherObjectCreate(ref List<IGameObject> list, ref ObjectPool<IGameObject> pool, Vector2 location)
        {
            if (pool.Count > 0)
            {
                list.Add(pool.Pop());
                list.Last().Location = location;
                list.Last().Enabled = false;
                lGlobal.Add(list.Last());
            }
        }




        private void ShellsLogic(ref List<IGameObject> sList, ref List<IGameObject> eList, ref List<IGameObject> tList)
        {
            for (short i = 0; i < sList.Count; i++)
            {
                if (sList[i].Enabled)
                {
                    lGlobal.Remove(sList[i]);
                }

                else
                {
                    for (short j = 0; j < tList.Count; j++)
                    {
                        if (!((Tower)tList[j]).Fire)
                        {
                            // this.OtherObjectCreate(ref lBloods, ref OPBloods, ((ElectricTower)tList[j]).Ene.Location);
                            ((Tower)tList[j]).Ene.Health -= 5;
                            ((Tower)tList[j]).Ene.Alert = true;
                        }
                    }
                }


            }
        }

        private void SnowballLogic(ref List<IGameObject> sList, ref List<IGameObject> eList)
        {
            for (short i = 0; i < sList.Count; i++)
            {
                if (sList[i].Enabled) lGlobal.Remove(sList[i]);
                else
                {
                    if (sList[i].Location.Y > 2000) sList[i].Enabled = true;
                }
            }

        }


        private void CreateGoldMonet(ref GoldMonet[] mArray, int index, Vector2 location)
        {
            for (int i = 0; i < index; i++)
            {
                for (int j = 0; j < mArray.Length; j++)
                {
                    if (mArray[j].Enabled)
                    {
                        mArray[j].Enabled = false;
                        mArray[j].Location = location;
                        lGlobal.Add(mArray[j]);
                        break;
                    }
                }
            }
        }


        private void CircleAndArrowLogic(Vector2 cLocation, Vector2 aLocation)
        {
            if (Map.WorldMatrix[MGCollision.RowIndexCollision, MGCollision.CollumnIndexCollision].CellCondition == CellsCondition.OccupiedByTower)
            {
                // !!!!
                firingRadius.LayerDepth = eric.LayerDepth - 0.1f;
                firingRadius.Enabled = arrow.Enabled = false;

                cLocation.X = Map.WorldMatrix[MGCollision.RowIndexCollision, MGCollision.CollumnIndexCollision].Location.X - 80;
                cLocation.Y = Map.WorldMatrix[MGCollision.RowIndexCollision, MGCollision.CollumnIndexCollision].Location.Y - 80;

                aLocation.X = Map.WorldMatrix[MGCollision.RowIndexCollision, MGCollision.CollumnIndexCollision].Location.X + 20;
                aLocation.Y = Map.WorldMatrix[MGCollision.RowIndexCollision, MGCollision.CollumnIndexCollision].Location.Y - 110;

                arrow.Location = aLocation;
                firingRadius.Location = cLocation;
            }
            else firingRadius.Enabled = arrow.Enabled = true;
        }


         bool key = true;




        public override void Update(GameTime gameTime)
        {
            //if (eric.Attack)
            //{
            //    for (short i = 0; i <  lShells.Count; i++)
            //    {
            //        if (lShells[i].Enabled)
            //        {
            //            lShells[i].Enabled = false;
            //            lShells[i].Location = eric.Location;
            //            ((Shell)lShells[i]).Rotation = rot;
            //            lGlobal.Add(lShells[i]);
            //            break;
            //        }
            //    } 
            //}

            SnowballLogic(ref lShells, ref lEnemies);



            //if (eric.MovementCondition == MovementCondition.Moving)
            //{
            //    this.CreateGoldMonet(ref aGoldMonet, 1, eric.Location);
            //    mBonus.CreateBonus(TypeBonus.Speed, eric.Location, ref lGlobal);
            //}




           // if (Keyboard.GetState().IsKeyDown(Keys.F10))
           // {
            if (SPDataHalper.SPEnemyCount <= 0)
            {


                SPDataHalper.SPEnemyName = " ";
                if (key)
                {
                    MView.Create(Game, "CanadianOne", SPDataHalper.SPEnemyCount = Randomize.Random.Next(10, 55), ref lEnemies, ref lGlobal);
                    key = false;
                }
                else
                {
                    MView.Create(Game, "GingersOne", SPDataHalper.SPEnemyCount = Randomize.Random.Next(10, 55), ref lEnemies, ref lGlobal);
                    key = true;
                }
                //  GC.Collect();

            }











            MGCollision.HCollisionWithCell(eric);




            MView.Update(gameTime);


            SPDataHalper.SPTowersCount = lTowers.Count;








            this.EnemiesCollision();



            // +++
            this.CameraZoom(eric.Location);
            // +++
            this.CreateTower(eric.BuildingCondition);











            this.TowersLogic();


            // +++
            this.ShellsLogic(ref lLightnings, ref lEnemies, ref lTowers);


            // Логика вспомогательных объектов
            // this.OtherObjectLogic(ref lBloods, ref OPBloods);
            //  this.OtherObjectLogic(ref lBangs, ref OPBangs);
            this.CircleAndArrowLogic(firingRadius.Location, arrow.Location);





           






            for (int i = 0; i < lGlobal.Count; i++)
                if (lGlobal[i].Enabled) lGlobal.Remove(lGlobal[i]);
                else lGlobal[i].Update(gameTime);

            

            // !!!!!



        }



        // Реф +
        public void Draw(GameTime gameTime)
        {


            for (short i = 0; i < lGlobal.Count; i++)
                if (lGlobal[i].Location.X >= SPHalper.RenderTarget.Bounds.Left - 100)
                    lGlobal[i].Draw(gameTime);


        }
    }
}

  