using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatGoesAround.Common
{
    public class Message
    {
        public string MessageType { get; set; }

        public Message(string messageType)
        {
            this.MessageType = messageType;
        }
    }

    public class BeginGameMessage : Message
    {
        public BeginGameMessage() : base("BeginGame")
        {
        }
    }

    public class BeginRoundMessage : Message
    {
        public int RoundNumber { get; set; }
        public BeginRoundMessage(int roundNumber) : base("BeginRound")
        {
            this.RoundNumber = roundNumber;
        }
    }

    public class SelectPlayerMessage : Message
    {
        public string DeviceId { get; set; }
        public bool Red { get; set; }
        public bool Blue { get; set; }
        public SelectPlayerMessage(string deviceId, bool red, bool blue) : base("SelectPlayer")
        {
            DeviceId = deviceId;
            Red = red;
            Blue = blue;
        }
    }

    public class EndRoundMessage : Message
    {
        public EndRoundMessage(int roundNumber) : base("EndRound")
        {
        }
    }

    public class BeginPlaySequenceMessage : Message
    {
        public BeginPlaySequenceMessage() : base("BeginPlaySequence")
        {
        }
    }

    public class EndPlaySequenceMessage : Message
    {
        public EndPlaySequenceMessage() : base("EndPlaySequence")
        {
        }
    }

    public class UpdatePlayerScoresMessage : Message
    {
        public Dictionary<string, int> PlayerScores { get; set; }
        public UpdatePlayerScoresMessage() : base ("UpdatePlayerScores") { }
    }



    public class PushButtonCombinationMessage : Message
    {
        public List<int> ButtonIds { get; set; }
        public PushButtonCombinationMessage() : base("PushButtonCombination") { }
    }
}
