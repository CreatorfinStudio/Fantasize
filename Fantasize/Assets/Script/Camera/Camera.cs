using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;        // 주인공 캐릭터의 Transform 컴포넌트

    public float x = 0f;
    public float y;
    public float z;
    //public Vector3 offset = new Vector3(_x, y, z);  // 카메라와 주인공 간의 거리 및 높이 설정
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 정도

    void LateUpdate()
    {
        Vector3 offset = new Vector3(x, y, z);
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 카메라의 회전을 항상 0, -90, 0으로 고정
        transform.eulerAngles = new Vector3(0f, -90f, 0f);
    }
}
