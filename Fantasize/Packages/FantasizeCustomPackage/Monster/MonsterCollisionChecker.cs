using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MonsterCollisionChecker : Monster
    {
        [SerializeField]
        private CircleCollider2D checkCollider;
        private SpriteRenderer spriteRenderer;


        private void Awake()
        {
            checkCollider = GetComponent<CircleCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
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
                bool isObjectALeft = trans.position.x < transform.position.x;
                spriteRenderer.flipX = isObjectALeft;
            }
        }

        /////////////////////   충돌 관련   /////////////////////
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                imonsterInfo?.SetIsCanRush(false);
                //SetSpriteFlipX(DefinitionManager.Instance.player.transform);
                StartCoroutine(ReRushToWall());
            }
        }

        IEnumerator ReRushToWall()
        {
            yield return new WaitForSeconds(1f);
            SetSpriteFlipX(DefinitionManager.Instance.player.transform);
            imonsterInfo?.SetIsCanRush(true);
        }


        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //    if (collision.gameObject.CompareTag("Wall"))
        //    {
        //        Debug.Log("벽 끝---");

        //        imonsterInfo?.SetIsCanRush(true);

        //    }
        //}

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (checkCollider.IsTouching(other) && other.CompareTag("Player"))
            {
                imonsterInfo?.SetCanSeePlayer(true);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                imonsterInfo?.SetCanSeePlayer(false);
            }
        }
    }
}