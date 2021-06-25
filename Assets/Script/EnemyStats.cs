using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    [Header("Health Settings")]
    [SerializeField] private float enemyHealth;

    [Header("Samage Settings")]
    [SerializeField] private float enemyDamage;
    [SerializeField] private float enemyAttackSpeed;
    private float canAttack;
    
    private void Update()
    {
        EnemyDeath();
    }

    private void EnemyReceiveDamage(float playerDamage)
    {
        enemyHealth -= playerDamage;
    }

    private void EnemyDeath()
    {
        if (enemyHealth <= 0)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (enemyAttackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerStats>().UpdateHealth(-enemyDamage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }
}
