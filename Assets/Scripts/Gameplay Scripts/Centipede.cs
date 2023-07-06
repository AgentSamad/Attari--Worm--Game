using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    
    private const int MAX_CENTIPEDE_SIZE = 11, MIN_CENTIPEDE_SIZE = 6;
    private const float CENTIPEDE_SPEED_MULTIPLIER = 1.1f;

    
    [Header("Parts")] public CentipedeSegments head;
    public CentipedeSegments body;


    [Header("Movement")] public LayerMask collisionMask = ~0;
    public BoxCollider homeArea;
    

    [Header("Values")]
    [SerializeField] private int size = 12;

    [Header("Scoring")] [SerializeField] private int pointsHead = 100;
    [SerializeField] private int pointsBody = 10;
    public float ObjectsDistance { get; set; }= 10f ;
    public float Speed { get; set; } = 45f;

    private List<CentipedeSegments> segments = new List<CentipedeSegments>();


    public void Respawn()
    {
        foreach (var segment in segments)
        {
            Destroy(segment.gameObject);
        }

        segments.Clear();

        for (int i = 0; i < size; i++)
        {
            Vector2 position = GridPosition(transform.position) + (Vector3.left * i * ObjectsDistance);
            CentipedeSegments segment = Instantiate(i == 0 ? head : body, position, Quaternion.identity,
                transform);
            segment.centipede = this;
            segments.Add(segment);
        }

        for (int i = 0; i < segments.Count; i++)
        {
            CentipedeSegments segment = segments[i];
            segment.Ahead = GetSegmentAt(i - 1);
            segment.Behind = GetSegmentAt(i + 1);
        }
    }

    private CentipedeSegments GetSegmentAt(int index)
    {
        if (index >= 0 && index < segments.Count)
        {
            return segments[index];
        }
        else
        {
            return null;
        }
    }


    public void Remove(CentipedeSegments segment)
    {
        int points = segment.HasHead ? pointsHead : pointsBody;
        GameEvents.InvokeIncreaseScore(points);

        GameEvents.InvokeCentipedeHitEvent(GridPosition(segment.transform.position));

        if (segment.Ahead != null)
        {
            segment.Ahead.Behind = null;
        }

        if (segment.Behind != null)
        {
            segment.Behind.Ahead = null;
            segment.Behind.UpdateHeadSegment();
        }

        segments.Remove(segment);
        Destroy(segment.gameObject);

        if (segments.Count == 0)
        {
            GameEvents.InvokeNextLevel();
        }
    }

    private Vector3 GridPosition(Vector3 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

    
    //The difficulty point increase after every next level
    
    public void UpdateDifficulty()
    {
        Speed *= CENTIPEDE_SPEED_MULTIPLIER;
        size = Mathf.Clamp(size + 1, MIN_CENTIPEDE_SIZE, MAX_CENTIPEDE_SIZE);
    }
}