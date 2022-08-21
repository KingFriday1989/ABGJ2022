using Core;
using DoubleAgent.Data;
using Helpers.Audio;
using System.Collections;
using UnityEngine;

namespace DoubleAgent.Controllers
{
    public sealed class Intro : Scene<Intro>
    {
        [SerializeField] private AudioClip MusicStart;

        private void Start()
        {
            Constants.Init();
            SoundManager.PlayGlobalMusic(MusicStart);
            StartCoroutine(Init());    
        }

        IEnumerator Init()
        {
            yield return new WaitForSeconds(8.25f);
            LoadScene(1); //Main Menu
        }
    }
}