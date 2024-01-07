using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnim : MonoBehaviour
{
    [SerializeField] float rate;
    private SpriteRenderer SR;
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        StartCoroutine(repeater());
    }

    private IEnumerator repeater()
    {
        SR.flipX = false;
        yield return new WaitForSeconds(rate);
        SR.flipX = true;
        yield return new WaitForSeconds(rate);
        StartCoroutine(repeater());
    }
}
