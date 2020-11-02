using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Animator anim;
    public GameObject pivot;
    public GameObject playermodel1;

    private float _moveSpeed;
    private float _rotatSpeed;
    private float _jumpForce;
    private Vector3 _charMove;
    //knockback
    private float _knockBackForce;
    private float _knockBackTime;
    private float _knockbackCounter;


    //Rigidbody rb;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
     _knockBackForce=3f;
     _knockBackTime=0.4f;
     _knockbackCounter=0;

     _moveSpeed = 10f;
     _jumpForce = 30f;
     _rotatSpeed = 10f;
        //rb = GetComponent<Rigidbody>();
     controller = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector3(Input.GetAxis("Horizontal")*_moveSpeed,rb.velocity.y, Input.GetAxis("Vertical")*_moveSpeed);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //rb.AddForce(Vector3.up * _jumpForce);
        //    rb.velocity = new Vector3(rb.velocity.x,_jumpForce,rb.velocity.z);
        //}

        //_charMove = new Vector3(Input.GetAxis("Horizontal") * _moveSpeed, _charMove.y, Input.GetAxis("Vertical") * _moveSpeed); hareketi globale tasiycaz -1

        //_charMove = (transform.forward * Input.GetAxis("Vertical")) * _moveSpeed  
        // + (transform.right * Input.GetAxis("Horizontal"))*_moveSpeed; // burdan da movesppedleri cikarip normalize edelim -2 


        if (_knockbackCounter <= 0) // hasar alma süresi bitince "WASD" komutlarini al
        {
            float Jumpy = _charMove.y;
            _charMove = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal")); //-3    
            _charMove = _charMove.normalized * _moveSpeed;// vektörü normalize ediyor aslinda 1 den büyük olmamasini saglio.
            _charMove.y = Jumpy; // ziplama degerini normalize etmeyelim.

            if (controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _charMove.y = (_jumpForce);
                }
            }

            if (!controller.isGrounded)
            {
                _charMove.y += Physics.gravity.y / 10;
            }
            
        }else
        {
            _knockbackCounter -= Time.deltaTime;
        }
        controller.Move(_charMove * Time.deltaTime);//Controller hareket ettirme koutu.

        //move player based on MouseLook

        if (Input.GetAxis("Vertical") !=0 || Input.GetAxis("Horizontal") != 0)
        {
            transform.rotation= Quaternion.Euler(0, pivot.transform.eulerAngles.y, 0); // su an pivotun baktigi yöne dogru dönecek bir tusa basinca.
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(_charMove.x, 0,_charMove.z));
            playermodel1.transform.rotation = Quaternion.Slerp(playermodel1.transform.rotation, newRotation, _rotatSpeed * Time.deltaTime);
        }

        //animasyon degerleri atama
        anim.SetBool("isGrounded", !controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal")))); // herhangi bir deger 1 olursa kossun.
    }

    public void Knockback(Vector3 knockDirection)
    {
        _knockbackCounter = _knockBackTime;
        _charMove = knockDirection * _knockBackForce;
        _charMove.y = _knockBackForce;

    }
}
