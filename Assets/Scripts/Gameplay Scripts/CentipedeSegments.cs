using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeSegments : MonoBehaviour, IHitable
{
    public Centipede centipede { get; set; }
    public CentipedeSegments Ahead { get; set; }
    public CentipedeSegments Behind { get; set; }
    public bool HasHead => Ahead == null;

    private Vector3 direction;
    private Vector3 targetPosition;
    private float downDist = 6;
   // private float objectWidth = 1.2f;

  //  private float minX, maxX;

    void Start()
    {
        direction = Vector3.right * centipede.ObjectsDistance + Vector3.down * downDist;
        targetPosition = transform.position;
        
        
        // Trying code but time was over 
      //  minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.05f, 0)).x + objectWidth / 2;
       // maxX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.95f, 0)).x - objectWidth / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasHead && Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            UpdateHeadSegment();
        }

        Vector3 currentPosition = transform.position;
        float speed = centipede.Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed);

        Vector3 movementDirection = (targetPosition - currentPosition).normalized;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        transform.rotation =
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }


    public void UpdateHeadSegment()
    {
        Vector3 gridPosition = GridPosition(transform.position);

        targetPosition = gridPosition;
        targetPosition.x += direction.x;

        //Trying this logic too keep bounded in the Boundaries but time was over 
        
        // print("Target Pos " + targetPosition.x);
        // if (targetPosition.x <= minX || targetPosition.x >= maxX)
        // {
        //     direction.x = -direction.x;
        //
        //     targetPosition.x = gridPosition.x;
        //     targetPosition.y = gridPosition.y + direction.y;
        // }

        Collider[] hitColliders = Physics.OverlapBox(targetPosition, transform.localScale / 2, Quaternion.identity,
            centipede.collisionMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            direction.x = -direction.x;
            
            targetPosition.x = gridPosition.x;
            targetPosition.y = gridPosition.y + direction.y;


            Bounds homeBounds = centipede.homeArea.bounds;

            if ((direction.y == downDist && targetPosition.y > homeBounds.max.y) ||
                (direction.y == -downDist && targetPosition.y < homeBounds.min.y))
            {
                direction.y = -direction.y;
                targetPosition.y = gridPosition.y + direction.y;
            }

            i++;
        }

        if (Behind != null)
        {
            Behind.UpdateBodySegment();
        }
    }

    private void UpdateBodySegment()
    {
        targetPosition = GridPosition(Ahead.transform.position);
        direction = Ahead.direction;

        if (Behind != null)
        {
            Behind.UpdateBodySegment();
        }
    }

    private Vector3 GridPosition(Vector3 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }


    public void Remove()
    {
        centipede.Remove(this);
        this.gameObject.SetActive(false);
    }


    public void HandleHit(GameObject other)
    {
        if (other.CompareTag("Bullet"))
            Remove();

        if (other.CompareTag("Player"))
            GameEvents.InvokeGameOver();
    }
}