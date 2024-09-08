using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PlayerScript : Agent
{
    Rigidbody _rBody; // Rigidbody component of the agent
    public float forceMultiplier = 10f; // Force multiplier for the agent
    public Transform target; // Referance to the goal
    [SerializeField] private Material winMaterial; // Material to apply when the agent reaches the goal
    [SerializeField] private Material loseMaterial; // Material to apply when the agent falls off the platform
    [SerializeField] private MeshRenderer floorMeshRenderer; // MeshRenderer component of the agent
    private bool isOnPlatform; // Check if the agent is on the platform

    public override void Initialize()
    {
        _rBody = GetComponent<Rigidbody>(); // Get the Rigidbody component
        isOnPlatform = true; // Set the initial value of isOnPlatform to true
    }

    public override void OnEpisodeBegin()
    {
        _rBody.angularVelocity = Vector3.zero; // Reset the angular velocity
        _rBody.velocity = Vector3.zero; // Reset the velocity
        this.transform.localRotation = Quaternion.Euler(0f,0,0f); // Reset the rotation of the agent

        // Reset the position of the agent and target
        this.transform.localPosition = new Vector3(Random.Range(-4f,-1f), 1f ,Random.Range(-4f,4f));
        target.localPosition = new Vector3(Random.Range(1f,4f),1f,Random.Range(-4f,4f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition); // Add the position of the agent
        sensor.AddObservation(target.localPosition); // Add the position of the target
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0]; // Get the value of the first action
        float moveZ = actions.ContinuousActions[1]; // Get the value of the second action

        Vector3 force = new Vector3(moveX, 0, moveZ) * forceMultiplier; // Calculate the force to apply
        _rBody.AddForce(force * forceMultiplier * Time.deltaTime, ForceMode.VelocityChange); // Apply the force

        // Check if the agent falls off the platform
        if (transform.localPosition.y < 0)
        {
            AddReward(-1.0f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }

        // Check if the agent leaves the platform
        if (!isOnPlatform)
        {
            AddReward(-1.0f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }

    // Improved collision handling
    private void OnTriggerEnter(Collider other)
    {
        // Check if the agent reached the goal
        if (other.CompareTag("Target"))
        {
            AddReward(1.0f); // Reward for reaching the goal
            floorMeshRenderer.material = winMaterial; // Change the material to winMaterial
            EndEpisode(); // End the episode
        }
    }

    // Improved collision handling
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // Check if the agent leaves the platform
        {
            isOnPlatform = false; // Agent is not on the platform
            AddReward(-1.0f); // Penalize for leaving the platform
            floorMeshRenderer.material = loseMaterial; // Change the material to loseMaterial
            EndEpisode(); // End the episode
        }
    }

    // Improved collision handling
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform")) // Check if the agent is on the platform
        {
            isOnPlatform = true; // Agent is on the platform
        }
    }

    // Manual control
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions; // Get the continuous actions

        // Initialize the moveX and moveZ variables
        float moveX = 0f;
        float moveZ = 0f;

        // Movement controls
        if (Input.GetKey(KeyCode.W))
        {
            moveX = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveX = -1f;
        }

        // Rotation controls
        if (Input.GetKey(KeyCode.A))
        {
            moveZ = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveZ = -1f;
        }

        // Assign the values to the continuous actions
        continuousActionsOut[0] = moveX;
        continuousActionsOut[1] = moveZ;
    }
}
