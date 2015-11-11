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

        protected virtual void RaiseIncomingCallEvent(int incomingNumber)
        {
            if (IncomingCallEvent != null)
                IncomingCallEvent(this, new CallEventArgs(incomingNumber, 0));
        }
        protected virtual void RaiseAnswerCallEvent(int outcomingNumber, CallState state)
        {
            if (PortAnswerEvent != null)
                PortAnswerEvent(this, new AnswerEventArgs(outcomingNumber, 0, state));
        }

        public void IncomingCall(int incomingNumber)
        {
            RaiseIncomingCallEvent(incomingNumber);
        }

        public void AnswerCall(int outcomingNumber, CallState state)
        {
            RaiseAnswerCallEvent(outcomingNumber, state);
        }


    }
}
