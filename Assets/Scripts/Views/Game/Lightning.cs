using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoubleAgent.Views.Utility;
using DoubleAgent.Controllers.Actors;
using Helpers.Extensions;
using DoubleAgent.Data;
using Helpers.Audio;

namespace DoubleAgent.Views.Game
{
    public class Lightning : RandomParticleCreator
    {
        public ParticleSystem lightningParticle;
        public AudioClip lightningStrike;
        public override void CreateParticles()
        {
            var particle = Instantiate(lightningParticle);
            particle.transform.position = FindObjectOfType<ActorPlayer>().transform.position;
            SoundManager.PlaySoundOnChannel(lightningStrike,2);

            this.gameObject.SetActive(false);
        }



        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareLayer(Constants.LAYER_PROJECTILE)) return;
            CreateParticles();
        }
    }
}

