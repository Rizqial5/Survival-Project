using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Survival.Stats;
using Survival.Combat;

namespace Survival.Spell
{
    public class SpellMechanic : MonoBehaviour
    {
        
        [SerializeField] GameObject spawnPosition;

        [SerializeField] float spellCooldown = 4f;
        [SerializeField] float spellTime = 3f;
        [SerializeField] TargetCharacter targetCharacter;
        

        private GameObject enemySpell;
        private SpellSO spellObject;
        private Animator spellAnimation;
        private Attributes attributes;
        private ISpell spellInterface;
        private int spellIndex;
        

        
         
        private void Awake() 
        {
            attributes = GetComponent<Attributes>();
            
        }
        private void Start() 
        {
            // spellConfig = GetComponent<SpellConfig>();
            // spellAnimation = GetComponent<Animator>();
            spellInterface = GetComponent<ISpell>();
            

        }

        private void Update() 
        {
    
            spellCooldown += Time.deltaTime;
            if(targetCharacter == TargetCharacter.Enemy)
            {
                
                spellObject = attributes.GetSpell(spellIndex);
            }
            else if(targetCharacter == TargetCharacter.Player)
            {
                spellObject = attributes.GetSpell();
            }
            

            
        }

        public bool isThereAnySpell()
        {
            if(!attributes.GetSpell(spellIndex)) return false;
            return true;
            
        }
        
        public void Spawn()
        {
            attributes.mana -= spellObject.GetManaConsumed();
            if(attributes.mana > 0 )
            {
                if(spellObject.GetSpellType() == SpellType.Projectile)
                {
                    SpawnMechanic(spawnPosition.transform.position);
                }
                else if(spellObject.GetSpellType() == SpellType.Buff)
                {
                    SpawnMechanic(transform.position);
                }
    
                
            }

        
            
            
        }
        

        public void SpawnMechanic(Vector3 spawnPosition)
        {
            if(!spellObject) return;
            
            var newSpell = Instantiate(spellObject.GetSpellPrefab(),spawnPosition,transform.rotation);

            
            newSpell.GetComponent<SpellConfig>().SetTargetCharacter(targetCharacter);
            
            float direction = GetDirection();
            SpellType spellType = spellObject.GetSpellType();

            
            if(!newSpell) return ;

            newSpell.GetComponent<SpellConfig>().SpellType = spellType;
            newSpell.DamageSpell = spellObject.GetDamageSpell();
            
            if(direction == 1)
            {
                if(spellType == SpellType.Projectile)
                {
                    newSpell.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right*spellObject.GetSpellSpeed());
                    newSpell.gameObject.GetComponent<Animator>().Play("LaunchRight");
                }
                else if(spellType == SpellType.Burst)
                {
                    //Mechanic Burst
                }
                else if(spellType == SpellType.Buff)
                {
                    newSpell.gameObject.GetComponent<Animator>().Play("BuffActivated");
                    BuffHealthEffect(spellObject.GetStatEffect(Stat.HealthPoint)); //Perlu revisi (TAG)
                }
                
                
                
            }
            else if(direction == -1)
            { 
                if(spellType == SpellType.Projectile)
                {
                    newSpell.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left*spellObject.GetSpellSpeed());
                    newSpell.gameObject.GetComponent<Animator>().Play("LaunchLeft");
                }
                else if(spellType == SpellType.Burst)
                {
                    //Mechanic Burst
                }
                else if(spellType == SpellType.Buff)
                {
                    newSpell.gameObject.GetComponent<Animator>().Play("BuffActivated");
                    BuffHealthEffect(spellObject.GetStatEffect(Stat.HealthPoint));
                }
                
                
            }

            
        }

        private int BuffHealthEffect(int amount)
        {
           return attributes.health += amount;
        }

        
        public void AddSpellReserves()
        {
            if(attributes.mana >= 4) return;
            attributes.mana += 1;
        }

        

        public int SpellReserves
        {
            get{return attributes.mana;} set{attributes.mana = value;}
        }

        public float SpellCooldown
        {
            get{return spellCooldown;} set{spellCooldown = value;}
        }

        public int SpellIndex
        {
            get{return spellIndex;} set{spellIndex = value;}
        }

        public float SpellTime
        {
            get{return spellTime;}
        }

        public bool isEquipped()
        {
            if(!spellObject) return false;
            if(spellObject) return true;

            return false;
        }
        

        public void PlayAnim(string nameAnim)
        {
            spellAnimation.Play(nameAnim);
            
        }

        public float GetDirection()
        {
            return spellInterface.GetDirection();
        }

        public TargetCharacter GetTargetCharacter()
        {
            return spellInterface.GetTargetCharacter();
        }
    }

}