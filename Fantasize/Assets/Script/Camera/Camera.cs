using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;        // ���ΰ� ĳ������ Transform ������Ʈ

    public float x = 0f;
    public float y;
    public float z;
    public float smoothSpeed = 0.125f; // ī�޶� �̵��� �ε巯�� ����

    void LateUpdate()
    {
        Vector3 offset = new Vector3(x, y, z);
        if (target != null)
            transform.position = target.position + offset;
        //    Vector3 offset = new Vector3(x, y, z);
        //    Vector3 desiredPosition = target.position + offset;
        //    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //    transform.position = smoothedPosition;
        //    transform.eulerAngles = new Vector3(0f, -90f, 0f);
    }
}
