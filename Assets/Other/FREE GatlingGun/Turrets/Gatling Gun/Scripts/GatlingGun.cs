using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingGun : MonoBehaviour
{
    // target the gun will aim at
    Transform player;

    // Gameobjects need to control rotation and aiming
    public Transform go_baseRotation;
    public Transform go_GunBody;
    public Transform go_barrel;
    public float time = 0.8f;

    // Gun barrel rotation
    public float barrelRotationSpeed;
    float currentRotationSpeed;

    // Distance the turret can aim and fire from
    public float firingRange = 29f;

    // Particle system for the muzzel flash
    public ParticleSystem muzzelFlash;

    private Health playerHealth;

    // Used to start and stop the turret firing
    bool canFire = false;

    int damage = 1;
    float timer;
    float timeBetweenAttacks = 0.5f;

    public AudioSource shootingSound;

    public Target target;

    private void Awake()
    {
        shootingSound.Stop();
    }
    void Start()
    {
        // Set the firing range distance
        player = PlayerManager.instance.player.transform;
        playerHealth = GameObject.Find("First Person Player").GetComponent<Health>();
        target = GetComponent<Target>();
    }

    void Update()
    {
        AimAndFire();
        timer += Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, firingRange);
    }

   
  

    void AimAndFire()
    {
        // Gun barrel rotation
        go_barrel.transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
        float distance = Vector3.Distance(player.position, transform.position);
        // if can fire turret activates
        if (distance <= firingRange)
        {
            // start rotation
            currentRotationSpeed = barrelRotationSpeed;
            
            // aim at enemy
            Vector3 baseTargetPostition = new Vector3(player.position.x, this.transform.position.y, player.position.z);
            Vector3 gunBodyTargetPostition = new Vector3(player.position.x, player.position.y, player.position.z);
            if (!shootingSound.isPlaying)
            {
                shootingSound.Play();
            }
            go_baseRotation.transform.LookAt(baseTargetPostition);
            go_GunBody.transform.LookAt(gunBodyTargetPostition);
            if (timeBetweenAttacks < timer && target.health > 0)
            {
                Attack(damage);
                
            }

            // start particle system 
            if (!muzzelFlash.isPlaying)
            {
                muzzelFlash.Play();
            }
        }
        else
        {
            // slow down barrel rotation and stop
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, 10 * Time.deltaTime);
            shootingSound.Stop();
            // stop the particle system
            if (muzzelFlash.isPlaying)
            {
                muzzelFlash.Stop();
            }
        }
    }
    void Attack(int damage)
    {
        timer = 0f;
        playerHealth.TakeDamage(damage);

        

    }
}