using DiceRoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{

    public Material enemyPicture;
    public GameObject enemyCardPrefab;
    public string enemyName;
    public bool isBoss;
    public string description;
    public int echoesCounter;
    public HunterDiceSO lantern;

    public List<EnemyEffectParameter> parameters;
    public EnemyEffect effect;
}
