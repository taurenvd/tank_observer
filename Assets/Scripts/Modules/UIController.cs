using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class UIController : Singleton<UIController>
{
    [Header("Output Formats: ")]
    [SerializeField] string kills_format = "Kills: {0}";
    [SerializeField] string weapon_id_format = "Weapon: {0}";
        

    Action<float> hp_change_delegate = hp => { UI.Instance.hp_s.value = hp; };
    Action<int> kills_change_delegate = kills => UI.Instance.kills_t.text = string.Format(Instance.kills_format, kills);
    Action<string> weapon_change_delegate = type => UI.Instance.projectile_type_t.text = string.Format(Instance.weapon_id_format, type);
    Action<bool> state_change_delegate = state => 
    {
        //Debug.Log(state);
        if (!state) UI.Instance.lose_panel.SetActive(true);
    };

    [Header("ScriptableValues: ")]
    [SerializeField] FloatValue hp;

    [Space]
    [SerializeField] IntValue kills;

    [Space]
    [SerializeField] BoolValue game_state;

    [Space]
    [SerializeField] StringValue weapon_id;

    void Awake()
    {
        base.Awake();
        UI.Instance.restart_b.onClick.AddListener(() => SceneManager.LoadScene(0));

        if (game_state) game_state.OnValueChanged += state_change_delegate;
        if (weapon_id) weapon_id.OnValueChanged += weapon_change_delegate;
    }

    void OnEnable()
    {
        if (hp) hp.OnValueChanged += hp_change_delegate;
        if (kills) kills.OnValueChanged += kills_change_delegate;
       
        
    }
    void OnDisable()
    {
        if (hp) hp.OnValueChanged -= hp_change_delegate;
        if (kills) kills.OnValueChanged -= kills_change_delegate;
       
        
    }
}
