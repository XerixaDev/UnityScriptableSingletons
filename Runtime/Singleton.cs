using UnityEngine;

namespace Singleton
{
    public abstract class Singleton<T> : Singleton where T : ScriptableObject
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = CreateInstance<T>();
                return _instance;
            }
        }
        protected override void OnEnable()
        {
            _instance = this as T;
        }
    }
    public abstract class Singleton : ScriptableObject
    {
        protected virtual void OnEnable() { }
        public virtual void OnAwake() { }
        public virtual void Update() { }
        public virtual void OnDisable() { }
        public virtual void OnDestroy() { }
    }
}
