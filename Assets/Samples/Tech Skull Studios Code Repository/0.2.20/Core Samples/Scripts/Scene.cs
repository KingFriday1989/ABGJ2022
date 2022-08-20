/*
 CORE CLASS
 SCENE
 v1.6
 LAST EDITED: SATURDAY JULY 23, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using Data;
using Data.Game;
using Controllers.Game;
using Controllers.Utility;
using Controllers.Utility.Input;
using Helpers.Extensions;

namespace Core
{
    /// <summary>
    /// Base class that all primary
    /// scene behaviour should inherit from.
    /// </summary>
    /// <typeparam name="S">Type of scene behaviour</typeparam>
    public abstract class Scene<S> : SceneBase<S> where S : SceneBase<S>
    {
        #region STATIC MANAGER CREATORS

        /// <summary>
        /// Creates the singetlon based manager if it does not exist in the scene.
        /// </summary>
        /// <typeparam name="Manager">Type of singelton based manager to create.</typeparam>
        /// <param name="name">Name of the object create.</param>
        /// <param name="manager">Optional reference to manager prefab.</param>
        public static void CreateManager<Manager>(string name, Manager manager = null)
        where Manager : Singelton<Manager>
        {
            string objectName = "@" + name;
            if (manager == null)
            {
                CreateGameObject<Manager>(objectName);
            }
            else if(manager.gameObject.IsPrefab())
            {
                manager = Instantiate(manager);
                manager.name = objectName;
            }
        }

        /// <summary>
        /// Creates the input manager if it does not exist in the scene.
        /// </summary>
        /// <typeparam name="Input">Type of input manager.</typeparam>
        /// <param name="name">Name of the object create.</param>
        public static void CreateInputManager<Input>(string name = "Input Manager")
        where Input : InputManagerBase<Input>
        {
            if(!InputManagerBase<Input>.Exists())
                CreateManager<Input>(name);
        }

        /// <summary>
        /// Creates the game manager if it does not exist in the scene.
        /// </summary>
        /// <typeparam name="Game"></typeparam>
        /// <param name="name">Name of the object create.</param>
        public static void CreateGameManager<Game>(string name = "Game Manager")
        where Game : GameManagerBase<Game>
        {
            if (!GameManagerBase<Game>.Exists())
                CreateManager<Game>(name);
        }

        /// <summary>
        /// Creates the settings reader if it does not exist in the scene and the debugging flag in the settings is true.
        /// </summary>
        /// <typeparam name="SettingsReader">Type of the settings reader.</typeparam>
        /// <typeparam name="Setttings">Type of settings.</typeparam>
        /// <typeparam name="GameSettings">Type of game settings.</typeparam>
        /// <typeparam name="DisplaySettings">Type of display settings.</typeparam>
        /// <typeparam name="AudioSettings">Type of audio settings.</typeparam>
        /// <typeparam name="KeyboardSettings">Type of keyboard settings.</typeparam>
        /// <param name="name">Name of the object create.</param>
        public static void CreateSettingsReader<SettingsReader, Setttings, GameSettings, DisplaySettings, AudioSettings, KeyboardSettings, Keys>(string name = "Settings Reader")
        where SettingsReader : SettingsReaderBase<SettingsReader, Setttings, GameSettings, DisplaySettings, AudioSettings, KeyboardSettings, Keys>
        where Setttings : SettingsBase<Setttings, GameSettings, DisplaySettings, AudioSettings, KeyboardSettings, Keys>, new()
        where GameSettings : GameSettingsBase<GameSettings>, new()
        where DisplaySettings : DisplaySettingsBase<DisplaySettings>, new ()
        where AudioSettings : AudioSettingsBase<AudioSettings>, new()
        where KeyboardSettings : KeyboardSettingsBase<KeyboardSettings, Keys>, new()
        {
            bool isDebugging = SettingsBase<Setttings, GameSettings, DisplaySettings, AudioSettings, KeyboardSettings, Keys>.Debugging;
            if (isDebugging && !SettingsReaderBase<SettingsReader, Setttings, GameSettings, DisplaySettings, AudioSettings, KeyboardSettings, Keys>.Exists())
                CreateManager<SettingsReader>(name);
        }

        /// <summary>
        /// Creates the game data reader if it does not exist in the scene and the debugging flag in the settings is true.
        /// </summary>
        /// <typeparam name="GameDataReader">Type of game data reader.</typeparam>
        /// <typeparam name="GameData">Type of game data.</typeparam>
        /// <typeparam name="States">Enum state list of game states.</typeparam>
        /// <param name="isDebugging">Are we currently debugging?</param>
        /// <param name="name">Name of the object create.</param>
        public static void CreateGameDataReader<GameDataReader, GameData, States>(bool isDebugging, string name = "Game Data Reader")
        where GameDataReader : GameDataReaderBase<GameDataReader, GameData, States>
        where GameData : GameDataBase<GameData, States>, new()
        where States : System.Enum
        {
            if (isDebugging && !GameDataReaderBase<GameDataReader, GameData, States>.Exists())
                CreateManager<GameDataReader>(name);
        }

        #if WINDOW_MANAGER
        /// <summary>
        /// Creates the window manager if it does not exist in the scene.
        /// </summary>
        /// <typeparam name="Windows">Type of window manager.</typeparam>
        /// <param name="name">Name of the object create.</param>
        public static void CreateWindowManager<Windows>(string name = "Window Manager")
        where Windows : Singelton<Windows>, IWindowManager
        {
            if(!WindowManager.Exists())
                CreateManager<Windows>(name);
        }
        #endif

        #endregion

        #if WINDOW_MANAGER
        protected override bool LoadingScreenActive
        {
            get => WindowManager.Exists() && WindowManager.Instance().LoadingScreenActive;
            set
            {
                if (value && WindowManager.Exists())
                    WindowManager.Instance().ShowLoadingScreen();
                else if(WindowManager.Exists())
                    WindowManager.Instance().HideLoadingScreen();

                loading = value;
            }
        }
        #endif
    }
}