using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRange : MonoBehaviour
{
    public Action<Collider> OnTurretRangeColliderEnter = null;
    public Action<Collider> OnTurretRangeColliderStay = null;
    public Action<Collider> OnTurretRangeColliderExit = null;


    private void OnTriggerEnter(Collider other)
    {
        if (OnTurretRangeColliderEnter != null)
            OnTurretRangeColliderEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (OnTurretRangeColliderStay != null)
            OnTurretRangeColliderStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (OnTurretRangeColliderExit != null)
            OnTurretRangeColliderExit(other);
    }
}
