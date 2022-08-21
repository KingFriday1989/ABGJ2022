using Editor;
using Helpers.Extensions;
using UnityEngine;

namespace DoubleAgent.Views.Game
{
    public class BombParticles : Core.Behaviour
    {
        [SerializeField] private ParticleSystem[] explosions;

        [SerializeField, ReadOnly] bool hasExploded;

        [InspectorButton("Explode")]
        [SerializeField] bool m_Explode;

        public void Explode()
        {
            if (hasExploded) return;
            hasExploded = true;
            ClearChildren();
            var explosion = explosions.SelectRandom();
            var explosionInstance = Instantiate(explosion, transform);
            explosionInstance.UnParent();
        }

        private void ClearChildren()
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}