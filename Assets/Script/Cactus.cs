using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
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
            Vector3 hitDirection = other.transform.position - transform.position; // hangi yöne savuracagi 
            hitDirection = hitDirection.normalized; // yine 1 e indirgedik
            FindObjectOfType<HealthManager>().DamagePlayer(2,hitDirection); // 10 vursun ve su yöne savursun
        }
    }
}
