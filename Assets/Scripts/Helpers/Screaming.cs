using Data.Utility.Lists;
using Editor;
using Helpers;
using Helpers.Extensions;
using UnityEngine;

namespace DoubleAgent.Helpers
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class Screaming : Core.Behaviour
    {
        [SerializeField] private ListObjectSounds screams;
        [SerializeField, Tag] private string ScreamNear;

        private AudioSource Channel;

        protected override void Awake()
        {
            base.Awake();
            Channel = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Verify(other))
            {
                Channel.loop = true;
                Channel.clip = screams.SelectRandom();
                Channel.Play();
            }
        }

        private async void OnTriggerExit(Collider other)
        {
            if(Verify(other))
            {
                Channel.loop = false;
                await Timer.WaitWhile(() => !Channel.IsFinished());
                Channel.Stop();
            }
        }

        private bool Verify(Collider other)
        {
            if(screams == null) return false;
            return other.CompareTag(ScreamNear);
        }
    }
}