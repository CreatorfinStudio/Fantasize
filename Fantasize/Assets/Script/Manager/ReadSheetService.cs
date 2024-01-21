using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Definition;
using Item;

public class ReadSheetService : MonoBehaviour
{
    public readonly string ADDRESS = "https://docs.google.com/spreadsheets/d/1pbjnBbcSnE9KC8wAdTDPMVrk7MTEApr83h4D0D4jFO4";
    public readonly string RANGE = "C5:N42";
    public readonly long SHEET_ID = 0;

    public static bool itemDataLoadDone = false;

    private void Start()
    {
        StartCoroutine(LoadData());
    }
    public string GetTSVAddress(string address, string range, long sheetID)
    {
        return $"{address}/export?format=tsv&range={range}&gid={sheetID}";
    }

    IEnumerator LoadData()
    {
        UnityWebRequest www = UnityWebRequest.Get(GetTSVAddress(ADDRESS, RANGE, SHEET_ID));
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError($"Error: {www.error}");
        }
        else
        {
            ProcessingSheetData(www.downloadHandler.text);
        }
    }

    private void ProcessingSheetData(string data)
    {
        string[] lines = data.Split('\n');
        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] rows = line.Split('\t');
                SetItemDetailInfo(rows);
            }
        }

        itemDataLoadDone = true;
    }

    /// <summary>
    /// 데이터 세팅
    /// 데이터가 없으면 값을 기본(ex.0)으로 처리
    /// </summary>
    /// <param name="data"></param>
    private void SetItemDetailInfo(string[] data)
    {
        ItemInfo itemInfo = new ItemInfo();

        itemInfo.Name = data[0];
        itemInfo.Image = Resources.Load<Sprite>(data[1]);
        if (!int.TryParse(data[2], out int price))
        {
            price = 0;
        }
        itemInfo.Price = price;
        if (!float.TryParse(data[3], out float hp))
        {
            hp = 0;
        }
        itemInfo.Hp = hp;
        if (!float.TryParse(data[4], out float maxHp))
        {
            maxHp = 0;
        }
        itemInfo.MaxHp = maxHp;
        if (!float.TryParse(data[5], out float attackDamage))
        {
            attackDamage = 0;
        }
        itemInfo.AttackDamage = attackDamage;
        if (!float.TryParse(data[6], out float attackSpeed))
        {
            attackSpeed = 0;
        }
        itemInfo.AttackSpeed = attackSpeed;
        if (!float.TryParse(data[7], out float moveSpeed))
        {
            moveSpeed = 0;
        }
        itemInfo.MoveSpeed = moveSpeed;
        if (!float.TryParse(data[8], out float specialAttackDamage))
        {
            specialAttackDamage = 0;
        }
        itemInfo.SpecialAttackDamage = specialAttackDamage;
        if (!float.TryParse(data[9], out float castingSpeed))
        {
            castingSpeed = 0;
        }
        itemInfo.CastingSpeed = castingSpeed;
        if (!ItemCalculation.TryParse(data[10], out ItemCalculation calculation))
        {
            calculation = ItemCalculation.Addition;
        }
        itemInfo.Calculation = calculation;
        if (!ItemSource.TryParse(data[11], out ItemSource itemSource))
        {
            itemSource = ItemSource.CommonItem;
        }
        itemInfo.ItemSource = itemSource;
        
        ItemService.itemInfoList.Add(itemInfo);
    }
}
