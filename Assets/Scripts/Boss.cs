using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject Player; // The GameObject whose trajectory you want to calculate
    [SerializeField] private GameObject Bullet; // The GameObject you want to launch
    [SerializeField] private float launchSpeed = 10f; // The speed at which the object will be launched

    private Rigidbody2D targetRigidbody;

    private void Start()
    {
        targetRigidbody = Player.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        // Calculate the predicted position of the target object
        Vector3 predictedPosition = PredictTargetPosition(Player.transform.position, targetRigidbody.velocity, Bullet.transform.position, launchSpeed);

        // Launch the object towards the predicted position
        LaunchObject(predictedPosition);
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

    private void LaunchObject(Vector3 targetPosition)
    {
        // Calculate the direction towards the predicted position
        Vector3 launchDirection = (targetPosition - Bullet.transform.position).normalized;

        // Launch the object towards the predicted position
        Bullet.GetComponent<Rigidbody2D>().velocity = launchDirection * launchSpeed;
    }
}
