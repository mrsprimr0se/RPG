using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimationPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnimation;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();    
        activeWeapon = GetComponentInParent<ActiveWeapon>();    
        myAnimator = GetComponent<Animator>();  
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);  

       slashAnimation =  Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimationEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimationEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true; 
        }
    }

    public void SwingDownFlipAnimationEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;  

        if (mousePos.x < playerScreenPoint.x) // naprzemienne przenoszenie mieczu wraz z myszka prawo, lewo
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
