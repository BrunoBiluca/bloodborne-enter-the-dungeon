using Assets.UnityFoundation.TimeUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.UnityFoundation.Code;

public class StockSystem : MonoBehaviour
{
    private int count;

    public int Count => count;

    private TextMeshProUGUI echoesCountText;

    private Transform echoesHolder;

    private void Awake()
    {
        echoesCountText = transform.parent
            .FindComponent<TextMeshProUGUI>("hunter_canvas.echoes_number.text");

        echoesHolder = transform.Find("echoes_holder");
        echoesCountText.text = count.ToString();
    }

    public void Add(int amount)
    {
        for(var i = 0; i < amount; i++)
        {
            count++;
            var echoGO = Instantiate(
                GameAssets.Instance.echoesPrefab,
                echoesHolder
            );
            echoGO.transform.localRotation = Quaternion.Euler(90, 0, 0);
            echoGO.transform.localPosition = new Vector3(
                 -1 + count % 3,
                 -4 + count / 3,
                 -0.5f
            );
        }

        echoesCountText.text = count.ToString();
    }

    public void RemoveAll()
    {
        StartCoroutine(RemoveEchoes(count));
    }

    public void Remove(int amount)
    {
        StartCoroutine(RemoveEchoes(amount));
    }

    private IEnumerator RemoveEchoes(int amount)
    {
        var echoes = new List<GameObject>();

        var removeAmount = 0;
        foreach(Transform child in echoesHolder)
        {
            if(child.CompareTag(Tags.echoes))
                echoes.Add(child.gameObject);

            if(++removeAmount == amount)
                break;
        }

        foreach(var echo in echoes)
        {
            Destroy(echo);
            yield return WaittingCoroutine.RealSeconds(.1f);
        }
        count -= amount;

        echoesCountText.text = count.ToString();
    }
}
