using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survival.Movement;
using Survival.Combat;
using Survival.Spell;

namespace Survival.Controller
{
    public class PlayerController : MonoBehaviour, ISpell
    {

        [SerializeField] float accel = 2f ;
        [SerializeField] float jumpPower = 15f;
        [SerializeField] float dashPower = 5f;
        [SerializeField] float dashAmount = 100f;
        [SerializeField] int jumpCount;
        [SerializeField] Animator animator;
        [SerializeField] float direction = 1;
        [SerializeField] float jumpState;
        [SerializeField] float fallState;
        [SerializeField] HitBoxMechanic hitBoxMechanic;
        

        
        Mover mover;
        ComboMechanic comboMechanic;
        SpellMechanic spellMechanic;
        TargetCharacter targetCharacter;


        


        void Awake()
        {
            mover = GetComponent<Mover>();
            comboMechanic = GetComponent<ComboMechanic>();
            spellMechanic = GetComponent<SpellMechanic>();
            targetCharacter = GetComponent<CombatBehaviour>().GetTargetCharacter();
            // hitBoxMechanic = GetComponent<HitBoxMechanic>();
;            
            
        }


        void Update()
        {
            
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDie") || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDieLeft") ) return;
            // Movement Controller
            MovementMechanic();

            //Attack Controller
            if(Input.GetKeyDown(KeyCode.Mouse0) && !comboMechanic.GetIsAttack())
            {
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) return;
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("Fall")) return;
                if(animator.GetCurrentAnimatorStateInfo(0).IsName("SpellAttack") || animator.GetCurrentAnimatorStateInfo(0).IsName("SpellAttackLeft") ) return;
                comboMechanic.ComboController();
            
                
                
            }

            //Spell Activation Controller
            if(Input.GetKeyDown(KeyCode.Mouse1))//Spell slot 1
            {
                SpellController(0);

            }
            else if(Input.GetKeyDown(KeyCode.Z)) //Spell slot 2
            {
               
                SpellController(1);
            }
            else if(Input.GetKeyDown(KeyCode.X))
            {
                
                SpellController(2);
            }

            

            // Jump Controller
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(comboMechanic.GetIsAttack()) return;
                JumpMechanic();
                

            }

            //Jump and Falling Animation
            if (mover.GetY() > 0)
            {
                if(direction == 1)
                {
                    jumpState = 1;
                }
                if(direction == -1)
                {
                    jumpState = -1;
                }
               
            }
            else if(mover.GetY() < 0 )
            {
                mover.SetIsFall(true);
                animator.SetBool("FallState", mover.GetIsFall());

                if(direction == 1)
                {
                    fallState = 1;
                }
                if(direction == -1)
                {
                    fallState = -1;
                }
               
                
            }
            else if(mover.GetY() == 0 )
            {
                mover.SetIsFall(false);
                mover.SetJumpState(false);
                jumpState = 0;
                fallState = 0;
                jumpCount = 0;
                animator.SetBool("FallState", mover.GetIsFall());
            }
            
            
           
            animator.SetFloat("LastFace", direction);
            animator.SetBool("JumpState", mover.GetJumpState());
            animator.SetFloat("Jump", jumpState);
            animator.SetFloat("Fall", fallState);

            

            
            

            //For Debug Only
            // print(comboCount);
            // print(isAttack);
            // print(fallState);
            // print(mover.GetJumpState());
            // print(DirectionSpells());
            
            



        }

        private void SpellController(int spellIndex)
        {
            if (!spellMechanic.isEquipped()) return;
            if (spellMechanic.SpellReserves == 1) return;
            if (spellMechanic.SpellCooldown < spellMechanic.SpellTime) return;
            

            spellMechanic.SpellIndex = spellIndex;

            if(!spellMechanic.isThereAnySpell()) return;

            spellMechanic.SpellCooldown = 0;
            
            if (direction == 1)
            {
                // spellMechanic.Spawn();
                animator.Play("SpellAttack");

            }
            else if (direction == -1)
            {
                // spellMechanic.Spawn();
                animator.Play("SpellAttackLeft");
            }
        }

        private void JumpMechanic()
        {
            jumpCount += 1;
            
            if (jumpCount <= 2 )
            {
                mover.SetJumpState(true);
                
                mover.JumpMovement(jumpPower);
                
                
                
            }
            
        }

        private void MovementMechanic()
        {
            // if(isAttack) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1 ")) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2 ")) return;
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("SpellAttack") || animator.GetCurrentAnimatorStateInfo(0).IsName("SpellAttackLeft") ) return;
            if (Input.GetKey(KeyCode.D))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash") || animator.GetCurrentAnimatorStateInfo(0).IsName("DashLeft")) return;
                direction = 1;
                mover.Movement(Vector3.right, accel);

                animator.SetFloat("RunRight", direction);



            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash") || animator.GetCurrentAnimatorStateInfo(0).IsName("DashLeft")) return;
                direction = -1;
                mover.Movement(Vector3.left, accel);

                animator.SetFloat("RunRight", direction);

            }
            else
            {
                animator.SetFloat("RunRight", 0);

            }

            DashMechanic("Player_Movement");

        }

        private void DashMechanic(string animName)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
            {
                print("true");
                if (direction == 1)
                {
                    animator.Play("Dash");
                    mover.DashMovement(Vector2.right, dashPower);
                }
                else if (direction == -1)
                {
                    animator.Play("DashLeft");
                    mover.DashMovement(Vector2.left, dashPower);
                }

            }
        }



        private void OnCollisionEnter2D(Collision2D other) {
            if(other.collider.tag == "Enemy")
            {

                mover.SetJumpState(false);
                animator.SetBool("FallState", mover.GetIsFall());
            }

            if(other.collider.tag == "Detection Ground")
            {
                
                
                mover.SetJumpState(false);
                animator.SetBool("FallState", mover.GetIsFall());
                
            }
        }

        public float GetDirection()
        {
            return direction;
        }

        public TargetCharacter GetTargetCharacter()
        {
            return targetCharacter;
        }

        public void DieAnimation()
        {
            if(direction == 1)
            {
                animator.Play("PlayerDie");
            }
            else if(direction == -1)
            {
                animator.Play("PlayerDieLeft");
            }
            
        }
    }
}
