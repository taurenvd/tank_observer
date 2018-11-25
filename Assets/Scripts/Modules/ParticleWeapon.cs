using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleWeapon : WeaponAbstract
{

    [SerializeField] ParticleSystem m_ps;

	void Awake ()
    {
        m_ps = GetComponent<ParticleSystem>();
	}	
	
	public override void Execute (Transform point)
    {
        m_ps.Play();
	}
}
