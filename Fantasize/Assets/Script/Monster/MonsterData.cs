using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    //���߿� ���� Ÿ���� ������ �ٽ� �з��Ұ�
    [Header("���� ����Ʈ")]
    [SerializeField]
    private GameObject[] monster;

    private void Awake()
    {
        var monster = Instantiate(GetRandomMonster(), this.gameObject.transform);
        var startPosi = monster.transform.position;
        startPosi.y = 0;
        monster.transform.position = startPosi;
        DefinitionManager.Instance.imonsterInfo = monster.GetComponent<IMonsterInfo>();
    }

    public GameObject GetRandomMonster()
    {
        System.Random rand = new System.Random();
        int index = rand.Next(monster.Length);
        return monster[index];
    }
}
