using UnityEngine;

/// <summary>
/// Создаёт и удаляет соединение Configurable Joint между игроком и объектом 
/// </summary>
public class JointCreator 
{
    private ConfigurableJoint handJoint;
    private HandJointConfig config;
    private Rigidbody playerRb;

    public JointCreator(HandJointConfig config, Rigidbody playerRb)
    {
        this.config = config;
        this.playerRb = playerRb;
    }
    /// <summary>
    /// Создаёт соединение между игроком и объектом. Точка захвата игрока берётся из HandJointConfig.
    /// </summary>
    /// <param name="handlerWorldPosition">Позиция зацепа в мировых координатах</param>
    public void Attach(
        Vector3 handlerWorldPosition)
    {
        handJoint = playerRb.gameObject.AddComponent<ConfigurableJoint>();

        handJoint.autoConfigureConnectedAnchor = false;
  
        handJoint.anchor = config.HandsGrabLocalPos;

        handJoint.connectedAnchor = handlerWorldPosition;
        Debug.Log(handJoint.connectedAnchor);

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
            positionDamper = config.YPosDamper
        };
        handJoint.zDrive = new JointDrive {
            positionSpring = config.ZPosSpring,
            positionDamper = config.ZPosDamper
        };

        handJoint.linearLimit = new SoftJointLimit { limit = config.LinearLimit };

    }

    /// <summary>
    /// Удаляет соединение
    /// </summary>
    public void Detach()
    {
        if (handJoint == null)
            return;

        GameObject.Destroy(handJoint);
        handJoint = null;
    }
}
