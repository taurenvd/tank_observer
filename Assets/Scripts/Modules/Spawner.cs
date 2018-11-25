using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utils;


public class Spawner : Singleton<Spawner>
{
    [Space]
    [SerializeField] List<Transform> m_spawn_points;

    [Space]
    [SerializeField] List<GameObject> m_enemy_pool = new List<GameObject>();
    [SerializeField] List<GameObject> m_enemy_prefs = new List<GameObject>();

    [Space]
    [SerializeField] Transform m_target;

    [Header("Ints: ")]
    [SerializeField] int max_enemy_count = 10;
    [SerializeField] int cur_enemy_count;

    [Header("Spawn point position + Random{.x,.y]")]
    [SerializeField] Vector2 x_random_offset = new Vector2(-1, 1);
    [SerializeField] Vector2 z_random_offset = new Vector2(-1, 1);

    void Update()
    {
        if (cur_enemy_count < max_enemy_count)
        {
            var new_zombie = GetFromPool();
            
            Vector3 new_pos;

            if (m_spawn_points.Count > 0)
            {
                new_pos = m_spawn_points[Random.Range(0, m_spawn_points.Count)].transform.position;
                new_pos += new Vector3(Random.Range(x_random_offset.x, x_random_offset.y), 0, Random.Range(z_random_offset.x, z_random_offset.y));
            }
            else if (m_target)
            {

                var player_pos = GameObject.FindGameObjectWithTag("Player").transform.position; ///
                new_pos = new Vector3(player_pos.x + Random.Range(-20f, 20f), 0, player_pos.z + Random.Range(-20f, 20f));
            }
            else
            {
                throw new System.NotImplementedException();

            }

            new_zombie.transform.position = new_pos;
                        

            StartCoroutine(Misc.Timer(Random.Range(1,10),()=> new_zombie.gameObject.SetActive(true)));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void MoveToPool(GameObject enemy)
    {
        enemy.gameObject.SetActive(false);

        cur_enemy_count--;
        m_enemy_pool.Add(enemy);
    }

    public GameObject GetFromPool()
    {
        cur_enemy_count++;

        if (m_enemy_pool.Count > 0)
        {
            var peek = m_enemy_pool[0];

            m_enemy_pool.Remove(peek);
            
            return peek;
        }
        else
        {
            var new_obj = Instantiate(m_enemy_prefs[0]);

            if (new_obj.GetComponent<ReceiveDamageModule>())
            {
                new_obj.GetComponent<ReceiveDamageModule>().OnDeath += () => 
                {
                    new_obj.GetComponent<HealthModule>().m_max_health.Value += 20;

                    //Debug.LogFormat(new_obj,"Max hp increased: -> {0}",new_obj.GetComponent<HealthModule>().m_max_health.Value);

                    StartCoroutine(Misc.Timer(2f,()=>MoveToPool(new_obj)));
                };
            }

            return new_obj;
        }

    }
}
