

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss_Radar : MonoBehaviour
{
    private Transform target;
    public float _range;
    public float speed;
    public float rotationSpeed;
    private Animator BossAnim;
    public GameObject Boss;


    void Start()
    {
        BossAnim = GetComponent<Animator>();

    }

  
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            BossAnim.SetInteger("anim_boss_controller", 1);
        }

        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("AAAAAAAAAAAAAAAAs");
            Destroy(Boss);

        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Hero")
        {

            target = collision.gameObject.transform;

            float dist = Vector3.Distance(target.position, transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);

            Debug.Log(dist);

            if (dist <= 5)
            {
                BossAnim.SetInteger("anim_boss_controller", 2);
            }
            else
            {
                BossAnim.SetInteger("anim_boss_controller", 1);
            }

          
                transform.position = Vector3.MoveTowards(transform.position,
                target.transform.position, speed);
            
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            BossAnim.SetInteger("anim_boss_controller", 0);
            transform.position = Vector3.MoveTowards(transform.position,
            target.transform.position, 0);
        }
    }


    IEnumerator timeAttack()
    {
        yield return new WaitForSeconds(5);

    }


}
