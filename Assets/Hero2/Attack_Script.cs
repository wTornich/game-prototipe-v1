using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Script : MonoBehaviour
{

    public Transform target;
    public GameObject bullet;
    [Space(20)]
    [Range(0.05f, 5)] public float nextBulletTime = 0.5f;
    bool wait;

   
    void Update()
    {
        GunController();
    }

    void GunController()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (!wait)
        {
            Instantiate(bullet, target.position, target.rotation);
            StartCoroutine("shootWait");
            wait = true;
        }
    }

    IEnumerator shootWait()
    {
        yield return new WaitForSeconds(nextBulletTime);
        wait = false;
    }



}
