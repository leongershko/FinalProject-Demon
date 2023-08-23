using UnityEngine;

public class batCapsule : MonoBehaviour
{
    [SerializeField]
    private batCapsuleFollower _batCapsuleFollowerPrefab;

    private void SpawnBatCapsuleFollower()
    {
        var follower = Instantiate(_batCapsuleFollowerPrefab);
        follower.transform.position = transform.position;
        follower.SetFollowTarget(this);
    }

    private void Start()
    {
        SpawnBatCapsuleFollower();
    }
}