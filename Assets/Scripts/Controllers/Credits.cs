using Core;
using Helpers.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Controllers
{
    public sealed class Credits : Scene<Credits>
    {
        [SerializeField] private VideoPlayer CreditsVideo;

        private void Start()
        {
            StartCoroutine(WaitForCredits());
        }

        IEnumerator WaitForCredits()
        {
            yield return new WaitUntil(() => CreditsVideo.time > 0);
            yield return new WaitUntil(() => CreditsVideo.IsFinished());
            LoadScene(1); //Main Menu
        }
    }
}