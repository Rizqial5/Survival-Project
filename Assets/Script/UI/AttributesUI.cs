using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Survival.Stats;
using Survival.Core;


namespace Survival.UI
{
    public class AttributesUI : MonoBehaviour
    {
        [SerializeField] Text healthValue;
        [SerializeField] Text manaValue;
        [SerializeField] Text[] spellEquipped;
        [SerializeField] Attributes character;


        // Update is called once per frame
        void Update()
        {
            if(!character) return;
            
            HealthText();
            manaValue.text = character.mana.ToString();

            for (int i = 0; i < spellEquipped.Length; i++)
            {
                spellEquipped[i].text = character.GetSpellName(i);
            }
            
            
             
            

            
        }

        private string HealthText()
        {
            if(!healthValue) return "0";
            if(character.health <= -1) return healthValue.text = "0";
            return healthValue.text = character.health.ToString();
        }
    }
}

