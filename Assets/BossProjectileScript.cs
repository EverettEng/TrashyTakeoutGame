using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileScript : MonoBehaviour
{
    public float timer;
    public GameObject player;
    public PlayerScript playerScript;
    public Rigidbody rb;
    public GameObject boss;
    public float projectileSpeed;
    public FirstPersonController fpController;
    public GameObject hitSound;
    public HitSoundScript hitSoundScript;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0;   
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        fpController = player.GetComponent<FirstPersonController>();
        hitSound = GameObject.FindGameObjectWithTag("HitSound");
        hitSoundScript = hitSound.GetComponent<HitSoundScript>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward * projectileSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer > 7)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            fpController.speed = 2.5f;
            fpController.hit = true;
            fpController.hitTimer = 0f;
            playerScript.health -= 5;
            hitSoundScript.hit = true;
            Destroy(this.gameObject);
        }
    }
}
