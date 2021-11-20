using Assets.UnityFoundation.TimeUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockSystem : MonoBehaviour
{

    private int count;

    public void Add(int amount)
    {
        for(var i = 0; i < amount; i++)
        {
            count++;
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

    public void RemoveAll()
    {
        StartCoroutine(RemoveEchoes());
    }

    private IEnumerator RemoveEchoes()
    {

        var echoes = new List<GameObject>();

        foreach(Transform child in transform)
        {
            if(child.CompareTag(Tags.echoes))
                echoes.Add(child.gameObject);
        }

        foreach(var echo in echoes)
        {
            Destroy(echo);
            yield return WaittingCoroutine.RealSeconds(.1f);
        }
        count = 0;
    }
}
