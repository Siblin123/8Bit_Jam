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

    // 단순히 UI를 그려주는 용도로만 사용하세요
    // Use it only for drawing UI.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //골드 슬라이더바 UI 업데이트
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


    //사용가능한 유닛의 이미지 업데이트
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
            //UnitUpgradeManager의 배열값으로 0 <- 텍스트[순서] , 1 <- 다음 코스트 
            UpgradeCost[num[0]].text = num[1].ToString();
        }
    }
   
}
