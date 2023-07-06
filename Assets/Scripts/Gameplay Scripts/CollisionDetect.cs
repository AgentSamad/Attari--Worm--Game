using System;
using UnityEngine;
using System.Collections;

public class CollisionDetect : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (BaseGame.Instance != null)
        {
       
            if (this.gameObject.TryGetComponent<IHitable>(out IHitable hit))
                BaseGame.Instance.HandleHit(hit, other.gameObject);
        }
    }
}