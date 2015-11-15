using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Args;
using Task3.Enums;

namespace Task3.AutomaticTelephoneExchange
{
    
    public class Port
    {
        public PortState State;
        public bool Flag;
        public delegate void PortEventHandler(object sender, CallEventArgs e);
        public event PortEventHandler IncomingCallEvent;
        public delegate void PortAnswerEventHandler(object sender, AnswerEventArgs e);
        public event PortAnswerEventHandler PortAnswerEvent;
        public delegate void CallEventHandler(object sender, CallEventArgs e);
        public event CallEventHandler CallEvent;
        public delegate void AnswerEventHandler(object sender, AnswerEventArgs e);
        public event AnswerEventHandler AnswerEvent;

        public Port()
        {
            State = PortState.Disconnect;
        }

        public bool Connect(Terminal terminal)
        {
            if (State == PortState.Disconnect)
            {
                State = PortState.Connect;
                terminal.CallEvent += CallingTo;
                terminal.AnswerEvent += AnswerTo;
                Flag = true;
            }
            return Flag;
        }

        public bool Disconnect(Terminal terminal)
        {
            if(State == PortState.Connect)
            {
                State = PortState.Disconnect;
                terminal.CallEvent -= CallingTo;
                terminal.AnswerEvent -= AnswerTo;
                Flag = false;
            }
            return false;
        }

        protected virtual void RaiseIncomingCallEvent(int number, int targetNumber)
        {
            if (IncomingCallEvent != null)
            {
                //IncomingCallEvent(this, new CallEventArgs(incomingNumber, number));
                IncomingCallEvent(this, new CallEventArgs(number, targetNumber));
            }
        }
        protected virtual void RaiseIncomingCallEvent(int number, int targetNumber, Guid id)
        {
            if (IncomingCallEvent != null)
            {
                //IncomingCallEvent(this, new CallEventArgs(incomingNumber, number));
                IncomingCallEvent(this, new CallEventArgs(number, targetNumber, id));
            }
        }
        protected virtual void RaiseAnswerCallEvent(int number, int targetNumber, CallState state)
        {
            if (PortAnswerEvent != null)
            {
                PortAnswerEvent(this, new AnswerEventArgs(number, targetNumber, state));
            }
        }
        protected virtual void RaiseAnswerCallEvent(int number, int targetNumber, CallState state, Guid id)
        {
            if (PortAnswerEvent != null)
            {
                PortAnswerEvent(this, new AnswerEventArgs(number, targetNumber, state, id));
            }
        }

        protected virtual void RaiseCallingToEvent(int number, int targetNumber)
        {
            if (CallEvent != null)
            {
                CallEvent(this, new CallEventArgs(number, targetNumber));
            }
        }

        protected virtual void RaiseAnswerToEvent(int number, int targetNumber, CallState state)
        {
            if (AnswerEvent != null)
            {
                AnswerEvent(this, new AnswerEventArgs(number, targetNumber, state));
            }
        }

        private void CallingTo(object sender, CallEventArgs e)
        {
            RaiseCallingToEvent(e.TelephoneNumber, e.TargetTelephoneNumber);
        }

        private void AnswerTo(object sender, AnswerEventArgs e)
        {
            RaiseAnswerToEvent(e.TelephoneNumber, e.TargetTelephoneNumber, e.StateInCall);
        }

        public void IncomingCall(int number, int targetNumber)
        {
            RaiseIncomingCallEvent(number, targetNumber);
        }
        public void IncomingCall(int number, int targetNumber, Guid id)
        {
            RaiseIncomingCallEvent(number, targetNumber, id);
        }

        public void AnswerCall(int number, int targetNumber, CallState state)
        {
            RaiseAnswerCallEvent(number, targetNumber, state);
        }
        public void AnswerCall(int number, int targetNumber, CallState state, Guid id)
        {
            RaiseAnswerCallEvent(number, targetNumber, state, id);
        }


    }
}
