using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // TextMeshPro를 사용하기 위해 추가

[System.Serializable]
public class UnitData
{
    public string unitName;
    public int curLV = 1;
}

public class UnitUpgradeManager : MonoBehaviour
{
    public static UnitUpgradeManager instance;

    // 인스펙터에서 할당할 TextMeshPro 변수 목록
    // 각 유닛의 레벨을 표시할 TMP 객체를 넣어주세요.
    [SerializeField] private List<TextMeshProUGUI> unitLevelTexts;
    [SerializeField] private Image FlowerImage;
    [SerializeField] private Sprite[] FlowerUpgrades;

    [SerializeField] UnitCreater_Lin unitCreater;
    public List<UnitData> unitDATA;
    [SerializeField] int[] upgradeCost;

    public int curFlowerLV = 1;
    [SerializeField] GameObject[] Flower;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpgradeUnit(int unitNum)
    {
        if (unitDATA[unitNum].curLV >= 3) // 레벨이 3 이상이면 더 이상 업그레이드하지 않음
        {
            // 텍스트를 "MAX" 등으로 변경하거나, 아무 것도 하지 않거나 선택 가능
            return;
        }

        // 1. 유닛의 curLV(임시 데이터)를 1 증가시킵니다.
        unitDATA[unitNum].curLV++;

        // 2. 증가된 레벨을 TextMeshProUGUI에 적용합니다.
        // unitLevelTexts의 인덱스(unitNum)가 unitDATA의 인덱스와 같다고 가정
        if (unitNum < unitLevelTexts.Count && unitLevelTexts[unitNum] != null)
        {
            unitLevelTexts[unitNum].text = "LV." + unitDATA[unitNum].curLV;
        }


        // 모든 유닛을 순회하며 레벨을 확인합니다.
        foreach (UnitData unit in unitDATA)
        {
            if (unit.curLV < curFlowerLV + 1)
            {
                return; // 하나라도 레벨이 부족하면 더 이상 확인할 필요가 없습니다.
            }
           
        }
        Flower[curFlowerLV - 1].SetActive(false);
        curFlowerLV++;
        Flower[curFlowerLV - 1].SetActive(true);
        unitLevelTexts[4].text = "LV." + curFlowerLV.ToString();
        FlowerImage.sprite = FlowerUpgrades[curFlowerLV - 1];
    }
}