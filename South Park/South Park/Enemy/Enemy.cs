using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace South_Park
{
    class Enemy : IGameObject, IEnemy
    {
        private Animation animation; // Анимация
        public Texture2D[] tMovement, tDamage;

        

        private HealthStrip healthStrip;
        private int alertTime;



       
        
        public Enemy(Game game, string typeEnemy = "None", float step = 1, ArmorType armoreTypeOne = ArmorType.None, 
                     ArmorType armoreTypeTwo = ArmorType.None)
        {
           
    

           

            this.Type = typeEnemy;
            this.Enabled = true;

            this.ArmorTypeOne = armoreTypeOne;
            this.ArmorTypeTwo = armoreTypeTwo;

 
            this.Direction = 4;
            this.Location = Vector2.Zero;

            tMovement = new Texture2D[20];
            tDamage = new Texture2D[10];

            this.LoadContent(game.Content);

            healthStrip = new HealthStrip(game);

            animation = new Animation(game, tMovement[0], EnemyManager.Size(this.Type), this.Location, 8, 2);

            this.Condition = EnemyCondition.Stopped;
            this.Health = 100;
            this.Step = step;
            this.Alert = false;
        }





        // +++
        private void LoadContentFuturePerson(ref ContentManager contentManager)
        {
            switch (this.Type)
            {
                case "FuturePersonOne":
                    EnemyManagerContent.FuturePerson(contentManager, ref tMovement, ref tDamage, 1); break;
                case "FuturePersonTwo":
                    EnemyManagerContent.FuturePerson(contentManager, ref tMovement, ref tDamage, 2); break;
                case "FuturePersonThree":
                    EnemyManagerContent.FuturePerson(contentManager, ref tMovement, ref tDamage, 1); break;
                case "FuturePersonFour":
                    EnemyManagerContent.FuturePerson(contentManager, ref tMovement, ref tDamage, 1); break;
                case "FuturePersonFive":
                    EnemyManagerContent.FuturePerson(contentManager, ref tMovement, ref tDamage, 1); break;
            }
        }
        private void LoadContentGingers(ref ContentManager contentManager)
        {
            switch (this.Type)
            {
                case "GingersOne":
                    EnemyManagerContent.Gingers(contentManager, ref tMovement, ref tDamage, 1); break;
                case "GingersTwo":
                    EnemyManagerContent.Gingers(contentManager, ref this.tMovement, ref tDamage, 2); break;
                case "GingersThree":
                    EnemyManagerContent.Gingers(contentManager, ref this.tMovement, ref tDamage, 3); break;
                case "GingersFour":
                    EnemyManagerContent.Gingers(contentManager, ref this.tMovement, ref tDamage, 4); break;
                case "GingersFive":
                    EnemyManagerContent.Gingers(contentManager, ref this.tMovement, ref tDamage, 5); break;
            }
        }
        private void LoadContentCanadian(ref ContentManager contentManager)
        {
            switch (this.Type)
            {
                case "CanadianOne":
                    EnemyManagerContent.Canadian(contentManager, ref tMovement, ref tDamage, 1); break;
                case "CanadianTwo":
                    EnemyManagerContent.Canadian(contentManager, ref this.tMovement, ref tDamage, 2); break;
                case "CanadianThree":
                    EnemyManagerContent.Canadian(contentManager, ref this.tMovement, ref tDamage, 3); break;
                case "CanadianFour":
                    EnemyManagerContent.Canadian(contentManager, ref this.tMovement, ref tDamage, 4); break;
                case "CanadianFive":
                    EnemyManagerContent.Canadian(contentManager, ref this.tMovement, ref tDamage, 5); break;
            }
        }
  



        private void LoadContent(ContentManager contentManager)
        {
            

            // если враг ЧЕЛОВЕК БУДУЩЕГО
            if (this.Type == "FuturePersonOne" || this.Type == "FuturePersonTwo" || this.Type == "FuturePersonThree"
                || this.Type == "FuturePersonFour" || this.Type == "FuturePersonFive") this.LoadContentFuturePerson(ref contentManager);

            // если враг РЫЖИЕ ДЕТИ
            if (this.Type == "GingersOne" || this.Type == "GingersTwo" || this.Type == "GingersThree"
                || this.Type == "GingersFour" || this.Type == "GingersFive") this.LoadContentGingers(ref contentManager);


            // если враг КАНАДЦЫ
            if (this.Type == "CanadianOne" || this.Type == "CanadianTwo" || this.Type == "CanadianThree"
                || this.Type == "CanadianFour" || this.Type == "CanadianFive") this.LoadContentCanadian(ref contentManager);






        }


        public void Construct(ContentManager contentManager)
        {
            this.LoadContent(contentManager);

            animation.Size = EnemyManager.Size(this.Type);
        }


        public bool CreateGold { get; set; }

        public bool Enabled { get; set; }

        public EnemyCondition Condition { get; set; }
   
        public Vector2 Location { get; set; }
     
        public float LayerDepth { get; set; }

        public string Type { get; set; }
      
   
        public sbyte ColumnIndex { get; set; }
       
        public sbyte Direction { get; set; }
        
        public sbyte RowIndex { get; set; }
        
        public float Health { get; set; }
       
        public float Step { get; set; }


        public bool Alert { get; set; }


        public ArmorType ArmorTypeOne { get; set; }
        public ArmorType ArmorTypeTwo { get; set; }

        





        
        public bool CheckCollision(Rectangle mask)
        {
            return EnemyManager.CollisionBounds(this.Type, this.Location).Intersects(mask);
        }
   
        public Rectangle GetBounds()
        {
            return EnemyManager.CollisionBounds(this.Type, this.Location);
        }

        public System.Drawing.Size Size()
        {
            return EnemyManager.Size(this.Type);
        }



        public void Stopped()
        {
            animation.Location = this.Location = EnemyManager.Stop(this.Direction, this.Location, (int)this.Step);
            this.Location = EnemyManager.Stop(this.Direction, this.Location, (int)this.Step);
        }


       

  



        public void Update(GameTime gameTime)
        {
            if (this.Health <= 0)
            {
                this.CreateGold = true;
                this.Enabled = true;
            }
            else this.CreateGold = false;

            //if (this.Health <= 0)
            //{
            //    this.Enabled = true;
            //    this.CreateGold = false;
            //}

           

                if (this.Alert)
                {
                    alertTime++;
                    //animation.Construct(this.tDamage[this.Direction], this.Location, this.LayerDepth);
                    if (alertTime > 8)
                    {
                        alertTime = 0;
                        this.Alert = false;

                    }
                }
                else animation.Construct(this.tMovement[this.Direction], this.Location, this.LayerDepth);


                this.Location = EnemyManager.Movement(this.Direction, this.Location, this.Step);
                this.LayerDepth = (this.Location.Y + EnemyManager.Size(this.Type).Height) / SPHalper.RenderTarget.Bounds.Height;





                healthStrip.Construct(this.Location.X, this.Location.Y, this.Health);

                animation.Update(gameTime);
            

            

        }



        




        // ~!~ переписать шейдерный эффект(проход) тк лагает сортировка при эффекте и это уменьшит кол-во переключений SP.Begin()
        public void Draw(GameTime gameTime)
        {
            if (Alert)
            {
                // добавить базовые эффекты дамага 
                animation.TimeFrame = 0.1f;
                animation.Frequency = 10;

        
              
         
                animation.Draw(gameTime);
            
                

            }
            else
            {


                animation.Frames = 2;
                animation.Draw(gameTime);
            }

            healthStrip.Draw(gameTime);

            if (SPHalper.EditorState) SPHalper.SpriteBatch.DrawRectangle(EnemyManager.CollisionBounds(this.Type, this.Location), Color.Lime);

        }
    }
}


