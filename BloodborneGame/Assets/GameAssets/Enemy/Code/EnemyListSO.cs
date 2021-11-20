using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/EnemyList")]
public class EnemyListSO : ScriptableObject
{

    public List<EnemySO> enemies;

}
