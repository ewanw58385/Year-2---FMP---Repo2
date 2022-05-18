using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBlocker : MonoBehaviour
{
    public GameObject player;
    public Animator anim;

    public BossCombatManager bcm;

    public GameObject bossBar;
    public Animator bossBarFillAnim;
    public Animator bossBarBorderAnim;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();


        bcm = GameObject.Find("MiniBoss").GetComponent<BossCombatManager>();

        bossBar = GameObject.Find("Boss Health Bar");
        bossBarFillAnim = bossBar.transform.GetChild(0).GetComponent<Animator>();
        bossBarBorderAnim = bossBar.transform.GetChild(1).GetComponent<Animator>();

        bossBar.SetActive(false);
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.Play("BlockAnim");

            bossBar.SetActive(true); 
            bcm.healthBar = GameObject.Find("Boss Health Bar").GetComponent<BossHealthbarScript>(); //get instance of UI healthbar script
            bcm.healthBar.SetMaxHealth(bcm.bossHealth); //pass initial health (max health) as max health on slider

            bossBarFillAnim.Play("BossBarFadeIn");
            bossBarBorderAnim.Play("BossBarFadeIn");

            if (bossBarBorderAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2f)
            {
                bossBarBorderAnim.SetTrigger("fadedIn");
            }

            
            if (bossBarFillAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2f)
            {
                bossBarFillAnim.SetTrigger("fadedIn");
            }
        }
    }
}
