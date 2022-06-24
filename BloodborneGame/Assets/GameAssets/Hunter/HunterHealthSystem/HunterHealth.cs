using Assets.UnityFoundation.Systems.HealthSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityFoundation.Code;

public class HunterHealth : MonoBehaviour, IHealthBar
{
    private float baseHealth;
    private float currentHealth;

    private Image healthImage;

    public void Setup(float baseHealth)
    {
        this.baseHealth = baseHealth;
        this.currentHealth = baseHealth;

        if(healthImage == null)
            healthImage = transform.FindComponent<Image>("mask.health");

        healthImage.fillAmount = this.currentHealth.Remap(0f, baseHealth, 0.564f, 0.935f);
    }

    public void SetCurrentHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
        healthImage.fillAmount = this.currentHealth.Remap(0f, baseHealth, 0.564f, 0.935f);
    }

}
