using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Enums;

namespace Task3
{
    public class Terminal
    {
        private int _number;
        public int Number
        {
            get
            { 
                return _number;
            }
        }
        private Port _terminalPort;
        public delegate void CallEventHandler(object sender, CallEventArgs e);
        public event CallEventHandler CallEvent;
        public delegate void AnswerEventHandler(object sender, AnswerEventArgs e);
        public event AnswerEventHandler AnswerEvent;
        public Terminal(int number, Port port)
        {
            this._number = number;
            this._terminalPort = port;
        }
        protected virtual void RaiseCallEvent(int targetNumber)
        {
            if (CallEvent != null)
                CallEvent(this, new CallEventArgs(_number, targetNumber));
        }

        protected virtual void RaiseAnswerEvent(int incomingNumber, CallState state)
        {
            if (AnswerEvent != null)
            {
                //AnswerEvent(this, new AnswerEventArgs(_number, incomingNumber, state));
                AnswerEvent(this, new AnswerEventArgs(incomingNumber, _number, state));
            }
        }

        public void Call(int targetNumber)
        {
            RaiseCallEvent(targetNumber);
        }

        public void TakeIncomingCall(object sender, CallEventArgs e)
        {
            Console.WriteLine("Have incoming Call at number: {0} to terminal {1}", e.telephoneNumber, e.targetTelephoneNumber);
        }

        public void ConnectToPort()
        {
            _terminalPort.Connect(this);
        }
        
        public void AnswerToCall(int incoming, CallState state)
        {
            RaiseAnswerEvent(incoming, state);
        }

        public void RejectIncomingCall()
        { 
            
        }

        public void TakeAnswer(object sender, AnswerEventArgs e)
        {
            if (e.StateInCall == CallState.Answered) 
            {
                Console.WriteLine("Terminal with number: {0}, have answer on call a number: {1}", e.targetTelephoneNumber, e.telephoneNumber);
            }
            else
            {
                Console.WriteLine("Terminal with number: {0}, have rejected on call a number: {1}", e.targetTelephoneNumber, e.telephoneNumber);
            }
            
            
        }

    }
}
