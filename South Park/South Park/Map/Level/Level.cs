using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace South_Park
{
    class Level
    {
        private ManagerGameObject MGObject;
        private ManagerGameInterface MGInterface;

        private Texture2D tCell;
        private Rectangle rCell;

        private Map GameWorld;
        private Rectangle BackgroundBounds;

        private Vector2 location;

        private Thread tImput;

        private MenuCost mCost;

        private FPS fPS;
        private SpriteFont SGC;
        private Dictionary<String, Vector2> loc;

        public Level(Game game)
        {
            rCell = new Rectangle();

            GameWorld = new Map();

            this.BackgroundBounds = new Rectangle(-1, -1, SPHalper.RenderTarget.Bounds.Width + 100, SPHalper.RenderTarget.Bounds.Height + 100);

            this.Texture = new Texture2D[4];
            Camera2D.Zoom = 1f; // Зум камеры

            MGInterface = new ManagerGameInterface(game); // Менеджер отрисовки интерфейса

            tImput = new Thread(new ThreadStart(this.UserInput));
            tImput.IsBackground = true;

            loc = new Dictionary<string, Vector2>();

            this.LoadContent(game);

            mCost = new MenuCost(game);


            isKey = false;

            tImput.Start();

            

        }

        Texture2D[] Texture { get; set; } // !!


        private Texture2D Background { get; set; }

        public static bool isKey { get; set; }


        private void UserInput()
        {
            while (true)
            {
                Thread.Sleep(SPHalper.GameTime.ElapsedGameTime);

                MGInterface.DataConstruct((short)Eric.Health, SPDataHalper.SPMoney, 0, SPDataHalper.SPTowersCount, SPDataHalper.SPCityHealth, SPDataHalper.SPEnemyCount, SPDataHalper.SPEnemyName);



                
            }
        }

        public void Initialize()
        {

        }

       


        public void StartNewGame(Game game)
        {
            this.BuildLevel(@"C:\Users\TEMP\Desktop\South Park\South Park\South ParkContent\Levels\EricHouse\EricHouse.sou");
            MGObject = new ManagerGameObject(game);

            location.X = game.GraphicsDevice.ScissorRectangle.Width / 2 - 134;
            location.Y = game.GraphicsDevice.ScissorRectangle.Height / 2 - 77;

            ManagerGameInscription.LevelStartInscription(location);
            GC.Collect();
           
            
        }

        public void RestartGame(Game game)
        {
            location.X = game.GraphicsDevice.ScissorRectangle.Width / 2 - 134;
            location.Y = game.GraphicsDevice.ScissorRectangle.Height / 2 - 77;

            ManagerGameInscription.LevelStartInscription(location);

        }


        




        private void BuildLevel(string patch)
        {
            string[] _LevelName = File.ReadAllLines(patch);
            int _X = 0;
            int _Y = 0;

            for (sbyte i = 0; i < 13; i++)
            {
                string str = _LevelName[i];

                for (sbyte j = 0; j < 23; j++)
                {
                    location.X = _X;
                    location.Y = _Y;

                    char ch = str[j];
                    Map.WorldMatrix[i, j] = new Cell(location, i, j, CellsCondition.Free, CellsMovementDirectionCondition.DirectionAbsent, null);
                   

                    Map.Construct(ch, i, j);

                    _X += 80;
                }
                _X = -80;
                _Y += 80;
            }
           
        }

       
        



        private void LoadContent(Game game)
        {
            this.Background = ManagerBackground.GetBackground("EricHouse");

            this.Texture[0] = game.Content.Load<Texture2D>("Map/Cell/Cell");
            this.Texture[1] = game.Content.Load<Texture2D>("111");
            this.Texture[2] = game.Content.Load<Texture2D>("222");
            this.Texture[3] = game.Content.Load<Texture2D>("333");

            loc = new Dictionary<String, Vector2>
            {
                { "GarbageCollector", new Vector2(15, 10) },
                { "ElapsedGameTime", new Vector2(15, 40) },
                { "TotalMemory", new Vector2(15, 70) },
                { "FPS", new Vector2(15, 100) },
                { "Processor", new Vector2(15, 130) },
            };

            fPS = new FPS();
            SGC = game.Content.Load<SpriteFont>("Fonts/SFAll");
        }

        
        
        

        int keyTimer = 0;
        int keyTimer2 = 0;

        public void Update(GameTime gameTime)
        {
            if (Eric.Health <= 30)
            {
                Shader.BlackAndWhiteS2.Parameters["force"].SetValue((float)Randomize.Random.NextDouble());
                Shader.BlackAndWhiteS2.Parameters["random1"].SetValue((float)Randomize.Random.NextDouble());
                Shader.BlackAndWhiteS2.Parameters["random2"].SetValue((float)Randomize.Random.NextDouble());
            }

            fPS.Update(gameTime);

            ManagerGameInscription.Update(gameTime);
     


                if (Eric.Block)
                {
                    mCost.Enabled = true;

                }
                else
                {
                    mCost.Enabled = false;
                }
         
            


            MGObject.Update(gameTime);


          //  else
           // {
           //      mCost.Enabled = false;
           //       Eric.Block = false;
           // }



            
            



            if (Keyboard.GetState().IsKeyDown(Keys.Back) && mCost.Enabled)
            {
                Eric.Block = false;
                mCost.Enabled = false;
            }



            keyTimer++;




            if (mCost.Enabled && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (keyTimer >= 10)
                {
                    mCost.Index--;
                    keyTimer = 0;
                }
            }

            if (mCost.Enabled && Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (keyTimer >= 10)
                {
                    mCost.Index++;
                    keyTimer = 0;
                }
            }


            mCost.Location = new Vector2(Eric.WorldLocation.X - 80, Eric.WorldLocation.Y - 20);
            mCost.Update(gameTime);

            

        }




        private void DrawCell(GameTime gameTime)
        {
            for (sbyte i = 0; i < 11; i++)
            {
                for (sbyte j = 0; j < 21; j++)
                {
                    switch (Map.WorldMatrix[i, j].CellCondition)
                    {
                        case CellsCondition.Free: tCell = this.Texture[0]; break;

                        case CellsCondition.OccupiedByTower: tCell = this.Texture[0]; break;

                        case CellsCondition.OccupiedByObstacle:
                            break;

                        case CellsCondition.DefeatZoneTower: tCell = this.Texture[1]; break;

                        case CellsCondition.DefeatZoneHero: tCell = this.Texture[2]; break;

                        case CellsCondition.Finish:
                            break;
                        default:
                            break;
                    }


                    rCell.X = (int)Map.WorldMatrix[i, j].Location.X;
                    rCell.Y = (int)Map.WorldMatrix[i, j].Location.Y;
                    rCell.Width = Map.WorldMatrix[i, j].Size.Width;
                    rCell.Height = Map.WorldMatrix[i, j].Size.Height;

                    SPHalper.SpriteBatch.Draw(tCell, rCell, Color.White);
                }
            }
        }





        
        
















        public void Draw(GameTime gameTime)
        {
            SPGame.graphics.GraphicsDevice.SetRenderTarget(SPHalper.RenderTarget);

            SPHalper.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, Camera2D.MatrixTransformation(SPGame.graphics.GraphicsDevice));

            SPHalper.SpriteBatch.Draw(this.Background, BackgroundBounds, Color.White);

            // !!!!
            SPHalper.SpriteBatch.End();


            SPHalper.SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, Camera2D.MatrixTransformation(SPGame.graphics.GraphicsDevice));

            if (SPHalper.EditorState) this.DrawCell(gameTime);




            MGObject.Draw(gameTime);

            
            SPHalper.SpriteBatch.End();



            SPHalper.SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Camera2D.MatrixTransformation(SPGame.graphics.GraphicsDevice));
            mCost.Draw(gameTime);

            SPHalper.SpriteBatch.DrawString(SGC, "GarbageCollector  -  " + GC.CollectionCount(0) + " - " + GC.CollectionCount(1), loc["GarbageCollector"], Color.Violet);
            SPHalper.SpriteBatch.DrawString(SGC, "ElapsedGameTime    -  " + gameTime.ElapsedGameTime.TotalSeconds, loc["ElapsedGameTime"], Color.Yellow);
            SPHalper.SpriteBatch.DrawString(SGC, "TotalMemory          -  " + GC.GetTotalMemory(false) / 1024 + " kb", loc["TotalMemory"], Color.Aqua);
            SPHalper.SpriteBatch.DrawString(SGC, "FPS          -  " + fPS.Count, loc["FPS"], Color.Aqua);

            fPS.Draw(gameTime);
            SPHalper.SpriteBatch.End();


              if (Eric.Health > 30) 
            SPHalper.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
             else SPHalper.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, Shader.BlackAndWhiteS2);

           
         
            MGInterface.Draw(gameTime);
            ManagerGameInscription.Draw(gameTime);

            SPGame.graphics.GraphicsDevice.SetRenderTarget(null);

            



            SPHalper.SpriteBatch.Draw(SPHalper.RenderTarget, SPHalper.RenderTarget.Bounds, Color.White);

            SPHalper.SpriteBatch.End();
        }
    }
}