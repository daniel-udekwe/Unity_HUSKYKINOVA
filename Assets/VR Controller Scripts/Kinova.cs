using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Valve.VR;
using System.IO;

namespace RosSharp.RosBridgeClient
{
    public class Kinova : UnityPublisher<MessageTypes.KortexDriver.TwistCommand>
    {

        public SteamVR_Action_Pose contOrient;
        public SteamVR_Action_Single squeezeAction;
        
        public SteamVR_Action_Boolean dpad_center;

        private MessageTypes.KortexDriver.TwistCommand message;

        float x_LinVel;
        float y_LinVel;
        float z_LinVel;

        float x_AngVel;
        float y_AngVel;
        float z_AngVel;

        private string x_pathLink;
        private string y_pathLink;
        private string z_pathLink;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();

            //initialize paths to save files
            //pathLink = Application.persistentDataPath + "/data.txt";
            x_pathLink = "C:\\Users\\Muhammad Ali Qadri\\Pictures" + "/x-controller-position.txt";
            y_pathLink = "C:\\Users\\Muhammad Ali Qadri\\Pictures" + "/y-controller-position.txt";
            z_pathLink = "C:\\Users\\Muhammad Ali Qadri\\Pictures" + "/z-controller-position.txt";

        }

        private void InitializeMessage()
        {

            message = new MessageTypes.KortexDriver.TwistCommand
            {
                reference_frame = 0,
                twist = new MessageTypes.KortexDriver.Twist { linear_x = 0.0f, linear_y = 0.0f, linear_z = 0.0f, angular_x = 0.0f, angular_y = 0.0f, angular_z = 0.0f },
                duration = 5,
            };
        }

        //set the initial toggle state of the button to false
        private bool isToggled = false;

        // Update is called once per frame
        void Update()
        {
            //obtain left trigger value
            float rightTriggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.RightHand);

            //obtain linear velocities of right controller
            Vector3 LinearVel = contOrient.GetVelocity(SteamVR_Input_Sources.RightHand);
            //obtain position of the right controller
            Vector3 controllerPosition = contOrient.GetLocalPosition(SteamVR_Input_Sources.RightHand);


            float x_LinVel = Mathf.Abs(LinearVel.x);
            float y_LinVel = Mathf.Abs(LinearVel.y);
            float z_LinVel = Mathf.Abs(LinearVel.z);

            //obtain angular velocities of right controller
            Vector3 AngularVel = contOrient.GetAngularVelocity(SteamVR_Input_Sources.RightHand);
            float x_AngVel = Mathf.Abs(AngularVel.x);
            float y_AngVel = Mathf.Abs(AngularVel.y);
            float z_AngVel = Mathf.Abs(AngularVel.z);

            //Get the current state of the toggle button
            isToggled = dpad_center.GetState(SteamVR_Input_Sources.RightHand);

            //save x,y,z values of controller to file
            /*File.AppendAllText(x_pathLink,"\n" + controllerPosition.x.ToString());
            File.AppendAllText(y_pathLink, "\n" + controllerPosition.y.ToString());
            File.AppendAllText(z_pathLink, "\n" + controllerPosition.z.ToString());*/
            
            
            //check the state of the button
            if (isToggled)
            {
                if (x_LinVel > y_LinVel && x_LinVel > z_LinVel)
                {
                    if (LinearVel.x > 0.1 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = -0.5f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                    else if (LinearVel.x < -0.1 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.5f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                }
                else if (y_LinVel > x_LinVel && y_LinVel > z_LinVel)
                {
                    if (LinearVel.y > 0.1 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.5f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                    else if (LinearVel.y < -0.1 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = -0.5f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                }
                else if (z_LinVel > x_LinVel && z_LinVel > y_LinVel)
                {
                    if (LinearVel.z > 0.1 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.5f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                    else if (LinearVel.z < -0.1 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = -0.5f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                }
            }
            //if the button is not toggled
            else
            {
                if (x_AngVel > y_AngVel && x_AngVel > z_AngVel)
                {
                    if (AngularVel.x > 1.0 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 1.73f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                    else if (AngularVel.x < -1.0 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = -1.73f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                }
                else if (y_AngVel > x_AngVel && y_AngVel > z_AngVel)
                {
                    if (AngularVel.y > 1.0 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 1.73f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                    else if (AngularVel.y < -1.0 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = -1.73f;
                        message.twist.angular_z = 0.0f;
                        Publish(message);
                    }
                }
                else if (z_AngVel > x_AngVel && z_AngVel > y_AngVel)
                {
                    if (AngularVel.z > 1.0 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = 1.73f;
                        Publish(message);
                    }
                    else if (AngularVel.z < -1.0 && rightTriggerValue >= 0.8)
                    {
                        message.twist.linear_x = 0.0f;
                        message.twist.linear_y = 0.0f;
                        message.twist.linear_z = 0.0f;

                        message.twist.angular_x = 0.0f;
                        message.twist.angular_y = 0.0f;
                        message.twist.angular_z = -1.73f;
                        Publish(message);
                    }
                }
                Publish(message); 
            }
     
            /*continuous movement*/
            //Publish(message);


            if (rightTriggerValue < 0.8f)
            {
                message.twist.linear_x = 0.0f;
                message.twist.linear_y = 0.0f;
                message.twist.linear_z = 0.0f;

                message.twist.angular_x = 0.0f;
                message.twist.angular_y = 0.0f;
                message.twist.angular_z = 0.0f;
                Publish(message);
            }
        }


    }
}