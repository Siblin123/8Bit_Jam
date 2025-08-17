using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.Mathematics;
[System.Serializable]


public class UnitCreater_Lin : MonoBehaviour
{

    [SerializeField] bool isPlayer;

    [Header("All units that can be created")]//��� ���� ����Ʈ
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



    // ���� �̹��� �������� ���� ��ȯ�ϴ� ��
    // Summon unit by selecting its image
    public void unitSpawn_BtnEvent(int curUnitNumber)
    {
        //curUnitNumber = ��ư�� ���� = curUnit�� ����
        //curUnitNumber = button order = curUnit order


        //���� ���� ��ȯ
        if (curUnitNumber == -1)
        {
            curUnitNumber = 0;
            isPlayer = false;
            if (curGold >= curUnit[curUnitNumber].price)
            {
                curGold -= curUnit[curUnitNumber].price;
                GameObject unit = Instantiate(curUnit[curUnitNumber].gameObject, unitSpawnLocation.position, Quaternion.identity);
                unit.GetComponent<UnitInfo>().isPlayer = isPlayer;
                curUnit[curUnitNumber] = null;
                GetCurUnit();

                if (isPlayer)
                    OnGoldChanged?.Invoke(curGold, maxGold);
            }
            return;
        }

        #region ī�� ��������� �ƴ� ������ ī�带 �÷����ϴ� ������� ����
        if (curGold >= allUnitList[curUnitNumber].price)
        {
            bool isPlayer = true;
            curGold -= allUnitList[curUnitNumber].price;
            GameObject unit = Instantiate(allUnitList[curUnitNumber].gameObject, unitSpawnLocation.position, Quaternion.identity);
            unit.GetComponent<UnitInfo>().isPlayer = isPlayer;
            if (isPlayer) OnGoldChanged?.Invoke(curGold, maxGold);
        }
        #endregion

    }

    // nextUnit�� ���� ���� �־��ֱ�
    // Assign a random unit to nextUnit
    protected void GetCurUnit()
    {
        for (int i = 0; i < curUnit.Count; i++)
        {
            if (curUnit[i] == null)
            {
                curUnit[i] = nextUnit;
                GetNextUnit();

                if (isPlayer)
                    UpdateUnitImg?.Invoke(curUnit[i], i);
            }
        }
    }


    // ���� ��� �� ���� ����Ʈ�� nextUnit �־��ֱ�
    // When using a unit, add nextUnit to the unit list
    protected void GetNextUnit()
    {
        //allUnitList �����ϰ� �̾Ƽ�
        int randomNum = UnityEngine.Random.Range(0, allUnitList.Count);
        nextUnit = allUnitList[randomNum];
        if (isPlayer)
            UpdateUnitImg?.Invoke(nextUnit, curUnit.Count);
    }


    // ��� ����
    // Gold acquisition
    protected void goldAcquisition()
    {
        if (curGold >= maxGold)
            return;

        curGold += Time.deltaTime * goldGenerationRate;
        if (isPlayer)
            OnGoldChanged?.Invoke(curGold, maxGold);
    }

}
