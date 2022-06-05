using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ring;
    [SerializeField]
    private ParticleSystem smoke;
    [SerializeField]
    private ParticleSystem shield;

    public ParticleSystem Smoke { get => smoke; set => smoke = value; }
    public ParticleSystem Shield { get => shield; set => shield = value; }
    public ParticleSystem Ring { get => ring; set => ring = value; }
}
