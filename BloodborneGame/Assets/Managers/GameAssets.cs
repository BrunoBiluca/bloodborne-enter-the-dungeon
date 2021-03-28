using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {

    private static GameAssets instance;
    public static GameAssets Instance {
        get {
            if(instance == null) {
                instance = Resources.Load<GameObject>("GameAssets").GetComponent<GameAssets>();
            }
            return instance;
        }
        private set { instance = value; }
    }

    public GameObject echoesPrefab;

    private void Awake() {
        Instance = this;
    }

}
