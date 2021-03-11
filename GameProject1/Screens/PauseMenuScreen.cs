using GameProject1.StateManagement;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameProject1.Screens
{
    // The pause menu comes up over the top of the game,
    // giving the player options to resume or quit.
    public class PauseMenuScreen : MenuScreen
    {
        private readonly MenuEntry _masterVolumeMenuEntry;
        private readonly MenuEntry _musicVolumeMenuEntry;

        private static int _currentMasterVolume = 10;
        private static int _currentMusicVolume = 10;
        private static readonly float[] VolumeChoices = { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };
        public PauseMenuScreen() : base("Paused")
        {
            _masterVolumeMenuEntry = new MenuEntry(string.Empty);
            _musicVolumeMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            var resumeGameMenuEntry = new MenuEntry("Resume Game");
            var restartGameMenuEntry = new MenuEntry("Restart Game");
            var quitGameMenuEntry = new MenuEntry("Quit Game");

            resumeGameMenuEntry.Selected += OnCancel;
            restartGameMenuEntry.Selected += RestartGameMenuEntrySelected;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
            _masterVolumeMenuEntry.Selected += MasterVolumeMenuEntrySelected;
            _musicVolumeMenuEntry.Selected += MusicVolumeMenuEntrySelected;

            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(restartGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
            MenuEntries.Add(_masterVolumeMenuEntry);
            MenuEntries.Add(_musicVolumeMenuEntry);
        }
        private void RestartGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to restart this game?";
            var confirmRestartMessageBox = new MessageBoxScreen(message);

            confirmRestartMessageBox.Accepted += ConfirmRestartMessageBoxAccepted;

            ScreenManager.AddScreen(confirmRestartMessageBox, ControllingPlayer);
        }

        // This uses the loading screen to transition from the game back to the main menu screen.
        private void ConfirmRestartMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, 0, new Hole1());
        }

        private void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";
            var confirmQuitMessageBox = new MessageBoxScreen(message);

            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }

        // This uses the loading screen to transition from the game back to the main menu screen.
        private void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, null, new BackgroundScreen(), new MainMenuScreen());
        }

        // Fills in the latest values for the options screen menu text.
        private void SetMenuEntryText()
        {
            _masterVolumeMenuEntry.Text = $"Master Volume (SFX): {VolumeChoices[_currentMasterVolume]}";
            _musicVolumeMenuEntry.Text = $"Background Music Volume: {VolumeChoices[_currentMusicVolume]}";
        }

        private void MasterVolumeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _currentMasterVolume = (_currentMasterVolume + 1) % VolumeChoices.Length;
            SoundEffect.MasterVolume = VolumeChoices[_currentMasterVolume];

            SetMenuEntryText();
        }

        private void MusicVolumeMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _currentMusicVolume = (_currentMusicVolume + 1) % VolumeChoices.Length;
            MediaPlayer.Volume = VolumeChoices[_currentMusicVolume];

            SetMenuEntryText();
        }
    }
}
