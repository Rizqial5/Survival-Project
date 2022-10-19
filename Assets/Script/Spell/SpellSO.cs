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
        [SerializeField] int damageSpell;
        [SerializeField] int manaConsumed;
        [SerializeField] float spellSpeed;
        [SerializeField] SpellType spellType;
        [SerializeField] SpellEffect[] spellEffects;
        

        Dictionary<Stat, int> lookUpStatTable;
        Dictionary<BehaveEnum,int> lookUpBehaveTable;

        BehaviourEffect behaviourEffect;

        public int GetStatEffect(Stat stat)
        {
            BuildLookUpStatTable();

            return lookUpStatTable[stat];
        }

        public int GetBehaveEffect(BehaveEnum behaviour)
        {
            BuildLookUpBehaveTable();
            return lookUpBehaveTable[behaviour];
        }

        public BehaveEnum GetBehaveEnum()
        {
            BuildLookUpBehaveTable();

            foreach (var item in lookUpBehaveTable)
            {
                return item.Key;
            }

            
            return behaviourEffect.bodyEffect;
            
        }

        
        public void BuildLookUpStatTable()
        {
            if(lookUpStatTable != null) return;

            lookUpStatTable = new Dictionary<Stat, int>();

            foreach (SpellEffect spellEffect in spellEffects)
            {
                foreach (StatEffect statEffect in spellEffect.statEffects)
                {
                    lookUpStatTable[statEffect.stat] = statEffect.amount;
                }
            }
        }

        public void BuildLookUpBehaveTable()
        {
            if(lookUpBehaveTable != null) return;

            lookUpBehaveTable = new Dictionary<BehaveEnum, int>();

            foreach (SpellEffect spellEffect in spellEffects)
            {
                foreach (BehaviourEffect behaveEffect in spellEffect.behaviourEffects)
                {
                    lookUpBehaveTable[behaveEffect.bodyEffect] = behaveEffect.amount;
                }
            }
        }
        public int GetDamageSpell()
        {
            return damageSpell;
        }
        public int GetManaConsumed()
        {
            return manaConsumed;
        }

        public float GetSpellSpeed()
        {
            return spellSpeed;
        }

        public SpellType GetSpellType()
        {
            return spellType;
        }


        public SpellConfig GetSpellPrefab()
        {
            return spell;
        }

        
    }

    
}
