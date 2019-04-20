using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb;

    Vector3 targetPosition;
    Vector2 movement;
    [SerializeField] float speed = 3;
    [SerializeField] float patrolChange = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            Transform charTransform = CharacterController.Character.transform;
            Vector3 diff = charTransform.position - this.transform.position;
            if (diff.magnitude<3)
            {
                targetPosition = charTransform.position;
                UpdateMovement();
            }
            else if (Random.value < patrolChange * Time.deltaTime)
            {
                targetPosition = this.transform.position + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                UpdateMovement();
            }
        }

        bool doPush = false;
        Vector3 push = Vector3.zero;
        foreach (LightSource l in LightSource.AllLightSources())
        {
            Vector3 diff = l.getPosition() - this.transform.position;
            if (diff.magnitude < l.getRange()*1.1f)
            {
                push -= diff;
                doPush = true;
            }
        }
        if (doPush)
        {
            rb.velocity = push.normalized * speed;
        }
        else
        {
            rb.velocity = new Vector3(movement.x, 0, movement.y) * speed;
        }
    }

    void UpdateMovement()
    {
        
        movement.x = targetPosition.x - this.transform.position.x;
        movement.y = targetPosition.z - this.transform.position.z;
        movement.Normalize();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, targetPosition);

        Gizmos.color = Color.red;
        foreach (LightSource l in LightSource.AllLightSources())
        {
            Vector3 lPos = l.getPosition();
            Gizmos.DrawLine(this.transform.position, (this.transform.position- lPos).normalized * l.getRange()+ lPos);
        }
       
    }
}
