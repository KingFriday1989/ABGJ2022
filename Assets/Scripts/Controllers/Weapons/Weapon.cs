using DoubleAgent.Controllers.Actors;
using DoubleAgent.Data;
using Editor;
using Helpers.Audio;
using Helpers.Extensions;
using System.Collections;
using UnityEngine;

namespace DoubleAgent.Controllers
{
    public class Weapon : MonoBehaviour
    {
        public float FireRt;
        public float lastShot;
        public float grenCool;
        public AudioClip gunshot, drawWeapon;
        public ParticleSystem hitBld, hitDust;
        public GameObject grenade;
        public int grenNum, grenMax;

        public Transform muzzlePt;
        public Actor actor;
        public Rigidbody bullet;

        private Transform aimPt;

        void Start()
        {
            grenNum = grenMax;
            lastShot = Time.time;
            aimPt = GameObject.FindGameObjectWithTag(Constants.TAG_TARGET).transform;
        }

        void Update()
        {
            //aimPt.position = actor.transform.position + actor.transform.forward * 50f + 1.5f * Vector3.up;
            aimPt.localPosition = new Vector3(0, 0, 50);
        }

        [ContextMenu("Shoot Gun")]
        public void Fire()
        {
            if (lastShot < Time.time)
            {
                FireBullet();

                //---------------------
                actor.ActorData.gun.damage = 1;
                actor.ActorData.gun.strength = 0;
                //---------------------
                actor.ActorData.gun.Shoot();
                actor.ActorAnimator.animator.SetLayerWeight(2, 1);
                actor.ActorAnimator.animator.SetTrigger("Fire");
                StopCoroutine(LayerDelay(0.5f));
                StartCoroutine(LayerDelay(0.5f));

                lastShot = Time.time + FireRt;
                SoundManager.PlaySoundOnChannel(gunshot, 1);
                Ray ray = new Ray(muzzlePt.position, actor.transform.forward + new Vector3(0, 1, 0));
                Physics.Raycast(ray, out RaycastHit hitinfo);
                if (hitDust != null && hitBld != null)
                {

                    if (hitinfo.transform.GetComponent<Actor>())
                    {
                        //Damage Actor and spawn blood particle effect
                        var prtcleBld = Instantiate(hitBld);
                        prtcleBld.transform.position = hitinfo.point;
                    }
                    else
                    {
                        //Create Dust puff and Damage Object hit
                        var prtcleDst = Instantiate(hitDust);
                        prtcleDst.transform.position = hitinfo.point;
                    }
                }
            }
        }

        public void TossGrenade()
        {
            actor.ActorAnimator.animator.SetLayerWeight(2, 1);
            actor.ActorAnimator.animator.Play("ThrowGrenade", 2);
            grenNum--;
            StopCoroutine(LayerDelay(1.25f));
            StartCoroutine(LayerDelay(1.25f));
        }

        IEnumerator LayerDelay(float time)
        {
            yield return new WaitForSeconds(time);
            actor.ActorAnimator.animator.SetLayerWeight(2, 0);
        }

        [InspectorButton("FireBullet")]
        [SerializeField] bool m_FireBullet;

        [ContextMenu("Fire Bullet")]
        public void FireBullet()
        {
            if (bullet == null) return;
            var bulletInstance = Instantiate(bullet, transform);
            bulletInstance.UnParent();
            bulletInstance.AddForce(1000 * Vector3.forward);
        }
    }
}