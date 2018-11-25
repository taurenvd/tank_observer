using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MovementModule : MonoBehaviour
{
    [SerializeField, HideInInspector] Rigidbody m_rb;
    
    [SerializeField,Space] Vector2ValueClass m_velocity;

    [Header("Speed vars:")]
    [SerializeField] FloatValueClass m_rotation_speed;
    [SerializeField] FloatValueClass m_speed;

    [Space]
    [SerializeField] BoolValueClass m_can_control;

    [Space]
    [SerializeField] BoolValueClass m_is_moving;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = true;
        m_rb.useGravity = false;

        m_rotation_speed.Value = 25;
        m_speed.Value = 3;

    }


    void Update()
    {
        if (m_can_control.Value)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                m_velocity.Value = new Vector2(horizontal, vertical);
               
            }
        }
    }


    void FixedUpdate()
    {

        if (m_velocity.Value.sqrMagnitude>0.03f)
        {
            Move(m_velocity);
            m_is_moving.Value = true;
        }
        else
        {
            m_velocity.Value = Vector2.zero;
            m_is_moving.Value = false;
        }
    }

    void Move(Vector2 velocity)
    {
        //Debug.Log(velocity.ToString("0:0.000"));

        transform.Rotate(0, velocity.x * Time.fixedDeltaTime * m_rotation_speed, 0);

        m_rb.MoveRotation(transform.rotation);
        m_rb.MovePosition(m_rb.position + transform.forward * velocity.y * Time.fixedDeltaTime * m_speed);
    }
}
