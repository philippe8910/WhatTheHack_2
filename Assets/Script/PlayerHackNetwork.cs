using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEditor;
using UnityEngine;

public class PlayerHackNetwork : EntityBehaviour<ICustomPlayerHackState>
{
    [Header("Value")] 
    
    [SerializeField] public float MoveSpeed;
    
    [Header("Basic")]
    
    [SerializeField] private PlayerBehaviour _PlayerBehaviour;

    [SerializeField] public Rigidbody2D rigidbody2D;

    [SerializeField] public Computers FixComputers;
    
    [SerializeField] public PlayerHUD PlayerHud;

    [SerializeField] public Camera MyCamera;

    [Header("Animation")] 
    
    [SerializeField] public Animator _animator;

    [SerializeField] public GameObject PlayerSprite;

    [SerializeField] public string RUN, IDLE, HARD;
    

    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.PlayeHackrTransform , transform);
        state.SetTransforms(state.PlayerAnimatorTransformHack , PlayerSprite.transform);
        state.SetAnimator(_animator);

        rigidbody2D = GetComponent<Rigidbody2D>();


       
        //PlayerHud = GameObject.Find("PlayerHUD").GetComponent<PlayerHUD>();
        
        //Camera.main.transform.parent = transform;
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        PlayerPhysicControll();
        StateMachineControll();
        FlipSpriteControll();
    }

    private void Update()
    {
        ControllAnimator();
        
        
        if (entity.IsOwner && !MyCamera.gameObject.activeInHierarchy)
        {
            //MyCamera.gameObject.SetActive(true);
            PlayerHud.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _PlayerBehaviour = PlayerBehaviour.IDLE;
            state.IsAttack = false;
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

    public virtual void ControllAnimator()
    {
        if (state.IsAttack)
        {
            state.Animator.Play(HARD);
        }
        else
        {
            if (state.IsMoveHack)
            {
                state.Animator.Play(RUN);
            }
            else
            {
                state.Animator.Play(IDLE);
            }
        }
    }

    public virtual void AnimatorControll()
    {
        
    }

    public virtual void StateMachineControll()
    {
        if (Input.GetMouseButtonDown(1) && !state.IsAttack)
        {
            _PlayerBehaviour = PlayerBehaviour.Hard;
            state.IsAttack = true;
            Invoke("AttackStop" , 0.5f);
        }
        

        if (FixComputers != null && Input.GetMouseButtonDown(0))
        {
            _PlayerBehaviour = PlayerBehaviour.Repiaring;
        }
        if (FixComputers != null && Input.GetMouseButtonUp(0))
        {
            _PlayerBehaviour = PlayerBehaviour.IDLE;
        }
        

        if (_PlayerBehaviour != PlayerBehaviour.Hard && _PlayerBehaviour != PlayerBehaviour.Repiaring)
        {
            if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
            {
                _PlayerBehaviour = PlayerBehaviour.RUN;

                state.IsMoveHack = true;
            }
            else
            {
                _PlayerBehaviour = PlayerBehaviour.IDLE;

                state.IsMoveHack = false;
            }
        }
    }

    public virtual void PlayerPhysicControll()
    {
        if (_PlayerBehaviour == PlayerBehaviour.RUN)
        {
            rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * MoveSpeed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        if (FixComputers != null && Input.GetMouseButtonDown(0))
        {
            FixComputers.StartRepiaringComputer();
        }

        if (FixComputers != null && Input.GetMouseButtonUp(0))
        {
            FixComputers.StopRepiaringComputer();
        }
    }

    public virtual void FlipSpriteControll()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            PlayerSprite.transform.rotation = Quaternion.Euler(0,180,0);
            
            // PlayerSprite.transform.localScale = 
            //    new Vector3(-Mathf.Abs(PlayerSprite.transform.localScale.x) , PlayerSprite.transform.localScale.y , PlayerSprite.transform.localScale.z);
        }
        
        if (Input.GetAxisRaw("Horizontal") < 0)
        { 
            PlayerSprite.transform.rotation = Quaternion.Euler(0,0,0);
            
            // PlayerSprite.transform.localScale = 
            //    new Vector3(Mathf.Abs(PlayerSprite.transform.localScale.x) , PlayerSprite.transform.localScale.y , PlayerSprite.transform.localScale.z);
        }
    }


    public void AttackStop()
    {
        state.IsAttack = false;
        _PlayerBehaviour = PlayerBehaviour.IDLE;
    }
}
