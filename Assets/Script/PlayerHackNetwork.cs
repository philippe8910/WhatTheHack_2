using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Photon.Bolt;
using UnityEditor;
using UnityEngine;

public class PlayerHackNetwork : EntityBehaviour<ICustomPlayerHackState>
{
    [Header("Value")] 
    
    [SerializeField] public float MoveSpeed;

    [SerializeField] public float AttackRangeValue;

    [SerializeField] public bool IsDie;
    
    [Header("Basic")]
    
    [SerializeField] public PlayerBehaviour _PlayerBehaviour;

    [SerializeField] public Rigidbody2D rigidbody2D;

    [SerializeField] public Computers FixComputers;
    
    [SerializeField] public PlayerHUD PlayerHud;

    [SerializeField] public Transform AttackRange;

    [SerializeField] public Camera MyCamera;

    [SerializeField] public LayerMask PlayerMask;

    [SerializeField] public GameObject SkillEffect;

   // [SerializeField] public GameObject AttackZone;

    [Header("Animation")] 
    
    [SerializeField] public Animator _animator;

    [SerializeField] public GameObject PlayerSprite;

    [SerializeField] public string RUN, IDLE, HARD;

    [Header("Computer")] 
    
    [SerializeField] private Computers[] _computersList;

    [SerializeField] private PlayerNetwork[] PlayerNetworks;
    

    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.PlayeHackrTransform , transform);
        state.SetTransforms(state.PlayerAnimatorTransformHack , PlayerSprite.transform);
        state.SetAnimator(_animator);

        rigidbody2D = GetComponent<Rigidbody2D>();

        _computersList = GameObject.FindObjectsOfType<Computers>();
       
        //PlayerHud = GameObject.Find("PlayerHUD").GetComponent<PlayerHUD>();
        
        //Camera.main.transform.parent = transform;
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        if (entity.IsOwner)
        {
            PlayerPhysicControll();
            FlipSpriteControll();
        }
    }
    

    private void Update()
    {
        if (PlayerNetworks.Length < 4)
        {
            PlayerNetworks = GameObject.FindObjectsOfType<PlayerNetwork>();
        }
        


        if (entity.IsOwner)
        { 
            StateMachineControll();
            
            if (Input.GetKeyDown(KeyCode.O))
            {
                BoltNetwork.LoadScene("END_1");
            }
            
            if (PlayerNetworks.Length >= 1 && Input.GetKeyDown(KeyCode.Space))
            {
                var evnt = PlayerOtherEvent.Create();
                evnt.OwO = "Teleport";
                transform.position = new Vector3(0, 50, 0);
                evnt.Send();
                Debug.Log(evnt.OwO);
            }
        }
        
        ControllAnimator();


        if (AllComputerCompelet())
        {
            Debug.Log("All Computer Compelet");
            BoltNetwork.LoadScene("BadEndMenu");
        }

        if (CheckPlayerAllDead() && PlayerNetworks.Length != 0)
        {
            Debug.Log("All Dead");
            BoltNetwork.LoadScene("GoodEndMenu");
        }

        


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
            state.IsAttack = false;
        }

        if (other.gameObject.tag == "Killer")
        {
            
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
        if (state.IsSkill)
        {
            state.Animator.Play("Hack_Skill");
        }
        else
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
            
            
            if (Physics2D.OverlapCircle(AttackRange.position , AttackRangeValue , PlayerMask))
            {
                var evnt = PlayerOnAttackEvent.Create();

                string name = Physics2D.OverlapCircle(AttackRange.position, AttackRangeValue, PlayerMask).GetComponent<PlayerNetwork>().GetName();
                evnt.Message = name;
                evnt.MyProflie = Physics2D.OverlapCircle(AttackRange.position, AttackRangeValue, PlayerMask)
                    .GetComponent<BoltEntity>();
                evnt.Send();

                Debug.Log(evnt.Message);
            }
            
            
            //BoltNetwork.Instantiate(AttackZone, AttackRange.position, Quaternion.identity);

            Invoke("AttackStop" , 0.5f);
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

        

        if (Input.GetMouseButtonDown(2))
        {
            BoltNetwork.Instantiate(SkillEffect, transform.position, transform.rotation);
            Invoke("StopSkill" , 0.8f);
            state.IsSkill = true;
            _PlayerBehaviour = PlayerBehaviour.Skill;
            
            if (Physics2D.OverlapCircle(AttackRange.position , AttackRangeValue , PlayerMask))
            {
                var evnt = PlayerOnAttackEvent.Create();

                string name = Physics2D.OverlapCircle(AttackRange.position, AttackRangeValue, PlayerMask).GetComponent<PlayerNetwork>().GetName();
                evnt.Message = name;
                evnt.MyProflie = Physics2D.OverlapCircle(AttackRange.position, AttackRangeValue, PlayerMask)
                    .GetComponent<BoltEntity>();
                evnt.Send();

                Debug.Log(evnt.Message);
            }
        }
        
        /*

        if (FixComputers != null && Input.GetMouseButtonDown(0))
        {
            FixComputers.StartRepiaringComputer();
        }

        if (FixComputers != null && Input.GetMouseButtonUp(0))
        {
            FixComputers.StopRepiaringComputer();
        }
        
        */
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

    private bool AllComputerCompelet()
    {
        bool IsAllComputer = true;

        for (int i = 0; i < _computersList.Length; i++)
        {
            if (!_computersList[i].GetCompeletFixed())
            {
                IsAllComputer = false;
            }
        }

        return IsAllComputer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackRange.position , AttackRangeValue);
        
    }


    public void AttackStop()
    {
        state.IsAttack = false;
        _PlayerBehaviour = PlayerBehaviour.IDLE;
    }

    public void StopSkill()
    {
        state.IsSkill = false;
    }

    private bool CheckPlayerAllDead()
    {
        bool IsAllDead = true;

        for (int i = 0; i < PlayerNetworks.Length; i++)
        {
            if (!PlayerNetworks[i].ReturnStateIsDie())
            {
                IsAllDead = false;
            }
        }

        return IsAllDead;
    }
}
