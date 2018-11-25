using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class MobileInput : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField,HideInInspector] Vector2 base_anchored;
    [SerializeField,HideInInspector] Vector2 parent_pos;
    [SerializeField] Vector2Value m_velocity;

    [SerializeField] RectTransform m_rect_tr;

    [SerializeField] float max_distance;

    public void OnDrag(PointerEventData eventData)
    {
        var distance = Vector2.Distance(parent_pos, m_rect_tr.anchoredPosition + eventData.delta);

        if (distance <= max_distance)
        {
            m_rect_tr.anchoredPosition += eventData.delta;
        }

        m_velocity.Value = (m_rect_tr.anchoredPosition - parent_pos).normalized;
        //Debug.Log("Distance: "+distance);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_rect_tr.anchoredPosition = base_anchored;
        m_velocity.Value = Vector2.zero;
    }

    void Awake()
    {
        m_rect_tr = GetComponent<RectTransform>();
    }

    void Start()
    {
        base_anchored = m_rect_tr.anchoredPosition;
        max_distance = Screen.height / 5f;
        parent_pos = transform.parent.GetComponent<RectTransform>().anchoredPosition;
    }
}
