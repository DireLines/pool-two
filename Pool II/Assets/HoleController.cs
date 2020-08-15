using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (null == (rb = collision.GetComponent<Rigidbody2D>())) return;

        StartCoroutine(EnterHole(rb));
    }

    private IEnumerator EnterHole(Rigidbody2D rb)
    {
        Vector2 direction;
        float t = 0f;
        rb.velocity = ((Vector2)transform.position - rb.position).normalized * 10f;
        rb.drag = 10f;
        while (t < 1f)
        {
            direction = ((Vector2)transform.position - rb.position).normalized;
            rb.velocity = direction * rb.velocity.magnitude * (1 - 2f * Time.deltaTime);
            Debug.DrawRay(rb.position, rb.velocity, Color.cyan);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
