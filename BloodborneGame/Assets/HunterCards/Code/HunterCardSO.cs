using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HunterCard")]
public class HunterCardSO : ScriptableObject {

    public string cardName;
    public int damage;
    public string effectDescription;
    public bool isInitialCard;

    public Material cardPicture;

    // TOOD: criar um atributo para verificar se o componente tem o tipo especificado
    public GameObject cardPrefab; // O cardPrefab deve ter o component HunterCard

    public CardEffect effect; // O effect tem que ter a implementação de algum efeito
}
