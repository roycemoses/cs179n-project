using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackDirection
{
    Left,
    Right,
    Up,
    Down
}

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public float attackRate = 10f;
    float nextAttackTime = 0f;

    public AttackDirection attackDirection;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                attackDirection = AttackDirection.Left;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                attackDirection = AttackDirection.Right;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                attackDirection = AttackDirection.Up;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                attackDirection = AttackDirection.Down;
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;                
            }
        }
    }

    void Attack()
    {
        // Play an attack animation based on direction
        if (attackDirection == AttackDirection.Left)
            animator.SetTrigger("LeftAttack");
        else if (attackDirection == AttackDirection.Right)
            animator.SetTrigger("RightAttack");
        else if (attackDirection == AttackDirection.Up)
            animator.SetTrigger("UpAttack");
        else if (attackDirection == AttackDirection.Down)
            animator.SetTrigger("DownAttack");

        // Detect enemies in range
        

        // Apply damage
    }
}
