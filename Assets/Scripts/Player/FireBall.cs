using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject damageEffect;
    public int damageAmount = 80;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            other.GetComponent<Enemy>().TakeDamage(damageAmount,1);
            Destroy(gameObject);
        }
    }
}
