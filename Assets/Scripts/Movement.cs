using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readabilty or speed 
    // STATE - private instance (member) variables

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip engineThrust;
    [SerializeField] ParticleSystem rocketJetParticle;
    [SerializeField] ParticleSystem leftSideThrusterParticle;
    [SerializeField] ParticleSystem rightSideThrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(engineThrust);
        if (!rocketJetParticle.isPlaying)
            rocketJetParticle.Play();
    }

     void StopThrusting()
    {
        audioSource.Stop();
        rocketJetParticle.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftSideThrusterParticle.isPlaying)
            leftSideThrusterParticle.Play();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightSideThrusterParticle.isPlaying)
            rightSideThrusterParticle.Play();
    }

    void StopRotating()
    {
        leftSideThrusterParticle.Stop();
        rightSideThrusterParticle.Stop();
    }

    void ApplyRotation(float rotationInThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(rotationInThisFrame * Time.deltaTime * Vector3.forward);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
