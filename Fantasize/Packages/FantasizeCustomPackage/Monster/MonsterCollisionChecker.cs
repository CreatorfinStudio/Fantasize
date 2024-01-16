using Definition;
using System.Collections;
using UnityEngine;

namespace Monster
{
    public class MonsterCollisionChecker : Monster
    {
        [SerializeField]
        private CircleCollider2D canSeeCollider;
        [SerializeField]
        private CapsuleCollider2D bodyCollider;
        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (DefinitionManager.Instance.imonsterInfo.GetIsSpriteCheck())
                SetSpriteFlipX(DefinitionManager.Instance.player.transform);
        }

        /// <summary>
        /// ���� �� �÷��̾� �������� ��������Ʈ ���� ��ȯ
        /// �ٸ� ������ ���� ��� �޼��� ��ġ �ٲ� ��
        /// </summary>
        /// <param name="trans"></param>
        protected override void SetSpriteFlipX(Transform trans)
        {
            if (trans != null && spriteRenderer != null)
            {
                bool isObjectLeft = trans.position.x < transform.position.x;
                spriteRenderer.flipX = isObjectLeft;
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
                    imonsterInfo.SetIsCollisionPlayer(true);
                imonsterInfo?.SetIsCanRush(false);
                StartCoroutine(ReRushToWall());
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
                imonsterInfo.SetIsCollisionPlayer(false);
        }

        IEnumerator ReRushToWall()
        {
            yield return new WaitForSeconds(.1f);

            imonsterInfo?.SetIsCanRush(true);
            DefinitionManager.Instance.imonsterInfo.SetIsSpriteCheck(true);
        }

        /// <summary>
        /// ���߿� �浹 , �������� ���� ����ȭ ���Ѿ���
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            var attackType = DefinitionManager.Instance.iplayerInfo.GetAttackType();

            if (canSeeCollider.IsTouching(other) && other.CompareTag("Player"))
            {
                imonsterInfo?.SetIsCanAttack(true);
            }
            if (Time.time - lastHitTime > hitCooldown && bodyCollider.IsTouching(other) && other.CompareTag("P_Weapon"))
            {
                if(attackType.Equals(AttackType.Attack))
                    imonsterInfo?.SetHp(-DefinitionManager.Instance.iplayerInfo.GetAttackPower());
                else if(attackType.Equals(AttackType.SpecialAttack))
                    imonsterInfo?.SetHp(-DefinitionManager.Instance.iplayerInfo.GetSpecialAttackPower());

                lastHitTime = Time.time; // ������ ��Ʈ �ð� ������Ʈ
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (canSeeCollider.IsTouching(collision) && collision.CompareTag("Player"))
            {
                imonsterInfo?.SetIsCanAttack(true);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                imonsterInfo?.SetIsCanAttack(false);
            }
        }

    }
}