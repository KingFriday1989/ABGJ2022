using Helpers.Audio;
using Helpers.Extensions;
using UnityEngine;

namespace DoubleAgent.Helpers
{
    public class PlayRandomSound : Core.Behaviour
    {
        [SerializeField] private int Channel;
        [SerializeField] private AudioClip[] Sounds;

        public void Play()
        {
            if (Sounds == null || Sounds.Length == 0) return;
            var sound = Sounds.SelectRandom();
            SoundManager.PlaySoundOnChannel(sound, Channel);
        }
    }
}