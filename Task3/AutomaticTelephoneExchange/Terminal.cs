using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Args;
using Task3.Enums;
using Task3.Interfaces;

namespace Task3.AutomaticTelephoneExchange
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
        private Guid _id;
        //public delegate void CallEventHandler(object sender, CallEventArgs e);
        //public event CallEventHandler CallEvent;
        //public delegate void AnswerEventHandler(object sender, AnswerEventArgs e);
        //public event AnswerEventHandler AnswerEvent;
        //public delegate void EndCallEventHandler(object sender, EndCallEventArgs e);
        //public event EndCallEventHandler EndCallEvent;
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<AnswerEventArgs> AnswerEvent;
        public event EventHandler<EndCallEventArgs> EndCallEvent;
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

        protected virtual void RaiseAnswerEvent(int targetNumber, CallState state, Guid id)
        {
            if (AnswerEvent != null)
            {
                //AnswerEvent(this, new AnswerEventArgs(_number, incomingNumber, state));
                AnswerEvent(this, new AnswerEventArgs(_number, targetNumber, state, id));
            }
        }

        protected virtual void RaiseEndCallEvent(Guid id)
        {
            if (EndCallEvent != null)
                EndCallEvent(this, new EndCallEventArgs(id, _number));
        }

        public void Call(int targetNumber)
        {
            RaiseCallEvent(targetNumber);
        }

        public void TakeIncomingCall(object sender, CallEventArgs e)
        {
            bool flag = true;
            _id = e.Id;
            Console.WriteLine("Have incoming Call at number: {0} to terminal {1}", e.TelephoneNumber, e.TargetTelephoneNumber);
            while (flag == true)
            {
                Console.WriteLine("Answer? Y/N");
                char k = Console.ReadKey().KeyChar;
                if (k == 'Y' || k == 'y')
                {
                    flag = false;
                    Console.WriteLine();
                    AnswerToCall(e.TelephoneNumber, CallState.Answered, e.Id);
                }
                else if (k == 'N' || k == 'n')
                {
                    flag = false;
                    Console.WriteLine();
                    EndCall();
                }
                else
                {
                    flag = true;
                    Console.WriteLine();
                }
            }
        }

        public void ConnectToPort()
        {
            if (_terminalPort.Connect(this))
            {
                _terminalPort.CallPortEvent += TakeIncomingCall;
                _terminalPort.AnswerPortEvent += TakeAnswer;
            } 
        }
        
        public void AnswerToCall(int target, CallState state, Guid id)
        {
            RaiseAnswerEvent(target, state, id);
        }

        public void EndCall()
        { 
            RaiseEndCallEvent(_id);
        }

        public void TakeAnswer(object sender, AnswerEventArgs e)
        {
            _id = e.Id;
            if (e.StateInCall == CallState.Answered)
            {
                Console.WriteLine("Terminal with number: {0}, have answer on call a number: {1}", e.TelephoneNumber, e.TargetTelephoneNumber);
            }
            else
            {
                Console.WriteLine("Terminal with number: {0}, have rejected call", e.TelephoneNumber);
            }
        }
    }
}
