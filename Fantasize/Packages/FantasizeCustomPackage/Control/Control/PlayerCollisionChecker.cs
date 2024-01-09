using Definition;
using System.Collections;
using UnityEngine;

namespace Control
{
    public class PlayerCollisionChecker : Controller
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "M_Body":
                    DefinitionManager.Instance.iplayerInfo.SetHp(-1);

                    DefinitionManager.Instance.iplayerInfo.SetIsCanJump(true);
                    DefinitionManager.Instance.iplayerInfo.SetIsJumping(false);
                    break;
                case "Floor":
                    DefinitionManager.Instance.iplayerInfo.SetIsCanJump(true);
                    DefinitionManager.Instance.iplayerInfo.SetIsJumping(false);
                    break;
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Floor":
                    DefinitionManager.Instance.iplayerInfo.SetIsCanJump(true);
                    DefinitionManager.Instance.iplayerInfo.SetIsJumping(false);
                    break;
            }
        }

        private bool isHit = false; // �ǰ� ���¸� ��Ÿ���� �÷���
        private float hitCooldown = 1f; // �ǰ� �� ���� �ǰݱ����� ��ٿ� �ð�

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "M_Weapon" && !isHit)
            {
                StartCoroutine(HitCooldown());
                Debug.LogError("�ǰ�");
                DefinitionManager.Instance.iplayerInfo.SetHp(-1);
            }
        }

        private IEnumerator HitCooldown()
        {
            isHit = true;
            yield return new WaitForSeconds(hitCooldown);
            isHit = false;
        }
    }
}
