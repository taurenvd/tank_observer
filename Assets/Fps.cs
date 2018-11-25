using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps : MonoBehaviour
{
    Rect fps_rect = new Rect(5, Screen.height - 25, 50, 25);

    [SerializeField] GameObject pps;

    [SerializeField] bool use_pps=false;

    void OnGUI()
    {
        GUI.Label(fps_rect, string.Format("{0:0.0} ms", Time.unscaledDeltaTime * 1000));

        var tgl_rect = new Rect(fps_rect.x+75,fps_rect.y,100,25);
        

        use_pps = GUI.Toggle(tgl_rect, use_pps, "use PostProc.");

        if (use_pps)
        {
            if (!pps.activeSelf)
            {
                pps.SetActive(true);
            }
        }
        else
        {
            if (pps.activeSelf)
            {
                pps.SetActive(false);
            }
        }
    }
}
