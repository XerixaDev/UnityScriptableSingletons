using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Singleton
{
    [InitializeOnLoad]
    public sealed class SingletonMasterHelper
    {
        static SingletonMasterHelper() => Refresh();

        private static SingletonMaster singletonMaster = null;

        [MenuItem("Tools/Singleton Master/Refresh")]
        private static void Refresh()
        {
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            preloadedAssets.RemoveAll(p => p == null);

            EnsureCreation(preloadedAssets);
            Setup();
        }

        private static void EnsureCreation(List<Object> preloadedAssets)
        {
            bool foundSingletonMaster = false;
            for (int i = 0; i < preloadedAssets.Count; i++)
            {
                if (preloadedAssets[i].GetType() == typeof(SingletonMaster))
                {
                    singletonMaster = (SingletonMaster)preloadedAssets[i];
                    foundSingletonMaster = true;
                    break;
                }
            }

            if (foundSingletonMaster == false)
            {
                var path = EditorUtility.SaveFilePanelInProject("Specify ScriptableObjectMasterManager Location", "ScriptableObjectMasterManager", "asset", string.Empty);
                if (string.IsNullOrEmpty(path))
                    //Use default name as fallback
                    path = "Assets/SingletonMaster.asset";

                singletonMaster = ScriptableObject.CreateInstance<SingletonMaster>();
                AssetDatabase.CreateAsset(singletonMaster, path);

                preloadedAssets.Add(singletonMaster);
                PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            }
        }
        private static void Setup()
        {
            singletonMaster.SetInstance();
            singletonMaster.singletons.RemoveAll(s => s == null);
            if (singletonMaster.autoCreateSingleton)
                FindSingletons();
            AddSingletonsToMaster();
        }

        private static void FindSingletons()
        {
            foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                System.Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Singleton)))
                    {
                        bool found = false;
                        if (singletonMaster.singletons.Count > 0)
                        {
                            for (int j = 0; j < singletonMaster.singletons.Count; j++)
                            {
                                if (singletonMaster.singletons[j].GetType() == type)
                                    found = true;
                            }
                        }

                        if (!found)
                        {
                            var singleton = ScriptableObject.CreateInstance(type);
                            AssetDatabase.CreateAsset(singleton, $"Assets/{type.Name}.asset");
                        }
                    }
                }
            }
        }

        private static void AddSingletonsToMaster()
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(Singleton).Name);
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                Singleton so = AssetDatabase.LoadAssetAtPath<Singleton>(path);

                bool found = false;
                if (singletonMaster.singletons.Count > 0)
                {
                    for (int j = 0; j < singletonMaster.singletons.Count; j++)
                    {
                        if (singletonMaster.singletons[j].name == so.name)
                            found = true;
                    }
                }

                if (!found)
                {
                    singletonMaster.singletons.Add(so);
                }
            }
        }


    }
}

