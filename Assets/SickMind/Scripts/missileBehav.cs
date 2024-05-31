using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class missileBehav : MonoBehaviour
{
    [SerializeField]Rigidbody MyRgb;
    [SerializeField] GameObject myPrefabExplosion;

    Transform target;
    [SerializeField] float forceStrength = 10f;
    Vector3 pos;

    public void StartFall() {
        target = GameObject.FindWithTag("Sight").transform;
        pos = target.transform.position;
        transform.parent.gameObject.transform.LookAt(pos);
         ApplyForceTowardsTarget();
    }

    void ApplyForceTowardsTarget()
    {
        // Calculate the direction to the target
        Vector3 direction = (pos - transform.position).normalized;

        //MyRgb.AddForce(direction * forceStrength,ForceMode.Impulse);
        MyRgb.velocity=direction*forceStrength;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer==3) {
            Vector3 contactPoint = CalculateContactPoint(other);
            var a =GameObject.Instantiate(myPrefabExplosion,contactPoint,Quaternion.identity);
            AudioManager.Instance.PlaySkill4();
            Destroy(a,1.5f);
            Destroy( transform.parent.gameObject,0.1f);
        }
    }

    private Vector3 CalculateContactPoint(Collider other)
    {
        // Get the closest point on the trigger collider to the other collider
        Vector3 closestPointOnTrigger = GetComponent<Collider>().ClosestPoint(other.transform.position);

        // Get the closest point on the other collider to the trigger collider
        Vector3 closestPointOnOther = other.ClosestPoint(transform.position);

        // Calculate the midpoint between these two closest points as the approximate contact point
        Vector3 contactPoint = (closestPointOnTrigger + closestPointOnOther) / 2;

        return contactPoint;
    }
}
