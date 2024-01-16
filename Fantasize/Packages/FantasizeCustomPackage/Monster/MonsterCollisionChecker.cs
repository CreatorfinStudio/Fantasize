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
        /// 돌진 후 플레이어 방향으로 스프라이트 방향 전환
        /// 다른 곳에서 사용될 경우 메서드 위치 바꿀 것
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

        /////////////////////   충돌 관련   /////////////////////
        private float lastHitTime = 0f;
        private float hitCooldown = 0.2f; // 콜라이더 간의 충돌 대기 시간.

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
        /// 나중에 충돌 , 공격판정 좀더 세분화 시켜야함
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

                lastHitTime = Time.time; // 마지막 히트 시간 업데이트
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