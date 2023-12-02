using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(player != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        CameraMovement();
    }

    void CameraMovement()
    {
        float posX=player.transform.position.x;
        float posZ=player.transform.position.z-25;

        transform.position=new Vector3(posX,transform.position.y,posZ);

    }
}
