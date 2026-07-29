using System;
using UnityEngine;

public class LV14Door : MonoBehaviour
{
    public Action OnPlayerCollider;

    [Header("Blow Out Settings")]
    [SerializeField] private float blowForceUp = 10f;
    [SerializeField] private float blowForceSide = 5f;
    [SerializeField] private float torqueForce = 20f;

    private BoxCollider2D boxCollider;
    private Rigidbody2D doorRig;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        doorRig = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        BlowOut();
    }

    public void BlowOut()
    {
        boxCollider.isTrigger = true;
        doorRig.bodyType = RigidbodyType2D.Dynamic;
        Vector2 force = new Vector2(blowForceSide, blowForceUp);
        doorRig.AddForce(force, ForceMode2D.Impulse);
        doorRig.AddTorque(-torqueForce, ForceMode2D.Impulse);
    }
}
