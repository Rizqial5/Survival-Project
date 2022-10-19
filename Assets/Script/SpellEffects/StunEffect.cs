using System.Collections;
using System.Collections.Generic;
using Survival.Movement;
using UnityEngine;

public class StunEffect : MonoBehaviour
{
    public void StunEffectFunction(float stunTime, float stunTimeLimit, GameObject target)
    {
        if(stunTime < stunTimeLimit)
        {
            target.GetComponent<Mover>().enabled = false;
        }
        


    }
}
