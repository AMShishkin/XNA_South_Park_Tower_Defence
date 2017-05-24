using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using South_Park;

namespace NetworkStateManagement
{
    class GameplayScreen : GameScreen
    {

        ContentManager content;
        SpriteFont gameFont;

        Vector2 playerPosition = new Vector2(100, 100);
        Vector2 enemyPosition = new Vector2(100, 100);

        Random random = new Random();




        float pauseAlpha;

        Level GLevel;


        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            GLevel = new Level(SPHalper.Game);
            GLevel.StartNewGame(SPHalper.Game);
            
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>("gamefont");

            // время отображения загрузки
            Thread.Sleep(2000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }
      
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            GLevel.Update(gameTime);

            // затухание и обратное
            if (coveredByOtherScreen) pauseAlpha = Math.Min(pauseAlpha + 1f / 16, 1);
            else pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

      
        }
        // управление
        public override void HandleInput(InputState input)
        {
            if (ControllingPlayer.HasValue)
            {
                // управление
                HandlePlayerInput(input, ControllingPlayer.Value);
            }
        }
        // ~управление
        bool HandlePlayerInput(InputState input, PlayerIndex playerIndex)
        {
        //    // Look up inputs for the specified player profile.
            KeyboardState keyboardState = input.CurrentKeyboardStates[(int)playerIndex];

            // создание паузы
            if (input.IsPauseGame(playerIndex))
           {
                ScreenManager.AddScreen(new PauseMenuScreen(), playerIndex);
                return false;
           }

   

            return true;
        }
        // +
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            GLevel.Draw(gameTime);

            // затухание в ччерный в паузе
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
