using Assets.UnityFoundation.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterHealthTest : MonoBehaviour {

    public Button damageButton;
    public Button restoreButton;
    public HealthSystem healthSystem;

    void Awake() {
        damageButton.onClick.AddListener(() => {
            healthSystem.Damage(1f);
        });

        restoreButton.onClick.AddListener(() => {
            healthSystem.HealFull();
        });        
    }
}
