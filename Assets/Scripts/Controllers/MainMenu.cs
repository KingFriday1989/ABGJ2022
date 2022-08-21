using Core;
using DoubleAgent.Data;
using Helpers;
using Helpers.Audio;
using Helpers.Extensions;
using UnityEngine;

namespace DoubleAgent.Controllers
{
    public sealed class MainMenu : Scene<MainMenu>
    {
        //[SerializeField] private AudioClip StartMusic;
        [SerializeField] private AudioClip LoopMusic;
        [SerializeField] private Canvas MenuCanvas;
        [SerializeField] private AudioSource Music;

        protected override void Awake()
        {
            base.Awake();
            Music.SetActive(false);
        }

        private async void Start()
        {
            //SoundManager.PlayMusicOnChannel(StartMusic, 0);
            //SoundManager.SetMusicVolumeOnChannel(0.3f, 0);
            //SoundManager.SetVolume(0.3f);

            if(GameData.GlobalMusicChannel != null)
                GameData.GlobalMusicChannel.volume = 0.8f;
            
            //await Timer.WaitForSeconds(StartMusic.length);
            await Timer.WaitWhile(() => this && GameData.GlobalMusicChannel && !GameData.GlobalMusicChannel.IsFinished());
            if (GameData.GlobalMusicChannel != null)
                Destroy(GameData.GlobalMusicChannel.gameObject);

            Music.SetActive(true);

            //!BROKEN
            //SoundManager.LoopMusicOnChannel(LoopMusic, 0);
            //SoundManager.SetMusicVolumeOnChannel(0.8f, 0);
        }

        public async void LoadGame()
        {
            MenuCanvas.enabled = false;
            ShowLoadingScreen();
            await Timer.WaitForSeconds(1.5f);
            await Timer.WaitForFrame();
            await LoadScene(2);
        }
    }
}