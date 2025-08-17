using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public string unitName;
    public int price;
    public Sprite unitImage;

    public GameObject Player_Unit;
    public GameObject Enemy_Unit;

    [SerializeField] protected float hp;
    [SerializeField] protected float speed;
    [SerializeField] protected float attackPower;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float curAttackSpeed;
    [SerializeField] float attackRange;
    // 플레이어의 가능한 상태들을 열거형으로 정의합니다.

    [SerializeField] AudioSource AudioSource;


    public enum PlayerState
    {
        Idle,
        Walk,
        Attack,
        Die,
    }
    protected Animator ani;
    // 현재 플레이어의 상태를 저장할 변수
    public PlayerState currentState;


    public bool isPlayer;
    [SerializeField] UnitInfo curTartget;
    RaycastHit2D[] hits;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask EnemyLayer;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
    }
    private void Start()
    {
        if (isPlayer)
        {
            if(!GetComponent<Tower>())
                transform.localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            gameObject.layer = LayerMask.NameToLayer("Player");
            targetLayer = EnemyLayer;
            if (Player_Unit != null && Enemy_Unit != null)
            {
                Player_Unit.SetActive(true);
                Enemy_Unit.SetActive(false);
            }
        }
        else
        {
            if(Player_Unit!=null && Enemy_Unit!=null)
            {
                Player_Unit.SetActive(false);
                Enemy_Unit.SetActive(true);
            }
          
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            targetLayer = playerLayer;
        }


        //upgrade
        for (int i=0; i<UnitUpgradeManager.instance.unitDATA.Count; i++)
        {
           if (UnitUpgradeManager.instance.unitDATA[i].unitName == unitName)
            {
                switch(UnitUpgradeManager.instance.unitDATA[i].curLV)
                {
                    case 2:
                        attackPower *= (1 + 0.1f);
                        break;

                    case 3:
                        attackPower *= (1 + 0.1f);
                        break;
                }
            }
        }
       

    }


    private void Update()
    {
        FSD();
        FindTarget();
    }

    void FindTarget()
    {
        hits = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero, 0f, targetLayer);

        // 감지된 콜라이더가 있다면
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                curTartget = hits[i].transform.GetComponent<UnitInfo>();
                break;
            }
        }
        else
        {
        }
    }

    void FSD()
    {
        if (hp <= 0)
        {
            currentState = PlayerState.Die;
        }

        if (curTartget == null && currentState != PlayerState.Die)
        {
            currentState = PlayerState.Walk;
        }
        if (curTartget != null && currentState != PlayerState.Die)
        {
            curAttackSpeed += Time.deltaTime;

            if (curAttackSpeed > attackSpeed)
            {
                currentState = PlayerState.Attack;
            }
            else
            {
                currentState = PlayerState.Idle;
            }
        }

        switch (currentState)
        {
            case PlayerState.Idle:
                ani.Play("Idle");
                break;

            case PlayerState.Walk:
                ani.Play("Walk");

                if (isPlayer)
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                else
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;

            case PlayerState.Attack:
                ani.Play("Attack");

                break;

            case PlayerState.Die:
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                ani.Play("Death");
                break;
        }




    }

    public void Attack_Event()
    {
        if (curTartget != null)
        {
            curTartget.GetDamege(attackPower);

        }
            curAttackSpeed = 0;
    }

    public void RageAttack_Event()
    {
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].transform.GetComponent<UnitInfo>().GetDamege(attackPower);
        }
        curAttackSpeed = 0;
    }

    public virtual void GetDamege(float damege)
    {
        hp -= damege;
    }

    public void DestroyUnit_Event()
    {
        Destroy(gameObject);
    }


    //Sound
    public void PlaySound_Event(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }

    private void OnDrawGizmosSelected()
    {
        // 1. 기즈모 색상 설정: 원하는 색상으로 설정하여 시각적으로 구분하기 쉽게 만듭니다.
        if (isPlayer)
        {

            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;

        }

        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
