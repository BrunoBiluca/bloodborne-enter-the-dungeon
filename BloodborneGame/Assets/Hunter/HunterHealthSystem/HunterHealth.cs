using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HunterHealth : MonoBehaviour, IHealthBar {

    private float baseHealth;
    private float currentHealth;

    private TMP_Text text;

    public void Setup(float baseHealth) {
        this.baseHealth = baseHealth;
        this.currentHealth = baseHealth;

        if(text == null)
            text = transform.Find("text").GetComponent<TMP_Text>();
        text.text = Mathf.FloorToInt(this.currentHealth).ToString();
    }

    public void SetCurrentHealth(float currentHealth) {
        this.currentHealth = Mathf.Clamp(currentHealth, 0, baseHealth);
        text.text = Mathf.FloorToInt(this.currentHealth).ToString();
    }

}
