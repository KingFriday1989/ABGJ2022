using Core;
using DoubleAgent.Data;
//using Helpers.Audio;

namespace DoubleAgent.Controllers
{
    public sealed class GameController : Scene<GameController>
    {
        private void Start()
        {
            if(GameData.GlobalMusicChannel != null)
                GameData.GlobalMusicChannel.Stop();
            //SoundManager.StopGlobalMusic();
            GameData.ResetGameData();
        }
    }
}