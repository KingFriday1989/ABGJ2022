using Core;
using DoubleAgent.Data;
using Helpers.Audio;
using Helpers.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace DoubleAgent.Controllers
{
    public sealed class Intro : Scene<Intro>
    {
        [SerializeField] private AudioClip MusicStart;
        [SerializeField] private VideoPlayer Player;

        private void Start()
        {
            Constants.Init();
            StartCoroutine(Init());    
        }

        IEnumerator Init()
        {
            yield return new WaitUntil(() => Player.time > 0);
            GameData.GlobalMusicChannel = SoundManager.PlayGlobalMusic(MusicStart).GetComponent<AudioSource>();
            if (GameData.GlobalMusicChannel != null)
                DontDestroyOnLoad(GameData.GlobalMusicChannel.gameObject);
            //yield return new WaitForSeconds(8.25f);
            yield return new WaitUntil(() => Player.IsFinished());
            LoadScene(1); //Main Menu
        }
    }
}