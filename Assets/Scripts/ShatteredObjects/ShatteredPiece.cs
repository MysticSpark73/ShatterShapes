using DG.Tweening;
using ShatterShapes.Core;
using UnityEngine;

namespace ShatterShapes.ShatteredObjects
{
    [RequireComponent(typeof(Rigidbody))]
    public class ShatteredPiece : MonoBehaviour, IInitable
    {
        private Rigidbody _rigidbody;
        private MeshRenderer _renderer;
        private Transform _selfTransform;
        private Vector3 _initialLocalPosition;
        private Vector3 _initialLocalRotation;
        private Vector3 _initialLocalScale;

        private float _shatterImpulse = 100;
        private float _disappearTime = 10;
        
        public void Init(object[] args = null)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _renderer = GetComponent<MeshRenderer>();
            _selfTransform = transform;
            _initialLocalPosition = _selfTransform.localPosition;
            _initialLocalRotation = _selfTransform.localRotation.eulerAngles;
            _initialLocalScale = _selfTransform.localScale;
        }

        public void ResetPiece()
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _selfTransform.localPosition = _initialLocalPosition;
            _selfTransform.eulerAngles = _initialLocalRotation;
            _selfTransform.localScale = _initialLocalScale;
        }

        public void OnShatter()
        {
            _rigidbody.isKinematic = false;
            ShrinkAsync();
            //_rigidbody.AddForce(-transform.forward * _shatterImpulse, ForceMode.Impulse);
        }

        public async void ShrinkAsync()
        {
            await new WaitForSeconds(_disappearTime / 10 * 8);
            transform.DOScale(Vector3.zero, _disappearTime / 10 * 2);
        }

        public void SetColor(Color color) => _renderer.materials[0].color = color;
    }
}
