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
    [SerializeField] List<UnitData> unitDATA;
    [SerializeField] int[] upgradeCost;

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
                    return a;
                }
                break;
            }
        }
        return a;
    }

}
