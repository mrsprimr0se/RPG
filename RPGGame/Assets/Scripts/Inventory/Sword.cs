using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimationPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    [SerializeField] private float swordAttackCD = .5f;
    [SerializeField] private Weaponinfo weaponInfo; 

    private Transform weaponCollider;
    private PlayerControls playerControls;
    private Animator myAnimator;
   
    private GameObject slashAnimation;

    private void Awake()
    {   
        myAnimator = GetComponent<Animator>();  
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimationSpawnPoint = GameObject.Find("SlashAnimationSpawnPoint").transform;
    }



    private void Update()
    {
        MouseFollowWithOffset();
    }

    public Weaponinfo GetWeaponInfo()
    {
        return weaponInfo;  
    }

    public void Attack()
    {
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);

            slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;
        
    }

    public void DoneAttackingAnimationEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimationEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true; 
        }
    }

    public void SwingDownFlipAnimationEvent()
    {
        slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;  

        if (mousePos.x < playerScreenPoint.x) // naprzemienne przenoszenie mieczu wraz z myszka prawo, lewo
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
