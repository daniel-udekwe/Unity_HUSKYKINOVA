using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class GripperPublisher : UnityPublisher<MessageTypes.Std.Float32>
    {
        private float speed = 10f;
        private float openGrip;

        private MessageTypes.Std.Float32 message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Float32();
        }


        private void Update()
        {
            float openGripper = Input.GetAxis("LeftTrigger");
            float closeGripper = Input.GetAxis("RightTrigger");
            

            if (openGripper == 1.0f)  //check if LT button is pressed
            {

                if (openGrip > 0.0f) //Gripper strenght must be greater than 0.0
                {
                    /*openGrip -= 0.02f;
                    openGrip = Math.Max(openGrip, 0.0f);
                    message.data = openGrip;
                    Publish(message);
                    print(openGrip);*/

                    openGrip = 0.0f;  //close the gripper
                    message.data = openGrip;
                    Publish(message); //publish the gripper strength
                    //print(openGrip);  //print gripper strength
                }

            }

            if (closeGripper == 1.0f)  //check if RT button is pressed
            {
                if (openGrip < 1.0f)  //Gripper strength must be less than 1.0
                {
                    /*openGrip += 0.02f;
                    openGrip = Math.Min(openGrip, 1.0f);
                    message.data = openGrip;
                    Publish(message);
                    print(openGrip);*/

                    openGrip = 1.0f;  //open the gripper
                    message.data = openGrip;
                    Publish(message); //pubish the gripper strenght
                    //print(openGrip); //print gripper strength
                }

            }

        }

    }
}