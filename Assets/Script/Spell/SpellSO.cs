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
        [SerializeField] StatEffect[] statEffects;
        

        Dictionary<Stat, int> lookUpTable;



        public int GetStatEffect(Stat stat)
        {
            BuildLookUpTable();

            return lookUpTable[stat];
        }
        public void BuildLookUpTable()
        {
            if(lookUpTable != null) return;

            lookUpTable = new Dictionary<Stat, int>();

            foreach (StatEffect statEffect in statEffects)
            {
                lookUpTable[statEffect.stat] = statEffect.amount;
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

    [System.Serializable]
    public class StatEffect
    {
        public Stat stat;
        public int amount;
    }
}
