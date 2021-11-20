using Assets.UnityFoundation.Code.Common;
using Assets.UnityFoundation.TimeUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollUI : Singleton<DiceRollUI>
{

    private Transform diceUI;
    private GameObject blurLayer;

    protected override void OnAwake()
    {
        diceUI = transform.Find("dice");
        blurLayer = transform.Find("blurLayer").gameObject;

        gameObject.SetActive(false);
    }

    public IEnumerator Roll(DiceSO dice, DiceSideSO result)
    {
        gameObject.SetActive(true);
        blurLayer.SetActive(true);
        yield return RollAnimation(dice, result);
    }

    private IEnumerator RollAnimation(DiceSO dice, DiceSideSO result)
    {
        for(int i = 0; i < 30; i++)
        {
            var sideIdx = UnityEngine.Random.Range(0, dice.SidesCount); ;

            FillUI(dice, dice.sides[sideIdx]);
            yield return WaittingCoroutine.RealSeconds(.1f);
        }

        blurLayer.SetActive(false);
        FillUI(dice, result);
        yield return WaittingCoroutine.RealSeconds(2f);
        gameObject.SetActive(false);
    }

    private void FillUI(DiceSO dice, DiceSideSO result)
    {
        diceUI.Find("image").GetComponent<Image>().sprite = dice.sprite;
        diceUI.Find("number").GetComponent<TMP_Text>().text = result.value.ToString();
        diceUI.Find("plusIcon").gameObject.SetActive(result.plus);
    }

}
