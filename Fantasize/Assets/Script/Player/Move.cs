using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float rotationSpeed = 3f;
    private Animator animator;

    private bool checkInteractioning = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        if (moveDirection.magnitude > 0.1f)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            animator.SetBool("Walk", true); // Walk �ִϸ��̼� ���
        }
        else
        {
            animator.SetBool("Walk", false); // Walk �ִϸ��̼� ����
        }


        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

        Vector3 lookAtDirection = mousePosition - transform.position;
        lookAtDirection.y = 0f; // ĳ���ʹ� y�� ȸ������ �����Ƿ� y ��ǥ���� 0���� ����
        Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F) && checkInteractioning)
            Debug.Log("��ȣ�ۿ� On");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("IntertactionCube"))
        {
            checkInteractioning = true;     
        }
        else
            checkInteractioning = false;
    }
}

