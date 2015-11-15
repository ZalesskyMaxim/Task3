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
        //public delegate void CallPortEventHandler(object sender, CallEventArgs e);
        //public event CallPortEventHandler CallPortEvent;
        //public delegate void AnswerPortEventHandler(object sender, AnswerEventArgs e);
        //public event AnswerPortEventHandler AnswerPortEvent;
        //public delegate void CallEventHandler(object sender, CallEventArgs e);
        //public event CallEventHandler CallEvent;
        //public delegate void AnswerEventHandler(object sender, AnswerEventArgs e);
        //public event AnswerEventHandler AnswerEvent;

        //public delegate void EndCallEventHandler(object sender, EndCallEventArgs e);
        //public event EndCallEventHandler EndCallEvent;

        public event EventHandler<CallEventArgs> CallPortEvent;
        public event EventHandler<AnswerEventArgs> AnswerPortEvent;
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerEvent;

        public event EventHandler<EndCallEventArgs> EndCallEvent;

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
                terminal.EndCallEvent += EndCall;
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
                terminal.EndCallEvent -= EndCall;
                Flag = false;
            }
            return false;
        }

        protected virtual void RaiseIncomingCallEvent(int number, int targetNumber)
        {
            if (CallPortEvent != null)
            {
                CallPortEvent(this, new CallEventArgs(number, targetNumber));
            }
        }
        protected virtual void RaiseIncomingCallEvent(int number, int targetNumber, Guid id)
        {
            if (CallPortEvent != null)
            {
                CallPortEvent(this, new CallEventArgs(number, targetNumber, id));
            }
        }
        protected virtual void RaiseAnswerCallEvent(int number, int targetNumber, CallState state)
        {
            if (AnswerPortEvent != null)
            {
                AnswerPortEvent(this, new AnswerEventArgs(number, targetNumber, state));
            }
        }
        protected virtual void RaiseAnswerCallEvent(int number, int targetNumber, CallState state, Guid id)
        {
            if (AnswerPortEvent != null)
            {
                AnswerPortEvent(this, new AnswerEventArgs(number, targetNumber, state, id));
            }
        }

        protected virtual void RaiseCallingToEvent(int number, int targetNumber)
        {
            if (CallEvent != null)
            {
                CallEvent(this, new CallEventArgs(number, targetNumber));
            }
        }

        protected virtual void RaiseAnswerToEvent(AnswerEventArgs eventArgs)
        {
            if (AnswerEvent != null)
            {
                AnswerEvent(this, new AnswerEventArgs(
                    eventArgs.TelephoneNumber, 
                    eventArgs.TargetTelephoneNumber,
                    eventArgs.StateInCall,
                    eventArgs.Id));
            }
        }

        protected virtual void RaiseEndCallEvent(Guid id, int number)
        {
            if (EndCallEvent != null)
            {
                EndCallEvent(this, new EndCallEventArgs(id, number));
            }
        }

        private void CallingTo(object sender, CallEventArgs e)
        {
            RaiseCallingToEvent(e.TelephoneNumber, e.TargetTelephoneNumber);
        }

        private void AnswerTo(object sender, AnswerEventArgs e)
        {
            RaiseAnswerToEvent(e);
        }

        private void EndCall(object sender, EndCallEventArgs e)
        {
            RaiseEndCallEvent(e.Id, e.TelephoneNumber);
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
