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
            { return _number; }
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
        protected virtual void RaiseCallEvent(int number)
        {
            if (CallEvent != null)
                CallEvent(this, new CallEventArgs(_number, number));
        }

        protected virtual void RaiseAnswerEvent(int number, CallState state)
        {
            if (AnswerEvent != null)
                AnswerEvent(this, new AnswerEventArgs(_number, number, state));
        }

        public void Call(int number)
        {
            RaiseCallEvent(number);
        }

        public void TakeIncomingCall(object sender, CallEventArgs e)
        {
            Console.WriteLine("Have incoming Call");
            AnswerToCall(e.telephoneNumber, CallState.Answered);
        }

        public void ConnectToPort()
        {
            _terminalPort.Connect(this);
        }
        
        public void AnswerToCall(int number, CallState state)
        {
            RaiseAnswerEvent(number, state);
        }

        public void RejectIncomingCall()
        { 
            
        }

        public void TakeAnswer(object sender, AnswerEventArgs e)
        {
            Console.WriteLine("Have answer");
        }

    }
}
