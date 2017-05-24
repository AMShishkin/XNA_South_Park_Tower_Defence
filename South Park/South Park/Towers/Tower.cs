using System;
using Microsoft.Xna.Framework;


namespace South_Park
{
    class Tower : IGameObject, ITower
    {
        // Анимация
        private Animation Animation;
        // Полоска здоровья
        private HealthStrip healthStrip;
        // Таймер перезарядки башни
        private short timer = 0;




        public Tower(Game game, TowerType towerType = TowerType.None, DamageType damageTypeOne = DamageType.None, 
                     DamageType damageTypeTwo = DamageType.None)
        {
            this.Enabled = true;

            this.Level = this.Experience = 0;

            this.Location = Vector2.Zero;
            this.RechargeTime = 1;
            this.Rotation = 0.0F;
            this.Health = 100;
            this.Fire = true;

            this.Type = towerType.ToString();
            this.DamageTypeOne = damageTypeOne;
            this.DamageTypeTwo = damageTypeTwo;
            this.Condition = TowerCondition.Scaning;

            healthStrip = new HealthStrip(game, false);

            Animation = new Animation(game, ManagerTower.Texture(this.Type, this.Condition),
                                      new System.Drawing.Size(80, 100), this.Location, 8, 9);
        }


        public TowerCondition         Condition { get; set; }
        public DamageType         DamageTypeOne { get; set; }
        public DamageType         DamageTypeTwo { get; set; }
        public string                      Type { get; set; }
        public bool                     Enabled { get; set; }
        public float               RechargeTime { get; set; }
        public float                 LayerDepth { get; set; }
        public Vector2                 Location { get; set; }
        public int                   Experience { get; set; }
        public float                   Rotation { get; set; }
        public int                       Health { get; set; }
        public int                        Level { get; set; }
        public bool                        Fire { get; set; }

        

        

        


        // !!!!!!

        public Enemy Ene { get; set; }


        public void FireZone(object sender, EventArgs e)
        {
            // !!!! 
            // this.Ene = (Gingers)sender;
            this.Ene = (Enemy)sender;


            if (this.RechargeTime >= 1)
            {
                this.RechargeTime = 0;
                this.Fire = false;

                timer = 45;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (timer > 0)
            {
                this.Condition = TowerCondition.Fire;

                // Переписать лагает из за строк типа this.type + this.condition;
                if (timer == 45) Animation.Construct(ManagerTower.Texture(this.Type, this.Condition), 9);
                timer--;
            }
            else
            {

                this.Condition = TowerCondition.Scaning;
   
                if (timer == 0) Animation.Construct(ManagerTower.Texture(this.Type, this.Condition), 9);
            }








            this.Fire = true;
            this.RechargeTime += 0.008f;



            Animation.Construct(this.Location, this.LayerDepth);
            healthStrip.Construct(this.Location.X + 10, this.Location.Y, this.Health);
            Animation.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            Animation.Draw(gameTime);
            healthStrip.Draw(gameTime);
        }



        public System.Drawing.Size Size()
        {
            return ManagerTower.Size(this.Type);
        }
       
        public bool CheckCollision(Rectangle mask)
        {
            return ManagerTower.CollisionBounds(this.Type, this.Location).Intersects(mask);
        }
       

        public Rectangle GetBounds()
        {
            return ManagerTower.CollisionBounds(this.Type, this.Location);
        }
    }
}
