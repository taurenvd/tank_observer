using UnityEngine;

[CreateAssetMenu]
public class Chars : ScriptableObject
{
    public float dmg;
    public float hp;

    [Range(.1f, .9f)]
    public float defence = .1f;
    [Range(1f, 10f)]
    public float speed = 1f;
}
