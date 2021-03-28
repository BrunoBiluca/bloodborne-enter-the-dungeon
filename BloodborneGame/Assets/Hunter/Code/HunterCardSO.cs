using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/HunterCard")]
public class HunterCardSO : ScriptableObject {

    public string cardName;
    public int damage;
    public bool isInitialCard;

    public Material cardPicture;
    public GameObject cardPrefab;
}
