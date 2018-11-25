using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackModule : MonoBehaviour
{
    [SerializeField, Space] Settings m_input_control;

    [SerializeField, Space] List<WeaponAbstract> weapons=new List<WeaponAbstract>();

    [SerializeField, Space] Transform weapon_point;

    [Header("Scriptable Vars:")]
    [SerializeField] IntValue m_cur_weapon_index;
    [SerializeField] StringValue m_cur_weapon_name;



    void Start()
    {
        System.Action<int> weapon_id_change_delegate = id =>
        {
            if (id > weapons.Count - 1) m_cur_weapon_index.Value = 0;
            if (id < 0) m_cur_weapon_index.Value = weapons.Count - 1;


            if (m_cur_weapon_index != null&& weapons[m_cur_weapon_index]!=null)
            {
                m_cur_weapon_name.Value = weapons[m_cur_weapon_index].name;
            }
            
        };

        m_cur_weapon_index.OnValueChanged += weapon_id_change_delegate;
        m_cur_weapon_index.Value = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(m_input_control.next_weapon_key)) m_cur_weapon_index.Value++;
        if (Input.GetKeyDown(m_input_control.prev_weapon_key)) m_cur_weapon_index.Value--;

        if (Input.GetKeyDown(m_input_control.fire_key)) Fire();
        
    }
    public void Fire()
    {
        var cur_weapon = weapons[m_cur_weapon_index];

        cur_weapon.Execute(weapon_point);
        cur_weapon.OnFire?.Invoke();
    }

    public void ChangeWeapon(int step)
    {
        m_cur_weapon_index.Value += step;
    }
}

public abstract class WeaponAbstract : MonoBehaviour // Editor does not inspect List<Interface> thats why Abstract Parent needed;
{
    public string Name { get { return name; } }
    public abstract void Execute(Transform point);

    [Space(30)]
    public UnityEvent OnFire;
}
