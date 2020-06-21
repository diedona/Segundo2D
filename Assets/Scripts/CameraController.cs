using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Speed += 1;
        }
        else if(Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Speed -= 1;
        }

        if (Input.GetKey("a"))
        {
            transform.position += new Vector3(-1, 0, 0) * Speed * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            transform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime;
        }

        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, 1, 0) * Speed * Time.deltaTime;
        }
        else if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0, -1, 0) * Speed * Time.deltaTime;
        }
    }
}
