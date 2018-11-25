using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class GameController : Singleton<GameController>
{
    public enum GameState { Menu, Game, End }

    [SerializeField] Camera m_camera;

    [SerializeField] ReceiveDamageModule m_target;

    #region Properties
    public BoolValue InGame;

    public Camera Camera
    {
        get
        {
            return m_camera;
        }
    }

    #endregion

    [SerializeField] ParticleSystem m_explosion;

    void Start()
    {
        InGame.Value = true;

        Time.timeScale = 1f;

        m_target.OnDeath += GameOver;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GameOver()
    {
        if (InGame.Value)
        {
            InGame.Value = false;
            Debug.Log("GameOverEvent");

            Instantiate(m_explosion, m_target.transform.position, m_explosion.transform.rotation);

            var old_pos = m_camera.transform.localPosition;
            var old_rot = m_camera.transform.localRotation;

            StartCoroutine(Misc.ProgressTimer(3f, (prog) =>
                {
                    m_camera.transform.localPosition = Vector3.Lerp(old_pos, new Vector3(0, 15, 0), prog);
                    m_camera.transform.localRotation = Quaternion.Lerp(old_rot, Quaternion.Euler(90, 0, 0), prog);
                },
                () => Time.timeScale = 0));
        }
    }

    public static IEnumerator Timer(float wait_time, System.Action OnWaitComplete)
    {
        yield return new WaitForSeconds(wait_time);
        OnWaitComplete();
    }
}
