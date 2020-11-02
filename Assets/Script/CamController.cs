using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CamController : MonoBehaviour
{
    public GameObject player;

    public Vector3 cam1;

    public float cam1speed;

    public Transform pivot;

    public bool isInvert;

    void Start()
    {
        cam1 = player.transform.position - transform.position;

        pivot.transform.position = player.transform.position;//oyuncuya kameranin hareketlerini taklit eden pivot ekledik.
        //pivot.transform.parent = player.transform; //pivot artik playerin childi degil. 
        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = player.transform.position;// pivot artik childi olmadigi icin böyle onla harket edecek
        // move the player based on camera

        float horizontal = Input.GetAxis("Mouse X")*cam1speed;
        pivot.transform.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Mouse Y") * cam1speed;    
        if (isInvert)  //invert
        {
            pivot.transform.Rotate(vertical / 4, 0, 0);//player idi ama pivot dönsünki  oyuncu yukari dönmezken pivot la birlikte kamera da dönebilsin yukari. 
                                                        //sens düssün diye 4 e böldük
        }else
        {
            pivot.transform.Rotate(-vertical / 4, 0, 0);
        }
        

        //move the camera based on rotation

        float cam1_Y_Angle = pivot.transform.eulerAngles.y; // normalde player idi. ama player yukari dönmesin sadece kamera dönsün istiyoruz.                
        float cam1_X_Angle = pivot.transform.eulerAngles.x; //kamera da pivottan aldigi degere göre dönuyor.

        Quaternion rotation = Quaternion.Euler(cam1_X_Angle, cam1_Y_Angle, 0); // buraya bir daha bak anlamadm aqqqqqq
        transform.position = player.transform.position - (rotation * cam1);

        // dont go under map
        if(transform.position.y< player.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y-0.5f, transform.position.z);
        }

        //Limit up and down
        if(pivot.rotation.eulerAngles.x > 53.82 && pivot.rotation.eulerAngles.x<180f)
        {
            pivot.rotation = Quaternion.Euler(53.82f, 0, 0);
        }
        if(pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 306.18f)
        {
            pivot.rotation = Quaternion.Euler(306.18f, 0, 0);
        }


        //transform.position = player.transform.position - cam1;
        transform.LookAt(player.transform);
    }
}
