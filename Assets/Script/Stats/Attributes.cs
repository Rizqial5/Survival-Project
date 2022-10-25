using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Survival.Core;
using Survival.Spell;
using Survival.CharType;
using System;

namespace Survival.Stats
{
    public class Attributes : MonoBehaviour
    {
       
        // [SerializeField] int healthPoints = 2;
        // [SerializeField] SpellSO spellEquip;
        // [SerializeField] int manaPoints = 10;
        [SerializeField] CharSO charSO;
        [SerializeField] CharCategories CharCategories;
        [SerializeField] SpellSO[] spellInventory;

        private int healthPoints;
        private int manaPoints;
        private int burnedHitCount;
        private float startBurned;
        [SerializeField] float burnTimeLimit = 2f;
        [SerializeField] float stunTimeLimit= 2f;
        [SerializeField] float pushTimeLimit = 1f;
        private float burnedStateTime;
        SpellSO spellEquip;



      

        public UnityEvent OnDie;
        public UnityEvent OnBurned;
        public UnityEvent OnStunned;
        public UnityEvent AfterStunned;
        private GameObject enemySpell;
        private GameManager gameManager;
        private Rigidbody2D rb;
        private bool isBurned;
        private bool isStunned;
        private bool isPushed;
        private float startStunned;
        private float startPushed;
        
        

        private void Awake() {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            healthPoints = (int) charSO.GetStat(Stat.HealthPoint, CharCategories);
            manaPoints = (int) charSO.GetStat(Stat.ManaPoint, CharCategories);
            spellEquip = charSO.GetSpellSO(CharCategories);
            rb = GetComponent<Rigidbody2D>();

        }
        private void Start() {
            enemySpell = GameObject.FindGameObjectWithTag("Enemy");
            if(!spellInventory[0])
            {
                spellInventory[0] = spellEquip;
            }
            
            print(gameObject.tag + spellInventory[0].GetBehaveEnum());
        }

        private void Update() {
            CharDie();

            if(isBurned)
            {
                BurnedState();
                startBurned += Time.deltaTime;
            }else if(isStunned)
            {
                
                StunnedState();
                startStunned += Time.deltaTime;
            } else if(isPushed)
            {
                PushedState();
                startPushed += Time.deltaTime;
            }


           
            

            
            
        }
        public int health
        {
            get{return healthPoints;} set{healthPoints = value;}
        }

        public int mana
        {
            get{return manaPoints;} set{manaPoints = value;}
        }

        public bool IsBurned
        {
            get{return isBurned;} set{isBurned = value;}
        }

        public bool IsStunned
        {
            get{return isStunned;} set{isStunned = value;}
        }
        public bool IsPushed
        {
            get{return isPushed;} set{isPushed = value;}
        }




        public void DecreaseHealth()
        {
            healthPoints -= 1;
            
        }

        

        

        private void CharDie()
        {
            if (healthPoints <= 0)
            {
                //Menjalankan Animasi Die 
                //Menggunakan Animation Event untuk memanggil Die() function
                OnDie.Invoke();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
            if(gameObject.tag == "Enemy")
            {
                gameManager.enemyDead += 1;
            }
            
        }

        private void BurnedState()
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            
            if(startBurned >  burnTimeLimit)
            {
                OnBurned.Invoke();
                startBurned = 0;
                CheckBurnedState();
            }
            // startBurned = 0;
            
        }

        private void CheckBurnedState()
        {
            burnedHitCount += 1;
            print(burnedHitCount);
            if(burnedHitCount ==2 )
            {
                isBurned = false;
                this.GetComponent<SpriteRenderer>().color = Color.white;
                burnedHitCount = 0;
            }
        }

        private void StunnedState()
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
            

            if(startStunned < stunTimeLimit)
            {
                OnStunned.Invoke();
            }else 
            {
                AfterStunned.Invoke();
                this.GetComponent<SpriteRenderer>().color = Color.white;
                isStunned = false;
                startStunned = 0;
            }
        }

        private void PushedState()
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
            
            
            if(startPushed < pushTimeLimit)
            {
                
            }else
            {
                this.GetComponent<SpriteRenderer>().color = Color.white;
                isPushed = false;
                startPushed = 0;
            }
        }

        public SpellSO GetSpell()
        {
            return spellEquip;
        }

        public SpellSO GetSpell(int spellIndex)
        {
            if(!spellInventory[spellIndex]) return null;
            return spellInventory[spellIndex];
        }

        

        public SpellSO SetSpell(SpellSO value)
        {
           int spellAmount = spellInventory.Length;
           for (int i = 0; i < spellAmount; i++)
           {
                if(!spellInventory[i]) return spellInventory[i] = value;
           }

            return null;
        }

        public string GetSpellName(int index)
        {
            if(!spellInventory[index])
            {
                return "none";
            }
            return spellInventory[index].name;

        }
        
        



    }
}
