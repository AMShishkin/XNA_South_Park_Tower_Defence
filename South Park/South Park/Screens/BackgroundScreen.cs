using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using South_Park;
using ProjectMercury;
using ProjectMercury.Renderers;
using C3.XNA;


namespace NetworkStateManagement
{

    class BackgroundScreen : GameScreen
    {
        private FPS fPS;
        private SpriteFont SGC;
        private Dictionary<String, Vector2> loc;

        //private ManagerContentMenu mCMenu; // Менеджер контента меню
        private ParticleEffect eMoon; // Эффект частиц
       // private KeyboardState oldState; // Состояние клавиши
        private ManagerClouds mClouds; // Менеджер облаков
        private SpriteFont fItems; // Шрифт пунктов меню
       
      
        private Renderer pRenderer;
        private Vector2 eMoonLocation, nLocation;
        private Color cItems, cDarkEffect;
        private Point nPointLocation;
        private int curetnItemMainMenu, curentItemOptionsMenu, nowStep;

        private float step;
        private Star[] stars;



        ContentManager content;
        private Dictionary<string, Texture2D> sprites;
        private Dictionary<string, Rectangle> bounds;
        private Dictionary<string, Vector2> locations;

        public BackgroundScreen()
        {


            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

                   
            // Эффект частиц
            pRenderer = new SpriteBatchRenderer
            {
                GraphicsDeviceService = SPGame.graphics,
            };
    

            mClouds = new ManagerClouds(SPHalper.Game);
           // sprites = new Texture2D[8];
           // cItems = Color.White;
           // bounds = new Rectangle[11];
            eMoon = new ParticleEffect();
            nowStep = 5;
            step = 0.3F;
            stars = new Star[200];

            loc = new Dictionary<string, Vector2>();

            this.LoadContent(SPHalper.ContentManager);
            this.Initialize();


        }

        

        private void Initialize()
        {

            cDarkEffect = new Color(255f, 255f, 255f, 0f);

       

            for (int i = 0; i < stars.Length; i++)
               stars[i] = new Star(new Vector2(Randomize.Random.Next(-250, SPHalper.Game.GraphicsDevice.Viewport.Bounds.Width + 250), Randomize.Random.Next(0, SPHalper.Game.GraphicsDevice.Viewport.Bounds.Height)));

            pRenderer.LoadContent(SPHalper.Game.Content);

            eMoon = SPHalper.Game.Content.Load<ParticleEffect>(("Effects/MoonEffect"));
           

         
           // sprites[4] = game.Content.Load<Texture2D>("LEvelInscriptions/DarkFone");
 

           // bounds[0] = new Rectangle((int)mCMenu.Locations["Moon"].X + 100, (int)mCMenu.Locations["Moon"].Y + 80, 100, 100);
           // bounds[1] = new Rectangle((int)mCMenu.Locations["Moon"].X + 200, (int)mCMenu.Locations["Moon"].Y + 80, 100, 100);
            //bounds[2] = new Rectangle((int)mCMenu.Locations["Moon"].X + 100, (int)mCMenu.Locations["Moon"].Y + 230, 100, 100);
           // bounds[3] = new Rectangle((int)mCMenu.Locations["Moon"].X + 200, (int)mCMenu.Locations["Moon"].Y + 230, 100, 100);


            eMoonLocation.X = locations["Moon"].X + 200;
            eMoonLocation.Y = locations["Moon"].Y + 200;

            eMoon.LoadContent(SPHalper.Game.Content);
            eMoon.Initialise();
            GC.Collect();
        }



        public void LoadContent(ContentManager content)
        {

            sprites = new Dictionary<string, Texture2D>()
            {
                { "Background", content.Load<Texture2D>("Menu/MainMenu/Background") },
                { "Moon", content.Load<Texture2D>("Menu/MainMenu/Moon") },
                { "Light", content.Load<Texture2D>("Menu/MainMenu/Light") },
                { "Coon", content.Load<Texture2D>("Menu/MainMenu/Coon") },
                { "Mysterion", content.Load<Texture2D>("Menu/MainMenu/Mysterion") },
                { "SPLogo", content.Load<Texture2D>("Menu/MainMenu/Logo") },
                { "Star", content.Load<Texture2D>("Star/Star") },
                { "DarkEffectUp", content.Load<Texture2D>("Menu/MainMenu/DarkEffectUp") },
                { "DarkEffectDown", content.Load<Texture2D>("Menu/MainMenu/DarkEffectDown") },
               // { "DarkFone", contentManager.Load<Texture2D>("LEvelInscriptions/DarkFone") },
            };

            bounds = new Dictionary<string, Rectangle>()
            {
                { "Background", new Rectangle(0, 0, SPHalper.RenderTarget.Bounds.Width, SPHalper.RenderTarget.Bounds.Height) },
                { "Moon", new Rectangle(0, 0, 400, 400) },
                { "Light", new Rectangle(0, 0, 640, 640) },
                { "Coon", new Rectangle(0, 0, 680, 420) },
                { "Mysterion", new Rectangle(0, 0, 700, 400) },
                { "SPLogo", new Rectangle(0, 0, 266, 146) },
                { "DarkEffect", new Rectangle(0, 0, SPHalper.RenderTarget.Width, 32) },


            };

            locations = new Dictionary<string, Vector2>()
            {
                { "Background", Vector2.Zero },
                { "Moon", new Vector2(SPHalper.RenderTarget.Bounds.Center.X - 200, 0) },
                { "Light", new Vector2(SPHalper.RenderTarget.Bounds.Center.X - 320, -120) },
                { "Coon", new Vector2(0, SPHalper.RenderTarget.Bounds.Height - 400) },
                { "Mysterion", new Vector2(SPHalper.RenderTarget.Bounds.Width, SPHalper.RenderTarget.Bounds.Height - 400) },
                { "SPLogo", new Vector2(30, 30) },
                { "DarkEffectUp", new Vector2(0, 0) },
                { "DarkEffectDown", new Vector2(0, SPHalper.RenderTarget.Height - 32) },
            };



            loc = new Dictionary<String, Vector2>
            {
                { "GarbageCollector", new Vector2(15, 10) },
                { "ElapsedGameTime", new Vector2(15, 40) },
                { "TotalMemory", new Vector2(15, 70) },
                { "FPS", new Vector2(15, 100) },
                { "Processor", new Vector2(15, 130) },
            };

            fPS = new FPS();
            SGC = content.Load<SpriteFont>("Fonts/SFAll");
            
        }


        
       

        private void MoonCollisionWhithClouds()
        {
            int _count = 0;

           


            //for (int i = 0; i < 4; i++)
            //    for (int j = 0; j < mClouds.Count; j++)
            //        if (mClouds[j].CheckCollision(bounds[i]))
            //        {
            //            _count++;
            //            break;
                   // }





            switch (_count)
            {
                case 1:
                    if (cDarkEffect.A <= 20) cDarkEffect.A += (byte)1f;
                    else if (cDarkEffect.A >= 20) cDarkEffect.A -= (byte)1f;
                    break;
                case 2:
                    if (cDarkEffect.A <= 40) cDarkEffect.A += (byte)1f;
                    else if (cDarkEffect.A >= 40) cDarkEffect.A -= (byte)1f;
                    break;
                case 3:
                    if (cDarkEffect.A <= 60) cDarkEffect.A += (byte)1f;
                    else if (cDarkEffect.A >= 60) cDarkEffect.A -= (byte)1f;
                    break;
                case 4:
                    if (cDarkEffect.A <= 80) cDarkEffect.A += (byte)1f;
                    else if (cDarkEffect.A >= 80) cDarkEffect.A -= (byte)1f;
                    break;
                default: if (cDarkEffect.A != 0) cDarkEffect.A -= (byte)1f; break;
            }
        }



        
        // Метод реализует эффект перемещения при переходе из одного меню в другое
        private void Moves(sbyte mode)
        {
           



            //// Отключаем эффект частиц луны и перемещаем его
            //eMoon.Terminate();
            //eMoonLocation.X = mCMenu.Locations["Moon"].X + 200;
            //// Перемещаем луну
            //nLocation = mCMenu.Locations["Moon"];
            //nLocation.X -= mode * 6;
            //mCMenu.Locations["Moon"] = nLocation;
            //// Перемещаем прямоулольники 
            //for (int i = 0; i <= bounds.Length - 1; i++) 
            //{
            //    nPointLocation = bounds[i].Location;
            //    nPointLocation.X -= mode * 6;
            //    bounds[i].Location = nPointLocation;
            //}
            //// Перемещаем облака
            //for (int i = 0; i < mClouds.Count; i++)
            //{
            //    nLocation = mClouds[i].Location;
            //    nLocation.X -= mode * 5;
            //    mClouds[i].Location = nLocation;
            //}
            //// Перемещаем звезды
            //for (int i = 0; i <= stars.Length - 1; i++)
            //{
            //    nPointLocation = stars[i].Location;
            //    nPointLocation.X -= mode * 5;
            //    stars[i].Location = nPointLocation;
            //}
        }



        private void DrawElements(GameTime gameTime)
        {

            SPGame.graphics.GraphicsDevice.SetRenderTarget(SPHalper.RenderTarget);

            SPHalper.SpriteBatch.Draw(sprites["Background"], locations["Background"], bounds["Background"], Color.White);

            for (int i = 0; i <= stars.Length - 1; i++) stars[i].Draw(gameTime, sprites["Star"]);
 

            SPHalper.SpriteBatch.End();


           SPHalper.SpriteBatch.Begin();
  
            pRenderer.RenderEffect(eMoon);
            SPHalper.SpriteBatch.Draw(sprites["Moon"], locations["Moon"], bounds["Moon"], Color.White);
        

            SPHalper.SpriteBatch.Draw(sprites["Light"], locations["Light"], bounds["Light"], Color.White);
            mClouds.Draw(gameTime);
            SPHalper.SpriteBatch.Draw(sprites["Coon"], locations["Coon"], bounds["Coon"], Color.White);
            SPHalper.SpriteBatch.Draw(sprites["Mysterion"], locations["Mysterion"], bounds["Mysterion"], Color.White);
            SPHalper.SpriteBatch.Draw(sprites["SPLogo"], locations["SPLogo"], bounds["SPLogo"], Color.White);

            

            ////!!!
            //SPHalper.SpriteBatch.Draw(sprites[4], new Rectangle(-50, -50, SPHalper.RenderTarget.Bounds.Width + 200, SPHalper.RenderTarget.Bounds.Height + 200), cDarkEffect);


            //    for (int i = 0; i < this.MainMenuItems.Count; i++)
            //    {
                    
            //        cItems = Color.White;
                    
            //        if (this.MainMenuItems[i].Active == false) cItems = Color.Gray;
            //        if (this.MainMenuItems[i] == this.MainMenuItems[curetnItemMainMenu]) cItems = Color.Lime;
                   
            //        SPHalper.SpriteBatch.DrawString(fItems, this.MainMenuItems[i].Name, this.MainMenuItems[i].Location, cItems);
            //    }
            

            //    for (int i = 0; i < this.OptionsMenuItems.Count; i++)
            //    {
            //        cItems = Color.White;
            //        if (this.OptionsMenuItems[i].Active == false) cItems = Color.Gray;
            //        if (this.OptionsMenuItems[i] == this.OptionsMenuItems[curentItemOptionsMenu]) cItems = Color.Lime;
                   
            //        SPHalper.SpriteBatch.DrawString(fItems, this.OptionsMenuItems[i].Name, this.OptionsMenuItems[i].Location, cItems);
            //    }

            //    SPHalper.SpriteBatch.Draw(mCMenu.Textures["DarkEffectUp"], mCMenu.Locations["DarkEffectUp"], mCMenu.Bounds["DarkEffect"], Color.White);
            //    SPHalper.SpriteBatch.Draw(mCMenu.Textures["DarkEffectDown"], mCMenu.Locations["DarkEffectDown"], mCMenu.Bounds["DarkEffect"], Color.White);
            


        }





        private void DrawRenderTarget()
        {
            SPHalper.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            SPGame.graphics.GraphicsDevice.SetRenderTarget(null);
            SPHalper.SpriteBatch.Draw(SPHalper.RenderTarget, SPHalper.RenderTarget.Bounds, Color.White);
            SPHalper.SpriteBatch.End();
        }

        private void DrawOnlineEditingMode(GameTime gameTime)
        {

           // Shader.BlackAndWhiteS2.Parameters["desaturation_float"].SetValue((float)Randomize.Random.NextDouble());

            SPHalper.SpriteBatch.Begin();
            this.DrawElements(gameTime);
           // for (int i = 0; i <= bounds.Length - 1; i++) SPHalper.SpriteBatch.DrawRectangle(bounds[i], new Vector2(bounds[i].Location.X, bounds[i].Location.Y), Color.Lime);

            //SPHalper.SpriteBatch.Draw(dFone, bFone, Color.Black);
            SPHalper.SpriteBatch.DrawString(SGC, "GarbageCollector  -  " + GC.CollectionCount(0) + " - " + GC.CollectionCount(1), loc["GarbageCollector"], Color.Violet);
            SPHalper.SpriteBatch.DrawString(SGC, "ElapsedGameTime    -  " + gameTime.ElapsedGameTime.TotalSeconds, loc["ElapsedGameTime"], Color.Yellow);
            SPHalper.SpriteBatch.DrawString(SGC, "TotalMemory          -  " + GC.GetTotalMemory(false) / 1024 + " kb", loc["TotalMemory"], Color.Aqua);
            SPHalper.SpriteBatch.DrawString(SGC, "FPS          -  " + fPS.Count, loc["FPS"], Color.Aqua);


         //   SPHalper.SpriteBatch.DrawRectangle(locations["Moon"], locations["Moon"], Color.Lime);
            

            //CollisionBounds.DrawRectangle(mCMenu.Bounds["Light"], mCMenu.Locations["Light"], Color.Lime);
            //CollisionBounds.DrawRectangle(mCMenu.Bounds["Coon"], mCMenu.Locations["Coon"], Color.Lime);
            //CollisionBounds.DrawRectangle(mCMenu.Bounds["Mysterion"], mCMenu.Locations["Mysterion"], Color.Lime);
            SPHalper.SpriteBatch.End();
            this.DrawRenderTarget();





        }

        private void DrawOfflineEditorMode(GameTime gameTime)
        {
            SPHalper.SpriteBatch.Begin();
            this.DrawElements(gameTime);
            SPHalper.SpriteBatch.End();
            this.DrawRenderTarget();
        }





        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            fPS.Update(gameTime);

            mClouds.Update(gameTime);

            
            eMoon.Trigger(ref eMoonLocation);
            eMoon.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
           

            this.MoonCollisionWhithClouds();


            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
           // SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
         //   Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
          //  Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);


            if (SPHalper.EditorState) this.DrawOnlineEditingMode(gameTime);
            else this.DrawOfflineEditorMode(gameTime);

            fPS.Draw(gameTime);
            //spriteBatch.Begin();

           // spriteBatch.Draw(backgroundTexture, fullscreen,
           //                  new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));

           // spriteBatch.End();
        }






         












        //public void MovesOptionsMode(sbyte mode)
        //{
        //    if (mCMenu.Locations["Moon"].X >= SPGame.graphics.GraphicsDevice.Viewport.Bounds.Center.X - 320 && mode > 0)
        //    {
        //        this.Moves(mode);

        //        if (mCMenu.Locations["Light"].X >= mCMenu.Locations["Moon"].X - 120)
        //        {
        //            nLocation = mCMenu.Locations["Light"];
        //            nLocation.X -= 6;
        //            mCMenu.Locations["Light"] = nLocation;

        //        }

        //        if (mCMenu.Locations["Coon"].X >= -700)
        //        {
        //            nLocation = mCMenu.Locations["Coon"];
        //            nLocation.X -= 20;
        //            mCMenu.Locations["Coon"] = nLocation;

        //        }

        //        if (mCMenu.Locations["Mysterion"].X >= SPHalper.RenderTarget.Bounds.Width - 590)
        //        {
        //            nLocation = mCMenu.Locations["Mysterion"];
        //            nLocation.X -= 17;
        //            mCMenu.Locations["Mysterion"] = nLocation;

        //        }

        //        this.ItemsEffect(-1); // Эффект перемещения пунктов игрового меню
        //    }
        //    else if (mCMenu.Locations["Moon"].X <= SPGame.graphics.GraphicsDevice.Viewport.Bounds.Center.X - 80 && mode < 0)
        //    {
        //        this.Moves(mode);

        //        if (mCMenu.Locations["Light"].X <= mCMenu.Locations["Moon"].X - 135)
        //        {
        //            nLocation = mCMenu.Locations["Light"];
        //            nLocation.X += 6;
        //            mCMenu.Locations["Light"] = nLocation;

        //        }

        //        if (mCMenu.Locations["Coon"].X <= -20)
        //        {
        //            nLocation = mCMenu.Locations["Coon"];
        //            nLocation.X += 20;
        //            mCMenu.Locations["Coon"] = nLocation;

        //        }

        //        if (mCMenu.Locations["Mysterion"].X <= SPHalper.RenderTarget.Bounds.Width)
        //        {
        //            nLocation = mCMenu.Locations["Mysterion"];
        //            nLocation.X += 20;
        //            mCMenu.Locations["Mysterion"] = nLocation;

        //        }
                


        //        this.ItemsEffect(1);

        //    }
            
        //}

    }
}
