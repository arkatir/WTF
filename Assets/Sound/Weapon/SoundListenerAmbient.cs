using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SoundListenerAmbient : MonoBehaviour
{
    public AudioClip machineGun;
    public AudioClip gravityGun;
    public AudioClip unicorn;
    public AudioClip gunMartin1;
    public AudioClip gunMartin2;
    public AudioClip flareGun;

    public AudioClip hurt;
    public AudioClip reload;

    public AudioClip mushroomDie;
    public AudioClip explosion;

    private UnityAction machineGunListener;
    private UnityAction gravityGunListener;
    private UnityAction unicornListener;
    private UnityAction gunMartin1Listener;
    private UnityAction gunMartin2Listener;
    private UnityAction flareGunListener;
    private UnityAction hurtListener;
    private UnityAction reloadListener;
    private UnityAction mushroomDieListener;
    private UnityAction explosionListener;

    void Awake()
    {
        machineGunListener = new UnityAction(PlayShootGun); 
        gravityGunListener = new UnityAction(PlayShootGun);
        unicornListener = new UnityAction(PlayShootGun);
        gunMartin1Listener = new UnityAction(PlayShootGun);
        gunMartin2Listener = new UnityAction(PlayShootGun);
        flareGunListener = new UnityAction(PlayShootGun);
        hurtListener = new UnityAction(PlayShootGun);
        reloadListener = new UnityAction(PlayShootGun);
        mushroomDieListener = new UnityAction(PlayShootGun);
        explosionListener = new UnityAction(PlayShootGun);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayShootGun()
    {

    }
    private void PlayShootGravityGun()
    {

    }
    private void PlayShootUnicorn()
    {

    }
    private void PlayShootGunMartin1()
    {

    }
    private void PlayShootGunMartin2()
    {

    }
    private void PlayShootFlareGun()
    {

    }
    private void PlayHurt()
    {

    }
    private void PlayReload()
    {

    }

    private void MushroomDie()
    {

    }

    private void Explosion()
    {

    }


}
