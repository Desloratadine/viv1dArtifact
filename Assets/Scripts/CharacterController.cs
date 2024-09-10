using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class hp
{
    public bool hurt = false;
    public float health;
    public float maxHP;

    public Image Image;
    public GameObject HP;

    
}

public class CharacterController : MonoBehaviour
{
    public hp hp;
    public GameObject MainCamera;
    public bool inpoision = false;
    private Camera Camera;

    public GameObject cat;
    private Rigidbody2D rb;
    private Animator animator;

    public GameObject Bag;//玩家的背包
    public bool isOpen;
    public GameObject set;

    public AudioSource walksound;
    private bool iswalking =false ;

    [SerializeField] private float speed = 5f;//行走的速度

    private float x_delay = 0f;

    private float y_delay = 0f;

    private bool isDashing = false;

    void Start()
    {
        hp.Image = hp.HP.GetComponent<Image>();

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        Camera = MainCamera.GetComponent<Camera>();

        walksound = GetComponent<AudioSource>();

    }

    void Update()
    {

        if(hp.health <= 0)
        {
            SceneManager.LoadScene("be");
            Timer.i = 0;
        }
        OpenSet();

        //基础的四向行走
        //当按下shift键，正在冲刺，移动速度增加；松开后恢复原来的速度
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashing = true;
            speed = 7f;
        }

        else if (isDashing && Input.GetKeyUp(KeyCode.LeftShift))
        {
            isDashing = false;
            speed = 4.5f;
        }

        var xspeed = Input.GetAxisRaw("Horizontal");

        var yspeed = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(xspeed, yspeed) * speed;



        //关于行走动画的
        if (Mathf.Abs(xspeed) > Mathf.Epsilon || Mathf.Abs(yspeed) > Mathf.Epsilon)
        {
            x_delay = xspeed;
            y_delay = yspeed;
            animator.SetBool("isMoving", true);
        }

        else
        {
            animator.SetBool("isMoving", false);
        }

        animator.SetFloat("deltaX", x_delay);

        animator.SetFloat("deltaY", y_delay);

        if (rb.velocity != Vector2.zero)
        {
            iswalking = true;
        }
        else
            iswalking = false;
        walkplay();
        

        hp.Image.fillAmount = hp.health / hp.maxHP;


        if (inpoision) poision();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BE")|| collision.gameObject.CompareTag("SF"))
        {
            hp.hurt = true;
            hp.health -= collision.gameObject.GetComponent<FSM>().Parameter.attack;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("SP"))
        {
            hp.hurt = true;
            inpoision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SP"))
        {
            inpoision = false;
        }
    }

    private void poision()
    {
        animator.SetTrigger("gethit");
        hp.health -= 1f * Time.deltaTime;
    }

    void OpenSet()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isOpen = !Bag.activeSelf;
            Bag.SetActive(isOpen);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            isOpen = !set.activeSelf;
            set.SetActive(isOpen);
        }
    }

    private void walkplay()
    {
        if (iswalking)
        {
            if(!walksound.isPlaying)
            walksound.Play();
        }
        else
            walksound.Pause();
    }
}
