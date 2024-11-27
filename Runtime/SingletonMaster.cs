using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{
    public sealed class SingletonMaster : ScriptableObject
    {
        [field: SerializeField]
        public bool autoCreateSingleton { get; private set; }

        public static List<Singleton> Singletons => Instance.singletons;

        [field: SerializeField, HideInInspector]
        public List<Singleton> singletons { get; private set; } = new();

        public static SingletonMaster Instance { get; private set; }

        private SingletonUpdater Updater;
        private void OnEnable() => SetInstance();

        public void SetInstance()
        {
            Instance = this;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateSingletonUpdater()
        {
            if (Instance == null)
                return;

            GameObject updaterObj = new GameObject("Singleton Updater");
            DontDestroyOnLoad(updaterObj);
            Instance.Updater = updaterObj.AddComponent<SingletonUpdater>();

            updaterObj.hideFlags = HideFlags.NotEditable;
        }
    }

}
