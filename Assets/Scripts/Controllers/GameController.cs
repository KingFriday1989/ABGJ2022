using Core;
using DoubleAgent.Data;

namespace DoubleAgent.Controllers
{
    public sealed class GameController : Scene<GameController>
    {
        private void Start()
        {
            GameData.ResetGameData();
        }
    }
}