using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterConfigHolder : MonoBehaviour
{
    [SerializeField] private HunterSO hunterConfig;

    public HunterSO HunterConfig => hunterConfig;
}
