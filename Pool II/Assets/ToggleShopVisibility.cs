using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShopVisibility : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform = null;

    [SerializeField]
    [Range(0f, 1f)]
    private float damping = 0.2f;

    private Vector2 positionSmoothing;
    private bool open;
    private Vector2 target;

    private void Start()
    {
        open = false;
        target = Vector2.zero;
    }

    public void Toggle()
    {
        open = !open;
        target = open ? Vector2.right * 128 : Vector2.zero;
    }

    private void Update()
    {
        SetPositionOfPivot(Vector2.SmoothDamp(rectTransform.anchoredPosition, target, ref positionSmoothing, damping));
    }

    private void SetPositionOfPivot(Vector2 newPos)
    {
        rectTransform.anchoredPosition = newPos;
    }
}
