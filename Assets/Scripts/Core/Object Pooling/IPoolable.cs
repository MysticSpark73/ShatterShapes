using ShatterShapes.Game.Level;
using UnityEngine;

namespace ShatterShapes.Core.Object_Pooling
{
    public interface IPoolable
    {
        public ObjectsPool KeyPool { get; set; }
        
        void OnPooled();

        void OnReturn();

        void SetPosition(Vector3 pos, Transform container = null);

        void SetActive(bool value);
    }
}
