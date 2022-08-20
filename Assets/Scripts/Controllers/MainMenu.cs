using Core;
using Helpers;
using Helpers.Audio;
using UnityEngine;

namespace DoubleAgent.Controllers
{
    public sealed class MainMenu : Scene<MainMenu>
    {
        [SerializeField] private AudioClip StartMusic;
        [SerializeField] private AudioClip LoopMusic;

        private async void Start()
        {
            SoundManager.PlayMusicOnChannel(StartMusic, 0);
            SoundManager.SetMusicVolumeOnChannel(0.3f, 0);
            await Timer.WaitForSeconds(StartMusic.length);
            SoundManager.LoopMusicOnChannel(LoopMusic, 0);
        }
    }
}