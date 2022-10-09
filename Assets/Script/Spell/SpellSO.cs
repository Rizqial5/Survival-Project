using System.Collections;
using System.Collections.Generic;
using Survival.Combat;
using UnityEngine;



namespace Survival.Spell
{
    [CreateAssetMenu(fileName = "SpellSO", menuName = "Survival Project/SpellSO", order = 0)]
    public class SpellSO : ScriptableObject 
    {
        [SerializeField] SpellConfig spell;
        [SerializeField] float damageSpell;
        [SerializeField] float manaConsumed;
        

        public float GetDamageSpell()
        {
            return damageSpell;
        }
        public float GetManaConsumed()
        {
            return manaConsumed;
        }


        public SpellConfig GetSpellPrefab()
        {
            return spell;
        }

        
    }
}
