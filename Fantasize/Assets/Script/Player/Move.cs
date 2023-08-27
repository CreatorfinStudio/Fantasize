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
            animator.SetBool("Walk", true); // Walk 애니메이션 재생
        }
        else
        {
            animator.SetBool("Walk", false); // Walk 애니메이션 정지
        }


        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));

        Vector3 lookAtDirection = mousePosition - transform.position;
        lookAtDirection.y = 0f; // 캐릭터는 y축 회전하지 않으므로 y 좌표값은 0으로 설정
        Quaternion targetRotation = Quaternion.LookRotation(lookAtDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F) && checkInteractioning)
            Debug.Log("상호작용 On");
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

