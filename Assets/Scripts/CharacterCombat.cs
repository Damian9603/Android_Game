using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    const float combatCooldown = 5f;
    public bool InCombat { get; private set; }
    float lastAttackTime;
    CharacterStats myStats;
    CharacterStats oponentStats;

    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;
    public float fireballCooldown = 0f;
    public float fireballDelay = 10f;
    public float fireballRange = 10f;
    public PlayerController controller;

    public event System.Action OnAttack;
    public event System.Action OnFireball;

    void Start()
    {
        //stats
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if(attackCooldown<0)
        {
            attackCooldown = 0;
        }
        fireballCooldown -= Time.deltaTime;
        if(fireballCooldown<0)
        {
            fireballCooldown = 0;
        }
        if(Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }
    public void PlayerAttack()
    {
        Interactable focus = controller.focus;
        if (focus != null && focus.tag == "Enemy")
        {
            float distance = Vector3.Distance(focus.transform.position, PlayerManager.instance.player.transform.position);
            if(distance>3)
            {
                Debug.Log("Out of range");
            }
            else
            {
                CharacterStats focusStats = focus.GetComponent<CharacterStats>();
                Attack(focusStats);
            }
            
        }
        else
        {
            Debug.Log("Noone to attack");
        }
    }
    public void PlayerFireball()
    {
        Interactable focus = controller.focus;
        if (focus != null && focus.tag == "Enemy")
        {
            CharacterStats focusStats = focus.GetComponent<CharacterStats>();
            CastFireball(focusStats);
            
        }
        else
        {
            Debug.Log("Noone to attack");
        }
    }
    public void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0f)
        {
            if(controller != null)
            {
                if(controller.focus==null)
                {
                    return;
                }
            }
            oponentStats = targetStats;
            string name = transform.name;
            int aim = Random.Range(1, 20);
            int damage = myStats.damage.GetValue();
            //targetStats.TakeDamage(damage);
            //Debug.Log(name + " dealing "+damage+" damage");
            if(transform.tag=="Player")
            {
                StartCoroutine(DoDamage(attackDelay));
            }
            if(OnAttack !=null)
            {
                OnAttack();
            }
            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
        
    }

    public void CastFireball(CharacterStats targetStats)
    {
        

        if(fireballCooldown<=0)
        {
            if (controller != null)
            {
                if (controller.focus == null)
                {
                    Debug.Log("No target to cast fireball");
                    return;
                }
                Vector3 position = transform.position;
                Vector3 enemyPosition = controller.focus.transform.position;
                float distance = Vector3.Distance(position, enemyPosition);
                if (distance> fireballRange)
                {
                    Debug.Log("Target too far away");
                    controller.Follow(fireballRange-3f);
                    return;
                }
            }
            oponentStats = targetStats;
            string name = transform.name;
            int damage = Random.Range(1, 20);
            damage += myStats.damage.GetValue();
            //targetStats.TakeDamage(damage);
            //Debug.Log(name + " Casting fireball, doing " + damage + " damage");
            StartCoroutine( DoDamage(1f, damage));
            if (OnFireball != null)
            {
                OnFireball();
            }
            fireballCooldown = fireballDelay;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }

    IEnumerator DoDamage(float delay, int damageBuff = 0)
    {
        yield return new WaitForSeconds(delay);
        oponentStats.TakeDamage(myStats.damage.GetValue() + damageBuff);
        if (oponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        oponentStats.TakeDamage(myStats.damage.GetValue());
        if (oponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
