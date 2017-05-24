using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;

using NetworkStateManagement;

using Microsoft.Xna.Framework.GamerServices;

namespace South_Park
{
    public class SPGame : Microsoft.Xna.Framework.Game
    {
        
       // bool flag = false;
        private Dictionary<String, Action<GameTime>> uState, dState;
        

        public static GraphicsDeviceManager graphics;

       
       // private Texture2D dFone;
       // private Rectangle bFone;
        



        ScreenManager screenManager;



        public SPGame()
        {
      



            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = 800,
                PreferredBackBufferWidth = 1600,
                
            };

            


            uState = new Dictionary<String, Action<GameTime>>
            {
                { "Game", this.GameUpdate },
                { "MainMenu", this.MainMenuUpdate },
                { "PauseMenu", this.PauseMenuUpdate },
                { "OptionsMenu", this.OptionsMenuUpdate },
            };




            dState = new Dictionary<String, Action<GameTime>>
            {
                { "Game", this.GameDraw },
                { "MainMenu", this.MainMenuDraw },
                { "PauseMenu", this.PauseMenuDraw },
                { "OptionsMenu", this.OptionsMenuDraw },
            };

            //bFone = new Rectangle(-1, -1, 350, 120);



          


            this.Window.AllowUserResizing = true;

            

            Content.RootDirectory = "Content";

            this.IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = true;
            this.IsFixedTimeStep = false;

        //    graphics.SynchronizeWithVerticalRetrace = true;



            if (ConfigurationManager.AppSettings["FullScreen"] == "true") graphics.IsFullScreen = true;
            else graphics.IsFullScreen = false;

            if (ConfigurationManager.AppSettings["MouseVisible"] == "true") this.IsMouseVisible = true;
            else this.IsMouseVisible = false;

            if (ConfigurationManager.AppSettings["PreferMultiSampling"] == "true") graphics.PreferMultiSampling = true;
            else graphics.PreferMultiSampling = false;

            if (ConfigurationManager.AppSettings["Editing"] == "true") SPHalper.EditorState = true;
            else SPHalper.EditorState = false;


            

         


            

         //    screenManager.AddScreen(new PauseMenuScreen(this), null);


            



          

        }

















        // +
        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = this.GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = this.GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();

            SPHalper.Initialize(this, new RenderTarget2D(this.GraphicsDevice, 1600, 800), new SpriteBatch(GraphicsDevice), this.Content);
            
            SPHalper.GameTime = new GameTime();
     
            SPHalper.GameState = "MainMenu";



            ManagerTower.Initialize();
            
            ManagerGameContent.Initialize();
            

            ManagerShell.Initialize();


            SPDataHalper.SPCityHealth = 100;
            SPDataHalper.SPEnemyCount = 0;
            SPDataHalper.SPEnemyName = " ";
            SPDataHalper.SPMoney = 100;
            SPDataHalper.SPTowersCount = 0;
            

            
            
            

            Randomize.Random = new Random();

            
         //   GLevel = new Level(this);
            
            
    
            
            //MMenu = new MainMenu(this);
            //PMenu = new PauseMenu(this);

            //MMenu.MainMenuItems.Add(new Item("Новая Игра", new Vector2(SPHalper.RenderTarget.Bounds.Right + 100, SPHalper.RenderTarget.Bounds.Bottom - 260), true));
            //MMenu.MainMenuItems.Add(new Item("Продолжить", new Vector2(SPHalper.RenderTarget.Bounds.Right + 100, SPHalper.RenderTarget.Bounds.Bottom - 200), false));
            //MMenu.MainMenuItems.Add(new Item("Настройки", new Vector2(SPHalper.RenderTarget.Bounds.Right + 100, SPHalper.RenderTarget.Bounds.Bottom - 140), true));
            //MMenu.MainMenuItems.Add(new Item("Выход", new Vector2(SPHalper.RenderTarget.Bounds.Right + 100, SPHalper.RenderTarget.Bounds.Bottom - 80), true));


            //MMenu.OptionsMenuItems.Add(new Item("Видео", new Vector2(120, SPHalper.RenderTarget.Bounds.Bottom - 200), true));
            //MMenu.OptionsMenuItems.Add(new Item("Звук", new Vector2(120, SPHalper.RenderTarget.Bounds.Bottom - 140), true));
            //MMenu.OptionsMenuItems.Add(new Item("Главное меню", new Vector2(120, SPHalper.RenderTarget.Bounds.Bottom - 80), true));



            //MMenu.MainMenuItems[0].Click += new EventHandler(newGameClick);
            //MMenu.MainMenuItems[1].Click += new EventHandler(resumeGameClick);
            //MMenu.MainMenuItems[2].Click += new EventHandler(optionsGameClick);
            //MMenu.MainMenuItems[3].Click += new EventHandler(exitGameClick);


            //MMenu.OptionsMenuItems[2].Click += new EventHandler(this.MainMenuClick);     



            
            

         //   GLevel.StartNewGame(this);



            
            Shader.BlackAndWhiteS2 = this.Content.Load<Effect>("Effects/Effect1");
            Shader.Damage = this.Content.Load<Effect>("Effects/DamageWhite");
            SPHalper.Effect = null;

            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
            

            base.Initialize();


            


            
            
        }





        private void MainMenuClick(object sender, EventArgs e)
        {
            SPHalper.GameState = "MainMenu";
        }




        private void newGameClick(object sender, EventArgs e)
        {


            SPHalper.GameState = "Game";
      
        }

        // +
        private void resumeGameClick(object sender, EventArgs e)
        {
            SPHalper.GameState = "Game";
        }

        private void optionsGameClick(object sender, EventArgs e)
        {
            SPHalper.GameState = "OptionsMenu";
           
        }

        // +
        private void exitGameClick(object sender, EventArgs e)
        {
            if (SPHalper.GameState == "OptionsMenu") SPHalper.GameState = "MainMenu";
             //this.Exit();
        }

        


        // +
        private void MainMenuUpdate(GameTime gameTime)
        {
          
            
        
         
        }
        // +
        private void MainMenuDraw(GameTime gameTime)
        {
          
            
        }

        

        // +
        private void PauseMenuUpdate(GameTime gameTime)
        {

        }
        private void PauseMenuDraw(GameTime gameTime)
        {
          //  SPHalper.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
           // PMenu.Draw(gameTime);
           // SPHalper.SpriteBatch.End();
        }



        // +
        private void GameUpdate(GameTime gameTime)
        {
          
            
        }
        // +
        private void GameDraw(GameTime gameTime)
        {
           
        }



        // +
        private void OptionsMenuUpdate(GameTime gameTime)
        {
             //MMenu.Update(gameTime);
            // fPS.Update(gameTime);
           //  MMenu.MovesOptionsMode(1);
        }

        private void OptionsMenuDraw(GameTime gameTime)
        {
            //MMenu.Draw(gameTime);
           // fPS.Draw(gameTime);
        }


        private void DrawOnlineEditingMode(GameTime gameTime)
        {
           // dState[SPHalper.GameState](gameTime);

       

           // SPHalper.SpriteBatch.End();
        }

        private void DrawOfflineEditorMode(GameTime gameTime)
        {
            dState[SPHalper.GameState](gameTime);
        }




       

        // +
        protected override void LoadContent()
        {
           // dFone = this.Content.Load<Texture2D>("LevelInscriptions/DarkFone");
            
            base.LoadContent();
        }

        private bool isNewGame = true;


        // +
        protected override void Update(GameTime gameTime)
        {
            SPHalper.GameTime = gameTime;


            //if (SPHalper.GameState == "Game" && isNewGame)
            //{
            //    GLevel.StartNewGame(this);
            //    isNewGame = false;
            //    screenManager.Visible = false;
            //}
            //else if (SPHalper.GameState == "Game" && !isNewGame)
            //    GLevel.Update(gameTime);
          

            base.Update(gameTime);
        }

        // +
        protected override void Draw(GameTime gameTime)
        {
            if (SPHalper.EditorState) this.DrawOnlineEditingMode(gameTime);
            else this.DrawOfflineEditorMode(gameTime);

            base.Draw(gameTime);
        }
    }
}