using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    // public Transform firePoint;
    public GameObject projectilePrefab;

    public float projectileForce;

    public void Shoot(float angleShift, Transform firePoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection = rotateVector(firePoint.right, 0f+angleShift);
        projectile.transform.Rotate(0f, 0f, 0f+angleShift);
        projectile.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(fireDirection * projectileForce, ForceMode2D.Impulse);

        GameObject projectile1 = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection1 = rotateVector(firePoint.right, 45f+angleShift);
        projectile1.transform.Rotate(0f, 0f, 45f+angleShift);
        projectile1.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb1 = projectile1.GetComponent<Rigidbody2D>();
        rb1.AddForce(fireDirection1 * projectileForce, ForceMode2D.Impulse);

        GameObject projectile2 = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection2 = rotateVector(firePoint.right, 90f+angleShift);
        projectile2.transform.Rotate(0f, 0f, 90f+angleShift);
        projectile2.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb2 = projectile2.GetComponent<Rigidbody2D>();
        rb2.AddForce(fireDirection2 * projectileForce, ForceMode2D.Impulse);

        GameObject projectile3 = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection3 = rotateVector(firePoint.right, 135f+angleShift);
        projectile3.transform.Rotate(0f, 0f, 135f+angleShift);
        projectile3.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb3 = projectile3.GetComponent<Rigidbody2D>();
        rb3.AddForce(fireDirection3 * projectileForce, ForceMode2D.Impulse);

        GameObject projectile4 = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection4 = rotateVector(firePoint.right, 180f+angleShift);
        projectile4.transform.Rotate(0f, 0f, 180f+angleShift);
        projectile4.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb4 = projectile4.GetComponent<Rigidbody2D>();
        rb4.AddForce(fireDirection4 * projectileForce, ForceMode2D.Impulse);

        GameObject projectile5 = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection5 = rotateVector(firePoint.right, 225f+angleShift);
        projectile5.transform.Rotate(0f, 0f, 225f+angleShift);
        projectile5.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb5 = projectile5.GetComponent<Rigidbody2D>();
        rb5.AddForce(fireDirection5 * projectileForce, ForceMode2D.Impulse);

        GameObject projectile6 = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection6 = rotateVector(firePoint.right, 270f+angleShift);
        projectile6.transform.Rotate(0f, 0f, 270f+angleShift);
        projectile6.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb6 = projectile6.GetComponent<Rigidbody2D>();
        rb6.AddForce(fireDirection6 * projectileForce, ForceMode2D.Impulse);

        GameObject projectile7 = Instantiate(projectilePrefab, transform.position, firePoint.rotation);
        Vector2 fireDirection7 = rotateVector(firePoint.right, 315f+angleShift);
        projectile7.transform.Rotate(0f, 0f, 315f+angleShift);
        projectile7.transform.parent = gameObject.transform.parent; // set projectile as child of the enemy
        Rigidbody2D rb7 = projectile7.GetComponent<Rigidbody2D>();
        rb7.AddForce(fireDirection7 * projectileForce, ForceMode2D.Impulse);
    }

    Vector2 rotateVector(Vector2 vector, float angle)
    {
        return new Vector2(vector.x * Mathf.Cos(angle * Mathf.Deg2Rad) - vector.y * Mathf.Sin(angle * Mathf.Deg2Rad), vector.x * Mathf.Sin(angle * Mathf.Deg2Rad) + vector.y * Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
