using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMercury;
using ProjectMercury.Renderers;

namespace South_Park
{
    class MainMenu 
    {

        private ManagerContentMenu mCMenu; // Менеджер контента меню
        private ParticleEffect eMoon; // Эффект частиц
        private KeyboardState oldState; // Состояние клавиши
        private ManagerClouds mClouds; // Менеджер облаков
        private SpriteFont fItems; // Шрифт пунктов меню
        private Rectangle[] bounds; // Массив прямоугольников для коллизий
        private Texture2D[] sprites; // Вспомогательные текстуры
        private Renderer pRenderer;
        private Vector2 eMoonLocation, nLocation;
        private Color cItems, cDarkEffect;
        private Point nPointLocation;
        private int curetnItemMainMenu, curentItemOptionsMenu, nowStep;

        private float step;
        private Star[] stars;

      //  int timer;
 
        public MainMenu(Game game)
        {
            
            // Эффект частиц
            pRenderer = new SpriteBatchRenderer
            {
                GraphicsDeviceService = SPGame.graphics,
            };
            // Список пунктов меню
            this.MainMenuItems = new List<Item>();
            this.OptionsMenuItems = new List<Item>();

            mClouds = new ManagerClouds(game);
            sprites = new Texture2D[8];
            cItems = Color.White;
            bounds = new Rectangle[11];
            eMoon = new ParticleEffect();
            nowStep = 5;
            step = 0.3F;
            stars = new Star[200];

            this.Initialize(game);

           
        }

        public List<Item> MainMenuItems { get; set; }

        public List<Item> OptionsMenuItems { get; set; }


        private void Initialize(Game game)
        {
            mCMenu = new ManagerContentMenu(game.Content);
            cDarkEffect = new Color(255f, 255f, 255f, 0f);

            for (int i = 0; i < stars.Length; i++)
                stars[i] = new Star(new Vector2(Randomize.Random.Next(-250, game.GraphicsDevice.Viewport.Bounds.Width + 250), Randomize.Random.Next(0, game.GraphicsDevice.Viewport.Bounds.Height)));

            pRenderer.LoadContent(game.Content);

            eMoon = game.Content.Load<ParticleEffect>(("Effects/MoonEffect"));
            fItems = game.Content.Load<SpriteFont>("Fonts/LogoSmall");

         
            sprites[4] = game.Content.Load<Texture2D>("LEvelInscriptions/DarkFone");
 

            bounds[0] = new Rectangle((int)mCMenu.Locations["Moon"].X + 100, (int)mCMenu.Locations["Moon"].Y + 80, 100, 100);
            bounds[1] = new Rectangle((int)mCMenu.Locations["Moon"].X + 200, (int)mCMenu.Locations["Moon"].Y + 80, 100, 100);
            bounds[2] = new Rectangle((int)mCMenu.Locations["Moon"].X + 100, (int)mCMenu.Locations["Moon"].Y + 230, 100, 100);
            bounds[3] = new Rectangle((int)mCMenu.Locations["Moon"].X + 200, (int)mCMenu.Locations["Moon"].Y + 230, 100, 100);


            eMoonLocation.X = bounds[1].Location.X + 200;
            eMoonLocation.Y = bounds[1].Location.Y + 200;

            eMoon.LoadContent(game.Content);
            eMoon.Initialise();
            GC.Collect();
        }

        // +
        private void MenuItemsLogic()
        {
            int _delta = 0;
            bool _ok = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) this.MainMenuItems[curetnItemMainMenu].OnClick();
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up)) _delta = -1;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down)) _delta = 1;

            curetnItemMainMenu += _delta;
            curentItemOptionsMenu += _delta;

            while (!_ok)
            {

                if (curetnItemMainMenu < 0) curetnItemMainMenu = this.MainMenuItems.Count - 1;
                else if (curetnItemMainMenu > this.MainMenuItems.Count - 1) curetnItemMainMenu = 0;
                else if (this.MainMenuItems[curetnItemMainMenu].Active == false) curetnItemMainMenu += _delta;
                else _ok = true;


                if (curentItemOptionsMenu < 0) curentItemOptionsMenu = this.OptionsMenuItems.Count - 1;
                else if (curentItemOptionsMenu > this.OptionsMenuItems.Count - 1) curentItemOptionsMenu = 0;
                else if (this.OptionsMenuItems[curentItemOptionsMenu].Active == false) curentItemOptionsMenu += _delta;
                else _ok = true;
            }

            oldState = Keyboard.GetState();


            for (int i = 0; i < this.MainMenuItems.Count; i++)
            {
                if (this.MainMenuItems[i] == this.MainMenuItems[curetnItemMainMenu])
                {
                    if (nowStep != 10)
                    {
                        nowStep++;
                        this.MainMenuItems[i].X -= step;
                       
                    }
                    else
                    {
                        nowStep = 0;
                        step = -step;
                    }
                }

            }




            //for (int i = 0; i < this.OptionsMenuItems.Count; i++)
            //{
            //    if (this.OptionsMenuItems[i] == this.OptionsMenuItems[curentItemOptionsMenu])
            //    {
            //        if (nowStep != 10)
            //        {
            //            nowStep++;
            //            this.OptionsMenuItems[i].X -= step;

            //        }
            //        else
            //        {
            //            nowStep = 0;
            //            step = -step;
            //        }
            //    }

            //}









        }


       

        private void MoonCollisionWhithClouds()
        {
            int _count = 0;

           


            for (int i = 0; i < 4; i++)
                for (int j = 0; j < mClouds.Count; j++)
                    if (mClouds[j].CheckCollision(bounds[i]))
                    {
                        _count++;
                        break;
                    }





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

        
        // Метод реализует эффект плавного перемешения пунктов меню за границы экрана.
        private void ItemsEffect(sbyte mode)
        {
            for (sbyte i = 0; i < MainMenuItems.Count; i++) this.MainMenuItems[i].X -= mode * 8;
            for (sbyte i = 0; i < this.OptionsMenuItems.Count; i++) this.OptionsMenuItems[i].X -= mode * 8;
        }

        // Метод реализует эффект перемещения при переходе из одного меню в другое
        private void Moves(sbyte mode)
        {
           



            // Отключаем эффект частиц луны и перемещаем его
            eMoon.Terminate();
            eMoonLocation.X = mCMenu.Locations["Moon"].X + 200;
            // Перемещаем луну
            nLocation = mCMenu.Locations["Moon"];
            nLocation.X -= mode * 6;
            mCMenu.Locations["Moon"] = nLocation;
            // Перемещаем прямоулольники 
            for (int i = 0; i <= bounds.Length - 1; i++) 
            {
                nPointLocation = bounds[i].Location;
                nPointLocation.X -= mode * 6;
                bounds[i].Location = nPointLocation;
            }
            // Перемещаем облака
            for (int i = 0; i < mClouds.Count; i++)
            {
                nLocation = mClouds[i].Location;
                nLocation.X -= mode * 5;
                mClouds[i].Location = nLocation;
            }
            // Перемещаем звезды
            for (int i = 0; i <= stars.Length - 1; i++)
            {
                nPointLocation = stars[i].Location;
                nPointLocation.X -= mode * 5;
                stars[i].Location = nPointLocation;
            }
        }


        public void MovesOptionsMode(sbyte mode)
        {
            if (mCMenu.Locations["Moon"].X >= SPGame.graphics.GraphicsDevice.Viewport.Bounds.Center.X - 320 && mode > 0)
            {
                this.Moves(mode);

                if (mCMenu.Locations["Light"].X >= mCMenu.Locations["Moon"].X - 120)
                {
                    nLocation = mCMenu.Locations["Light"];
                    nLocation.X -= 6;
                    mCMenu.Locations["Light"] = nLocation;

                }

                if (mCMenu.Locations["Coon"].X >= -700)
                {
                    nLocation = mCMenu.Locations["Coon"];
                    nLocation.X -= 20;
                    mCMenu.Locations["Coon"] = nLocation;

                }

                if (mCMenu.Locations["Mysterion"].X >= SPHalper.RenderTarget.Bounds.Width - 590)
                {
                    nLocation = mCMenu.Locations["Mysterion"];
                    nLocation.X -= 17;
                    mCMenu.Locations["Mysterion"] = nLocation;

                }

                this.ItemsEffect(-1); // Эффект перемещения пунктов игрового меню
            }
            else if (mCMenu.Locations["Moon"].X <= SPGame.graphics.GraphicsDevice.Viewport.Bounds.Center.X - 80 && mode < 0)
            {
                this.Moves(mode);

                if (mCMenu.Locations["Light"].X <= mCMenu.Locations["Moon"].X - 135)
                {
                    nLocation = mCMenu.Locations["Light"];
                    nLocation.X += 6;
                    mCMenu.Locations["Light"] = nLocation;

                }

                if (mCMenu.Locations["Coon"].X <= -20)
                {
                    nLocation = mCMenu.Locations["Coon"];
                    nLocation.X += 20;
                    mCMenu.Locations["Coon"] = nLocation;

                }

                if (mCMenu.Locations["Mysterion"].X <= SPHalper.RenderTarget.Bounds.Width)
                {
                    nLocation = mCMenu.Locations["Mysterion"];
                    nLocation.X += 20;
                    mCMenu.Locations["Mysterion"] = nLocation;

                }
                


                this.ItemsEffect(1);

            }
            
        }

      
        public void Update(GameTime gameTime)
        {
            
   
            mClouds.Update(gameTime);

            
            eMoon.Trigger(ref eMoonLocation);
            eMoon.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
           

            this.MoonCollisionWhithClouds();
            this.MenuItemsLogic();
        }


        private void DrawElements(GameTime gameTime)
        {

            SPGame.graphics.GraphicsDevice.SetRenderTarget(SPHalper.RenderTarget);

            SPHalper.SpriteBatch.Draw(mCMenu.Textures["Background"], mCMenu.Locations["Background"], mCMenu.Bounds["Background"], Color.White);

   

            for (int i = 0; i <= stars.Length - 1; i++) stars[i].Draw(gameTime, mCMenu.Textures["Star"]);
 

            SPHalper.SpriteBatch.End();


            SPHalper.SpriteBatch.Begin();
  
            pRenderer.RenderEffect(eMoon);
            SPHalper.SpriteBatch.Draw(mCMenu.Textures["Moon"], mCMenu.Locations["Moon"], mCMenu.Bounds["Moon"], Color.White);
        

            SPHalper.SpriteBatch.Draw(mCMenu.Textures["Light"], mCMenu.Locations["Light"], mCMenu.Bounds["Light"], Color.White);
            mClouds.Draw(gameTime);
            SPHalper.SpriteBatch.Draw(mCMenu.Textures["Coon"], mCMenu.Locations["Coon"], mCMenu.Bounds["Coon"], Color.White);
            SPHalper.SpriteBatch.Draw(mCMenu.Textures["Mysterion"], mCMenu.Locations["Mysterion"], mCMenu.Bounds["Mysterion"], Color.White);
            SPHalper.SpriteBatch.Draw(mCMenu.Textures["SPLogo"], mCMenu.Locations["SPLogo"], mCMenu.Bounds["SPLogo"], Color.White);

            

            //!!!
            SPHalper.SpriteBatch.Draw(sprites[4], new Rectangle(-50, -50, SPHalper.RenderTarget.Bounds.Width + 200, SPHalper.RenderTarget.Bounds.Height + 200), cDarkEffect);


                for (int i = 0; i < this.MainMenuItems.Count; i++)
                {
                    
                    cItems = Color.White;
                    
                    if (this.MainMenuItems[i].Active == false) cItems = Color.Gray;
                    if (this.MainMenuItems[i] == this.MainMenuItems[curetnItemMainMenu]) cItems = Color.Lime;
                   
                    SPHalper.SpriteBatch.DrawString(fItems, this.MainMenuItems[i].Name, this.MainMenuItems[i].Location, cItems);
                }
            

                for (int i = 0; i < this.OptionsMenuItems.Count; i++)
                {
                    cItems = Color.White;
                    if (this.OptionsMenuItems[i].Active == false) cItems = Color.Gray;
                    if (this.OptionsMenuItems[i] == this.OptionsMenuItems[curentItemOptionsMenu]) cItems = Color.Lime;
                   
                    SPHalper.SpriteBatch.DrawString(fItems, this.OptionsMenuItems[i].Name, this.OptionsMenuItems[i].Location, cItems);
                }

                SPHalper.SpriteBatch.Draw(mCMenu.Textures["DarkEffectUp"], mCMenu.Locations["DarkEffectUp"], mCMenu.Bounds["DarkEffect"], Color.White);
                SPHalper.SpriteBatch.Draw(mCMenu.Textures["DarkEffectDown"], mCMenu.Locations["DarkEffectDown"], mCMenu.Bounds["DarkEffect"], Color.White);
            


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

           // SPHalper.SpriteBatch.DrawRectangle(mCMenu.Bounds["Moon"], mCMenu.Locations["Moon"], Color.Lime);
            

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

        // +
        public void Draw(GameTime gameTime)
        {
            if (SPHalper.EditorState) this.DrawOnlineEditingMode(gameTime);
            else this.DrawOfflineEditorMode(gameTime);
        }
    }
}