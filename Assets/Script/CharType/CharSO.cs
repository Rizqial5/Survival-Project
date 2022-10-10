using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival.Spell;



namespace Survival.CharType
{
    [CreateAssetMenu(fileName = "CharSO", menuName = "Survival Project/CharSO", order = 0)]
    public class CharSO : ScriptableObject 
    {
        [SerializeField] EnemyStats[] enemyStats;

        Dictionary<CharCategories,Dictionary<Stat,int>> lookUpTable;
        Dictionary<CharCategories,SpellSO> lookUpTableSpellSO;


        public float GetStat(Stat stat, CharCategories CharCategories)
        {
            BuildLookUpTable();

            return lookUpTable[CharCategories][stat];
        }

        public SpellSO GetSpellSO(CharCategories CharCategories)
        {
            BuildLookUpTableSpell();

            return lookUpTableSpellSO[CharCategories];
        }

        public void BuildLookUpTableSpell()
        {
            if(lookUpTableSpellSO != null) return;

            lookUpTableSpellSO = new Dictionary<CharCategories, SpellSO>();

            foreach (EnemyStats enemyBaseStats in enemyStats)
            {
                lookUpTableSpellSO[enemyBaseStats.CharCategories] = enemyBaseStats.spellEquip;
            }
           
        }
        public void BuildLookUpTable()
        {
            if(lookUpTable != null) return;

            lookUpTable = new  Dictionary<CharCategories,Dictionary<Stat,int>>();

            foreach (EnemyStats enemyBaseStats in enemyStats)
            {
               var baseLookUpTable = new Dictionary<Stat,int>();

               foreach (EnemyBase enemyBase in enemyBaseStats.enemyBase)
               {
                    baseLookUpTable[enemyBase.stat] = enemyBase.amount;
               }

               lookUpTable[enemyBaseStats.CharCategories] = baseLookUpTable;

            }
        }
    }

    [System.Serializable]
    public class EnemyStats
    {
        public CharCategories CharCategories;
        public EnemyBase[] enemyBase;
        public SpellSO spellEquip;
        
        ///Equipped Spell

    }

    [System.Serializable]
    public class EnemyBase
    {
        public Stat stat;
        public int amount;
    }

}
