using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;


namespace NetworkStateManagement
{

    class LoadingScreen : GameScreen
    {
        bool loadingIsSlow;
        bool otherScreensAreGone;

        GameScreen[] screensToLoad;

        GameTime loadStartTime;
        TimeSpan loadAnimationTimer;

        private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow, GameScreen[] screensToLoad)
        {
            this.loadingIsSlow = loadingIsSlow;
            this.screensToLoad = screensToLoad;

         //   TransitionOnTime = TimeSpan.FromSeconds(0.5);

      
        }

        public static void Load(ScreenManager screenManager, bool loadingIsSlow, PlayerIndex? controllingPlayer, params GameScreen[] screensToLoad)
        {
            // Tell all the current screens to transition off.
            foreach (GameScreen screen in screenManager.GetScreens())
                screen.ExitScreen();

            // Create and activate the loading screen.
            LoadingScreen loadingScreen = new LoadingScreen(screenManager,
                                                            loadingIsSlow,
                                                            screensToLoad);

            screenManager.AddScreen(loadingScreen, controllingPlayer);
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // If all the previous screens have finished transitioning
            // off, it is time to actually perform the load.
            if (otherScreensAreGone)
            {
                // Start up the background thread, which will update the network
                // session and draw the animation while we are loading.
                // if (backgroundThread != null)
                // {
                loadStartTime = gameTime;
                // backgroundThread.Start();
                // }

                // Perform the load operation.
                ScreenManager.RemoveScreen(this);

                foreach (GameScreen screen in screensToLoad)
                {
                    if (screen != null)
                    {
                        ScreenManager.AddScreen(screen, ControllingPlayer);
                    }
                }

             
                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            
            if ((ScreenState == ScreenState.Active) &&
                (ScreenManager.GetScreens().Length == 1))
            {
                otherScreensAreGone = true;
            }

          
            if (loadingIsSlow)
            {
                SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
                SpriteFont font = ScreenManager.Font;

                string message = "Loading";

                // Center the text in the viewport.
                Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
                Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
                Vector2 textSize = font.MeasureString(message);
                Vector2 textPosition = (viewportSize - textSize) / 2;

                Color color = Color.White * TransitionAlpha;

                // Animate the number of dots after our "Loading..." message.
                loadAnimationTimer += gameTime.ElapsedGameTime;

                int dotCount = (int)(loadAnimationTimer.TotalSeconds * 5) % 10;

                message += new string('.', dotCount);

                // Draw the text.
                spriteBatch.Begin();
                spriteBatch.DrawString(font, message, textPosition, color);
                spriteBatch.End();
            }
        }
    }
}
