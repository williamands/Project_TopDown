using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private GameObject deathEffect;

    //[SerializeField] private float distance;
    //[SerializeField] private Transform player;
    //[SerializeField] private float enemyDamage;
    //[SerializeField] private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDeath();
        //EnemyMeleeAttack();
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

    //private void EnemyMeleeAttack()
    //{
    //    distance = Vector3.Distance(player.position, transform.position);

    //    if(distance <= 1.5f)
    //    {
    //        other.transform.SendMessage("PlayerReceiveDamage", enemyDamage);
    //    }
    //}
}
