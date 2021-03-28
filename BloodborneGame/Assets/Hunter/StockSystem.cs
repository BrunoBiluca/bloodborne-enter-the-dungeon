using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockSystem : MonoBehaviour {

    private int count;

    public void Add(int amount) {
        count += amount;

        for(var i = 0; i < amount; i++) {
            var echoGO = Instantiate(
                GameAssets.Instance.echoesPrefab, 
                transform
            );
            echoGO.transform.localRotation = Quaternion.Euler(90, 0, 0);
            echoGO.transform.localPosition = new Vector3(
                 -1 + count % 3,
                 -4 + count / 3,
                 -0.5f
            );
        }
    }
}
