using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HunterCard : MonoBehaviour {

    public void Setup(HunterCardSO hunterCardSO) {

        var front = transform.Find("front");

        front.Find("name").GetComponent<TextMeshPro>().text = hunterCardSO.cardName;
        front.Find("picture").GetComponent<MeshRenderer>().material = hunterCardSO.cardPicture;
        front.Find("echoesIcon")
            .Find("echoesCount")
            .GetComponent<TextMeshPro>().text = hunterCardSO.damage.ToString();

        front.Find("initialCard").gameObject.SetActive(hunterCardSO.isInitialCard);

        if(!string.IsNullOrEmpty(hunterCardSO.effectDescription)) {
            front.Find("effectText").GetComponent<TextMeshPro>().text = hunterCardSO.effectDescription;
        }

        if(hunterCardSO.effect != null) {
            gameObject.AddComponent(hunterCardSO.effect.GetType());
        }
    }

}
