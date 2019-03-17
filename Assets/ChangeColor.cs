using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    ParticleSystem particles;
    ParticleSystem.MainModule psmain;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        psmain = particles.main;
    }

    public void ChangeCOlor(Color color)
    {
        psmain.startColor = color;
    }
}

