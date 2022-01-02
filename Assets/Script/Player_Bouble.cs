using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum PlayerBehaviour
{
    IDLE,
    RUN,
    Repiaring,
    Hard,
    Die,
    Dash
}

public class Player_Bouble : MonoBehaviour
{
    [Header("Value")] 
    
    [SerializeField] private float MoveSpeed;
    
    [Header("Basic")]
    
    [SerializeField] private PlayerBehaviour _PlayerBehaviour;

    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private Computers FixComputers;
    
    [SerializeField] private PlayerHUD PlayerHud;

    [Header("Animation")] 
    
    //[SerializeField] private PlayerAnimatorSystem PlayerAnimatorSystem;

    [SerializeField] private GameObject PlayerSprite;

    [SerializeField] private string RUN, IDLE, HARD;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        PlayerHud = GameObject.Find("PlayerHUD").GetComponent<PlayerHUD>();
        //PlayerAnimatorSystem = GetComponentInChildren<PlayerAnimatorSystem>();
        Camera.main.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        ControllAnimator();
        StateMachineControll();
        PlayerPhysicControll();
        FlipSpriteControll();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _PlayerBehaviour = PlayerBehaviour.IDLE;
        }

        if (other.gameObject.tag == "Killer")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Computer")
        {
            if (other.GetComponent<Computers>())
            {
                FixComputers = other.GetComponent<Computers>();
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Computer")
        {
            FixComputers = null;
        }
    }

    private void ControllAnimator()
    {
        /*
        if (!(_PlayerBehaviour == PlayerBehaviour.Hard))
        {
            GetComponent<SpriteRenderer>().color = ColorNex;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = ColorCur;
        }
        */
    }

    public void StateMachineControll()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _PlayerBehaviour = PlayerBehaviour.Hard;
            Debug.Log("Hard!!");
            
            //PlayerAnimatorSystem.ChangeAnimator(HARD);
        }

        if (FixComputers != null && Input.GetKeyDown(KeyCode.X))
        {
            _PlayerBehaviour = PlayerBehaviour.Repiaring;
        }

        if (FixComputers != null && Input.GetKeyUp(KeyCode.X))
        {
            _PlayerBehaviour = PlayerBehaviour.IDLE;
        }


        if (_PlayerBehaviour != PlayerBehaviour.Hard && _PlayerBehaviour != PlayerBehaviour.Repiaring)
        {
            if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            {
                _PlayerBehaviour = PlayerBehaviour.RUN;
                
               // PlayerAnimatorSystem.ChangeAnimator(RUN);
            }
            else
            {
                _PlayerBehaviour = PlayerBehaviour.IDLE;
                
                //PlayerAnimatorSystem.ChangeAnimator(IDLE);
            }
        }
    }

    public void PlayerPhysicControll()
    {
        if (_PlayerBehaviour == PlayerBehaviour.RUN)
        {
            rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * MoveSpeed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        if (FixComputers != null && Input.GetKeyDown(KeyCode.X))
        {
            FixComputers.StartRepiaringComputer();
        }

        if (FixComputers != null && Input.GetKeyUp(KeyCode.X))
        {
            FixComputers.StopRepiaringComputer();
        }
    }

    private void FlipSpriteControll()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            PlayerSprite.transform.localScale = 
                new Vector3(-Mathf.Abs(PlayerSprite.transform.localScale.x) , PlayerSprite.transform.localScale.y , PlayerSprite.transform.localScale.z);
        }
        
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            PlayerSprite.transform.localScale = 
                new Vector3(Mathf.Abs(PlayerSprite.transform.localScale.x) , PlayerSprite.transform.localScale.y , PlayerSprite.transform.localScale.z);
        }
    }
}