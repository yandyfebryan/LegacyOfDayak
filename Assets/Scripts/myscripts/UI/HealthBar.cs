using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Image healthBarBackground;
    public Image healthBarFill;

    void Start()
    {
        healthBarFill.fillAmount = 1;
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float currentHealthRatio = (float)playerHealth.CurrentHealth / (float)playerHealth.MaxHealth;
        healthBarFill.fillAmount = currentHealthRatio;
    }
}
