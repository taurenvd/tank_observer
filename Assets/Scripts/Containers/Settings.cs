using UnityEngine;

[CreateAssetMenu]
public class Settings : ScriptableObject
{
    [Header("Controls:")]
    public KeyCode next_weapon_key = KeyCode.E;
    public KeyCode prev_weapon_key = KeyCode.Q;
    public KeyCode fire_key = KeyCode.X;
}
