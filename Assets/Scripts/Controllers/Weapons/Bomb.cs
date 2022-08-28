using DoubleAgent.Data;
using UnityEngine;
using UnityEngine.Events;
using RayFire;
using System.Collections.Generic;
using Helpers;

namespace Controllers.Game
{
    public sealed class Bomb : Core.Behaviour
    {
        [SerializeField] private MeshRenderer rend;
        [SerializeField] private UnityEvent _OnExplode;

        private void OnCollisionEnter(Collision collision)
        {
            //if(collision.gameObject.CompareTag(Constants.TAG_GROUND))
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                rend.enabled = false;
                _OnExplode?.Invoke();
            }
        }
        public async void CheckForDestructibles()
        {
            var rayfirebombscript = GetComponent<RayfireBomb>();
            var radius = rayfirebombscript.range;
            var hitobjects = Physics.SphereCastAll(transform.position,radius,transform.forward,radius+1);
            var rigidbodies = new List<RayfireRigid>();
            foreach (var hitobject in hitobjects)
            {
                if (hitobject.collider.TryGetComponent(out RayfireRigid rigid))
                {
                    rigidbodies.Add(rigid);
                }
            }
            await Timer.WaitForFrame();
            for (int i = rigidbodies.Count-1; i >= 0; i--)
            {
                if (rigidbodies[i].demolitionType!=DemolitionType.None)
                { rigidbodies[i].Demolish(); }
            }
        }
    }
}