using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public GameObject damageEffect;
    public float damage;
    public float speed;

    public GameObject hintObject;
    private PlayerManager player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (transform.position.y > 0)
        {
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
        else if (transform.position.y <= 0)
        {
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            Destroy(hintObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            player.Damage(damage);
            Destroy(hintObject);
            Destroy(gameObject);
        }
    }
}
