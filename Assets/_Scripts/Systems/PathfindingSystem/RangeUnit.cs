using System.Collections;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Systems;
using UnityEngine;

public class RangeUnit : Unit
{
    /// <summary>
    /// The distance at which the NPC stops chasing the player.
    /// </summary>
    [SerializeField] private float _stopRadius = 0f;

    protected override IEnumerator FollowPath()
    {
        float currentSpeed = _npc.RuntimeData.BaseStats.Speed;

        if (_animator.GetInteger(nameof(CharacterState)) == (int)CharacterState.Attack) currentSpeed = 0f;

        Vector2 playerPosition = PlayerMovementSystem.Instance.PlayerTransform.position;

        if (_path.Length > 0 && (playerPosition - (Vector2)transform.position).sqrMagnitude > _stopRadius * _stopRadius)
        {
            Vector3 currentWaypoint = _path[0];
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    _targetIndex++;
                    if (_targetIndex >= _path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = _path[_targetIndex];
                }

                if (_npc.RuntimeData.BaseStats.IsAlive)
                    transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, currentSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
