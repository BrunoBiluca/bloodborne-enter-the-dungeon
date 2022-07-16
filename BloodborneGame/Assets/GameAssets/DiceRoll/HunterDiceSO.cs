using System.Collections.Generic;
using UnityEngine;

namespace DiceRoll
{
    public enum DiceType
    {
        green,
        yellow,
        red
    }

    [CreateAssetMenu(menuName = "ScriptableObjects/Dice")]
    public class HunterDiceSO : ScriptableObject
    {
        public DiceType diceType;
        public List<DiceSideSO> sides;
        public int SidesCount { get { return sides.Count; } }

        public Sprite sprite;
        public Sprite lanternSprite;
    }
}
