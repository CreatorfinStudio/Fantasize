using Definition;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Item
{
    public class ItemBox : Item
    {
        private Animator animator;
        [Header("�ڽ� �б� ���� Ƚ��")]
        [SerializeField]
        private int getTryCount;
        [Header("FŰ")]
        [SerializeField]
        private GameObject openKey;
        [Header("������")]
        [SerializeField]
        private GameObject item;

        protected override void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// �ڽ� �б� ������ ��������? (ȹ�� Ƚ��)
        /// </summary>
        /// <returns></returns>
        private bool CanGetTry() => getTryCount > 0;
        
        private void Update()
        {
            //FŰ�� Ȱ��ȭ �Ǿ������� ������
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