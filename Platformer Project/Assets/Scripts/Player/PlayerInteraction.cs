using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public ScoreHolder scoreHolder;
    public LayerMask coinMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coinMask == (coinMask | (1 << collision.gameObject.layer)))
        {
            Coin coin = collision.GetComponentInParent<Coin>();

            if (scoreHolder != null) scoreHolder._AddScore(coin.coinValue);

            Destroy(coin.coinTransform.gameObject);
        }
    }
}
