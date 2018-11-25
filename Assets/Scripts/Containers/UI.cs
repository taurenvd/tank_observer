using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : Singleton<UI>
{
    [Header("Sliders:")]
    public Slider hp_s;

    [Header("Texts:")]
    public TextMeshProUGUI kills_t;
    public TextMeshProUGUI projectile_type_t;

    [Header("Buttons:")]
    public Button start_b;
    public Button restart_b;

    [Header("Panels:")]
    public GameObject lose_panel;
}