using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstPersonController : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 5.0f;
    private bool canJump;
    public Rigidbody rb;
    public float jumpForce;
    public GameObject projectile;
    public GameObject arm;
    public Transform projectileSpawn;
    public float projectileSpeed;
    public float delay;
    public bool canFire;
    public GameObject target;
    public int bulletCount;
    public bool reload;
    public float reloadTimer;
    public TMP_Text gunInfoText;
    public Transform playerSpawn;
    public bool hit;
    public TMP_Text statusText2;
    public float hitTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        canJump = true;
        canFire = true;
        delay = 0;
        reloadTimer = 0;
        reload = false;
        bulletCount = 30;
        gunInfoText.text = bulletCount + "/30";
        transform.position = playerSpawn.position;
        statusText2.text = "";
        hit = false;
    }
    
    void Update()
    {
        if (hit)
        {
            hitTimer += Time.deltaTime;
            statusText2.text = "You have been hit! Your speed has been halved for 5 seconds.";
            if (hitTimer >= 5.0f)
            {
                speed = 5.0f;
                statusText2.text = "";
                hit = false;
                hitTimer = 0;
            }
        }
        

        if (reload)
        {
            gunInfoText.text = "Reloading...";
        }
        else
        {
            gunInfoText.text = bulletCount + "/30";
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
        delay -= Time.deltaTime;
        if (delay < 0 && reload == false)
        {
            canFire = true;
            delay = 0.1f;
            
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canFire && bulletCount > 0)
            {
                GameObject tempProjectile = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                tempProjectile.GetComponent<Rigidbody>().AddForce(arm.transform.forward * projectileSpeed);
                bulletCount--;
                canFire = false;
                GetComponent<AudioSource>().Play();
            }
        }
        if (Input.GetKey(KeyCode.R) || bulletCount <= 0)
        {
            reload = true;
            canFire = false;
        }
        if (Input.GetKey(KeyCode.X) && bulletCount > 0 && reload)
        {
            reload = false;
            canFire = true;
        }
        if (reload)
        {
            reloadTimer += Time.deltaTime;
            if(reloadTimer > 3)
            {
                bulletCount = 30;
                reloadTimer = 0;
                reload = false;
            }
        }

        arm.transform.LookAt(target.transform);
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector3(x, 0, z);
        transform.Translate(moveDirection * Time.deltaTime * speed);
        
            
    }

    void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }
}
