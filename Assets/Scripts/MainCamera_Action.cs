using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Action : MonoBehaviour
{
    public GameObject player;

    public float followSpeed = 5f;
    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = -35f;

    Vector3 cameraPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        //Player 기준 카메라 위치
        cameraPosition.x = player.transform.position.x + offsetX; 
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);
    }
}
