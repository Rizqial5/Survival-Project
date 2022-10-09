using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival.Combat;



namespace Survival.Spell
{
    public class SpellConfig : MonoBehaviour
    {
        [SerializeField] float speedLaunch;
        [SerializeField] TargetCharacter targetCharacter = TargetCharacter.Null;
       
       
        
        
        Animator animator;
         

        private void Start() 
        {
            animator = GetComponent<Animator>();
            
            
            
        }

        private void Update()
        {
            
            StartCoroutine("DestroyObject");
            
            
        }
        private void OnEnable() {
            targetCharacter = GetTargetCharacter();
        }

        private float DirectionSpell()
        {
            
            foreach (ISpell spellInterface in GetComponents<ISpell>())
            {
                print("bisa");
                return spellInterface.GetDirection();
            }

            return 0 ;
        }

        

        private IEnumerator DestroyObject()
        {
            
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }

        public void DestoryGameobject()
        {
            Destroy(gameObject);
            
        }

        public TargetCharacter SetTargetCharacter(TargetCharacter value)
        {
            return targetCharacter = value;
        }

        public TargetCharacter GetTargetCharacter()
        {
            return targetCharacter;
        }
    
        

        private void OnTriggerEnter2D(Collider2D other) {

            
            if(targetCharacter == TargetCharacter.Enemy && other.tag == "Enemy" )
            {
                // print("Kena Enemy");
                this.GetComponent<Rigidbody2D>().Sleep();
                this.GetComponent<Collider2D>().enabled = false;
                animator.Play("HitAnimation");
                //SpellCount -1
            }
            else if(targetCharacter == TargetCharacter.Player && other.tag == "Player")
            {
                // print("Kena Player");
                
            }
            
        } 

        

        
    }
}
