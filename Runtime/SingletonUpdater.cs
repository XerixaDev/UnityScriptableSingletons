using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{
    [DisallowMultipleComponent]
    public sealed class SingletonUpdater : MonoBehaviour
    {
        private void Awake()
        {
            for (int i = 0; i < SingletonMaster.Singletons.Count; i++)
            {
                SingletonMaster.Singletons[i].OnAwake();
            }
        }

        private void Update()
        {
            for (int i = 0; i < SingletonMaster.Singletons.Count; i++)
            {
                SingletonMaster.Singletons[i].Update();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < SingletonMaster.Singletons.Count; i++)
            {
                SingletonMaster.Singletons[i].OnDisable();
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < SingletonMaster.Singletons.Count; i++)
            {
                SingletonMaster.Singletons[i].OnDestroy();
            }
        }

        private void OnApplicationQuit()
        {
            Destroy(gameObject);
        }
    }
}

