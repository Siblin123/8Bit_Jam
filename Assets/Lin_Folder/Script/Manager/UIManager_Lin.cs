using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class UnitCreation_Img
{
    public Image unitImage;
    public TextMeshProUGUI unitPrice;
}

public class UIManager_Lin : MonoBehaviour
{
    [Header("gold")]
    [SerializeField] Slider goldSlider;
    [SerializeField] TextMeshProUGUI GoldText;

    [SerializeField] List<UnitCreation_Img> unitCreation_Image;

    [Header("Upgrade")]
    [SerializeField] Animator upgradeAni;
    [SerializeField] TextMeshProUGUI [] UpgradeCost;
    private void OnEnable()
    {
        UnitCreater_Lin.OnGoldChanged += UpdateGoldSlider;
        UnitCreater_Lin.UpdateUnitImg += UpdateUnitImg;
    }


    private void OnDisable()
    {
        UnitCreater_Lin.OnGoldChanged -= UpdateGoldSlider;
        UnitCreater_Lin.UpdateUnitImg -= UpdateUnitImg;
    }

    // �ܼ��� UI�� �׷��ִ� �뵵�θ� ����ϼ���
    // Use it only for drawing UI.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��� �����̴��� UI ������Ʈ
    //Gold slider bar UI update
    void UpdateGoldSlider(float newGoldValue , int maxValue)
    {
        if (goldSlider != null)
        {
            goldSlider.value = newGoldValue;
            goldSlider.maxValue = maxValue;
            GoldText.text = ((int)newGoldValue).ToString();
        }
    }


    //��밡���� ������ �̹��� ������Ʈ
    //Update images of available units
    public void UpdateUnitImg(UnitInfo unit, int num)
    {
        unitCreation_Image[num].unitImage.sprite = unit.unitImage;
        unitCreation_Image[num].unitPrice.text = unit.price.ToString();
    }
        
    public void OpenClose_Upgrade_BtnEvent()
    {
        if (upgradeAni.GetCurrentAnimatorStateInfo(0).IsName("CloseUpgrade") || upgradeAni.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            upgradeAni.Play("OpenUpgrade");
        }
        else
        {
            upgradeAni.Play("CloseUpgrade");
        }
    }


    public void UpgradeUnit(string unitName)
    {
        int [] num= UnitUpgradeManager.instance.UpgradeUnit(unitName);
        if(num[0] != -1)
        {
            //UnitUpgradeManager�� �迭������ 0 <- �ؽ�Ʈ[����] , 1 <- ���� �ڽ�Ʈ 
            UpgradeCost[num[0]].text = num[1].ToString();
        }
    }
   
}
