using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Profile
{
    public class SwipeHolder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<float> Swipe;
        
        private Vector2 _startPoint;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPoint = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
           
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var delta = _startPoint.x - eventData.position.x;
            Swipe?.Invoke(delta);
        }
    }
}