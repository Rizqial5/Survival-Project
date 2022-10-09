using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockBackSystem : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb ;

    [SerializeField] private float strength = 16, delay = 0.15f;

    [SerializeField] GameObject sender;

    public UnityEvent OnBegin, OnDone;

    private void Start() 
    {
        sender = GameObject.FindGameObjectWithTag("Player");    
    }

    public void PlayKnockback()
    {
        StopAllCoroutines();
        OnBegin?.Invoke();

        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);

        // print(direction);

        StartCoroutine(Reset());

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }

}
