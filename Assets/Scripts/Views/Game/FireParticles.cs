using DoubleAgent.Data;
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
        [SerializeField, ReadOnly] bool isFalling;
        
        public override void CreateParticles()
        {
            if(!isLit)
            {
                //if (transform.childCount > 0) return;
                var flame = pilotFlame.SelectRandom();
                //transform.ClearChildren();
                ClearChildren();
                Instantiate(flame, transform);
                isLit = true;
            }
            else if(!isOnFire)
            {
                //if (transform.childCount > 0) return;
                ClearChildren();
                //transform.ClearChildren();
                base.CreateParticles();
                isOnFire = true;
                OnFire?.Invoke();
            }
        }

        [ContextMenu("Smoke")]
        public async void Smoke(float waitTime = 0f)
        {
            if (isSmoking) return;
            await Timer.WaitForSeconds(waitTime);
            var fire = transform.GetChild(0);
            Smoke smoke = fire.GetComponentInChildren<Smoke>(true);
            if (smoke == null) return;
           
            smoke.ShowSmoke();
            isSmoking = true;
        }

        //--------------------------
        private void Update()
        {
            if(isFalling)
                transform.eulerAngles = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            //Log(other.gameObject.name);
            if (isOnFire || !other.CompareLayer(Constants.LAYER_PROJECTILE)) return;
            CreateParticles();
        }

        private void OnCollisionEnter(Collision collision)
        {
            //Freeze falling when we hit the ground
            if(isFalling && collision.gameObject.CompareTag(Constants.TAG_GROUND))
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        private void ClearChildren()
        {
            for(var i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        [InspectorButton("EnableGravity")]
        [SerializeField] bool m_EnableGravity;

        [ContextMenu("Enable Gravity")]
        public void EnableGravity()
        {
            if(isFalling) return;
            isFalling = true;
            transform.UnParent();
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<Rigidbody>().useGravity = true;
        }

        public void CreateParticles(int repeat)
        {
            for (int i = 0; i < repeat; i++)
                CreateParticles();
        }
    }
}