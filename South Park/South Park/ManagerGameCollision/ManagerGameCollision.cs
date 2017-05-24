using System;
using Microsoft.Xna.Framework;

namespace South_Park
{
    class ManagerGameCollision
    {
       





        // Координаты клетки с которой произошло столкновение
        public Vector2 LocationCellCollistion { get; set; }
        // Номер столбца клетки с которой произошло столкновение
        public int RowIndexCollision { get; set; }
        // Номер строки клетки с которой произошло столкновение
        public int CollumnIndexCollision { get; set; } 

        


        public void CollisionWithCell(IHero hero, sbyte rowIndex, sbyte columnIndex)
        {
            if (hero.CheckCollision(Map.WorldMatrix[hero.RowIndex - rowIndex, hero.ColumnIndex - columnIndex].Rectangle))
            {
                hero.CollisionCondition = CollisionCondition.Faces;
              //  Map.WorldMatrix[hero.RowIndex - rowIndex, hero.ColumnIndex - columnIndex].CellCondition = CellsCondition.DefeatZoneHero;
                LocationCellCollistion = Map.WorldMatrix[hero.RowIndex - rowIndex, hero.ColumnIndex - columnIndex].Location;
                RowIndexCollision = hero.RowIndex = (sbyte)(hero.RowIndex - rowIndex);
                CollumnIndexCollision = hero.ColumnIndex = (sbyte)(hero.ColumnIndex - columnIndex);
            }
            else hero.CollisionCondition = CollisionCondition.NotFaces;
        }


        private void met(ref IEnemy enemie, sbyte direction)
        {
            switch (direction)
            {
                case 0: enemie.RowIndex--; break;

                case 1:
                    {
                        enemie.RowIndex--;
                        enemie.ColumnIndex--;
                        break;
                    }

                case 2: enemie.ColumnIndex--; break;
                case 3:
                    {
                        enemie.RowIndex++;
                        enemie.ColumnIndex--;
                        break;
                    }
                   
                case 4: enemie.RowIndex++; 
                    break;



                case 5:
                    {
                        enemie.RowIndex++;
                        enemie.ColumnIndex++;
                        break;
                    }

                case 6: enemie.ColumnIndex--; 
                    break;

                case 7:
                    {
                        enemie.RowIndex--;
                        enemie.ColumnIndex--;
                        break;
                    }
            }
        }


   
        public void CollisionWithCell(IEnemy enemy, sbyte rowIndex, sbyte columnIndex)
        {
            if (enemy.CheckCollision(Map.WorldMatrix[enemy.RowIndex - rowIndex, enemy.ColumnIndex - columnIndex].Rectangle))
            {
                if (Map.WorldMatrix[enemy.RowIndex, enemy.ColumnIndex].CellCondition == CellsCondition.DefeatZoneTower)
                    Map.WorldMatrix[enemy.RowIndex, enemy.ColumnIndex].OnCollision(enemy);

                if (Map.WorldMatrix[enemy.RowIndex, enemy.ColumnIndex].CellCondition == CellsCondition.DefeatZoneHero)
                    Eric.Health--;


                switch(Map.WorldMatrix[enemy.RowIndex - rowIndex, enemy.ColumnIndex - columnIndex].CellMovementDirectionCondition)
                {
                    case CellsMovementDirectionCondition.DirectionLeft:
                        {
                            enemy.Direction = 0;

                            break;
                        }
                    case CellsMovementDirectionCondition.DirectionLeftUp : enemy.Direction = 1; break;
                    case CellsMovementDirectionCondition.DirectionUp :
                        this.met(ref enemy, enemy.Direction);
                        enemy.Direction = 2; 
                        break;
                    case CellsMovementDirectionCondition.DirectionRightUp : enemy.Direction = 3; break;

                    case CellsMovementDirectionCondition.DirectionRight:
                        {
                            this.met(ref enemy, enemy.Direction);
                          //  location.X = enemie.Location.X;
                          //  location.Y = Randomize.Random.Next((int)enemie.Location.Y, (int)enemie.Location.Y + 10);

                          //  enemie.Location = location;


                            enemy.Direction = 4;
                            break;
                        }
                    case CellsMovementDirectionCondition.DirectionRightDown : enemy.Direction = 5; break;

                    case CellsMovementDirectionCondition.DirectionDown:
                        {
                            this.met(ref enemy, enemy.Direction);

                            
                           // location.X = Randomize.Random.Next((int)enemie.Location.X, (int)enemie.Location.X + 10);
                           // location.Y = enemie.Location.Y;

                           // enemie.Location = location;
                            enemy.Direction = 6;
                            break;
                        }
                    case CellsMovementDirectionCondition.DirectionLeftDown : enemy.Direction = 7; break;
                    case CellsMovementDirectionCondition.DirectionAbsent : break;       
                }

                if (Map.WorldMatrix[enemy.RowIndex - rowIndex, enemy.ColumnIndex - columnIndex].CellCondition == CellsCondition.Finish)
                {
                    enemy.Location = Vector2.Zero;
                    enemy.Condition = EnemyCondition.Stopped;
                    ((Enemy)enemy).Enabled = true;
                    enemy.Health = 100;
                    enemy.Step = 0;
                    enemy.RowIndex = 0;
                    enemy.ColumnIndex = 0;
                    SPDataHalper.SPEnemyCount--;
                }

                enemy.RowIndex = (sbyte)(enemy.RowIndex - rowIndex);
                enemy.ColumnIndex = (sbyte)(enemy.ColumnIndex - columnIndex);
                
            }
        }



       
        public Vector2 HExitFromMap(Vector2 location)
        {

            if (location.X < 81 || location.X > 1520)
            {
                if (location.X < 81) location.X += 3;
                else location.X -= 3;
            }

            if (location.Y < 220 || location.Y > 685)
            {
                if (location.Y < 220) location.Y += 3;
                else location.Y -= 3;
            }
            return location;
        }
       
        // Проверка коллизий Эрика с игровыми клетками
        public void HCollisionWithCell(IHero hero)
        {
            // Если Эрик жив
            if (hero.Condition == Condition.Alive) 
            {
                // Если Эрик движется
                if (hero.MovementCondition == MovementCondition.Moving)
                {
                    // Проверяем не вышел ли Эрик за ганицы игровой зоны
                    hero.Location = HExitFromMap(hero.Location);
                    // Проверяем столкновения с клетками по направлению движения
                    if (hero.MDCondition == MovementDirectionCondition.MovingLeft)
                    {
                        CollisionWithCell(hero, 0, 1);
                        CollisionWithCell(hero, 1, 1);
                        CollisionWithCell(hero, -1, 1);
                    }
                    else if (hero.MDCondition == MovementDirectionCondition.MovingLeftUp)
                    {
                        CollisionWithCell(hero, 1, 1);
                        CollisionWithCell(hero, 1, 0);
                        CollisionWithCell(hero, 0, 1);
                    }
                    else if (hero.MDCondition == MovementDirectionCondition.MovingLeftDown)
                    {
                        CollisionWithCell(hero, -1, 1);
                        CollisionWithCell(hero, 0, 1);
                        CollisionWithCell(hero, -1, 0);
                    }
                    else if (hero.MDCondition == MovementDirectionCondition.MovingRight)
                    {
                        CollisionWithCell(hero, 0, -1);
                        CollisionWithCell(hero, 1, -1);
                        CollisionWithCell(hero, -1, -1);
                    }
                    else if (hero.MDCondition == MovementDirectionCondition.MovingRightUp)
                    {
                        CollisionWithCell(hero, 1, -1);
                        CollisionWithCell(hero, 0, -1);
                        CollisionWithCell(hero, 1, 0);
                    }
                    else if (hero.MDCondition == MovementDirectionCondition.MovingRightDown)
                    {
                        CollisionWithCell(hero, -1, -1);
                        CollisionWithCell(hero, -1, -1);
                        CollisionWithCell(hero, -1, -1);
                    }
                    else if (hero.MDCondition == MovementDirectionCondition.MovingUp)
                    {
                        CollisionWithCell(hero, 1, 0);
                        CollisionWithCell(hero, 1, -1);
                        CollisionWithCell(hero, 1, 1);
                    }
                    else if (hero.MDCondition == MovementDirectionCondition.MovingDown)
                    {
                        CollisionWithCell(hero, -1, 0);
                        CollisionWithCell(hero, -1, -1);
                        CollisionWithCell(hero, -1, 1);
                    }
                }
            }
        }
                
            
       public void CEnemieWithIndexMove(IEnemy enemie)
            {
                switch (enemie.Direction)
                {
                    case 0: CollisionWithCell(enemie, 0, 1); break;
                    case 1: CollisionWithCell(enemie, 1, 1); break;
                    case 2: CollisionWithCell(enemie, 1, 0); break;
                    case 3: CollisionWithCell(enemie, 1, -1); break;
                    case 4: CollisionWithCell(enemie, 0, -1); break;
                    case 5: CollisionWithCell(enemie, -1, -1); break;
                    case 6: CollisionWithCell(enemie, -1, 0); break;
                    case 7: CollisionWithCell(enemie, -1, 1); break;
                }



                
            }


 
    }
}
