using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float turnSpeed = 30f; // 마우스 회전 속도    
    float xRotate; // X축 회전량(카메라 위 아래 방향)
    public float moveSpeed = 2.0f; // 이동 속도


    private void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            Move();
        }
    }

    private void Move()
    {
        // 좌우로 움직인 마우스의 이동량 * 속도에 따라 카메라가 좌우로 회전할 양 계산
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;

        // 현재 y축 회전값에 더함
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // 위아래로 움직인 마우스의 이동량 * 속도에 따라 카메라 회전량 = 실제 회전량
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;

        // 위아래 회전량을 더함, -45도 ~ 80도로 제한 (-45:하늘방향, 80:바닥방향)
        xRotate = xRotate + xRotateSize;

        // 카메라 회전
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);


        //  키보드에 따른 이동량 측정
        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");


        // 이동
        transform.position += move * moveSpeed * Time.deltaTime;
    }

}
