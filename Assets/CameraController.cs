using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    public TilesController tilesController;

    private float  movementSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        
    }
    public void CameraMovement()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.fieldOfView -= 5f;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.fieldOfView += 5f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);
    }
}
