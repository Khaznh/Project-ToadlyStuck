using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public Action OnSwipeLeft;

    [SerializeField] private float swipeThreshold = 500f;
    private Vector2 startTouchPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startTouchPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 endTouchPosition = eventData.position;

        float distanceX = endTouchPosition.x - startTouchPosition.x;

        if (distanceX < 0 && Mathf.Abs(distanceX) >= swipeThreshold)
        {
            OnSwipeLeft?.Invoke();
        } 
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
}
