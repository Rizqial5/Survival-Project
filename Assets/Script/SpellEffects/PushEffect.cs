using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : MonoBehaviour
{
    public void PushEffectFunction(float pushAmount, Vector2 direction, GameObject target)
    {
        target.GetComponent<Rigidbody2D>().AddForce(direction * pushAmount * Time.deltaTime);
    }

}
