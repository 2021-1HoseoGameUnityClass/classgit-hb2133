using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float playerJumpforce = 10f;

    [SerializeField]
    private GameObject bulletObj = null;

    [SerializeField]
    private GameObject InstantiateObj = null;

    private bool jump = false;

    private bool move = false;
    private float moveHorizontal = 0;



    void Start()
    {

    }

    
    void Update()
    {
        if(move == true)
        {
            PlayerMove();
        }

        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void PlayerMove()
    {
        // float h = Input.GetAxis("Horizontal");
        float h = moveHorizontal;
        float playerSpeed = h * moveSpeed * Time.deltaTime;
        Vector3 vector3 = new Vector3();
        vector3.x = playerSpeed;

        transform.Translate(vector3);

        if (h < 0)
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void PlayerJump()
    {
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Jump", true);

            //점프량만큼 Add force
            Vector2 vector2 = new Vector2(0, playerJumpforce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
        jump = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            jump = false;
        }
    }

    private void Fire()
    {
        AudioClip audioClip = Resources.Load<AudioClip>("RangedAttack") as AudioClip;
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bulletObj, InstantiateObj.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);
    }

    private void OnCollisionEnter2D(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            DataManager.instance.playerHP -= 1;
            if (DataManager.instance.playerHP < 0)
            {
                //DataManager.instance.playerHP < 0;
            }

        }
    }

    public void OnMove(bool _right)
    {
        if(_right)
        {
            moveHorizontal = 1;
        }
        else
        {
            moveHorizontal = -1;
        }

        move = true;
    }

    public void OffMove()
    {
        moveHorizontal = 0;
        move = false;

    }
}
