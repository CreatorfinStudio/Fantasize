using BehaviorDesigner.Runtime.Tasks;
using Definition;
using Player;
using UnityEngine;

namespace PlayerBehavior
{
    public class Attack : Action
    {
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        public override void OnStart()
        {           
            animator = GetComponent<Animator>();
        }
        public override TaskStatus OnUpdate()
        {
            var attackType = DefinitionManager.Instance.iplayerInfo.GetAttackType();

            switch(attackType)
            {
                case AttackType.Attack:
                    animator.Play("Attack", -1);
                    break;
                case AttackType.SpecialAttack:
                    animator.Play("SpecialAttack", -1);
                    break;
            }

            PlayerService.setCurrWeaponPosi?.Invoke(attackType);

            //attackType에 따라서 애니메이션 재생하든지 파티클넣든지 등등
            return TaskStatus.Success;
        }
    }
}