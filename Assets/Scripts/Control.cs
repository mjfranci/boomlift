using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private GameObject camera;
    private GameObject platform;
    private Vector3 position;
    private Vector3 rotation;
    private float x;
    private float y;
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        platform = GameObject.Find("Platform_DOOR");
        position = new Vector3(-0.7f,-0.7f,0);
        rotation = new Vector3(270,0,90);
    }

    // Update is called once per frame
    void Update()
    {
        //position.x = platform.transform.position.x;
        //position.y = platform.transform.position.y + 0.75f;
        //position.z = platform.transform.position.z + 0.95f;

        //camera.transform.position = position;
        camera.transform.SetParent(platform.transform, false);

        
        // #### X ####
        if(Input.GetKeyDown(KeyCode.Y))
        {
            rotation.x = rotation.x + 5f;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            rotation.x = rotation.x - 5f;
        }
        // #### Y ####
        if (Input.GetKeyDown(KeyCode.U))
        {
            rotation.y = rotation.y + 5f;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            rotation.y = rotation.y - 5f;
        }
        // #### Z ####
        if (Input.GetKeyDown(KeyCode.I))
        {
            rotation.z = rotation.z + 5f;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            rotation.z = rotation.z - 5f;
        }

        camera.transform.localPosition = position;
        camera.transform.localEulerAngles = rotation;

        string message = "Vector: " + rotation.ToString() + "   ;   Transform: " + camera.transform.localEulerAngles.ToString();
        UnityEngine.Debug.Log(message);
    }
}
