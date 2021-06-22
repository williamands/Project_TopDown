using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletEffect;
    [SerializeField] private float playerDamage;

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        GameObject effect = Instantiate(bulletEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
        if (enemy.gameObject.CompareTag("Enemy"))
        {
            enemy.transform.SendMessage("EnemyReceiveDamage", playerDamage);
        }
    }
}
