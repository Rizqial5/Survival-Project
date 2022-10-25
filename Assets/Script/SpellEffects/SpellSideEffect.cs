using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival.Stats;
using Survival.Combat;

namespace Survival.Spell
{
    public class SpellSideEffect : MonoBehaviour
    {

        

        float burnTime;
        
        SpellConfig spellConfig;
        [SerializeField] BehaveEnum behaveEnum;

        float direction;
        int pushAmount;
        
        public float Direction
        {
            set{direction = value;} get{return direction;}
        }
        public int PushAmount
        {
            set{pushAmount = value;} get{return pushAmount;}
        }
       
    
        
        private void Awake() 
        {
            spellConfig = GetComponent<SpellConfig>();
        }

        private Vector2 TranslateDirection(float direction)
        {
            if(direction == 1)
            {
                return Vector2.right;
            }
            else if(direction == -1)
            {
                return Vector2.left;
            }

            return Vector2.zero;
        }
        
        public void SpellEffectFunction(GameObject attributes, BehaveEnum behaveEnum)
        {
            if(behaveEnum == BehaveEnum.burnEffect)
            {
                attributes.GetComponent<Attributes>().IsBurned = true;

            } else if(behaveEnum == BehaveEnum.stunEffect)
            {

                attributes.GetComponent<Attributes>().IsStunned = true;

            } else if(behaveEnum == BehaveEnum.pushEffect)
            {
                attributes.GetComponent<Attributes>().IsPushed = true;
                attributes.GetComponent<Rigidbody2D>().AddRelativeForce(TranslateDirection(direction)*pushAmount);
            }
        
            
            
            
        }

        

        private void OnTriggerEnter2D(Collider2D other) {
            if( spellConfig.GetTargetCharacter() == TargetCharacter.Enemy  && other.tag == "Enemy")
            {
                SpellEffectFunction(other.gameObject, behaveEnum);
            }
            else if(spellConfig.GetTargetCharacter() == TargetCharacter.Player  && other.tag == "Player")
            {
                SpellEffectFunction(other.gameObject, behaveEnum);
            }
            
        }
    }
}
