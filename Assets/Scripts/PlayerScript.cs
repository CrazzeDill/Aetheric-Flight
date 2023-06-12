using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float flapStrength = 14;
    private LogicScript logic;
    private ExpManager expControl;
    public bool birdIsAlive = true;
    private Animator animator;
    [SerializeField]private Animator jumpAnimate;
    private AudioSource playerSound;
    public List<AudioClip> audioClips;
    private Collider2D playerCollider;
    [SerializeField] private ShieldScript shield;


    //
    //Animation States
    const string PlayerJump = "Player_jump";
    const string PlayerPeak = "Player_jumppeak";
    const string PlayerFall = "Player_falling";
    const string PlayerStartJump = "Player_jumpinit";
    const string PlayerAttack = "Player_attack";




    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        expControl = GameObject.FindGameObjectWithTag("Logic").GetComponent<ExpManager>();
        playerSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        //jumpAnimate = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);

        Vector3 targetPosition = transform.position;
        targetPosition.x = -4;
        if (transform != null && transform.position.x != -4)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5 * Time.deltaTime);

        }

        if (Input.GetButtonDown("Jump") && birdIsAlive && !logic.isPaused)
        {
            myRigidBody.velocity = Vector2.up * flapStrength;
            if (playerSound.clip != audioClips[0])
            {
                playerSound.clip = audioClips[0];
            }
            playerSound.Play();
            //animator.Play(PlayerStartJump);
            jumpAnimate.Play("Jump_particle", 0, 0f);
        }

        if (transform.position.y > 8 || transform.position.y < -8)
        {
            logic.gameOver();
            birdIsAlive = false;
        }
    }

    private void FixedUpdate()
    {
        if (!animator.GetBool("onAttack") && logic.isStarted)
        {
            if (myRigidBody.velocity.y > 0.5)
            {
                animator.Play(PlayerJump);
            }
            else if (myRigidBody.velocity.y < -0.5)
            {
                animator.Play(PlayerFall);
            }
            else
            {
                animator.Play(PlayerPeak);
            }
        }
    }
        

    private void AttackAnimationComplete()
    {
        animator.SetBool("onAttack", false);
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (shield.isShielded)
        {
            Debug.Log("SHIELDED");
            shield.GoCooldown();
            Physics2D.IgnoreCollision(playerCollider, collision.collider);
            StartCoroutine(DisablePlayerCollision(5f));
        }
        else
        {
            if (playerSound.clip != audioClips[1])
            {
                playerSound.clip = audioClips[1];
                playerSound.Play();
            }

            if (logic.canRevive)
            {
                //birdIsAlive = false;
                logic.canRevive = false;

            } else {
                logic.gameOver();
                birdIsAlive = false;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && birdIsAlive)
        {
            animator.SetBool("onAttack", true);
            animator.Play(PlayerAttack);
            myRigidBody.velocity = Vector2.up * 0;
            playerSound.PlayOneShot(audioClips[2]);
            logic.addScore(30);
            expControl.AddExp(20);
        } else if (collision.gameObject.layer == 8)
        {
           

        }
    }

    private IEnumerator DisablePlayerCollision(float duration)
    {
        // Ignore collisions between the player and other objects on the "IgnorePlayerCollision" layer
        playerCollider.gameObject.layer = LayerMask.NameToLayer("IgnorePlayerCollision");


        yield return new WaitForSeconds(duration);

        // Restore collisions between the player and other objects
        playerCollider.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private IEnumerator Revive()
    {

        yield return new WaitForSecondsRealtime(3f);
    }
}
