using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
    public class PrefabDevLoader : MonoBehaviour
    {
        public enum Prefab { Player, Captain, Ship, AiDisc, DialogBox, AiGenerator }
        [System.Serializable]
        public class PrefabResource {
            public Prefab type;
            public GameObject prefab;
        }

        [Header("This Scene")]
        public Prefab thisScenesPrefab;

        [Header("Prefabs")]
        public PrefabResource[] allPrefabs;


        void Start()
        {
            foreach(var prefab in allPrefabs) {
                if(prefab.type != thisScenesPrefab) {
                    if(prefab.prefab == null) {
                        Debug.LogWarning($"There is currently no object assigned to the prefab {prefab.type.ToString()}");
                    }
                    else {
                        GameObject.Instantiate(prefab.prefab);
                    }                    
                }
            }
        }
    }
}