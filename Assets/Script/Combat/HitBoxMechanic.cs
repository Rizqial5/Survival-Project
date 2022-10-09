using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Survival.Combat
{
    public class HitBoxMechanic : MonoBehaviour
    {
        [SerializeField] Collider2D hitBox;
        
        
        [SerializeField] TargetCharacter target;
        
        public UnityEvent OnHit;
        private bool isHit;
        private string targetString;
        private void Awake() 
        {
            hitBox = GetComponent<Collider2D>();
            
            
            
        }

        private void Start() 
        {
            targetString = target.ToString();
        }

        private void Update() {
            // print(IsHit);
            // print(targetString);
        }

        public void SetColliderON()
        {
            hitBox.enabled = true;
        }

        public void SetColliderOff()
        {
            hitBox.enabled = false;
            
        }

        public bool GetIsEnabled()
        {
            return hitBox.isActiveAndEnabled;
        }

        public bool IsHit
        {
            get { return isHit; }
            set {isHit = value; }
        }



        private void OnTriggerEnter2D(Collider2D other) {
            if(other.tag == targetString)
            {
                isHit = true;
                // print(isHit);
                OnHit.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(other.tag == targetString)
            {
                isHit = false;
            }
        }

        
    }

    
}
