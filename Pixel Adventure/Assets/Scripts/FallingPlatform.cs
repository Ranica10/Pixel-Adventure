using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private float fallDelay = 1f;
    private float respawnDelay = 3f;

    private Animator anim;

    [SerializeField] private Rigidbody2D rb;

    private Vector3 originalPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        originalPos = gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(respawnDelay);
        
        anim.SetBool("activated", false);
        gameObject.transform.position = originalPos;
        rb.bodyType = RigidbodyType2D.Static;
        StopCoroutine(Fall());
    }
}
