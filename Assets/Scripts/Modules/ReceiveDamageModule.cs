using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthModule),typeof(Collider))]
public class ReceiveDamageModule : MonoBehaviour
{

    enum CollisionSource {Trigger, Collision, ParticleCollision }

    [SerializeField, HideInInspector] Collider m_collider;
    [SerializeField, HideInInspector] Animator m_anim;
    [SerializeField, HideInInspector] HealthModule m_hp_module;

    [Header("Animation: ")]
    [SerializeField] string m_death_trigger_name = "death";

    [Header("Collision: ")]
    [SerializeField] List<string> m_trigger_tags;
    [SerializeField] bool destroy_on_trigger;


    public Collider Collider
    {
        get
        {
            return m_collider;
        }
    }

    #region Events

    public event System.Action OnDeath;
         
    #endregion

    public IntValueClass m_death_count;

    void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_hp_module = GetComponent<HealthModule>();
        m_anim = GetComponent<Animator>();

        m_death_count.Value = 0;
    }

    void Start()
    {
        //Debug.Log("Health module found;");
        Collider.enabled = true;
        m_hp_module.m_health.OnValueChanged.AddListener( hp =>
        {
            if (hp <= 0)
            {
                OnDeath?.Invoke();
            }
        });

        OnDeath += () => {
            m_death_count.Value++;

            //Debug.LogFormat(gameObject, "Death. Unit: {0}", m_death_count.Value);
            if (m_anim)
            {
                m_anim.SetTrigger(m_death_trigger_name);
            }

            Collider.enabled = false;
       
        };
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triger: "+other.name);

        OnColliderEvent(other.gameObject,CollisionSource.Trigger);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnColliderEvent(collision.collider.gameObject,CollisionSource.Collision);
    }

    void OnColliderEvent(GameObject other, CollisionSource source)
    {
        //Debug.LogFormat(other,"<color=#0000FF>ColliderEvent:</color> {0} and {1}", other.name, name);

        foreach (var tag in m_trigger_tags)
        {
            if (other.CompareTag(tag))
            {
                if (destroy_on_trigger)
                {
                    Destroy(gameObject);
                }
                else
                {
                    var dd_module = other.GetComponent<DamageDealerModule>();

                    if (dd_module)
                    {
                        m_hp_module.m_health.Value -= dd_module.m_damage * (1 - m_hp_module.Defence);
                    }
                }
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        OnColliderEvent(other,CollisionSource.ParticleCollision);       
    }

    void OnEnable()
    {        
        m_hp_module.m_health.Value = m_hp_module.m_max_health;
        Collider.enabled = true;
    }
}
