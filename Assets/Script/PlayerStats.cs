using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float maxPlayerHealth = 5f;
    [SerializeField] private GameObject deathEffect;

    //Start is called before the first frame update
    private void Start()
    {
        playerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    public void UpdateHealth(float mod)
    {
        playerHealth += mod;

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
