using Definition;
using System.Collections;
using UnityEngine;

namespace Monster
{
    public class MonsterCollisionChecker : MonoBehaviour
    {
        [SerializeField]
        private CircleCollider2D canSeeCollider;
        [SerializeField]
        private CapsuleCollider2D bodyCollider;


        private void Update()
        {
            if (DefinitionManager.Instance.imonsterInfo != null &&
                DefinitionManager.Instance.player != null)
            {
                if (DefinitionManager.Instance.imonsterInfo.GetIsDirectionCheck())
                    SetSpriteFlipX(DefinitionManager.Instance.player.transform);
            }
        }

        /// <summary>
        /// ���� �� �÷��̾� �������� ��������Ʈ ���� ��ȯ
        /// �ٸ� ������ ���� ��� �޼��� ��ġ �ٲ� ��
        /// </summary>
        /// <param name="trans"></param>
        protected void SetSpriteFlipX(Transform trans)
        {
            if (trans != null)
            {
                Vector3 currentScale = transform.localScale;
                currentScale.x = trans.position.x < transform.position.x ? Mathf.Abs(currentScale.x) : -Mathf.Abs(currentScale.x);
                this.transform.localScale = currentScale;
            }
        }

        /////////////////////   �浹 ����   /////////////////////
        private float lastHitTime = 0f;
        private float hitCooldown = 0.2f; // �ݶ��̴� ���� �浹 ��� �ð�.

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.CompareTag("Player"))
                    DefinitionManager.Instance.imonsterInfo.SetIsCollisionPlayer(true);
                DefinitionManager.Instance.imonsterInfo?.SetIsCanRush(false);
                StartCoroutine(ReRushToWall());
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                DefinitionManager.Instance.imonsterInfo.SetIsCollisionPlayer(false);
        }

        IEnumerator ReRushToWall()
        {
            yield return new WaitForSeconds(.1f);

            DefinitionManager.Instance.imonsterInfo?.SetIsCanRush(true);
            DefinitionManager.Instance.imonsterInfo.SetIsDirectionCheck(true);
        }

        /// <summary>
        /// ���߿� �浹 , �������� ���� ����ȭ ���Ѿ���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            var attackType = DefinitionManager.Instance.iplayerInfo?.GetAttackType();

            if (canSeeCollider.IsTouching(other) && other.CompareTag("Player"))
            {
                DefinitionManager.Instance.imonsterInfo?.SetIsCanAttack(true);
            }
            if (Time.time - lastHitTime > hitCooldown && bodyCollider.IsTouching(other) && other.CompareTag("P_Weapon"))
            {
                if (attackType.Equals(AttackType.Attack) || attackType.Equals(AttackType.AirAttack))
                    DefinitionManager.Instance.imonsterInfo?.SetHp(-DefinitionManager.Instance.iplayerInfo.GetAttackPower());
                else if (attackType.Equals(AttackType.SpecialAttack))
                    DefinitionManager.Instance.imonsterInfo?.SetHp(-DefinitionManager.Instance.iplayerInfo.GetSpecialAttackPower());

                lastHitTime = Time.time; // ������ ��Ʈ �ð� ������Ʈ
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (canSeeCollider.IsTouching(collision) && collision.CompareTag("Player"))
            {
                DefinitionManager.Instance.imonsterInfo?.SetIsCanAttack(true);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                DefinitionManager.Instance.imonsterInfo?.SetIsCanAttack(false);
            }
        }

    }
}