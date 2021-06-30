using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    [Header("Health Settings")]
    [SerializeField] private float playerHealth;
    [SerializeField] private float maxPlayerHealth = 5f;
    
    [Header("Health UI Settings")]
    public HealthBar healthBar;
    [SerializeField] private GameObject hitFeedback;
    [SerializeField] private GameObject healFeedback;

    [Header("Game Over Settings")]
    [SerializeField] private GameObject gameOverScreen;

    private void Start()
    {
        playerHealth = maxPlayerHealth;
        healthBar.SetMaxHealth(maxPlayerHealth);
    }

    private void Update()
    {
        ScreenFeedback();
    }

    public void UpdateHealth(float mod)
    {
        playerHealth += mod;
        healthBar.SetHealth(playerHealth);

        if (playerHealth > maxPlayerHealth)
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
            gameOverScreen.SetActive(true);
        }

        if (mod < 0)
        {
            var color = hitFeedback.GetComponent<Image>().color;
            color.a = 0.2f;
            hitFeedback.GetComponent<Image>().color = color;
        }
        else if (mod > 0)
        {
            var color = healFeedback.GetComponent<Image>().color;
            color.a = 0.2f;
            hitFeedback.GetComponent<Image>().color = color;
        }
    }

    private void ScreenFeedback()
    {
        if (hitFeedback != null)
        {
            if (hitFeedback.GetComponent<Image>().color.a > 0)
            {
                var color = hitFeedback.GetComponent<Image>().color;
                color.a -= 0.001f;
                hitFeedback.GetComponent<Image>().color = color;
            }
        }

        if (healFeedback != null)
        {
            if (healFeedback.GetComponent<Image>().color.a > 0)
            {
                var color = healFeedback.GetComponent<Image>().color;
                color.a -= 0.001f;
                healFeedback.GetComponent<Image>().color = color;
            }
        }
    }
}
