using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float turnSpeed = 30f; // ���콺 ȸ�� �ӵ�    
    float xRotate; // X�� ȸ����(ī�޶� �� �Ʒ� ����)
    public float moveSpeed = 2.0f; // �̵� �ӵ�


    private void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            Move();
        }
    }

    private void Move()
    {
        // �¿�� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� �¿�� ȸ���� �� ���
        float yRotateSize = Input.GetAxis("Mouse X") * turnSpeed;

        // ���� y�� ȸ������ ����
        float yRotate = transform.eulerAngles.y + yRotateSize;

        // ���Ʒ��� ������ ���콺�� �̵��� * �ӵ��� ���� ī�޶� ȸ���� = ���� ȸ����
        float xRotateSize = -Input.GetAxis("Mouse Y") * turnSpeed;

        // ���Ʒ� ȸ������ ����, -45�� ~ 80���� ���� (-45:�ϴù���, 80:�ٴڹ���)
        xRotate = xRotate + xRotateSize;

        // ī�޶� ȸ��
        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);


        //  Ű���忡 ���� �̵��� ����
        Vector3 move = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");


        // �̵�
        transform.position += move * moveSpeed * Time.deltaTime;
    }

}
