using Assets.UnityFoundation.Code.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectType : EnumX<CardEffectType>
{

    public static CardEffectType immediateEffect
        = new CardEffectType(0, "Immediate Effect");

    public CardEffectType(int index, string name) : base(index, name) { }
}


public abstract class CardEffect : MonoBehaviour
{

    public abstract CardEffectType EffectType();

}
