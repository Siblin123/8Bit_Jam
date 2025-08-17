using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // TextMeshPro�� ����ϱ� ���� �߰�

[System.Serializable]
public class UnitData
{
    public string unitName;
    public int curLV = 1;
}

public class UnitUpgradeManager : MonoBehaviour
{
    public static UnitUpgradeManager instance;

    // �ν����Ϳ��� �Ҵ��� TextMeshPro ���� ���
    // �� ������ ������ ǥ���� TMP ��ü�� �־��ּ���.
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
        if (unitDATA[unitNum].curLV >= 3) // ������ 3 �̻��̸� �� �̻� ���׷��̵����� ����
        {
            // �ؽ�Ʈ�� "MAX" ������ �����ϰų�, �ƹ� �͵� ���� �ʰų� ���� ����
            return;
        }

        // 1. ������ curLV(�ӽ� ������)�� 1 ������ŵ�ϴ�.
        unitDATA[unitNum].curLV++;

        // 2. ������ ������ TextMeshProUGUI�� �����մϴ�.
        // unitLevelTexts�� �ε���(unitNum)�� unitDATA�� �ε����� ���ٰ� ����
        if (unitNum < unitLevelTexts.Count && unitLevelTexts[unitNum] != null)
        {
            unitLevelTexts[unitNum].text = "LV." + unitDATA[unitNum].curLV;
        }


        // ��� ������ ��ȸ�ϸ� ������ Ȯ���մϴ�.
        foreach (UnitData unit in unitDATA)
        {
            if (unit.curLV < curFlowerLV + 1)
            {
                return; // �ϳ��� ������ �����ϸ� �� �̻� Ȯ���� �ʿ䰡 �����ϴ�.
            }
           
        }
        Flower[curFlowerLV - 1].SetActive(false);
        curFlowerLV++;
        Flower[curFlowerLV - 1].SetActive(true);
        unitLevelTexts[4].text = "LV." + curFlowerLV.ToString();
        FlowerImage.sprite = FlowerUpgrades[curFlowerLV - 1];
    }
}