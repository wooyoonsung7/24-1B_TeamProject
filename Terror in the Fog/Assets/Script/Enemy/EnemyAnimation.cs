using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyAnimation : MonoBehaviour
{
    public static EnemyAnimation instance; 

    [SerializeField] private Enemy enemy;

    public Sequence sequence;  //시작시, 바라보는 방향
    public Sequence sequence2; //그 반대방향
    public Sequence sequence3; //시작시, 바라보는 방향
    public Sequence sequence4; //그 반대방향

    public Sequence checkAroundSeq; //주위를 둘러봄

    public Sequence tutoSequence; //튜토리얼용 애니메이션
    public Sequence atStudySeq;   //서재에서의 애니메이션


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        sequence = DOTween.Sequence();
        sequence2 = DOTween.Sequence();
        sequence3 = DOTween.Sequence();
        sequence4 = DOTween.Sequence();

        checkAroundSeq = DOTween.Sequence();
    }
    private void Start()
    {
        DOMotion();
        CheckAround();
    }
    private void DOMotion()
    {
        sequence.Prepend(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 3f))
       .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 3f))
       .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 3f))
       .SetAutoKill(false)
       .Pause()
       .OnComplete(() => Distribute(1));

        sequence2.Prepend(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 3f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 3f))
        .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 90, 0), 3f))
        .SetAutoKill(false)
        .Pause()
        .OnComplete(() => Distribute(1));


        sequence3.Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, -180, 0), 3f))
         .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 3f))
         .SetAutoKill(false)
         .Pause()
         .OnComplete(() => Distribute(2));

        sequence4.Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 3f))
         .Append(enemy.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 3f))
         .SetAutoKill(false)
         .Pause()
         .OnComplete(() => Distribute(2));
    }

    private void CheckAround()
    {
        checkAroundSeq.Prepend(transform.DOLocalRotate(new Vector3(0, 90, 0), 1.5f))
                      .Append(transform.DOLocalRotate(new Vector3(0, 180, 0), 1.5f))
                      .Append(transform.DOLocalRotate(new Vector3(0, -90, 0), 1.5f))
                      .SetAutoKill(false)
                      .Pause()
                      .OnComplete(() => enemy.isCheckAround = false);
    }

    private void DoTutoMotion()
    {

    }

    public void DoDayOneMotion()
    {
        //서재에서의 애니메이션
        
    }

    private void Distribute(int number)
    {
        if (ResearchManager.instance != null)
        {
            ResearchManager.instance.stepNumber = number;
        }
        if (ResearchManager_Simple.instance != null)
        {
            ResearchManager_Simple.instance.stepNumber = number;
        }
    }
    public void StopSquance()
    {
        sequence.Pause();
        sequence2.Pause();
        sequence3.Pause();
        sequence4.Pause();
    }
}
