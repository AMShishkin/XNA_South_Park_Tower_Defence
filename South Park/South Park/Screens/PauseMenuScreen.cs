using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;

namespace NetworkStateManagement
{
    class PauseMenuScreen : MenuScreen
    {
      

        public PauseMenuScreen()
            : base("Resources.Paused")
        {
          
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resources.ResumeGame");
            resumeGameMenuEntry.Selected += OnCancel;
            MenuEntries.Add(resumeGameMenuEntry);

          
                MenuEntry quitGameMenuEntry = new MenuEntry("Resources.QuitGame");
                quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
                MenuEntries.Add(quitGameMenuEntry);
         
        }

        // При нажати выход - подтверждение
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MessageBoxScreen confirmQuitMessageBox =
                                    new MessageBoxScreen("Resources.ConfirmQuitGame");

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }
       // подтверждение выхода 
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
        }
    }
}
