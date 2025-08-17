using UnityEngine;

public class Tower : UnitInfo
{
    [SerializeField] float healTimer;

    [SerializeField] GameObject GameOver_panel;
    [SerializeField] GameObject GameClear_panel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp > 20)
            healTimer += Time.deltaTime;
        if(healTimer>10)
        {
            healTimer -= 10;
            hp += UnitUpgradeManager.instance.curFlowerLV;
        }
    }

    public override void GetDamege(float damege)
    {
        base.GetDamege(damege);
        
        if(hp<=0)
        {
            ani.Play("Death");

        }
        else
        {
            ani.Play("Hit");

        }
    }

    public void GameOver()
    {
        GameOver_panel.SetActive(true);
    }

    public void GameClear()
    {
        GameClear_panel.SetActive(true);

    }
}
