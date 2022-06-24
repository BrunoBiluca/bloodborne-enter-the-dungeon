using UnityFoundation.Code;
using UnityEngine;

public class CardEffectType : EnumX<CardEffectType>
{
    public static CardEffectType immediateEffect
        = new CardEffectType(0, "Immediate Effect");

    public static CardEffectType huntersDreamEffect
        = new CardEffectType(1, "Hunters Dream");

    public static CardEffectType attackEffect
        = new CardEffectType(2, "Attack Effect");

    public CardEffectType(int index, string name) : base(index, name) { }
}


public abstract class CardEffect : MonoBehaviour
{

    public abstract CardEffectType EffectType();

}
