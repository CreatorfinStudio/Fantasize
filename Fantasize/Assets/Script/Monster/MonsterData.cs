using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterData : MonoBehaviour
{
    //나중에 몬스터 타입이 나뉘면 다시 분류할것
    [Header("몬스터 리스트")]
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
