using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents;

public class PlayerAgent : Agent
{
    private Rigidbody rBody;
    public Transform target;
    public Transform platform;

    public float moveSpeed = 10f;

    private float approachReward = 0.15f;
    private float approachReward2 = 0.05f;
    private float farPenalty = -0.1f;
    private float fallPenalty = -5.0f;

    // Epsilon - Greedy
    public float epsilon = 0.7f;
    public float minEpsilon = 0.01f;
    public float decayRate = 0.996f;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        if (this.transform.localPosition.y < 0)
        {
            rBody.angularVelocity = Vector3.zero;
            rBody.velocity = Vector3.zero;
            this.transform.localPosition = platform.localPosition + new Vector3(Random.Range(-5, 5), 1.5f, Random.Range(-5, 5));
        }
        target.localPosition = platform.localPosition + new Vector3(Random.Range(-5, 5), 1.5f, Random.Range(-5, 5));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(target.localPosition - platform.localPosition);
        sensor.AddObservation(this.transform.localPosition - platform.localPosition);

        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        /*
        Vector3 move = new Vector3(actionBuffers.ContinuousActions[0], 0, actionBuffers.ContinuousActions[1]);
        rBody.AddForce(move * moveSpeed * 5);

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);

        float trainingProgress = GetCumulativeReward() / MaxStep;
        float approachFactor = Mathf.Lerp(1.0f, 0.0f, trainingProgress);
        float penaltyFactor = Mathf.Lerp(0.1f, 0.5f, trainingProgress);
        float shrinkingFarLimit = Mathf.Lerp(9, 5, trainingProgress);

        if (distanceToTarget < 2.1f)
        {
            SetReward(10.0f);
            EndEpisode();
        }

        if (distanceToTarget < 2.5f)
        {
         AddReward(approachReward * approachFactor);
        }

        if (distanceToTarget < 4)
        {
            if (StepCount < 500)
            {
                AddReward(approachReward2 * approachFactor);
            }
        }

        if (distanceToTarget > shrinkingFarLimit)
        {
            AddReward(farPenalty * penaltyFactor);
        }

        if (this.transform.localPosition.y < 0)
        {
            AddReward(fallPenalty * penaltyFactor);
            EndEpisode();
        }
        */

        Vector3 move;
        // Epsilon-greedy stratejisi: Keşif ve sömürü dengesi
        float randomValue = Random.Range(0f, 1f);

        // Eğer rastgele sayı epsilon'dan küçükse keşif yap (rastgele hareket)
        if (randomValue < epsilon)
        {
            // Rastgele hareket seç
            move = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }
        else
        {
            // Mevcut politikaya göre en iyi hareketi yap (greedy hareket)
            move = new Vector3(actionBuffers.ContinuousActions[0], 0, actionBuffers.ContinuousActions[1]);
        }

        // Hareketi uygula
        rBody.AddForce(move * moveSpeed * 5);

        // Epsilon'u her adımda azalt
        epsilon = Mathf.Max(minEpsilon, epsilon * decayRate);

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, target.localPosition);

        float trainingProgress = GetCumulativeReward() / MaxStep;
        float approachFactor = Mathf.Lerp(1.0f, 0.0f, trainingProgress);
        float penaltyFactor = Mathf.Lerp(0.1f, 0.5f, trainingProgress);
        float shrinkingFarLimit = Mathf.Lerp(9, 5, trainingProgress);

        // Hedefe ulaşma ödülü
        if (distanceToTarget < 2.1f)
        {
            SetReward(15.0f);
            EndEpisode();
        }

        // Hedefe yaklaşma ödülleri
        if (distanceToTarget < 2.5f)
        {
            AddReward(approachReward * approachFactor);
        }

        if (distanceToTarget < 4)
        {
            if (StepCount < 500)
            {
                AddReward(approachReward2 * approachFactor);
            }
        }

        // Hedeften uzak kalma cezası
        if (distanceToTarget > shrinkingFarLimit)
        {
            AddReward(farPenalty * penaltyFactor);
        }

        // Ajan düşerse ceza
        if (this.transform.localPosition.y < 0)
        {
            AddReward(fallPenalty * penaltyFactor);
            EndEpisode();
        }

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

}
