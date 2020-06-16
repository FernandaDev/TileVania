using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float turnThreshold;

    private int index = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var distanceToNextWaypoint = waypoints[index].position - transform.position;

        if (distanceToNextWaypoint.sqrMagnitude < turnThreshold)
        {
            Flip();

            if (index >= waypoints.Length - 1)
                index = 0;
            else
                index++;
        }

        if (IsFacingRight())
            rb.velocity = new Vector2(moveSpeed, 0f);
        else
            rb.velocity = new Vector2(-moveSpeed, 0f);
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)), 1f);
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}
