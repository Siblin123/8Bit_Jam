using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
[System.Serializable]


public class UnitCreater_Lin : MonoBehaviour
{
    [Header("All units that can be created")]//��� ���� ����Ʈ
    [SerializeField] List<UnitInfo> allUnitList;

    [SerializeField] float curGold;
    [SerializeField] int maxGold;
    [SerializeField] float goldGenerationRate;

    [SerializeField] UnitInfo nextUnit;
    [SerializeField] List<UnitInfo> curUnit = new List<UnitInfo>(4);

    [SerializeField] Transform unitSpawnLocation;

    public static event Action<float, int> OnGoldChanged;
    public static event Action<UnitInfo, int> UpdateUnitImg;

    private void Start()
    {
        GetNextUnit();
        GetCurUnit();
    }

    private void Update()
    {
        goldAcquisition();
    }

    // ���� �̹��� �������� ���� ��ȯ�ϴ� ��
    // Summon unit by selecting its image
    public void unitSpawn_BtnEvent(int curUnitNumber)
    {
        //curUnitNumber = ��ư�� ���� = curUnit�� ����
        //curUnitNumber = button order = curUnit order

        if (curGold >= curUnit[curUnitNumber].price)
        {
            curGold -= curUnit[curUnitNumber].price;
            OnGoldChanged?.Invoke(curGold, maxGold);
            Instantiate(curUnit[curUnitNumber].gameObject, unitSpawnLocation.position, Quaternion.identity);
            curUnit[curUnitNumber] = null;
            GetCurUnit();
        }


    }

    // nextUnit�� ���� ���� �־��ֱ�
    // Assign a random unit to nextUnit
    void GetCurUnit()
    {
        for (int i = 0; i < curUnit.Count; i++)
        {
            if (curUnit[i] == null)
            {
                curUnit[i] = nextUnit;
                UpdateUnitImg?.Invoke(curUnit[i], i);
                GetNextUnit();
            }
        }
    }


    // ���� ��� �� ���� ����Ʈ�� nextUnit �־��ֱ�
    // When using a unit, add nextUnit to the unit list
    void GetNextUnit()
    {
        //allUnitList �����ϰ� �̾Ƽ�
        int randomNum = UnityEngine.Random.Range(0, allUnitList.Count);
        nextUnit = allUnitList[randomNum];
        UpdateUnitImg?.Invoke(nextUnit, curUnit.Count);
    }


    // ��� ����
    // Gold acquisition
    void goldAcquisition()
    {
        if (curGold >= maxGold)
            return;

        curGold += Time.deltaTime * goldGenerationRate;
        OnGoldChanged?.Invoke(curGold, maxGold);
    }

}
