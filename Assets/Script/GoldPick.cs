using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPick : MonoBehaviour
{
    public int value;
    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(pickupEffect, transform.position,transform.rotation);
            FindObjectOfType<GameManager>().AddGold(value);
            Destroy(gameObject, 0.1f);
            //Destroy(pickupEffect.gameObject, 1f); // Destroying assets not permitted diye hata veriyor buraya yazinca. O yuzden paricle icine script yazmamiz gerek
        }
    }
}
