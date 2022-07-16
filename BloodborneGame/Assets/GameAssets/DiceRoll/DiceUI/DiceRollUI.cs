using UnityFoundation.Code;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DiceRoll
{
    public class DiceRollUI : Singleton<DiceRollUI>
    {

        private Transform diceUI;

        protected override void OnAwake()
        {
            diceUI = transform.Find("dice");

            gameObject.SetActive(false);
        }

        public IEnumerator Roll(HunterDiceSO dice, DiceSideSO result)
        {
            gameObject.SetActive(true);
            yield return RollAnimation(dice, result);
        }

        private IEnumerator RollAnimation(HunterDiceSO dice, DiceSideSO result)
        {
            for(int i = 0; i < 30; i++)
            {
                var sideIdx = Random.Range(0, dice.SidesCount); ;

                FillUI(dice, dice.sides[sideIdx]);
                yield return new WaitForSecondsRealtime(.1f);
            }

            FillUI(dice, result);
            yield return new WaitForSecondsRealtime(.1f);
            gameObject.SetActive(false);
        }

        private void FillUI(HunterDiceSO dice, DiceSideSO result)
        {
            diceUI.Find("image").GetComponent<Image>().sprite = dice.sprite;
            diceUI.Find("number").GetComponent<TMP_Text>().text = result.value.ToString();
            diceUI.Find("plusIcon").gameObject.SetActive(result.plus);
        }

    }
}