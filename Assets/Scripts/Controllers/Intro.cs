using Core;
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
            SoundManager.PlayGlobalMusic(MusicStart);
            StartCoroutine(Init());    
        }

        IEnumerator Init()
        {
            yield return new WaitForSeconds(9);
            LoadScene(1); //Main Menu
        }
    }
}