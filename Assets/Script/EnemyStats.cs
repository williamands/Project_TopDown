using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private GameObject deathEffect;

    [SerializeField] private float enemyDamage = 2f;
    [SerializeField] private float enemyAttackSpeed = 1f;
    [SerializeField] private float canAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDeath();
    }

    public void EnemyReceiveDamage(float playerDamage)
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
