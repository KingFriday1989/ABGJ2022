using Editor;
using Helpers.Extensions;
using UnityEngine;

namespace DoubleAgent.Views.Utility
{
    public class RandomParticleCreator : DoubleAgentCore
    {
        [SerializeField] private ParticleSystem[] Particles;

        [InspectorButton("CreateParticles")]
        [SerializeField] bool m_CreateParticles;

        [ContextMenu("Create Particles")]
        public virtual void CreateParticles()
        {
            //if (transform.childCount > 0) return;
            var p = Particles.SelectRandom();
            Instantiate(p, transform);
        }
    }
}