using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundDetector : MonoBehaviour
{
    public static SoundDetector instance;


    public bool isDetectOFF = false;
    private int g_level = 0;
    public int G_level
    {
        get { return g_level; }
        set 
        {
            if (g_level > value)
            {
                if (g_level != 3 && value == 0)  //현재레벨이 3이 아니고 전달값이 0 일때, 강제로 바꾸어준다
                {
                    g_level = value;
                    ResetPos();
                }
                else return;
            }
            else
            {
                g_level = value;
            }

            if (value == 3)
            {
                SoundPos.Clear();  //이렇게 하더라도 오류발생가능
            }
        }
    }
    private int defultLevel = 0;

    public List<Vector3> SoundPos = new List<Vector3>();

    private Enemy enemy;
    private bool isPlay_3 = false;
    private bool isPlay_2 = false;
    private bool isPlay_1 = false;

    public bool isFind = false;

    [SerializeField]
    private float detectRadius = 11f;
    public float r_DetectRadius = 11f;
    /*
    [SerializeField]
    private LayerMask layerMask; */
    private PlayerController playerController;

    private Vector3 myPos;
    public bool isOneTime = true;
    private bool isOneTime_2 = true;
    private bool isOneTime_3 = true;
    private float timer = 0f;

    public bool isLevel3End = false;
    public enum LEVEL
    {
        Level3, Level2, Level1, Level0
    }
    public LEVEL level;
    void Awake()
    {
        instance = this;
        enemy = GetComponent<Enemy>();
        ChangeLevelState(LEVEL.Level0);
        playerController = FindObjectOfType<PlayerController>();
    }

    public void OnDetect()
    {
        if (isDetectOFF) return;

        //Debug.Log("isPlay_1" + isPlay_1);
        //Debug.Log("isPlay_2" + isPlay_2);
        //Debug.Log("isPlay_3" + isPlay_3);
        //Debug.Log("현재 사운드레벨" + g_level);
        //Debug.Log("현재 사운드레벨" + level);
        //Debug.Log("인식중인 사운드개수" + SoundPos.Count);
        switch (level)
        {
            case LEVEL.Level3:
                HurryToPos();
                isPlay_3 = true;
                //Debug.Log("레벨3실행중");
                break;

            case LEVEL.Level2:
                MoveToPos();
                isPlay_2 = true;
                //Debug.Log("레벨2실행중");
                break;

            case LEVEL.Level1:
                CheckPos();
                isPlay_1 = true;
                //Debug.Log("레벨1실행중");
                break;

            case LEVEL.Level0:
                ResetPos();
                //Debug.Log("레벨0실행중");
                break;

        }
    }
    public void ChangeLevelState(LEVEL newLevel)
    {
        level = newLevel;
    }

    public void CheckLevel()
    {
        //Debug.Log("된다2");             g_level, isPlay 순으로 상위판단값이기에 이를 바꾸어야 다른 것이 바뀐다.
        if (g_level == 3)
        {
            //Debug.Log("된당ㅇㅇㅇㅇ");
            ChangeLevelState(LEVEL.Level3);
            isPlay_3 = true;
            isPlay_2 = false;
            isPlay_1 = false;
        }
        if (g_level == 2)
        {
            if (!isPlay_3)
            {
                ChangeLevelState(LEVEL.Level2);
                isPlay_2 = true;
                isPlay_1 = false;
            }
        }
        if (g_level == 1)
        {
            if (!isPlay_2 && !isPlay_3)
            {
                ChangeLevelState(LEVEL.Level1);
                isPlay_1 = true;
            }
        }
        if (g_level == 0)
        {
            if (!isPlay_2 && !isPlay_3 && !isPlay_1)
            {
                ChangeLevelState(LEVEL.Level0);
            }
        }
    }

    private void HurryToPos()
    {
        timer += Time.deltaTime;
        if (timer > 4 && isOneTime)
        {
            //Debug.Log("레벨3 가는중");
            enemy.navMeshAgent.SetDestination(SoundPos[0]);
            timer = 0f;

  
        }

        float distance = Vector3.Distance(transform.position, SoundPos[0]);
        if (distance < 1.5)
        {
            //Debug.Log("도착했다");
            isOneTime = false;
            if (GameManager.Days == 2)
            {
                SoundManager.instance.PauseSound("Toy");
                ResetHurry();
            }
            if (GameManager.Days == 3)
            {
                //Debug.Log("오르골 끄기3");
                SoundManager.instance.PauseSound("Toy");
                FindObjectOfType<Toy>().isGen = true;
                //p_collider.GetComponent<Toy>().isGen = true;
                ResetHurry();

                SoundManager.instance.PauseSound("Emergency");
                //집주인이 절규하는 목소리 재생
                if (timer >= 5)
                {
                    ResetHurry();
                }

            }
        }
    }

    public void ResetHurry()
    {
        //Debug.Log("레벨3에 도착했다");
        SoundPos.Clear();
        isPlay_3 = false;
        g_level = defultLevel;
        isLevel3End = false;
        ChangeLevelState(LEVEL.Level0);
    }
    private void MoveToPos()
    {
        //Collider[] target = Physics.OverlapSphere(myPos, r_DetectRadius, layerMask);
        float distance = Vector3.Distance(transform.position, playerController.transform.position);
        if(distance <= r_DetectRadius)
        {
            //Debug.Log("된다");
            SoundPos.Add(playerController.transform.position);
            enemy.navMeshAgent.updateRotation = false;
            enemy.transform.DOLookAt(SoundPos[0], 0.5f).OnComplete(() => enemy.navMeshAgent.updateRotation = true);

            if (isOneTime_2)
            {
                //Debug.Log("이거 안되?");
                enemy.navMeshAgent.SetDestination(SoundPos[0]);
                isOneTime_2 = false;
            }
        }

        if (isOneTime_2) return;

        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                SoundPos.Clear();
                isPlay_2 = false;
                g_level = defultLevel;
                ChangeLevelState(LEVEL.Level0);
                isOneTime_2 = true;
            }
        }
    }
    
    private void CheckPos()
    {
        float distance = Vector3.Distance(transform.position, playerController.transform.position);
        //Collider[] target = Physics.OverlapSphere(myPos, detectRadius, layerMask);
        if (distance <= detectRadius)
        {
            //Debug.Log("된다된다");
            SoundPos.Add(playerController.transform.position);
            enemy.navMeshAgent.updateRotation = false;
            enemy.transform.DOLookAt(SoundPos[0], 0.5f).OnComplete(() => enemy.navMeshAgent.updateRotation = true);

            if (isOneTime_3)
            {
                enemy.navMeshAgent.SetDestination(SoundPos[0]);
                isOneTime_3 = false;
            }
        }

        if (isOneTime_3) return;

        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                SoundPos.Clear();
                isPlay_1 = false;
                g_level = defultLevel;
                ChangeLevelState(LEVEL.Level0);
                isOneTime_3 = true;
            }
        }
    }

    public void ResetPos()
    {
        if (g_level != 3)
        {
            isPlay_1 = false;
            isPlay_2 = false;
            isPlay_3 = false;
            g_level = defultLevel;
            SoundPos.Clear();
            //Debug.Log("아무런 것도 없다");
            isOneTime = true;
        }
        else
        {
            isPlay_3 = true;
            isOneTime = true;
        }
    }

    public void OnDrawGizmos()
    {
        myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, detectRadius);
        Gizmos.DrawWireSphere(myPos, r_DetectRadius);
    }
}
