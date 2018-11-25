using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextHelper : MonoBehaviour {

    [MenuItem("Custom/Parent name->Child Text %&S")]
    public static void SynxParentNameAndChildText()
    {
        
        var obj = Selection.activeGameObject;
        
        if (obj.GetComponentInChildren<Text>())
        {
            Undo.RecordObject(obj.GetComponentInChildren<Text>(), string.Format("Parent({0})->Text({1})", obj.name, obj.GetComponentInChildren<Text>().name));
            obj.GetComponentInChildren<Text>().text=Selection.activeGameObject.name;
        }
        EditorUtility.SetDirty(obj.GetComponentInChildren<Text>());
    }

    [MenuItem("Custom/Convert Text to TMP %&R")]
    public static void ConvertTextToTMP()
    {
        var objs = Selection.GetFiltered<Text>(SelectionMode.Unfiltered);
        var str = "Objects will be modified:\n\n";

        System.Array.ForEach(objs, x => str += x.name + "\n");

        if (EditorUtility.DisplayDialog("Convert Text to TMP",str,"OK","Cancel"))
        {
            foreach (var item in objs)
            {
                Undo.RecordObject(item, "Convert Text to TMP");
                item.name += "(Converted)";

                var temp_str = item.text;
                var go = item.gameObject;
                var rect = item.GetComponent<RectTransform>();
                var color = item.color;

                EditorUtility.SetDirty(item.gameObject);
                DestroyImmediate(item);

                var tmp = go.AddComponent<TextMeshProUGUI>();

                tmp.text = temp_str;
                tmp.alignment = TMPro.TextAlignmentOptions.Center;
                tmp.color = color;
                rect.anchoredPosition = Vector2.zero;
                rect.sizeDelta = Vector2.zero;
            }
        }       
    }
}
