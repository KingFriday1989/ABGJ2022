using Core;
using DoubleAgent.Data;
//using Helpers.Audio;
using UnityEngine;
using UnityEngine.UI;
using Views.Utility;

namespace DoubleAgent.Controllers
{
    public sealed class GameController : Scene<GameController>
    {
        [SerializeField] private CountdownTimer GameTimer;
        [SerializeField] private Image StartInfo;

        protected override void Awake()
        {
            if(StartInfo)
                StartInfo.gameObject.SetActive(true);
            base.Awake();
        }

        private void Start()
        {
            UnityEngine.Cursor.visible = false;
            if(GameData.GlobalMusicChannel != null)
                GameData.GlobalMusicChannel.Stop();
            //SoundManager.StopGlobalMusic();
            GameData.ResetGameData();
            //StartCoroutine(GameStart());
        }

        //IEnumerator GameStart()
        //{
        //    yield return new WaitForSeconds(5);
        //    Destroy(StartInfo);
        //}

        public void StartGame()
        {
            GameData.State = GameStates.GameRunning;
            if(StartInfo != null)
                Destroy(StartInfo.gameObject);
            if (GameTimer != null)
                GameTimer.enabled = true;
        }

        public void FinishGame()
        {
            //ShowLoadingScreen();
            LoadScene("Credits");
        }
    }
}