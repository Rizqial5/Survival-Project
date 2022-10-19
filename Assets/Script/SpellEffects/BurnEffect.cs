using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival.Stats;
using Survival.Combat;

namespace Survival.Spell
{
    public class BurnEffect : MonoBehaviour
    {

        

        float burnTime;
        SpellConfig spellConfig;
        TargetCharacter targetCharacter;
    
        
        private void Awake() 
        {
            spellConfig = GetComponent<SpellConfig>();
        }
        
        public void BurnEffectFunction(GameObject attributes)
        {
            
            attributes.GetComponent<Attributes>().IsBurned = true;
            
            
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if( spellConfig.GetTargetCharacter() == TargetCharacter.Enemy  && other.tag == "Enemy")
            {
                BurnEffectFunction(other.gameObject);
            }
            else if(spellConfig.GetTargetCharacter() == TargetCharacter.Player  && other.tag == "Player")
            {
                BurnEffectFunction(other.gameObject);
            }
            
        }
    }
}
