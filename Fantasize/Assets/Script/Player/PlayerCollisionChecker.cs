using Definition;
using System.Collections;
using UnityEngine;

namespace Control
{
    public class PlayerCollisionChecker : Controller
    {
        [SerializeField]
        private BoxCollider2D floorCheckCollider;

        ///테스트용 _ 무적
        public bool invincibilit = false;

        public void Invincibilit() => invincibilit = !invincibilit;
                

        /// <summary>
        /// 점프 가능한 상태로 변환
        /// </summary>
        private void CanBeJump()
        {
            DefinitionManager.Instance.iplayerInfo.SetIsCanJump(true);
            DefinitionManager.Instance.iplayerInfo.SetIsJumping(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!invincibilit && !isHit)
            {
                switch (collision.gameObject.tag)
                {
                    case "M_Body":
                        StartCoroutine(HitCooldown());
                        DefinitionManager.Instance.iplayerInfo.SetHp(-DefinitionManager.Instance.imonsterInfo.GetAttackPower());
                        CanBeJump();
                        break;
                    case "Floor":
                        CanBeJump();
                        break;
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Floor":
                    CanBeJump();
                    break;
            }
        }

        private bool isHit = false; // 피격 상태를 나타내는 플래그
        private float hitCooldown = 1f; // 피격 후 다음 피격까지의 쿨다운 시간

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!invincibilit)
            {
                if (collision.tag == "M_Weapon" && !isHit)
                {
                    Attacked();
                }
            }
        }

        private void Attacked()
        {
            StartCoroutine(HitCooldown());
            DefinitionManager.Instance.iplayerInfo.SetHp(-DefinitionManager.Instance.imonsterInfo.GetAttackPower());
        }
        private IEnumerator HitCooldown()
        {
            isHit = true;
            yield return new WaitForSeconds(hitCooldown);
            isHit = false;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (floorCheckCollider.IsTouching(collision) && collision.CompareTag("Floor"))
            {
                CanBeJump();
            }
        }
    }
}
