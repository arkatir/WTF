using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class SoundListenerAmbient : MonoBehaviour
{
    public AudioSource audiosrc;

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
        gravityGunListener = new UnityAction(PlayShootGravityGun);
        unicornListener = new UnityAction(PlayShootUnicorn);
        gunMartin1Listener = new UnityAction(PlayShootGunMartin1);
        gunMartin2Listener = new UnityAction(PlayShootGunMartin2);
        flareGunListener = new UnityAction(PlayShootFlareGun);
        hurtListener = new UnityAction(PlayHurt);
        reloadListener = new UnityAction(PlayReload);
        mushroomDieListener = new UnityAction(MushroomDie);
        explosionListener = new UnityAction(Explosion);
    }

    private void OnEnable()
    {
        EventManager.StartListening("machineGun", machineGunListener);
        EventManager.StartListening("gravityGun", gravityGunListener);
        EventManager.StartListening("unicorn", unicornListener);
        EventManager.StartListening("gunMartin1", gunMartin1Listener);
        EventManager.StartListening("gunMartin2", gunMartin2Listener);
        EventManager.StartListening("flareGun", flareGunListener);
        EventManager.StartListening("hurt", hurtListener);
        EventManager.StartListening("reload", reloadListener);
        EventManager.StartListening("mushroomDie", mushroomDieListener);
        EventManager.StartListening("explosion", explosionListener);

    }

    private void OnDisable()
    {
        EventManager.StopListening("machineGun", machineGunListener);
        EventManager.StopListening("gravityGun", gravityGunListener);
        EventManager.StopListening("unicorn", unicornListener);
        EventManager.StopListening("gunMartin1", gunMartin1Listener);
        EventManager.StopListening("gunMartin2", gunMartin2Listener);
        EventManager.StopListening("flareGun", flareGunListener);
        EventManager.StopListening("hurt", hurtListener);
        EventManager.StopListening("reload", reloadListener);
        EventManager.StopListening("mushroomDie", mushroomDieListener);
        EventManager.StopListening("explosion", explosionListener);
    }

    private void PlayShootGun()
    {
        audiosrc.PlayOneShot(machineGun);
    }
    private void PlayShootGravityGun()
    {
        audiosrc.PlayOneShot(gravityGun);
    }
    private void PlayShootUnicorn()
    {
        audiosrc.PlayOneShot(unicorn);
    }
    private void PlayShootGunMartin1()
    {
        audiosrc.PlayOneShot(gunMartin1);
    }
    private void PlayShootGunMartin2()
    {
        audiosrc.PlayOneShot(gunMartin2);
    }
    private void PlayShootFlareGun()
    {
        audiosrc.PlayOneShot(flareGun);
    }
    private void PlayHurt()
    {
        audiosrc.PlayOneShot(hurt);
    }
    private void PlayReload()
    {
        audiosrc.PlayOneShot(reload);
    }

    private void MushroomDie()
    {
        audiosrc.PlayOneShot(mushroomDie);
    }

    private void Explosion()
    {
        audiosrc.PlayOneShot(explosion);
    }


}
