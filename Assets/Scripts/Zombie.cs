using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    Animator animator;
    Target target;

    public float lookRadius = 10f;
    public int damage = 5;

    Transform player;
    NavMeshAgent agent;
    private Health playerHealth;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;
    float timer;
    float timeBetweenAttacks = 1.5f;
    //sounds
    public AudioSource audio;
    public AudioClip idle;
    public AudioSource attack;
    public AudioClip death;
    

    
    
    
    
    



    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = GetComponent<Target>();
        audio = GetComponent<AudioSource>();
        
    }
    void Start()
    {
        player = PlayerManager.instance.player.transform;
        playerHealth = GameObject.Find("First Person Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        timer += Time.deltaTime;
        
        if(distance <= lookRadius && target.health > 0)
        {
            animator.SetBool("Enter", true);
            animator.SetBool("Attack", false);
            agent.SetDestination(player.position);
            lookRadius = 100f;
           

            if (distance < 3 && distance > 0 && target.health > 0)
            {
                

                animator.SetBool("Attack", true);
                animator.SetBool("Enter", false);

                if (timeBetweenAttacks < timer)
                   
                Attack(damage);
                
                

            }
            if (distance < 30 && !audio.isPlaying)
            {
                audio.PlayOneShot(idle, 1.0f);
            }

        }
       


        if (target.health <= 0)
        {
            audio.Stop();
            animator.SetBool("Dead", true);

            audio.PlayOneShot(death,1.0f);
            
        }
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void Attack(int damage)
    {
        timer = 0f;
        
        playerHealth.TakeDamage(damage);


        attack.Play();
    }
    
}
