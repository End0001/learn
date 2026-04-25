using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float offsetZ = 4.5f;
    private float smoothing = 2f;//相机平滑位移时间
    private Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos=FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        //摄像机位置
        Vector3 targetPosition = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z-offsetZ);
        //平滑移动
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }
}
