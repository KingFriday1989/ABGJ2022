/*
 CORE CLASS
 GAME SCENE
 v1.1
 LAST EDITED: TUESDAY FEBRUARY 22, 2022
 COPYRIGHT © TECH SKULL STUDIOS
*/

using Controllers.Game;

#if WINDOW_MANAGER
using Controllers.Utility;
#endif

namespace Core
{
    /// <summary>
    /// Base class used for all Game Managers which are also Scenes.
    /// </summary>
    /// <typeparam name="G">Type of Game Behaviour</typeparam>
    public abstract class GameScene<G> : GameManagerBase<G>
    where G : GameManagerBase<G>
    {
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