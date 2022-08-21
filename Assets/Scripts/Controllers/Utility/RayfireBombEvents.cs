using RayFire;
using UnityEngine;
using UnityEngine.Events;

namespace DoubleAgent.Controllers.Utility
{
    [RequireComponent(typeof(RayfireBomb))]
    public class RayfireBombEvents : DoubleAgentCore
    {
        [SerializeField] private UnityEvent _OnExplode;

        private RayfireBomb bomb;

        protected override void Awake()
        {
            base.Awake();

            bomb = GetComponent<RayfireBomb>();
            bomb.explosionEvent.LocalEvent += Explode;
        }

        private void Explode(RayfireBomb bomb)
        {
            _OnExplode?.Invoke();
            OnExplode(bomb);
        }

        protected virtual void OnExplode(RayfireBomb bomb) { }
    }
}