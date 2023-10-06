using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class KinovaArmController : UnityPublisher<MessageTypes.KortexDriver.TwistCommand>
    {
        float buttonSpeed = 0.4f;

        private MessageTypes.KortexDriver.TwistCommand message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
   
            message = new MessageTypes.KortexDriver.TwistCommand
            {
                reference_frame = 0,
                twist = new MessageTypes.KortexDriver.Twist {linear_x = 0.0f, linear_y = 0.0f, linear_z = 0.0f, angular_x = 0.0f, angular_y = 0.0f, angular_z = 0.0f },
                duration = 0,
            };
        }

        private void Update()
        {
            //Get.Axis returns a -1 or +1 depending on what side the button is pushed to
            float horizontal = Input.GetAxis("LeftStickHorizontal") * 0.1f;
            float vertical = Input.GetAxis("LeftStickVertical") * 0.1f;
            float vertical2 = Input.GetAxis("RightStickVertical") * 0.1f;
            float xRotationButton = Input.GetAxis("DPADHorizontal") * 0.3f;
            float zRotationButton = Input.GetAxis("DPADVertical") * 0.3f;
            float yRotationButton = Input.GetAxis("RightStickHorizontal") * 0.1f;
           
            bool changeToHusky = Input.GetButton("Start"); //detect button state
            /*We need the robot arm to move only if the start button is not held down */

            //send a velocity of +0.25 or -0.25 to the arm
            message.twist.linear_x = (float)horizontal;
            message.twist.linear_y = (float)vertical;
            message.twist.linear_z = (float)vertical2;
            message.twist.angular_y = (float)xRotationButton;
            message.twist.angular_z = (float)zRotationButton;
            message.twist.angular_x = (float)yRotationButton;

            if (changeToHusky == false) {
            Publish(message);
            }
            

            

        }
    }
}