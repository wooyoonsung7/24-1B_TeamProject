using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KillAnimation : MonoBehaviour
{
    public GameObject target;
    public PlayerController player;
    public Animator animator;
    //public float maxdistance = 3;
    //public float mindistance = 3;

    private void Return()
    {
        transform.DOLookAt(target.transform.position + new Vector3(0f, 1.5f, 0f), 0.3f);
    }

    private void Motion_1()
    {
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.x += 29f;
        currentRot.y -= 41f;
        transform.DOLocalRotate(currentRot, 0.12f);
    }
    private void Motion_2()
    {
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.x += 25f;
        currentRot.y += 18f;
        transform.DOLocalRotate(currentRot, 0.1f);
    }

    private void Motion_3()
    {
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.x -= 18f;
        currentRot.y -= 24f;
        transform.DOLocalRotate(currentRot, 0.1f);
    }

    private void Motion_4()
    {
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.x += 63f;
        transform.DOLocalRotate(currentRot, 0.1f);
    }
    private Vector3 MovePos()
    {

        
        Vector3 currentPos = player.transform.position;

        Vector3 direction = (target.transform.position - currentPos).normalized;
        Vector3 moveToPos = target.transform.position - direction * 1.5f;
        /*
        float target_x = (currentPos.x + target.transform.position.x) / 2;
        float target_z = (currentPos.z + target.transform.position.z) / 2;
        float target_X = (target_x + currentPos.x) / 2;
        float target_Z = (target_z + currentPos.z) / 2;
        */
        return new Vector3(moveToPos.x, player.transform.position.y, moveToPos.z);
    }

    private IEnumerator Animation()
    {
        Vector3 targetLookPos = transform.position;
        targetLookPos.y = target.transform.position.y;

        transform.DOLookAt(target.transform.position + new Vector3(0f,1.5f,0f), 0.1f);
        target.transform.DOLookAt(targetLookPos, 0.01f);
        player.transform.DOMove(MovePos(), 0.3f);
        player.enabled = false;
        yield return new WaitForSeconds(0.1f);
        Motion_2(); SoundManager.instance.PlaySound("PlayerDead");
        yield return new WaitForSeconds(0.19f);
        Return();
        yield return new WaitForSeconds(0.34f);
        Motion_1(); SoundManager.instance.PlaySound("PlayerDead");
        yield return new WaitForSeconds(0.18f);
        Return();
        yield return new WaitForSeconds(0.36f);
        Motion_3(); SoundManager.instance.PlaySound("PlayerDead");
        yield return new WaitForSeconds(0.2f);
        Return();
        yield return new WaitForSeconds(0.38f);
        Motion_4(); SoundManager.instance.PlaySound("PlayerDead");
        yield return new WaitForSeconds(0.2f);
        target.gameObject.SetActive(false);
        EventManager.instance.PlayerDead();
    }
}
