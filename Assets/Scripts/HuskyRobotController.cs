using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class HuskyRobotController : UnityPublisher<MessageTypes.Geometry.Twist>
    {

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

        private void FixedUpdate()
        {
            //Get.Axis returns a -1 or +1 depending on what side the button is pushed to
            float horizontal = Input.GetAxis("LeftStickHorizontal") * 0.1f;  //get value from button and scale it down since testing in the lab
            float vertical = Input.GetAxis("LeftStickVertical") * -0.1f;   //get value from button and scale it down since testing in the lab
            /*negative sign in the code above is because Unity is weird */

            bool changeToHusky = Input.GetButton("Start"); //detect button state
            /*We need the husky robot to respond only if a secondary button is held donw*/

         
            Vector3 linearVelocity = new Vector3(vertical, 0.0f, 0.0f);
            Vector3 angularVelocity = new Vector3(0.0f, 0.0f, horizontal);
            
            message.linear = GetGeometryVector3(linearVelocity);
            message.angular = GetGeometryVector3(angularVelocity);
           
            if (changeToHusky == true)
            {
                Publish(message);
            } 
                   
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