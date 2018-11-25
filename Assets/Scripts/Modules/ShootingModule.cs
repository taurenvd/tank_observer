using System.Collections.Generic;
using UnityEngine;

public class ShootingModule : WeaponAbstract
{
    [Header("SerializedObjects:")]

    [SerializeField] GameObject projectile_pref;

    [SerializeField, Space] ParticleSystem m_muzzle;

    public override void Execute(Transform point)
    {
        var new_projectile = Instantiate(projectile_pref);

        new_projectile.transform.forward = transform.forward;
        new_projectile.transform.position = point.position;

        m_muzzle.Play();

        //Debug.Log("Fire");
    }
}
