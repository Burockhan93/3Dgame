using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int _maxHealth;
    public int currentHealth;

    private float _afterDamage;
    private float _afterDamageCounter;

    private float _flash;
    private float _flashCounter;

    private bool isRespawn;
    private Vector3 _respawnPoint;


    public PlayerMove player; // kendi sciptimizden aliyoruz.
    Renderer playerRenderer;
    public GameObject  playerParticle;
    public Image Screen;

    void Start()
    {
        Screen.gameObject.SetActive(false);
        //player = FindObjectOfType<PlayerMove>(); public aldik zaten
        playerRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>(); // flash yapmak icin damage dan sonra
        
        
        _flashCounter = 0.1f;

        _afterDamageCounter = 1;

        _maxHealth = 10;
        currentHealth = _maxHealth;

        _respawnPoint = player.transform.position; //respan icin nokta
    }

    // Update is called once per frame
    void Update()
    {
        if(_afterDamage > 0)
        {
            _afterDamage -= Time.deltaTime;
            // damage aldik demek _afterDamage 0'dan büyük olmasi

            _flash -= Time.deltaTime; // Damage aldiktan sonra flash basliyor ve off oluyor. 0.1 f bitince tekrar on oluyor ve bu durum _afterDamage sifir olana kadr devam ediyor.
            if (_flash <= 0) { 
                playerRenderer.enabled = !playerRenderer.enabled;
                _flash = _flashCounter;
            }

            if (_afterDamage <= 0) { playerRenderer.enabled = true; } // damage dan sonra true olarak kalsin.
        }
    }
    public void DamagePlayer(int damage, Vector3 direction)
    {
        if (_afterDamage <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                
                Respawn();
            }
            else
            {
                player.Knockback(direction); // playermove icindeki fonksiyon. cactus icinden aldigimiz direction burdan playera iletioz.
                _afterDamage = _afterDamageCounter;

                playerRenderer.enabled = false;
                _flash = _flashCounter;
            }
            
        }
    
    }

    public void HealPlayer(int heal)
    {
        currentHealth += heal;
        if (currentHealth > _maxHealth) { currentHealth = _maxHealth; }
    }

    public void Respawn()//ölürse respawn
    {
        if (!isRespawn)
        {
            StartCoroutine("RespawnCo");
        }   
    }
    public IEnumerator RespawnCo()
    {
        Instantiate(playerParticle, player.transform.position, player.transform.rotation);
        isRespawn = true;
        player.gameObject.SetActive(false);
        Screen.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Screen.gameObject.SetActive(false);
        isRespawn = false;
        player.transform.position = _respawnPoint;
        player.gameObject.SetActive(true);
        

        currentHealth = _maxHealth;

        _afterDamage = _afterDamageCounter; // respawn olduktan sonra damage almis gibi olsun ama ucma yok.
        playerRenderer.enabled = false;
        _flash = _flashCounter;
    }
}
