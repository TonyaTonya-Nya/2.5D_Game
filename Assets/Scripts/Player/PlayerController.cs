using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed;
    public Animator animator;
    public Transform model;
    public float atkDamage;
    public float comboDamage;

    public AudioClip[] gameAudio;
    private AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        /*
        if (PlayerManager.gameOver)
        {
            //play death animation
            animator.SetTrigger("die");

            //disable the script
            this.enabled = false;
        }*/



        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Run", true);

        }
        else
        {
            animator.SetBool("Run", false);
        }

        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput;
        float vInput = Input.GetAxis("Vertical");
        direction.z = vInput;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && (hInput != 0 || vInput != 0))
        {
            controller.Move(direction * Time.deltaTime * speed);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(hInput, 0, vInput));
            gameObject.transform.rotation = newRotation;
        }




        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            myAudioSource.PlayOneShot(gameAudio[0]);
            animator.SetTrigger("Attack1");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && model.GetComponent<AnimEvents>().comboCheck)
        {
            myAudioSource.PlayOneShot(gameAudio[0]);
            animator.SetTrigger("Attack2");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && model.GetComponent<AnimEvents>().comboCheck)
        {
            myAudioSource.PlayOneShot(gameAudio[1]);
            animator.SetTrigger("Attack3");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            myAudioSource.PlayOneShot(gameAudio[2]);
            animator.SetTrigger("Magic");
            Invoke("MagicAttack", 1);
        }

        //win level
        /*
        if (PlayerManager.winLevel)
        {
            animator.SetTrigger("win");
            this.enabled = false;
        }*/
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Enemy") && (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) && model.GetComponent<AnimEvents>().attackCheck)
        {
            float damage = atkDamage;
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                damage = atkDamage;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
                damage = atkDamage + comboDamage;
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
                damage = atkDamage+2*comboDamage;
            other.GetComponent<Enemy>().TakeDamage(damage,0);
        }
        else if (other.CompareTag("Npc") && other.GetComponent<Npc>().DialogueCheck())
        {
            other.GetComponent<Npc>().StartDialogue();
        }

    }

    private void MagicAttack()
    {
        model.GetComponent<PlayerAttacks>().FireBallAttack();
    }
}

