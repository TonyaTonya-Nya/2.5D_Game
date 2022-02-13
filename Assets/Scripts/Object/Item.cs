using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float recoverAmount;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, 1);
            FindObjectOfType<PlayerManager>().Damage(-recoverAmount);
            Destroy(gameObject);
        }
    }
}
