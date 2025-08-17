using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData 
{
   public string unitName;
    public int curLV =1;
}

public class UnitUpgradeManager : MonoBehaviour
{
    public static UnitUpgradeManager instance;
    [SerializeField] UnitCreater_Lin unitCreater; 
    public List<UnitData> unitDATA;
    [SerializeField] int[] upgradeCost;

    public int curFlowerLV=1;
    [SerializeField] GameObject[] Flower;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int [] UpgradeUnit(string _unitName)
    {

     
        int[] a = { -1, -1 };
        for (int i=0; i<unitDATA.Count; i++)
        {
            if(unitDATA[i].unitName == _unitName && unitDATA[i].curLV != 3)
            {
                if(unitCreater.GoldCheck(upgradeCost[unitDATA[i].curLV]))
                {
                    unitDATA[i].curLV++;
                    a[0] = i;
                    a[1] = upgradeCost[unitDATA[i].curLV];

                    //꽃 이미지 변경
                    for (int j = 0; j < unitDATA.Count; j++)
                    {
                        if (unitDATA[j].curLV == curFlowerLV + 1)
                        {
                            if (j == unitDATA.Count - 1)
                            {
                                Flower[curFlowerLV - 1].SetActive(false);
                                curFlowerLV++;
                                Flower[curFlowerLV - 1].SetActive(true);

                            }
                        }
                    }

                    return a;
                }
                break;
            }
        }

        
      


        return a;
    }

}
