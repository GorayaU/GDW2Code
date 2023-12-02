using Unity.Mathematics;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // The GameObject whose trajectory you want to calculate
    [SerializeField] private GameObject objectPrefab; // The GameObject you want to launch
    [SerializeField] private float launchDelay = 2f; // The delay before launching the object
    [SerializeField] private float launchSpeed = 10f; // The speed at which the object will be launched

    public bool inBossFight = false; // Variable to control if the boss is in a fight

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= launchDelay)
        {
            LaunchObjectDelayed();
            timer = 0;
        }
    }
    private void LaunchObjectDelayed()
    {
        if (inBossFight)
        {
            Vector2 targetVelocity = new Vector2(targetObject.GetComponent<Rigidbody2D>().velocity.x, math.clamp(-targetObject.GetComponent<Rigidbody2D>().velocity.y, -5, 1));

            Debug.Log(targetVelocity);

            Vector3 predictedPosition = PredictTargetPosition(targetObject.transform.position, targetVelocity , objectPrefab.transform.position, launchSpeed);
            LaunchObject(predictedPosition);
        }
    }

    private Vector2 PredictTargetPosition(Vector2 targetPosition, Vector2 targetVelocity, Vector2 launchPosition, float launchSpeed)
    {
        Vector2 targetOffset = targetPosition - launchPosition;
        float time = targetOffset.magnitude / launchSpeed;

        Vector2 predictedPosition = targetPosition + targetVelocity * time;

        return predictedPosition;
    }

    private void LaunchObject(Vector3 targetPosition)
    {
        Vector2 launchDirection = (targetPosition - transform.position).normalized;

        GameObject launchedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = launchedObject.GetComponent<Rigidbody2D>();
        rb.AddForce(launchDirection * launchSpeed, ForceMode2D.Impulse);

        Destroy(launchedObject, 3);
    }
}
