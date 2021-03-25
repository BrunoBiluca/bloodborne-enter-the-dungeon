using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {

    public static GameAssets Instance { get; private set; }

    public GameObject echoesPrefab;

    private void Awake() {
        Instance = this;
    }

}
