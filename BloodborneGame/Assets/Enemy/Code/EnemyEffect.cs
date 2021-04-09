using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class EnemyEffect : MonoBehaviour {

    public List<EnemyEffectParameter> Parameters { get; set; }

}
