

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameProject1.Screens
{
    // The options screen is brought up over the top of the main menu
    // screen, and gives the user a chance to configure the game
    // in various hopefully useful ways.
    public class OptionsMenuScreen : MenuScreen
    {
        private readonly MenuEntry _masterVolumeMenuEntry;
        private readonly MenuEntry _musicVolumeMenuEntry;

        private static int _currentMasterVolume = 10;
        private static int _currentMusicVolume = 10;
        private static readonly float[] VolumeChoices = { 0f, 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.0f };

        public OptionsMenuScreen() : base("Options")
        {
            _masterVolumeMenuEntry = new MenuEntry(string.Empty);
            _musicVolumeMenuEntry = new MenuEntry(string.Empty);

            SetMenuEntryText();

            var back = new MenuEntry("Back");

            _masterVolumeMenuEntry.Selected += MasterVolumeMenuEntrySelected;
            _musicVolumeMenuEntry.Selected += LanguageMenuEntrySelected;
            back.Selected += OnCancel;

            MenuEntries.Add(_masterVolumeMenuEntry);
            MenuEntries.Add(_musicVolumeMenuEntry);
            MenuEntries.Add(back);
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

        private void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            _currentMusicVolume = (_currentMusicVolume + 1) % VolumeChoices.Length;
            MediaPlayer.Volume = VolumeChoices[_currentMusicVolume];

            SetMenuEntryText();
        }
    }
}
