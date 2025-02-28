using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;

    private Weaponinfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; 
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();   
    }

    public void UpdateWeaponInfo(Weaponinfo weaponInfo)
    {
        this.weaponInfo = weaponInfo;   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indes indes = other.gameObject.GetComponent<Indes>();   

        if (!other.isTrigger && (enemyHealth || indes))
        {
            enemyHealth?.TakeDamage(weaponInfo.weaponDamage);
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }  
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);    
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);    
    }
}
