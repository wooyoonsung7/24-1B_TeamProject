using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundDetector : MonoBehaviour
{
    public static SoundDetector instance;

    public int g_level = 0;
    private int defultLevel = 0;

    public List<Vector3> SoundPos = new List<Vector3>();

    private Enemy enemy;
    private bool isPlay_3 = false;
    private bool isPlay_2 = false;
    private bool isPlay_1 = false;

    bool isHurry = true;
    private bool isFind = false;

    [SerializeField]
    private float detectRadius = 1f;
    [SerializeField]
    private LayerMask layerMask;

    private Vector3 myPos;

    private enum LEVEL
    {
        Level3, Level2, Level1, Level0
    }
    private LEVEL level;
    void Awake()
    {
        instance = this;
        enemy = GetComponent<Enemy>();
        ChangeState(LEVEL.Level0);
    }

    void Update()
    {
        CheckLevel();
        OnDetect();
    }

    public void OnDetect()
    {
        switch (level)
        {
            case LEVEL.Level3:
                HurryToPos();
                isPlay_3 = true;
                break;

            case LEVEL.Level2:
                MoveToPos();
                isPlay_2 = true;
                break;

            case LEVEL.Level1:
                CheckPos();
                break;

            case LEVEL.Level0:
                ResetPos();
                break;

        }
    }
    private void ChangeState(LEVEL newLevel)
    {
        level = newLevel;
    }

    public void CheckLevel()
    {
        if(g_level == 3)
        {
            ChangeState(LEVEL.Level3);
            isHurry = true;
            isPlay_2 = false;
            isPlay_1 = false;
        }
        if (g_level == 2)
        {
            if (!isPlay_3)
            {
                ChangeState(LEVEL.Level2);
                isPlay_1 = false;
            }
        }
        if (g_level == 1)
        {
            if (!isPlay_2 && !isPlay_3)
            {
                ChangeState(LEVEL.Level1);
            }
        }
        if (g_level == 0)
        {
            if (!isPlay_2 && !isPlay_3 && !isPlay_1)
            {
                ChangeState(LEVEL.Level0);
            }
        }
    }

    public void HurryToPos()
    {
        if (isHurry)
        {
            enemy.navMeshAgent.SetDestination(SoundPos[0]);
            isHurry = false;
        }
        //Debug.Log("남은 거리 : " + enemy.navMeshAgent.remainingDistance);
        //Debug.Log("멈추었는가 : " + enemy.navMeshAgent.velocity.sqrMagnitude);
        //Debug.Log("존재하는가 : " + enemy.navMeshAgent.hasPath);

        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                Debug.Log("도착했다");
                SoundPos.Clear();
                isPlay_3 = false;
                g_level = 0;
            }
        }
    }
    public void MoveToPos()
    {

    }
    public void CheckPos()
    {
        Collider[] target = Physics.OverlapSphere(myPos, detectRadius, layerMask);
        foreach (Collider p_collider in target)
        {
            isPlay_1 = true;
            Vector3 targetPos = p_collider.transform.position;
            enemy.navMeshAgent.updateRotation = false;
            enemy.transform.DOLookAt(targetPos, 0.5f).OnComplete(() => enemy.navMeshAgent.updateRotation = true);
            if (isFind)
            {
                //이때, 시각감지코드와 연결
            }
            else
            {
                enemy.navMeshAgent.SetDestination(targetPos);
            }
        }

        if (!enemy.navMeshAgent.pathPending && enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
        {
            if (enemy.navMeshAgent.hasPath || enemy.navMeshAgent.velocity.sqrMagnitude == 0f)
            {

                isPlay_1 = false;
                g_level = 0;
            }
        }
    }

    public void ResetPos()
    {
        Debug.Log("아무런 것도 없다");
    }

    public void OnDrawGizmos()
    {
        myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, detectRadius);
    }
}
