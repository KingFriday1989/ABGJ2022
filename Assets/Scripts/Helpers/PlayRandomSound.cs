using Data.Utility.Lists;
using Helpers.Audio;
using UnityEngine;

namespace DoubleAgent.Helpers
{
    public class PlayRandomSound : Core.Behaviour
    {
        [SerializeField] private int Channel;
        //[SerializeField] private AudioClip[] Sounds;
        [SerializeField] private ListObjectSounds Sounds;

        public void Play()
        {
            if (Sounds == null || Sounds.Count == 0) return;
            var sound = Sounds.SelectRandom();
            SoundManager.PlaySoundOnChannel(sound, Channel);
        }
    }
}