using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public float distance;
    public GameObject player;
    public GameObject projectileSpawn;
    public GameObject projectile;
    public GameObject boss;
    public float timer;
    public int health;
    public float timer2;
    public TMP_Text bossName;
    public Slider bossHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        distance = 0;
        timer = 0;
        health = 500;
        timer2 = 0;
        bossName.text = "Boss: Trash Monster";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        distance = Vector3.Distance(player.transform.position, transform.position);
        projectileSpawn.transform.LookAt(player.transform);
        if (distance < 30 && timer > 1)
        {
            Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.localRotation);
            timer = 0;
        }
        if(timer2 > 4 && health < 481)
        {
            health += 20;
            timer2 = 0;
        }
        else if (timer2 > 4 && health <= 500)
        {
            health = 500;
            timer2 = 0;
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        bossHealthBar.value = health;
    }
}
