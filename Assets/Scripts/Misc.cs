using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using System.Linq;

namespace Utils
{
    public class Misc
    {
        #region consts
        const string startColorTag = "<color=";
        const string closeColorTag = ">";
        const string endColorTag = "</color>";
        #endregion

        public static string RichTextColor(Color c, string text)
        {

            var colorToText = "#" + ColorUtility.ToHtmlStringRGB(c);
            //Debug.Log(colorToText);
            var result = startColorTag + colorToText + closeColorTag + text + endColorTag;
            return result;
        }

        public static IEnumerator Timer(float time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback();
        }

        public static IEnumerator WaitUntil(Func<bool> rule, Action after)
        {
            yield return new WaitUntil(rule);
            yield return null;
            after();
        }

        public static IEnumerator ProgressTimer(float life_time, Action<float, float> update_call, params Action[] final_actions)
        {
            var cur_time = 0f;
            while (cur_time < life_time)
            {
                cur_time += Time.deltaTime;
                update_call(cur_time / life_time, Time.deltaTime);
                yield return null;
                //Debug.LogAssertionFormat("Time: {0:0.00} Progress: {1:0.00}",cur_time, cur_time / life_time);
            }
            foreach (var action in final_actions)
            {
                action();
            }
        }
        public static IEnumerator ProgressTimer(float life_time, Action<float> update_call, params Action[] final_actions)
        {
            var cur_time = 0f;
            while (cur_time < life_time)
            {
                cur_time += Time.deltaTime;
                update_call(cur_time / life_time);
                yield return null;
                //Debug.LogAssertionFormat("Time: {0:0.00} Progress: {1:0.00}",cur_time, cur_time / life_time);
            }
            foreach (var action in final_actions)
            {
                action();
            }
        }
    }
}
