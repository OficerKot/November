using UnityEngine;

public class JointCreator : MonoBehaviour 
{
    private ConfigurableJoint handJoint;
    private HandJointConfig config;
    private Rigidbody playerRb;

    public JointCreator(HandJointConfig config, Rigidbody playerRb)
    {
        this.config = config;
        this.playerRb = playerRb;
    }
    public void Attach(
        Rigidbody hookBody,
        Vector3 handWorldPosition,
        Vector3 hookWorldPosition)
    {
        handJoint = playerRb.gameObject.AddComponent<ConfigurableJoint>();

        handJoint.autoConfigureConnectedAnchor = false;
        handJoint.connectedBody = hookBody;

        handJoint.anchor =
            playerRb.transform.InverseTransformPoint(handWorldPosition);

        handJoint.connectedAnchor =
            hookBody.transform.InverseTransformPoint(hookWorldPosition);

        ConfigureJoint();
    }

    /// <summary>
    /// Создаёт ограничения для вращения игрока. Убирает ограничения движения (для раскачивания)
    /// </summary>
    private void ConfigureJoint()
    {
        handJoint.xMotion = config.XMotion;
        handJoint.yMotion = config.YMotion;
        handJoint.zMotion = config.ZMotion;

        handJoint.angularXMotion = config.XAngularMotion;
        handJoint.angularYMotion = config.YAngularMotion;
        handJoint.angularZMotion = config.ZAngularMotion;

        handJoint.xDrive = new JointDrive {
            positionSpring = config.XPosSpring,
            positionDamper = config.XPosDamper
        };
        handJoint.yDrive = new JointDrive {
            positionSpring = config.YPosSpring,
            positionDamper  =config.YPosDamper
        };
        handJoint.zDrive = new JointDrive {
            positionSpring = config.ZPosSpring,
            positionDamper = config.ZPosDamper
        };

    }
    public void Detach()
    {
        if (handJoint == null)
            return;

        Destroy(handJoint);
        handJoint = null;
    }
}
