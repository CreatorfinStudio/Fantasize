using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class MouseRotation : Controller
    {
        [Header("ȸ�� �ӵ�")]
        public float rotationSpeed = 3f;
        void Update()
        {
            Rotation();
        }

        private void Rotation()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

            Vector3 lookAtDirection = mousePosition - transform.position;
            lookAtDirection.y = 0f; // ĳ���ʹ� y�� ȸ������ �����Ƿ� y ��ǥ���� 0���� ����
            Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}