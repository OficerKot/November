using UnityEngine;

[CreateAssetMenu(fileName = "HandJointConfig", menuName = "Scriptable Objects/HandJointConfig")]
public class HandJointConfig : ScriptableObject
{
    [Header("Grab points")]
    [SerializeField] private Vector3 handsGrabLocalPos;

    [Header("Motion constraints")]
    [SerializeField] private ConfigurableJointMotion xMotion;
    [SerializeField] private ConfigurableJointMotion yMotion;
    [SerializeField] private ConfigurableJointMotion zMotion;

    [Header("Angular motion constraints")]
    [SerializeField] private ConfigurableJointMotion xAngularMotion;
    [SerializeField] private ConfigurableJointMotion yAngularMotion;
    [SerializeField] private ConfigurableJointMotion zAngularMotion;

    [Header("Springs")]
    [SerializeField] private float xPosSpring;
    [SerializeField] private float yPosSpring;
    [SerializeField] private float zPosSpring;

    [Header("Dampers")]
    [SerializeField] private float xPosDamper;
    [SerializeField] private float yPosDamper;
    [SerializeField] private float zPosDamper;


    public Vector3 HandsGrabLocalPos => handsGrabLocalPos;

    public ConfigurableJointMotion XMotion => xMotion;
    public ConfigurableJointMotion YMotion => yMotion;
    public ConfigurableJointMotion ZMotion => zMotion;

    public ConfigurableJointMotion XAngularMotion => xAngularMotion;
    public ConfigurableJointMotion YAngularMotion => yAngularMotion;
    public ConfigurableJointMotion ZAngularMotion => zAngularMotion;

    public float XPosSpring => xPosSpring;
    public float YPosSpring => yPosSpring;
    public float ZPosSpring => zPosSpring;

    public float XPosDamper => xPosDamper;
    public float YPosDamper => yPosDamper;
    public float ZPosDamper => zPosDamper;

    public float AngularYLimit => angularYLimit;
}