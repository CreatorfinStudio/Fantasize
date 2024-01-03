using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterCollisionChecker : Monster
    {
        private CircleCollider2D checkCollider;
        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            checkCollider = GetComponent<CircleCollider2D>();
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


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
            {
                if(collision.gameObject.CompareTag("Player"))
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
            if (checkCollider.IsTouching(other) && other.CompareTag("Player"))
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