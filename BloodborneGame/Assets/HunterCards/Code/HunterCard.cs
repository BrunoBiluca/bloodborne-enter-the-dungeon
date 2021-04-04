using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HunterCard : MonoBehaviour {

    public void Setup(HunterCardSO hunterCardSO) {

        var front = transform.Find("front");

        front.Find("name").GetComponent<TextMeshPro>().text = hunterCardSO.cardName;
        front.Find("picture").GetComponent<MeshRenderer>().material = hunterCardSO.cardPicture;

        if(hunterCardSO.damage > 0) {
            front.Find("echoesIcon")
                .Find("echoesCount")
                .GetComponent<TextMeshPro>().text = hunterCardSO.damage.ToString();
        }

        front.Find("initialCard").gameObject.SetActive(hunterCardSO.isInitialCard);

        if(hunterCardSO.effect != null) {
            if(!string.IsNullOrEmpty(hunterCardSO.effectDescription)) {
                front.Find("effectDescription")
                    .GetComponent<TextMeshPro>().text = hunterCardSO.effectDescription;
            }

            if(hunterCardSO.effect is IImmediateEffect) {
                front.Find("effectType")
                    .GetComponent<TextMeshPro>().text = hunterCardSO.effect.EffectType();
            }
            
            gameObject.AddComponent(hunterCardSO.effect.GetType());
        }
    }

}
