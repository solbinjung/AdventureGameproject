using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public float jumpPower = 5f;

    public int maxHp = 100;
    public int curHp = 100;

    public Slider hpbar;

    float hAxis;
    float vAxis;

    bool jDown;
    bool isJump;

    private Rigidbody rigid;
    private Animator anim;

    Vector3 moveVec;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        hpbar.maxValue = maxHp;
        UpdateHpBar();
    }
    void Update ()
    {
        GetInput();
        Run();
        Turn();
        Jump();
        Attack();
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
    }
    void Run()
    {
        //Player 이동
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += moveVec * speed * Time.deltaTime;
        anim.SetBool("isRun", moveVec != Vector3.zero);
    }
    void Turn()
    {
        //Player 회전
        transform.LookAt(transform.position + moveVec); 
    }
    void Jump()
    {
        //Player 점프
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //Player 착지
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
    void Attack()
    {
        //Player 공격
        if(Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("doAttack");
            Enemy enemy = FindObjectOfType<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(20);
            }
        }
    }
    public void TakeDamage(int damageAmount)
    {
        curHp -= damageAmount;
        curHp = Mathf.Clamp(curHp, 0, maxHp); 
        UpdateHpBar(); 

        anim.SetTrigger("getHit");

        if (curHp <= 0)
        {
            Die();
        }
    }

    private void UpdateHpBar()
    {
        hpbar.value = curHp; 
    }

    private void Die()
    {
        anim.SetTrigger("isDead");

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}