using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleControl : MonoBehaviour
{
    [Header("Wheels tranfs:")]
    public Transform[] LeftWheels;
    public Transform[] RightWheels;

    [Header("Tracks:")]
    public MeshRenderer LeftTrack;
    public MeshRenderer RightTrack;

    [Header("Stats:")]
    public float wheelsSpeed = 2f;
    public float tracksSpeed = 2f;
    public float forwardSpeed = 1f;
    public float rotateSpeed = 1f;

    [Space]
    [SerializeField] Vector2Value m_velocity;

    void Update()
    {
        var temp_velocity = m_velocity ?? new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (temp_velocity.y != 0)
        {
            var sign = temp_velocity.y > 0 ? 1 : -1;

            Move(sign,-sign,sign,sign);
        }

        if (Mathf.Abs(temp_velocity.x) >= 0.75f)
        {
            var sign = temp_velocity.x > 0 ? 1 : -1;

            Move(-sign,-sign,-sign,sign);
        }
    }

    void Move(int l_sign, int r_sign, int l_offset_sign, int r_offset_sign)
    {

        foreach (Transform wheelL in LeftWheels)
        {
            wheelL.Rotate(new Vector3(l_sign * wheelsSpeed, 0f, 0f));
        }

        foreach (Transform wheelR in RightWheels)
        {
            wheelR.Rotate(new Vector3(-r_sign * wheelsSpeed, 0f, 0f));
        }

        LeftTrack.material.mainTextureOffset += new Vector2(0f, Time.deltaTime * l_offset_sign * tracksSpeed);
        RightTrack.material.mainTextureOffset += new Vector2(0f, Time.deltaTime * r_offset_sign * tracksSpeed);

    }
}
