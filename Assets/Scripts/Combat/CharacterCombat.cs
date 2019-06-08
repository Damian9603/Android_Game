using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    protected const float combatCooldown = 5f;
    public bool InCombat { get; protected set; }
    protected float lastAttackTime;
    protected CharacterStats myStats;
    protected CharacterStats oponentStats;

    public float attackSpeed = 1f;
    protected float attackCooldown = 0f;
    public float attackDelay = .6f;


    public event System.Action OnAttack;
    

    protected virtual void Start()
    {
        //stats
        myStats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {
        attackCooldown -= Time.deltaTime;
        if(attackCooldown<0)
        {
            attackCooldown = 0;
        }
        if(Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }
    
    public virtual void Attack(CharacterStats targetStats)
    {
        if(attackCooldown <= 0f)
        {
            bool critFlag = false;
            oponentStats = targetStats;
            string name = transform.name;

            #region hitLandCalculation
            int hitModifier = myStats.agility.GetValue();
            int dodgeModifier = targetStats.agility.GetValue();
            hitModifier = modifierCalculate(hitModifier);
            dodgeModifier = modifierCalculate(dodgeModifier);
            int hitRoll = Random.Range(1, 20) + hitModifier - dodgeModifier;
            if(hitRoll <8)
            {
                Debug.Log(name + " misssed");
                attackCooldown = 1f / attackSpeed;
                InCombat = true;
                lastAttackTime = Time.time;
                return;
            }
            else if (hitRoll>18)
            {
                critFlag = true;
            }
            #endregion

            #region damageCalculation

            int damageModifier = myStats.strenght.GetValue();
            damageModifier = modifierCalculate(damageModifier);
            int defenceModifier = targetStats.condition.GetValue();
            int enemyArmor = targetStats.armour.GetValue();
            defenceModifier = modifierCalculate(defenceModifier);

            int maxDamage = myStats.maxDamage.GetValue();
            int minDamage = myStats.minDamage.GetValue();
            int damageRoll = Random.Range(minDamage, maxDamage) + damageModifier-defenceModifier-enemyArmor;
            if(damageRoll<0)
            {
                damageRoll = 0;
            }
            if(critFlag==true)
            {
                Debug.Log(name + ": Critical strike!");
                damageRoll += Random.Range(1, 20);
            }
            #endregion
            //targetStats.TakeDamage(damage);
            //Debug.Log(name + " dealing "+damage+" damage");
            if (transform.tag=="Player" || transform.tag=="Enemy")
            {
                StartCoroutine(DoDamage(attackDelay, damageRoll));
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

    protected IEnumerator DoDamage(float delay, int damage)
    {
        yield return new WaitForSeconds(delay);
        oponentStats.TakeDamage(damage);
        if (oponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        oponentStats.TakeDamage(myStats.maxDamage.GetValue());
        if (oponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }

    protected int modifierCalculate(int stat)
    {
        int modifier = (stat - 10) / 2;
        return modifier;
    }
}
