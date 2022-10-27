using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
    public class PrefabDevLoader : MonoBehaviour
    {
        public enum Prefab { Player, Captain, Ship, AiDisc, DialogBox, AiGenerator, GameManager }
        [System.Serializable]
        public class PrefabResource {
            public Prefab type;
            public GameObject prefab;
            public bool isUi = false;
        }

        [Header("This Scene")]
        public Prefab thisScenesPrefab;
        public bool createCanvas = true;

        [Header("Prefabs")]
        public PrefabResource[] allPrefabs;
        public GameObject canvas;
        public GameObject eventSystem;


        void Start()
        {
            // create canvas if it doesn't exist
            if(createCanvas) {
                GameObject.Instantiate(canvas);
                GameObject.Instantiate(eventSystem);
            }

            // create all prefabs
            foreach(var prefab in allPrefabs) {
                // skip if this is the prefab we're testing in this scene...
                if(prefab.type != thisScenesPrefab) {
                    // ignore null prefabs
                    if(prefab.prefab == null) {
                        Debug.LogWarning($"There is currently no object assigned to the prefab {prefab.type.ToString()}");
                    }
                    else {
                        // if it's UI, we want to put it inside the Canvas...
                        if(prefab.isUi) {
                            GameObject.Instantiate(prefab.prefab, GameObject.Find("Canvas(Clone)").transform);
                        }
                        else {
                            GameObject.Instantiate(prefab.prefab);
                        }                        
                    }                    
                }
            }
        }
    }
}