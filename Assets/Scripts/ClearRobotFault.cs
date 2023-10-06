using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace RosSharp.RosBridgeClient
{
    public class ClearRobotFault : UnityPublisher<MessageTypes.Std.Empty>
    {
      

        private MessageTypes.Std.Empty message;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Empty();
        }


        private void Update()
        {
           
           Publish(message); 
 
        }

    }
}