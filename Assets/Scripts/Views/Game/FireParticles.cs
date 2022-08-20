using DoubleAgent.Views.Utility;
using Editor;
using Helpers;
using Helpers.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace DoubleAgent.Views.Game
{
    public class FireParticles : RandomParticleCreator
    {
        [SerializeField] private ParticleSystem[] pilotFlame;
        [SerializeField] private UnityEvent OnFire;

        [SerializeField, ReadOnly] bool isLit;
        [SerializeField, ReadOnly] bool isOnFire;
        [SerializeField, ReadOnly] bool isSmoking;

        public override void CreateParticles()
        {
            if(!isLit)
            {
                //if (transform.childCount > 0) return;
                var flame = pilotFlame.SelectRandom();
                transform.DestroyChildren();
                Instantiate(flame, transform);
                isLit = true;
            }
            else if(!isOnFire)
            {
                //if (transform.childCount > 0) return;
                transform.DestroyChildren();
                base.CreateParticles();
                isOnFire = true;
                OnFire?.Invoke();
            }
        }

        [ContextMenu("Smoke")]
        public async void Smoke(float waitTime = 0f)
        {
            if (isSmoking) return;
            var fire = transform.GetChild(0);
            var smoke = fire.GetComponentInChildren<Smoke>(true);
            if (smoke == null) return;
           
            await Timer.WaitForSeconds(waitTime);
            smoke.ShowSmoke();
            isSmoking = true;
        }

        //--------------------------
        private void OnTriggerEnter(Collision collision)
        {
            Log(collision.gameObject.name);
        }
    }
}