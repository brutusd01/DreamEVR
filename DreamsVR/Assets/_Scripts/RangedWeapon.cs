using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public Transform barrelLocation;
    public Animator anim;
    public GameObject muzzleFlash;
    public GameObject projectilePrefab;
    public AudioClip fireSound;
    [SerializeField] private float destroyTimer = .1f;
    [SerializeField] private AudioSource source;
    [SerializeField] private float projectileSpeed = 500f;
    void Start()
    {
        if (anim == null)
        {
            return;
        }

        if (!source)
        {
            source = GetComponent<AudioSource>();
        }
    }
    void Update()
    {
        
    }

    public void TriggerPull()
    {
        if(anim!=null) anim.SetTrigger("Fire");
    }

    void Fire()
    {
        if(fireSound && source)source.PlayOneShot(fireSound);
        if (muzzleFlash)
        {
            GameObject flash;
            flash = Instantiate(muzzleFlash, barrelLocation.position, barrelLocation.rotation);
            
            Destroy(flash, destroyTimer);
            
        }

        if (!projectilePrefab)
            return;
        //TODO: Make this a "Bullet" class
        GameObject projectile;
        projectile = Instantiate(projectilePrefab, barrelLocation.position, barrelLocation.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * projectileSpeed); 
        print("Boom.");
    }
}
