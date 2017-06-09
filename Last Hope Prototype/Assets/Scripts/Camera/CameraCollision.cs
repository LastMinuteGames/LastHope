using System;
using System.Collections;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField]
    private float clipMoveTime = 0.05f;
    [SerializeField]
    private float returnTime = 0.4f;
    [SerializeField]
    private float sphereCastRadius = 0.1f;
    [SerializeField]
    private float closestDistance = 0.5f;
    [SerializeField]
    private string dontClipTag = "Player";

    private Transform camT;
    private Transform pivotT;
    private float originalDist;
    private float moveVelocity;
    private float currentDist;
    private Ray m_Ray = new Ray();
    private RaycastHit[] hits;
    private RayHitComparer rayHitComparer;


    private void Start()
    {
        camT = GetComponentInChildren<Camera>().transform;
        pivotT = camT.parent;
        originalDist = camT.localPosition.magnitude;
        currentDist = originalDist;
        rayHitComparer = new RayHitComparer();
    }


    private void LateUpdate()
    {
        float targetDist = originalDist;

        m_Ray.origin = pivotT.position + pivotT.forward * sphereCastRadius;
        m_Ray.direction = -pivotT.forward;

        Collider[] colliders = Physics.OverlapSphere(m_Ray.origin, sphereCastRadius);

        bool initialIntersect = false;
        bool hitSomething = false;

        for (int i = 0; i < colliders.Length; i++)
        {
            if ((!colliders[i].isTrigger) &&
                !(colliders[i].attachedRigidbody != null && colliders[i].attachedRigidbody.CompareTag(dontClipTag)))
            {
                initialIntersect = true;
                break;
            }
        }

        if (initialIntersect)
        {
            m_Ray.origin += pivotT.forward * sphereCastRadius;
            hits = Physics.RaycastAll(m_Ray, originalDist - sphereCastRadius);
        }
        else
        {
            hits = Physics.SphereCastAll(m_Ray, sphereCastRadius, originalDist + sphereCastRadius);
        }

        Array.Sort(hits, rayHitComparer);

        float nearest = Mathf.Infinity;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].distance < nearest && (!hits[i].collider.isTrigger) &&
                !(hits[i].collider.attachedRigidbody != null &&
                  hits[i].collider.attachedRigidbody.CompareTag(dontClipTag)))
            {
                nearest = hits[i].distance;
                targetDist = -pivotT.InverseTransformPoint(hits[i].point).z;
                hitSomething = true;
            }
        }

        if (hitSomething)
        {
            Debug.DrawRay(m_Ray.origin, -pivotT.forward * (targetDist + sphereCastRadius), Color.red);
        }

        currentDist = Mathf.SmoothDamp(currentDist, targetDist, ref moveVelocity,
                                       currentDist > targetDist ? clipMoveTime : returnTime);
        currentDist = Mathf.Clamp(currentDist, closestDistance, originalDist);
        camT.localPosition = -Vector3.forward * currentDist;
    }

    public class RayHitComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo(((RaycastHit)y).distance);
        }
    }
}