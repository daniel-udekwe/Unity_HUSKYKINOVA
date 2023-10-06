using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR; // For SteamVR classes

namespace RosSharp.RosBridgeClient
{
    public class Husky : UnityPublisher<MessageTypes.Geometry.Twist>
    {
        public SteamVR_Action_Vector2 dpadAction;
        float horizontal;
        float vertical;
        private MessageTypes.Geometry.Twist message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();

        }

        private void InitializeMessage()
        {

            message = new MessageTypes.Geometry.Twist();
        }

        void Update()
        {
            // Get the D-pad values as a Vector2
            Vector2 dpadValues = dpadAction.GetAxis(SteamVR_Input_Sources.LeftHand);

            //use a threshold to determine which button was pushed.
            if (dpadValues.x >= 0.6)
            {
                horizontal = -0.5f;
            }
            else if (dpadValues.x < -0.6)
            {
                horizontal = 0.5f;
            }
            else
                horizontal = 0.0f;

            if (dpadValues.y >= 0.6)
            {
                vertical = 0.3f;
            }
            else if (dpadValues.y < -0.6)
            {
                vertical = -0.3f;
            }
            else
                vertical = 0.0f;

            //print(vertical);
            //print(horizontal);
            Vector3 linearVelocity = new Vector3(vertical, 0.0f, 0.0f);
            Vector3 angularVelocity = new Vector3(0.0f, 0.0f, horizontal);

            message.linear = GetGeometryVector3(linearVelocity);
            message.angular = GetGeometryVector3(angularVelocity);
            Publish(message);
           
        }

        private static MessageTypes.Geometry.Vector3 GetGeometryVector3(Vector3 vector3)
        {
            MessageTypes.Geometry.Vector3 geometryVector3 = new MessageTypes.Geometry.Vector3();
            geometryVector3.x = vector3.x;
            geometryVector3.y = vector3.y;
            geometryVector3.z = vector3.z;
            return geometryVector3;
        }
    }
}
