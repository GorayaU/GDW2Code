using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject Player; // The GameObject whose trajectory you want to calculate
    [SerializeField] private GameObject BulletPrefab; // The GameObject you want to launch
    [SerializeField] private float launchSpeed = 10f; // The speed at which the object will be launched

    private Rigidbody2D targetRigidbody;
    private Vector3 NewPos;

    private void Start()
    {
        targetRigidbody = Player.GetComponent<Rigidbody2D>();
        NewPos = Vector3.right / 5;
    }

    private void FixedUpdate()
    {
        // Get the player's position and velocity
        Vector3 playerPosition = Player.transform.position;
        Vector3 playerVelocity = targetRigidbody.velocity;

        // Calculate the predicted position of the player in future time
        Vector3 predictedPosition = PredictTargetPosition(playerPosition, playerVelocity, BulletPrefab.transform.position, launchSpeed);

        // Keep the Y position of the player but track the X position
        Vector3 trackedPosition = new Vector3(predictedPosition.x, predictedPosition.y, 0);

        gameObject.transform.position += NewPos;

        ShootBullet(trackedPosition);
    }

    private Vector3 PredictTargetPosition(Vector3 targetPosition, Vector3 targetVelocity, Vector3 launchPosition, float launchSpeed)
    {
        // Calculate the distance to the target
        Vector3 targetOffset = targetPosition - launchPosition;
        float time = targetOffset.magnitude / launchSpeed;

        // Calculate the predicted position using the target's velocity
        Vector3 predictedPosition = targetPosition + targetVelocity * time;

        return predictedPosition;
    }

    private void ShootBullet(Vector3 targetPosition)
    {
        // Calculate the direction towards the predicted position of the player
        Vector3 launchDirection = (targetPosition - transform.position).normalized;

        GameObject Bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

        Rigidbody2D bulletRigidbody = Bullet.GetComponent<Rigidbody2D>();

        bulletRigidbody.AddForce(launchDirection * launchSpeed, ForceMode2D.Impulse);
        Destroy(Bullet, 3);
    }
}
