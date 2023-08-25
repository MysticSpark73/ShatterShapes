using ShatterShapes.Core;
using UnityEngine;

namespace ShatterShapes.ShatteredObjects
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShatteredPiece : MonoBehaviour, IInitable
    {
        private Rigidbody _rigidbody;
        private Transform _selfTransform;
        private Vector3 _initialLocalPosition;
        private Vector3 _initialLocalRotation;
        private Vector3 _initialLocalScale;
        
        public void Init()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _selfTransform = transform;
            _initialLocalPosition = _selfTransform.localPosition;
            _initialLocalRotation = _selfTransform.localRotation.eulerAngles;
            _initialLocalScale = _selfTransform.localScale;
        }

        public void ResetPiece()
        {
            _selfTransform.localPosition = _initialLocalPosition;
            _selfTransform.eulerAngles = _initialLocalRotation;
            _selfTransform.localScale = _initialLocalScale;
            _rigidbody.isKinematic = true;
        }

        public void OnShatter()
        {
            _rigidbody.isKinematic = false;
            //todo: probably apply some force es well
        }
    }
}
