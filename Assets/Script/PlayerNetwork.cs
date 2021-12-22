using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Bolt;
using UnityEditor;
using UnityEngine;

public class PlayerNetwork : EntityBehaviour<ICustomPlayerState>
{
    [Header("Value")]

    [SerializeField] public string PlayerName;
    
    [SerializeField] public float OrginalSpeed; 
    
    [SerializeField] public float MoveSpeed;
    
    [SerializeField] public float Dashtime;
    
    [SerializeField] public float Dashpower;

    [SerializeField] public bool IsDie;

    [SerializeField] public bool IsDashing;
    
    [Header("Basic")]
    
    [SerializeField] private PlayerBehaviour _PlayerBehaviour;

    [SerializeField] public Rigidbody2D rigidbody2D;

    [SerializeField] public Computers FixComputers;
    
    [SerializeField] public PlayerHUD PlayerHud;

    [SerializeField] public Camera MyCamera;

    [Header("Animation")] 
    
    [SerializeField] public Animator _animator;

    [SerializeField] public GameObject PlayerSprite;

    [SerializeField] public string RUN, IDLE, HARD , FIX , DIE,DASH;
    

    // Start is called before the first frame update
    public override void Attached()
    {
        MoveSpeed = OrginalSpeed;
        state.SetTransforms(state.PlayerTransform , transform);
        state.SetTransforms(state.PlayerAnimatorTransform , PlayerSprite.transform);
        state.SetAnimator(_animator);

        rigidbody2D = GetComponent<Rigidbody2D>();

        PlayerName = PlayerPrefs.GetString("UserName");
        state.PlayerName = PlayerName;
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

    public void PlayerNameCallBack()
    {

    }

    private void Update()
    {
        ControllAnimator();
        
        
        if (entity.IsOwner && !MyCamera.gameObject.activeInHierarchy)
        {
            MyCamera.gameObject.SetActive(true);
            PlayerHud.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerOnAttack();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            if (_PlayerBehaviour != PlayerBehaviour.Die)
            {
                _PlayerBehaviour = PlayerBehaviour.IDLE;
                state.IsHard = false;
            }
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
        
        if (other.gameObject.tag == "Killer")
        {
            PlayerOnAttack();
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
        if (!state.IsDie)
        {
            if (state.IsFix)
            {
                state.Animator.Play(FIX);
            }
            else
            {
                if (state.IsHard)
                {
                    //PlayerHud.PlayerFreeze();
                    state.Animator.Play(HARD);
                }
                else
                {
                    if (state.IsMove)
                    {
                        //PlayerHud.PlayerNormal();
                        state.Animator.Play(RUN);
                    }
                    else
                    {
                        if (state.IsDash)
                        {
                            state.Animator.Play(DASH);
                        }
                        else
                        {
                            state.Animator.Play(IDLE);
                        }
                        
                    }
                }
            }
        }
        else
        {
            state.Animator.Play(DIE);
            PlayerHud.PlayerDie();
        }
        
    }

    public virtual void AnimatorControll()
    {
     
    }


    public IEnumerator PlayerDash()
    {
            IsDashing = true;
            _PlayerBehaviour = PlayerBehaviour.Dash;
            state.IsDash = true;
            MoveSpeed *= Dashpower;
            yield return new WaitForSeconds(Dashtime);
            MoveSpeed = OrginalSpeed;
            state.IsDash = false;
            IsDashing = false;
            

    }
    public virtual void StateMachineControll()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (!IsDashing)
            {
                StartCoroutine(PlayerDash());
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _PlayerBehaviour = PlayerBehaviour.Hard;

            state.IsHard = true;
        }
        

        if (FixComputers != null && Input.GetMouseButtonDown(0))
        {
            _PlayerBehaviour = PlayerBehaviour.Repiaring;

            state.IsFix = true;
        }
        if (FixComputers != null && Input.GetMouseButtonUp(0))
        {
            _PlayerBehaviour = PlayerBehaviour.IDLE;
            
            state.IsFix = false;
        }
        

        if (_PlayerBehaviour != PlayerBehaviour.Hard && _PlayerBehaviour != PlayerBehaviour.Repiaring && _PlayerBehaviour != PlayerBehaviour.Die)
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

    public string GetName()
    {
        return PlayerName;
    }

    public void PlayerOnAttack()
    {
        state.IsDie = true;
        _PlayerBehaviour = PlayerBehaviour.Die;
    }
    
}