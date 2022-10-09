using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Survival.Movement
{
    public class Mover : MonoBehaviour
    {
        Rigidbody2D rb;
        [SerializeField] bool jumpState;
        [SerializeField] bool isFall;
        

        private void Start() 
        {
            rb = GetComponent<Rigidbody2D>();    
        }
        public void Movement(Vector3 dir, float accel)
        {
            transform.Translate(dir * accel * Time.deltaTime);
        }

        public void JumpMovement(float jumpPower)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        public void DashMovement(Vector2 direction, float dashPower)
        {
            rb.AddForce(direction * dashPower, ForceMode2D.Impulse);
        }

        public bool GetJumpState()
        {
            return jumpState;
        }

        public void SetJumpState(bool newState)
        {
            jumpState = newState;
        }

        public bool GetIsFall()
        {
            return isFall;
        }

        public void SetIsFall(bool newState)
        {
            isFall = newState;
        }

        public float GetY()
        {
            return rb.velocity.y ;
        }

        






    }
}
