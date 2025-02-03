using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockback;

    private void Awake()
    {
        knockback = GetComponent<KnockBack>();  
        rb = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) { return; }
        

        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;   
    }
}
