using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{

    public float TimeBullet = 20f;
    public float bulletVelocity = 2f;
  

    // Update is called once per frame
    void Update()
    {
        TimeBullet -= Time.deltaTime * 1.5f;

        transform.Translate(0, 0, bulletVelocity * 50 * Time.deltaTime);

        if(TimeBullet <= 0)
        {
            Destroy(transform.gameObject, 0.001f);
        }
    }

    private void OnCollisionEnter()
    {
        Destroy(transform.gameObject, 0.001f);  
    }

}
