using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helpers.Audio;
using DoubleAgent.Controllers.Actors;

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

    private Transform aimPt;

    void Start()
    {
        grenNum = grenMax;
        lastShot = Time.time;
        aimPt = GameObject.FindGameObjectWithTag("Target").transform;
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
            actor.ActorData.gun.Shoot();
            actor.ActorAnimator.animator.SetLayerWeight(2, 1);
            actor.ActorAnimator.animator.Play("FireWeapon", 2);
            StopCoroutine(LayerDelay(0.5f));
            StartCoroutine(LayerDelay(0.5f));

            lastShot = Time.time + FireRt;
            SoundManager.PlaySoundOnChannel(gunshot, 1);
            Ray ray = new Ray(muzzlePt.position, actor.transform.forward + new Vector3(0,1,0));
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
}
