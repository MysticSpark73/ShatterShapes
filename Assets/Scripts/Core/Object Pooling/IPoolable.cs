using UnityEngine;

namespace ShatterShapes.Core.Object_Pooling
{
    public interface IPoolable
    {
        void OnPooled();

        void OnReturn();

        void SetPosition(Vector3 pos, Transform container = null);

        void SetActive(bool value);

        void SetParent(Transform parent);
    }
}
