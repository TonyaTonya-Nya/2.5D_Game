using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoom : MonoBehaviour
{
    public GameObject damageEffect;
    public int damageAmount = 80;
    private PlayerManager player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            player.Damage(damageAmount);
            Destroy(gameObject);
        }
    }
}
