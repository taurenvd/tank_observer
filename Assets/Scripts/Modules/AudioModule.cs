using UnityEngine;

[DefaultExecutionOrder(10)]
[RequireComponent(typeof(AudioSource))]
public class AudioModule : MonoBehaviour
{
    [SerializeField, HideInInspector] AudioSource m_audio;

    [Header("Audio: ")]
    [SerializeField] AudioClip[] m_audio_clips;


    [Space]
    [SerializeField] bool include_condition_property;

    [SerializeField] bool skip_first_update;

    public bool Condition { get; set; }

    void Awake()
    {
        m_audio = GetComponent<AudioSource>();

        m_audio.mute = skip_first_update;
    }

    void Update()
    {
        if (include_condition_property && Condition && !m_audio.isPlaying)
        {
            Play();
        }

        if (include_condition_property && !Condition && m_audio.isPlaying)
        {
            m_audio.Stop();
        }
    }


    public void Play()
    {
        if (m_audio)
        {
            if (!m_audio.mute)
            {
                var rand_clip = m_audio_clips[Random.Range(0, m_audio_clips.Length - 1)];

                //Debug.Log("Play: "+rand_clip,gameObject);

                m_audio.PlayOneShot(rand_clip);
            }
            else
            {
                m_audio.mute = true;
            }
        }
    }

}
