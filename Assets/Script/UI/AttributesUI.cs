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
        [SerializeField] Text spellEquipped;
        [SerializeField] Attributes characterHealth;


        // Update is called once per frame
        void Update()
        {
            
            healthValue.text = characterHealth.health.ToString();
            spellEquipped.text = characterHealth.GetSpellName();
             
            

            
        }
    }
}

