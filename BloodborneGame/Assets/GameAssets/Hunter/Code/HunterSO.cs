using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Hunter", fileName = "new_hunter_so")]
public class HunterSO : ScriptableObject
{
    [SerializeField] private string hunterName;
    public string HunterName => hunterName;
    
    [SerializeField] private GameObject hunterPrefab;
    public GameObject HunterPrefab => hunterPrefab;
}
