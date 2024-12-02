using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Unity.PlasticSCM.Editor.WebApi;
using System.Security.Cryptography;

public class Test : MonoBehaviour
{
    public GameObject target;
    public PlayerController player;
    public Animator animator;
    //public float maxdistance = 3;
    //public float mindistance = 3;

    void Start()
    {
        transform.DOLookAt(target.transform.position, 0.1f);
        player.transform.DOMove(MovePos(), 0.3f);
        player.enabled = false;
        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        Tests();
    }

    private void Tests()
    {

    }
    private void Return()
    {
        transform.DOLookAt(target.transform.position, 0.3f);
    }

    private void Motion_1()
    {
        Vector3 currentRot = transform.localEulerAngles;
        currentRot.x += 29f;
        currentRot.y -= 41f;
        transform.DOLocalRotate(currentRot, 0.1f);
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
        currentRot.x += 45f;
        transform.DOLocalRotate(currentRot, 0.1f);
    }
    private Vector3 MovePos()
    {
        Vector3 currentPos = player.transform.position;

        float target_x = (currentPos.x + target.transform.position.x) / 2;
        float target_z = (currentPos.z + target.transform.position.z) / 2;
        float target_X = (target_x + currentPos.x) / 2;
        float target_Z = (target_z + currentPos.z) / 2;

        return new Vector3(target_X, player.transform.position.y, target_Z);
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(0.1f);
        Motion_1(); SoundManager.instance.PlaySound("PlayerDead");
        yield return new WaitForSeconds(0.19f);
        Return();
        yield return new WaitForSeconds(0.34f);
        Motion_2(); SoundManager.instance.PlaySound("PlayerDead");
        yield return new WaitForSeconds(0.18f);
        Return();
        yield return new WaitForSeconds(0.36f);
        Motion_3(); SoundManager.instance.PlaySound("PlayerDead");
        yield return new WaitForSeconds(0.2f);
        Return();
        yield return new WaitForSeconds(0.38f);
        Motion_4(); SoundManager.instance.PlaySound("PlayerDead");
    }
}
