using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShatterShapes.UI
{
    public class OnPointerDownListener : MonoBehaviour, IPointerDownHandler
    {
        private Action _onPointerDownListener;

        public void AddListener(Action listener) => _onPointerDownListener = listener;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _onPointerDownListener?.Invoke();
        }
    }
}