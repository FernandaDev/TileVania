using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] AudioClip pickUpSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(pickUpSFX, Camera.main.transform.position + Vector3.forward);
        GameSession.instance.AddCoin();
        Destroy(gameObject);
    }
}
