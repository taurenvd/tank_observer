using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI NavMesh component to follow the target 
/// </summary>
[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class FollowModule : MonoBehaviour
{
    [SerializeField, HideInInspector] NavMeshAgent m_agent;
    [SerializeField, HideInInspector] Animator m_anim;

    public Chars chars;

    [Space]

    [SerializeField] Transform m_target;
    [SerializeField] Vector3 player_pos;

    [Space]
    [SerializeField] string m_follow_tag;
    [SerializeField] string m_walk_anim_name = "walk";

    public BoolValueClass can_move;

    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_anim = GetComponent<Animator>();
        can_move.Value = true;
    }

    void Start()
    {
        var hp_module = GetComponent<HealthModule>();

        if (hp_module)
        {
            hp_module.m_health.OnValueChanged.AddListener(hp =>
              {
                  if (hp <= 0)
                  {
                      can_move.Value = false;
                  }
                  else if (hp == hp_module.m_max_health)
                  {
                      can_move.Value = true;
                  }
              });
        }

        m_agent.speed = chars.speed;

        m_target = GameObject.FindWithTag(m_follow_tag).transform;

        can_move.OnValueChanged.AddListener(can_move => 
        {
            m_anim.SetBool(m_walk_anim_name, can_move);
            m_agent.isStopped = !can_move;
        });
        
    }

    void Update()
    {
        if (m_target&&can_move)
        {

            if (!player_pos.Equals(m_target.position))
            {
                player_pos = m_target.position;
                m_agent.SetDestination(player_pos);
            }

            if (m_agent.isOnNavMesh && !m_agent.hasPath)
            {
                player_pos = m_target.position;
                m_agent.SetDestination(player_pos);
                m_anim.SetBool(m_walk_anim_name, true);
                
            }

        }
    }
}
