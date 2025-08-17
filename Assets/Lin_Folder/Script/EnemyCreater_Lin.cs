using UnityEngine;

public class EnemyCreater_Lin : UnitCreater_Lin
{
    public override void Start()
    {
        base.Start();

    }

    public override void Update()
    {
        goldAcquisition();
        unitSpawn_BtnEvent(-1);
       
    }

}
