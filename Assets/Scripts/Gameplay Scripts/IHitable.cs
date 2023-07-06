using UnityEngine;
using System.Collections;

public interface IHitable
{
    void HandleHit(GameObject other);
}