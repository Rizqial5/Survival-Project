using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Survival.Spell;


namespace Survival.Combat
{
    public class CombatBehaviour : MonoBehaviour
    {

        public UnityEvent OnHit;
        public UnityEvent spellHit;
        SpriteRenderer spriteRenderer;


        
        
        [SerializeField] TargetCharacter targetCharacter;

        GameObject confrontHitBox;

        private void Awake() 
        {
           spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            GetColliderOther();

        }


        private void Update() 
        {
            GetColliderOther();
            
        }

        private void GetColliderOther()
        {
            if (targetCharacter == TargetCharacter.Enemy)
            {
                confrontHitBox = GameObject.FindGameObjectWithTag("EnemyHitBox");
            }
            else if (targetCharacter == TargetCharacter.Player)
            {
                confrontHitBox = GameObject.FindGameObjectWithTag("PlayerHitBox");
            }
        }

        public IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(.5f);
            spriteRenderer.color = Color.white;
        }

        public TargetCharacter GetTargetCharacter()
        {
            return targetCharacter;
        }

        private void OnTriggerEnter2D(Collider2D other) {

            if(!confrontHitBox) return;

            if(other.tag == confrontHitBox.tag)
            {
            
                // print("Kena hit");
                spriteRenderer.color = Color.red;
                OnHit.Invoke();
                StartCoroutine("ChangeSprite");
                

            }

            if(other.tag == "Spell" && other.GetComponent<SpellConfig>().GetTargetCharacter().ToString() == tag)
            {
                
                print(other.GetComponent<SpellConfig>().GetTargetCharacter().ToString() == tag);
                print($"Kena hit {this.tag}");
                spriteRenderer.color = Color.blue;
                spellHit.Invoke();
                StartCoroutine("ChangeSprite");
                

                
                
            }
        }

        

        

        
    }
}
