using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


namespace RosSharp.RosBridgeClient
{
    public class Gripper : UnityPublisher<MessageTypes.Std.Float32>
    {
        public SteamVR_Action_Single squeezeAction;

        private MessageTypes.Std.Float32 message;
        private bool _publishedMessage = true;
        private bool _activeState = false;
        


        protected override void Start()
        {
            base.Start();
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Std.Float32();
        }

        private void PublishMessage()
        {
            StartCoroutine(WaitForSeconds());
        }

        // Update is called once per frame
        private void FixedUpdate()
        {

            float triggerValue = squeezeAction.GetAxis(SteamVR_Input_Sources.LeftHand);

            //might take out the _activeState mechanism
            if (triggerValue >= 0.0f)
            {
                _activeState = true;
            }
            else
                _activeState = false;

            message.data = triggerValue;

            if (_publishedMessage && _activeState)
            {
                PublishMessage();
                _publishedMessage = false;
            }

        }
        private IEnumerator WaitForSeconds()
        {
            yield return new WaitForSeconds(1.2f);
            Publish(message);
            _publishedMessage = true;
        }


    }
}