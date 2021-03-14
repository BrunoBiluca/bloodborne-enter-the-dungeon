using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HunterHealth : MonoBehaviour, IHealthBar {

    private float baseHealth;
    private float currentHealth;

    private TMP_Text text;

    void Awake() {
        text = transform.Find("text").GetComponent<TMP_Text>();
    }

    public void Setup(float baseHealth) {
        this.baseHealth = baseHealth;
        this.currentHealth = baseHealth;
        text.text = Mathf.FloorToInt(this.currentHealth).ToString();
    }


    public void SetSize(float currentHealth) {
        this.currentHealth = Mathf.Clamp(currentHealth, 0, baseHealth);
        text.text = Mathf.FloorToInt(this.currentHealth).ToString();
    }

}
