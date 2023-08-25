using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShatterShapes.UI
{
    public class OnPointerDownListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Action _onPointerDownListener;
        private bool _isPointerDown;

        public void AddListener(Action listener) => _onPointerDownListener = listener;

        private void Update()
        {
            if (_isPointerDown)
            {
                _onPointerDownListener?.Invoke();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _onPointerDownListener?.Invoke();
            _isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;
        }
    }
}