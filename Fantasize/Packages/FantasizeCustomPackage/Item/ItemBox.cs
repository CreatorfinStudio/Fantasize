using Definition;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Item
{
    public class ItemBox : Item
    {
        private Animator animator;
        [Header("박스 털기 가능 횟수")]
        [SerializeField]
        private int getTryCount;
        [Header("F키")]
        [SerializeField]
        private GameObject openKey;
        [Header("아이템")]
        [SerializeField]
        private GameObject item;

        protected override void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// 박스 털기 가능한 상태인지? (획득 횟수)
        /// </summary>
        /// <returns></returns>
        private bool CanGetTry() => getTryCount > 0;
        
        private void Update()
        {
            //F키가 활성화 되어있을때 누르면
            if (openKey.activeSelf && Input.GetKey(KeyCode.F))
            {
                AnimationManager.TriggerAnim(animator,"Open");
                item.SetActive(true);
                getTryCount--;
                if (getTryCount <= 0)
                    openKey.SetActive(false);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if(IsPlayer(collision.gameObject) && CanGetTry())
            {
                openKey.SetActive(true);
            }
        }
        protected override void OnTriggerExit2D(Collider2D collision)
        {
            base.OnTriggerExit2D(collision);
            if (IsPlayer(collision.gameObject))
            {
                openKey.SetActive(false);
            }
        }
    }
}