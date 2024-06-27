using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int EnemyHealth = 100;
    private AudioSource MyPlayer;
    [SerializeField] AudioSource StabPlayer;
    public bool HasDied = false;
    private Animator Anim;
    [SerializeField] GameObject EnemyObject;
    [SerializeField] GameObject BloodSplatKnife;
    [SerializeField] GameObject BloodSplatBat;
    [SerializeField] GameObject BloodSplatAxe;
    private bool DamageOn = false;


    // Start is called before the first frame update
    void Start()
    {
        MyPlayer = GetComponent<AudioSource>();
        Anim = GetComponentInParent<Animator>();
        StartCoroutine(StartElements());
    }

    // Update is called once per frame
    void Update()
    {
        if (DamageOn == true)
        {
            if (EnemyHealth <= 0)
            {
                if (HasDied == false)
                {
                    Anim.SetTrigger("Death");
                    Anim.SetBool("IsDead", true);
                    HasDied = true;
                    SaveScript.EnemiesOnScreen--;
                    Destroy(this.transform.parent.gameObject, 25f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("PKnife"))
            {
                EnemyHealth -= 10;
                MyPlayer.Play();
                StabPlayer.Play();
                BloodSplatKnife.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("PBat"))
        {
            EnemyHealth -= 15;
            MyPlayer.Play();
            StabPlayer.Play();
            BloodSplatBat.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("PAxe"))
        {
            EnemyHealth -= 20;
            MyPlayer.Play();
            StabPlayer.Play();
            BloodSplatAxe.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("PCrossbow"))
        {
            EnemyHealth -= 50;
            MyPlayer.Play();
            StabPlayer.Play();
            Destroy(other.gameObject, 0.05f);
        }
    }

    IEnumerator StartElements()
    {
        yield return new WaitForSeconds(0.1f);
        StabPlayer = SaveScript.StabSound;
        BloodSplatKnife = SaveScript.SplatKnife;
        BloodSplatBat = SaveScript.SplatBat;
        BloodSplatAxe = SaveScript.SplatAxe;
        BloodSplatKnife.gameObject.SetActive(false);
        BloodSplatBat.gameObject.SetActive(false);
        BloodSplatAxe.gameObject.SetActive(false);
        DamageOn = true;
    }
}
