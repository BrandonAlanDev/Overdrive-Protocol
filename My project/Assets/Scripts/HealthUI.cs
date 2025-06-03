using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    private PlayerHealth playerHealth;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.maxHealth;
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            healthSlider.value = Mathf.Clamp(playerHealth.GetHealth(), 0, playerHealth.maxHealth);
        }
    }
}
