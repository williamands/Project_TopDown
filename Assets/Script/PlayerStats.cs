using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    [Header("Health Settings")]
    [SerializeField] private float playerHealth;
    [SerializeField] private float maxPlayerHealth = 5f;
    
    [Header("Health UI Settings")]
    public HealthBar healthBar;

    private void Start()
    {
        playerHealth = maxPlayerHealth;
        healthBar.SetMaxHealth(maxPlayerHealth);
    }

    public void UpdateHealth(float mod)
    {
        playerHealth += mod;
        healthBar.SetHealth(playerHealth);

        if(playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
        else if (playerHealth <= 0f)
        {
            playerHealth = 0f;
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
            Debug.Log("Player Died");
        }
    }
}
