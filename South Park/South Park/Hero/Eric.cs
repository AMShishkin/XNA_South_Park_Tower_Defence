using System;
using System.Collections.Generic;
using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace South_Park
{
    class Eric : IGameObject, IHero
    {
        // Текстуры передвижения
        private Dictionary<String, Texture2D> tMovement; 
        // Методы обновления логики в режиме енота(uForce) и отрисовка в режиме енота(dForce)
        private Dictionary<String, Action<GameTime>> uForce;
        // uFCoon - логика енота, uFNormal - логика картмана, dFCoon - отрисовка енота, dFNormla - отрисовка картмана 
        private Action<GameTime> uFCoon, uFNormal;

        // Полоска здоровья
        private HealthStrip healthStrip;
        // Полоска отката атаки
        private HealthStrip attackStrip;

        // Анимация
        private Animation animation, aCircleFireZone;

        private ManagerHero mHero;

        private Rectangle bFireZone;

        int timer = 0;
       
 
        public Eric(Game game, Vector2 startLocation)
        {
            // Стартовая позиция
            this.Location = Eric.WorldLocation = startLocation;
            // Инициализация
            this.Initialize(game);
            // Общее состояние - живой
            this.Condition = Condition.Alive;
            // Cостояние передвижения - стоит
            this.MovementCondition = MovementCondition.Stopped;
            // Cостояние доступности - доступен
            this.Enabled = false;
            // Состояние коллизий - не сталкивается
            this.CollisionCondition = CollisionCondition.NotFaces; 
            // Глубина отрисовки
            this.LayerDepth = (this.Location.Y + mHero.Size().Height) / SPHalper.RenderTarget.Bounds.Height;
            // Состояние силы - нормальный
            this.FCondition = "Normal";

            Block = false;


            Level = 1;
            Experience = 0;
            Step = 3;
            ColumnIndex = 4;
            RowIndex = 4;
            IsCollision = false;
            

            this.Attack = false;
            bFireZone = new Rectangle((int)this.Location.X, (int)this.Location.Y, 320, 320);



        }
        
        // Инициализация
        private void Initialize(Game game)
        {
            mHero = new ManagerHero();
            tMovement = new Dictionary<String, Texture2D>();
            mHero.LoadContent(game.Content, ref tMovement);

            healthStrip = new HealthStrip(game);
            attackStrip = new HealthStrip(game);
            
            animation = new Animation(game)
            {
                Texture = this.tMovement["EMGDown"],
                Location = this.Location,
                Size = mHero.Size(),
                Frames = 2,
                Frequency = 8
            };

            aCircleFireZone = new Animation(game)
            {
                Texture = ManagerGameContent.Texture("CircleFireArea"),
                Location = new Vector2(bFireZone.X, bFireZone.Y),
                Size = new System.Drawing.Size(320, 320),
                Frames = 5,
                Frequency = 8,
                TimeFrame = 0.07f,
            };



            uFNormal = new Action<GameTime>(this.UpdateForceNormal);
            uFCoon = new Action<GameTime>(this.UpdateForceCoon);


            uForce = new Dictionary<String, Action<GameTime>>
            {
                { "Normal", this.UpdateForceNormal },
                { "Coon", this.UpdateForceCoon },
            };

        }



        public static Vector2 WorldLocation { get; set; }

        public Vector2 Location { get; set; }


        public static bool Block { get; set; }


        public Condition Condition { get; set; }
        public MovementCondition MovementCondition { get; set; }
        public MovementDirectionCondition MDCondition { get; set; }
        public bool Enabled { get; set; }
        public CollisionCondition CollisionCondition { get; set; }
        public BuildingCondition BuildingCondition { get; set; }

        public string FCondition { get; set; }

        public sbyte RowIndex { get; set; }

        public sbyte ColumnIndex { get; set; }


        
        public float LayerDepth { get; set; }
      
        public short InactivityTime { get; set; }
       
        public static int Health { get; set; }
      
        public byte Level { get; set; }
       
        public float Experience { get; set; }

      
        public float Step { get; set; }
        
        public bool IsCollision { get; set; }


        public bool Attack { get; set; }


     



        public Rectangle GetBounds()
        {
            return mHero.Bounds(this.Location);
        }


        public System.Drawing.Size Size()
        {
            return mHero.Size();
        }



        public bool CheckCollision(Rectangle mask)
        {
            return mHero.Bounds(this.Location).Intersects(mask);
        }

 

        private void BuildTowers()
        {
            this.BuildingCondition = BuildingCondition.ElectricTowers; 
        }



        public bool CollisionWithFireZone(Rectangle bounds)
        {
            return bFireZone.Intersects(bounds);
        }



        // Обработка нажатия клавиши
        private int KeyIsPressed(KeyboardState keyBord)
        {
            
                if (keyBord.IsKeyDown(Keys.Left)) return 0;
                else if (keyBord.IsKeyDown(Keys.Up)) return 1;
                else if (keyBord.IsKeyDown(Keys.Right)) return 2;
                else if (keyBord.IsKeyDown(Keys.Down)) return 3;
                else return 4;
         
        }

    


        // +++ Анимация движения
        private void ExecuteMovementAnimation(string name, sbyte startFrame)
        {
            if (this.MovementCondition != MovementCondition.Moving) this.MovementCondition = MovementCondition.Moving;
            Eric.WorldLocation = this.Location = mHero.Movement(this.MDCondition, this.Location, this.Step);

            if (this.MDCondition != MovementDirectionCondition.MovingLeft ||
                this.MDCondition != MovementDirectionCondition.MovingRight) this.LayerDepth = (this.Location.Y + mHero.Size().Height) / SPHalper.RenderTarget.Bounds.Height;

            if (this.MDCondition == MovementDirectionCondition.MovingRight || 
                this.MDCondition == MovementDirectionCondition.MovingRightDown ||
                this.MDCondition == MovementDirectionCondition.MovingRightUp) animation.MirrorDisplay = true;
            else animation.MirrorDisplay = false;

            animation.Construct(this.tMovement[name], this.Location, this.LayerDepth);
        }

      


        





       
        private void WithoutMovement()
        {
            animation.MirrorDisplay = false;
            this.MovementCondition = MovementCondition.Stopped;

            this.InactivityTime++;

            if (this.FCondition == "Normal")
            {
                if (Eric.Health > 0 && Eric.Health < 31)
                    animation.Construct(this.tMovement["ESBad"], this.Location, this.LayerDepth);
                else if (Eric.Health > 30 && Eric.Health < 66)
                    animation.Construct(this.tMovement["ESNormal"], this.Location, this.LayerDepth);
                else if (Eric.Health > 66 && Eric.Health < 101)
                    animation.Construct(this.tMovement["ESGood"], this.Location, this.LayerDepth);
            }
            else animation.Construct(this.tMovement["CSDown"], this.Location, this.LayerDepth);
                
          //  if (this.Direction != 3) this.Direction = 3;
        }






        private void UpdateForceCoon(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right)
                            || Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up)
                            || Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down)
                            || Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up)
                            || Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left)) this.MDCondition = MovementDirectionCondition.MovingLeft;
                if (Keyboard.GetState().IsKeyDown(Keys.Right)) this.MDCondition = MovementDirectionCondition.MovingRight;

                if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up)) this.MDCondition = MovementDirectionCondition.MovingLeftUp;
                if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down)) this.MDCondition = MovementDirectionCondition.MovingLeftDown;

                if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up)) this.MDCondition = MovementDirectionCondition.MovingRightUp;
                if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down)) this.MDCondition = MovementDirectionCondition.MovingRightDown;

                this.ExecuteMovementAnimation("CMLeftRight", 0);
            }
            else
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        this.MDCondition = MovementDirectionCondition.MovingUp;
                        this.ExecuteMovementAnimation("CMUp", 0);
                    }
                    else
                    {
                        this.MDCondition = MovementDirectionCondition.MovingDown;
                        this.ExecuteMovementAnimation("CMDown", 0);
                    }
                }
                else this.WithoutMovement();
           
                   
                    
                        

                   
        }

        private void UpdateForceNormal(GameTime gameTime)
        {
            if (!Block && this.Condition != Condition.Dead)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right)
                    || Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up)
                    || Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down)
                    || Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up)
                    || Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left)) this.MDCondition = MovementDirectionCondition.MovingLeft;
                    if (Keyboard.GetState().IsKeyDown(Keys.Right)) this.MDCondition = MovementDirectionCondition.MovingRight;

                    if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Up)) this.MDCondition = MovementDirectionCondition.MovingLeftUp;
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Down)) this.MDCondition = MovementDirectionCondition.MovingLeftDown;

                    if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Up)) this.MDCondition = MovementDirectionCondition.MovingRightUp;
                    if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Down)) this.MDCondition = MovementDirectionCondition.MovingRightDown;

                    this.ExecuteMovementAnimation("EMLeftRight", 0);
                }
                else
                    if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            this.MDCondition = MovementDirectionCondition.MovingUp;
                            this.ExecuteMovementAnimation("EMUp", 0);
                        }
                        else
                        {
                            this.MDCondition = MovementDirectionCondition.MovingDown;
                            if (Health > 0 && Health < 31) this.ExecuteMovementAnimation("EMBDown", 0);
                            else if (Health > 30 && Health < 66) this.ExecuteMovementAnimation("EMNDown", 0);
                            else if (Health > 66 && Health < 101) this.ExecuteMovementAnimation("EMGDown", 0);
                        }
                    }
                    else this.WithoutMovement();
            }
        }

        int keyTimer = 0;



        public void Update(GameTime gameTime)
        {
            bFireZone.X = (int)this.Location.X - 120;
            bFireZone.Y = (int)this.Location.Y - 120;


            if (timer <= 100)
            {
                timer++;

                this.Attack = false;
            }
            else
            {
                this.Attack = true;
                timer = 0;

            }





           


            


            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {

                Eric.Health--;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.F2)) Eric.Health++;

            if (Keyboard.GetState().IsKeyDown(Keys.F12)) SPDataHalper.SPMoney++;
            if (Keyboard.GetState().IsKeyDown(Keys.F11)) SPDataHalper.SPMoney--;



             keyTimer++;




             if (Keyboard.GetState().IsKeyDown(Keys.Space) && keyTimer > 10)
             {
                 if (Block) this.BuildTowers();
                 if (this.MDCondition != MovementDirectionCondition.MovingDown) this.WithoutMovement();
                 Block = !Block;
                 keyTimer = 0;
             }
             else if (this.BuildingCondition != BuildingCondition.Anything) this.BuildingCondition = BuildingCondition.Anything;
               





            


             if (Keyboard.GetState().IsKeyDown(Keys.F5))
             {
                 this.FCondition = "Coon";
                 ManagerGameInscription.InscriptionCondition = "CoonPower";
                 ManagerGameInscription.CPowerInscription(Vector2.Zero);

             }
             if (Keyboard.GetState().IsKeyDown(Keys.F6)) this.FCondition = "Normal";



             uForce[this.FCondition](gameTime);    

            

            
                
                    
                  


                    


            

                animation.Update(gameTime);
                aCircleFireZone.Construct(new Vector2(bFireZone.X, bFireZone.Y));

                aCircleFireZone.Update(gameTime);

                healthStrip.Construct(this.Location.X + 10, this.Location.Y - 10, Health);
                attackStrip.Construct(this.Location.X + 10, this.Location.Y + 80, timer);
             
            
        }






        public void Draw(GameTime gameTime)
        {
            animation.Draw(gameTime);
            healthStrip.Draw(gameTime);
            attackStrip.Draw(gameTime);
            if (SPHalper.EditorState) SPHalper.SpriteBatch.DrawRectangle(mHero.Bounds(this.Location), Color.Lime);
            aCircleFireZone.Draw(gameTime);
        }
    }
}
