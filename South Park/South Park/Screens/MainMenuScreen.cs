
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;

namespace NetworkStateManagement
{

    class MainMenuScreen : MenuScreen
    {

        public MainMenuScreen()
            : base("Test Menu")
        {
            // Create our menu entries.
            MenuEntry singlePlayerMenuEntry = new MenuEntry("Start NeW GAME");
            MenuEntry systemLinkMenuEntry = new MenuEntry("TEST options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            singlePlayerMenuEntry.Selected += SinglePlayerMenuEntrySelected;
            systemLinkMenuEntry.Selected += SystemLinkMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(singlePlayerMenuEntry);
            MenuEntries.Add(systemLinkMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        // жмакаем 
        void SinglePlayerMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }

        void SystemLinkMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen("Loading Test");

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}