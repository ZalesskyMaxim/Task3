using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;

namespace Task3
{
    
    public class Port
    {
        public PortState State;
        private ATS ats;
        public bool Flag;
        public delegate void PortEventHandler(object sender, CallEventArgs e);
        public event PortEventHandler IncomingCallEvent;
        public delegate void PortAnswerEventHandler(object sender, AnswerEventArgs e);
        public event PortAnswerEventHandler PortAnswerEvent;

        public Port(ATS ats)
        {
            State = PortState.Disconnect;
            this.ats = ats;
        }
        public Port()
        {
            State = PortState.Disconnect;
        }

        public bool Connect(Terminal terminal)
        {
            if (State == PortState.Disconnect)
            {
                State = PortState.Connect;
                terminal.CallEvent += ats.CallingTo;
                IncomingCallEvent += terminal.TakeIncomingCall;
                terminal.AnswerEvent += ats.AnswerTo;
                PortAnswerEvent += terminal.TakeAnswer;
                Flag = true;
            }
            return Flag;
        }

        public bool Disconnect(Terminal terminal)
        {
            if(State == PortState.Connect)
            {
                State = PortState.Disconnect;
                terminal.CallEvent -= ats.CallingTo;
                IncomingCallEvent -= terminal.TakeIncomingCall;
                Flag = false;
            }
            return false;
        }

        protected virtual void RaiseIncomingCallEvent(int number, int incomingNumber)
        {
            if (IncomingCallEvent != null)
            {
                //IncomingCallEvent(this, new CallEventArgs(incomingNumber, number));
                IncomingCallEvent(this, new CallEventArgs(number, incomingNumber));
            }
        }
        protected virtual void RaiseAnswerCallEvent(int number, int outcomingNumber, CallState state)
        {
            if (PortAnswerEvent != null)
            {
                PortAnswerEvent(this, new AnswerEventArgs(outcomingNumber, number, state));
            }
        }

        public void IncomingCall(int number, int incomingNumber)
        {
            RaiseIncomingCallEvent(number, incomingNumber);
        }

        public void AnswerCall(int number, int outcomingNumber, CallState state)
        {
            RaiseAnswerCallEvent(number, outcomingNumber, state);
        }


    }
}
