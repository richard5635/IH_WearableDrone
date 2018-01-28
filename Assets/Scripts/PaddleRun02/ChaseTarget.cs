using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTarget : MonoBehaviour
{
    public GameObject target;
    public PlayerMovementPaddle02 playerMovement;
    Vector3 diff;
    private float followSpeed;
    bool playerAlive;

    // Use this for initialization
    void Start()
    {
        diff = target.transform.position - transform.position;
        followSpeed = playerMovement.forwardSpeed;
    }

    void OnEnable()
    {
        transform.position = new Vector3(0, 0, target.transform.position.z - 5.0f);
        diff = target.transform.position - transform.position;
        followSpeed = playerMovement.forwardSpeed + 0.2f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerAlive = target != null;
        if (playerAlive)
        {
            Vector3 playerPosition = target.transform.position;

            if (Vector3.Distance(transform.position, target.transform.position) > 0)
            {
                //transform.position += transform.forward * followSpeed * 10 * Time.deltaTime;
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Lerp(
                        transform.position.y,
                        target.transform.position.y,
                        Time.deltaTime * followSpeed * 15),
                    transform.position.z + 1 * followSpeed * 20 * Time.deltaTime);
            }
        }
        if(!playerAlive)
        {
            StartCoroutine(Sink());
        }

    }

    IEnumerator Sink()
    {
        transform.position += new Vector3(0, -5 * Time.deltaTime,0);
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        yield return null;
    }
}
