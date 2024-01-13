using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public interface IItemProcessing
    {
        /// <summary>
        /// ������ ���� ����
        /// </summary>
        //public void SaveSlotItem();

        /// <summary>
        /// ��Ƽ�� ������ ���� ���� ����
        /// </summary>
        public void SetActiveItemInfo();

        public string GetSlotItemName();

        /// <summary>
        /// ������ ���ͷ��� �������� �ִ��� ����
        /// </summary>
        /// <returns></returns>
        public bool IsUseItem();

        /// <summary>
        /// ���Կ� �ִ� ���ͷ��� ������ ���
        /// </summary>
        public void UseItem();
    }
}
