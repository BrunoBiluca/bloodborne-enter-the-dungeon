using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiceRoll
{
    [CreateAssetMenu(menuName = "ScriptableObjects/DiceSide")]
    public class DiceSideSO : ScriptableObject
    {
        public int value;
        public bool plus;
    }
}