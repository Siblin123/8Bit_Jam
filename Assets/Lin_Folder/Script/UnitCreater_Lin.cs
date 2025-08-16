using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
[System.Serializable]


public class UnitCreater_Lin : MonoBehaviour
{

    [SerializeField] bool isPlayer;

    [Header("All units that can be created")]//모든 유닛 리스트
    [SerializeField] protected List<UnitInfo> allUnitList;

    [SerializeField] float curGold;
    [SerializeField] int maxGold;
    [SerializeField] float goldGenerationRate;

    [SerializeField] UnitInfo nextUnit;
    [SerializeField] List<UnitInfo> curUnit = new List<UnitInfo>(4);

    [SerializeField] Transform unitSpawnLocation;

    public static event Action<float, int> OnGoldChanged;
    public static event Action<UnitInfo, int> UpdateUnitImg;

    public virtual void Start()
    {
        GetNextUnit();
        GetCurUnit();
    }

    public virtual void Update()
    {
        goldAcquisition();
    }

    public bool GoldCheck(int cost)
    {
        if (cost <= curGold)
        {
            curGold -= cost;
            if (isPlayer)
                OnGoldChanged?.Invoke(curGold, maxGold);
            return true;
        }
        else
        {
            return false;
        }
    }



    // 유닛 이미지 선택으로 유닛 소환하는 것
    // Summon unit by selecting its image
    public void unitSpawn_BtnEvent(int curUnitNumber)
    {
        //curUnitNumber = 버튼의 순서 = curUnit의 순서
        //curUnitNumber = button order = curUnit order

        if (curGold >= curUnit[curUnitNumber].price)
        {
            curGold -= curUnit[curUnitNumber].price;
            Instantiate(curUnit[curUnitNumber].gameObject, unitSpawnLocation.position, Quaternion.identity);
            curUnit[curUnitNumber] = null;
            GetCurUnit();
           
            if(isPlayer)
                OnGoldChanged?.Invoke(curGold, maxGold);
        }


    }

    // nextUnit에 랜덤 유닛 넣어주기
    // Assign a random unit to nextUnit
    protected void GetCurUnit()
    {
        for (int i = 0; i < curUnit.Count; i++)
        {
            if (curUnit[i] == null)
            {
                curUnit[i] = nextUnit;
                GetNextUnit();

                if(isPlayer)
                    UpdateUnitImg?.Invoke(curUnit[i], i);
            }
        }
    }


    // 유닛 사용 시 유닛 리스트에 nextUnit 넣어주기
    // When using a unit, add nextUnit to the unit list
    protected void GetNextUnit()
    {
        //allUnitList 랜덤하게 뽑아서
        int randomNum = UnityEngine.Random.Range(0, allUnitList.Count);
        nextUnit = allUnitList[randomNum];
        if(isPlayer)
            UpdateUnitImg?.Invoke(nextUnit, curUnit.Count);
    }


    // 골드 수급
    // Gold acquisition
    protected void goldAcquisition()
    {
        if (curGold >= maxGold)
            return;

        curGold += Time.deltaTime * goldGenerationRate;
        if(isPlayer)
            OnGoldChanged?.Invoke(curGold, maxGold);
    }

}
