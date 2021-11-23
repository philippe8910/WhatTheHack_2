using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEditor;
using UnityEngine;

public class PlayerNetwork : EntityBehaviour<ICustomPlayerState>
{
    [Header("Value")] 
    
    [SerializeField] private float MoveSpeed;
    
    [Header("Basic")]
    
    [SerializeField] private PlayerBehaviour _PlayerBehaviour;

    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private Computers FixComputers;
    
    [SerializeField] private PlayerHUD PlayerHud;

    [SerializeField] private Camera MyCamera;

    [Header("Animation")] 
    
    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject PlayerSprite;

    [SerializeField] private string RUN, IDLE, HARD;
    

    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.PlayerTransform , transform);
        state.SetTransforms(state.PlayerAnimatorTransform , PlayerSprite.transform);
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
            MyCamera.gameObject.SetActive(true);
            PlayerHud.gameObject.SetActive(true);
        }
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
        if (state.IsHard)
        {
            state.Animator.Play(HARD);
        }
        else
        {
            if (state.IsMove)
            {
                state.Animator.Play(RUN);
            }
            else
            {
                state.Animator.Play(IDLE);
            }
        }
    }

    private void AnimatorControll()
    {
        
    }

    public void StateMachineControll()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _PlayerBehaviour = PlayerBehaviour.Hard;

            state.IsHard = true;
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

                state.IsMove = true;
            }
            else
            {
                _PlayerBehaviour = PlayerBehaviour.IDLE;

                state.IsMove = false;
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

        if (FixComputers != null && Input.GetMouseButtonDown(0))
        {
            FixComputers.StartRepiaringComputer();
        }

        if (FixComputers != null && Input.GetMouseButtonUp(0))
        {
            FixComputers.StopRepiaringComputer();
        }
    }

    private void FlipSpriteControll()
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
}