using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ReceiveDamageModule))]
public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem m_explosion_pref;
    [SerializeField] ReceiveDamageModule m_rec_module;
    
    [SerializeField] float explosion_radius;

    void Start()
    {
        m_rec_module = GetComponent<ReceiveDamageModule>();
        m_rec_module.OnDeath += () => 
            {
                var explosion = Instantiate(m_explosion_pref,transform.position,Quaternion.Euler(-90f,0,0));
                var time = explosion.main.duration + 0.1f;

                Destroy(explosion.gameObject,time);
                Destroy(gameObject);
            };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosion_radius);
    }
}
