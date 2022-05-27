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

    public float angle;

    public string[] weapons = {"Fists", "Sword", "Projectile"};
    public string currentWeapon;
    public int currentWeaponIndex = 0;

    public AudioSource projectileSound;
    public AudioSource swordSound;

    public GameObject playerFirepoint;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.C)) // Change weapons!
        {
            currentWeaponIndex++;
            if (currentWeaponIndex == weapons.Length) // Cycle through the weapons array! Go back to 0 index
                currentWeaponIndex = 0;
            currentWeapon = weapons[currentWeaponIndex];
        }*/

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 1;
            currentWeapon = weapons[currentWeaponIndex];
            playerFirepoint.GetComponent<SpriteRenderer>().enabled = false;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 2;
            currentWeapon = weapons[currentWeaponIndex];
            playerFirepoint.GetComponent<SpriteRenderer>().enabled = true;
        }
        
        angle = PlayerShootCam.angle;
        // if (Input.GetButtonDown("Fire1"))
        //     Debug.Log("angle: " + angle);

        if (Time.time >= nextAttackTime)
        {
            if (currentWeapon == "Sword")
            {
                if ( Input.GetButtonDown("Fire1") && ((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180)) ) // LEFT **
                {
                    attackDirection = AttackDirection.Left;
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    animator.SetFloat("Xinput", -1.0f);
                    animator.SetFloat("Yinput", 0f);
                    Debug.Log("LEFT");
                    swordSound.Play();
                }
                else if ( Input.GetButtonDown("Fire1") && ((angle <= 45 && angle >= 0) || (angle >= -45 && angle <= 0)) ) // RIGHT
                {
                    attackDirection = AttackDirection.Right;
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    animator.SetFloat("Xinput", 1.0f);
                    animator.SetFloat("Yinput", 0f);
                    Debug.Log("RIGHT");
                    swordSound.Play();
                }
                else if (Input.GetButtonDown("Fire1") && angle <= 135 && angle >= 45) // UP
                {
                    attackDirection = AttackDirection.Up;
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    animator.SetFloat("Xinput", 0f);
                    animator.SetFloat("Yinput", 1f);
                    Debug.Log("UP");       
                    swordSound.Play();  
                }
                else if (Input.GetButtonDown("Fire1") && angle >= -135 && angle <= -45) // DOWN
                {
                    attackDirection = AttackDirection.Down;
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                    animator.SetFloat("Xinput", 0f);
                    animator.SetFloat("Yinput", -1f);
                    Debug.Log("DOWN");   
                    swordSound.Play();
                }
            }
            else if (currentWeapon == "Projectile")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    GameObject.Find("PlayerFirepoint").GetComponent<PlayerShoot>().Shoot();
                    nextAttackTime = Time.time + 1f / attackRate;
                    projectileSound.Play();
                }
                if ( Input.GetButtonDown("Fire1") && ((angle >= 135 && angle <= 180) || (angle <= -135 && angle >= -180)) ) // LEFT **
                {
                    animator.SetFloat("Xinput", -1.0f);
                    animator.SetFloat("Yinput", 0f);
                    // Debug.Log("LEFT");
                    projectileSound.Play();
                }
                else if ( Input.GetButtonDown("Fire1") && ((angle <= 45 && angle >= 0) || (angle >= -45 && angle <= 0)) ) // RIGHT
                {
                    animator.SetFloat("Xinput", 1.0f);
                    animator.SetFloat("Yinput", 0f);
                    // Debug.Log("RIGHT");
                    projectileSound.Play();
                }
                else if (Input.GetButtonDown("Fire1") && angle <= 135 && angle >= 45) // UP
                {
                    animator.SetFloat("Xinput", 0f);
                    animator.SetFloat("Yinput", 1f);
                    // Debug.Log("UP");     
                    projectileSound.Play();           
                }
                else if (Input.GetButtonDown("Fire1") && angle >= -135 && angle <= -45) // DOWN
                {
                    animator.SetFloat("Xinput", 0f);
                    animator.SetFloat("Yinput", -1f);
                    // Debug.Log("DOWN");   
                    projectileSound.Play();
                }
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
