using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Survival.Movement;
using Survival.Stats;
using Survival.Spell;
using System;
using Survival.Combat;

public class EnemyController : MonoBehaviour, ISpell
{


    [SerializeField] Transform waypoint;
    [SerializeField] float accel = 2f;

    [SerializeField] Collider2D attackRange;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] int direction;
    [SerializeField] float distanceRequirement = 5;
  

    SpriteRenderer spriteRenderer;
    Mover mover;
    Animator animator;
    SpellMechanic spellMechanic;
    TargetCharacter targetCharacter;
    



    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mover = GetComponent<Mover>();
        animator = GetComponent<Animator>();
        spellMechanic = GetComponent<SpellMechanic>();
        targetCharacter = GetComponent<CombatBehaviour>().GetTargetCharacter();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!GameObject.FindGameObjectWithTag("Player")) return;
        waypoint = GameObject.FindGameObjectWithTag("Player").transform;
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        spellMechanic.SpellCooldown = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!GameObject.FindGameObjectWithTag("Player"))
        {
            animator.SetFloat("direction",0);
            animator.Play("Idle");
            return;
        }
          
        
        ShootSpell();

        if (attackRange.IsTouching(playerCollider))
        {
            Attack();
            EnemyDirection();
        }
        else if (!attackRange.IsTouching(playerCollider))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackLeft") || animator.GetCurrentAnimatorStateInfo(0).IsName("SpellAttackLeft")) return;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackRight") || animator.GetCurrentAnimatorStateInfo(0).IsName("SpellAttackRight")) return;
            
            accel = 2;
        }

        

        EnemyDirection();
        animator.SetFloat("direction", direction);
        // print(targetCharacter);

        transform.position = Vector3.MoveTowards(transform.position, waypoint.position, accel * Time.deltaTime);

    }

    private void ShootSpell()
    {
        if (Distance() > distanceRequirement || Distance() < -distanceRequirement && DistanceYRequire())
        {
            if (!spellMechanic.isEquipped()) return;
            // if(spellMechanic.SpellReserves == 1) return; 
            if (spellMechanic.SpellCooldown < spellMechanic.SpellTime) return;

            spellMechanic.SpellCooldown = 0;

            if (direction == 1)
            {
                // spellMechanic.Spawn();
                animator.Play("SpellAttackRight");

            }
            else if (direction == -1)
            {
                // spellMechanic.Spawn();
                animator.Play("SpellAttackLeft");
            }
        }
    }

    private bool DistanceYRequire()
    {
        return DistanceY() <= 0.5 && DistanceY() >= 0;
    }

    private float DistanceY()
    {
        return waypoint.transform.position.y - this.transform.position.y ;
    }

    private float Distance()
    {
        return waypoint.position.x - this.transform.position.x;
    }

    private void Attack()
    {
        if(direction == 1)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("AttackLeft")) return;   
            animator.Play("AttackRight");
        }
        else if(direction == -1)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("AttackRight")) return;  
            animator.Play("AttackLeft");
        }
        accel = 0;
    }

    private void EnemyDirection()
    {
        // if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) return;  
        if(playerCollider.transform.position.x > transform.position.x)
        {
            
            direction = 1;
        }
        else if(playerCollider.transform.position.x < transform.position.x)
        {
            
            direction = -1;
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
}
