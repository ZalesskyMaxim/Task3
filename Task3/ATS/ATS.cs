using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class ATS
    {
        private IList<Port> _listPorts;
        private IList<int> _listTelephoneNumbers;
        Random rnd;
        public ATS()
        {
            _listPorts = new List<Port>();
            _listTelephoneNumbers = new List<int>();
            rnd = new Random();
        }

        public Terminal GetNewTerminal(int numberTelephone)
        {
            //var number = rnd.Next(1000000, 9999999);

            _listTelephoneNumbers.Add(numberTelephone);
            var newPort = new Port(this);
            _listPorts.Add(newPort);
            var newTerminal = new Terminal(numberTelephone, newPort);
            return newTerminal;
        }

        public void CallingTo(object sender, CallEventArgs e)
        {
            if (_listTelephoneNumbers.Contains(e.targetTelephoneNumber) && e.targetTelephoneNumber != e.telephoneNumber)
            {
                var index = _listTelephoneNumbers.IndexOf(e.targetTelephoneNumber);
                if (_listPorts[index].State == Enums.PortState.Connect)
                {
                    _listPorts[index].IncomingCall(e.telephoneNumber, e.targetTelephoneNumber);
                }
            }
            else if (!_listTelephoneNumbers.Contains(e.targetTelephoneNumber))
            {
                Console.WriteLine("You have calling a non-existent number!!!");
            }
            else
            {
                Console.WriteLine("You have calling a your number!!!");
            }
        }

        public void AnswerTo(object sender, AnswerEventArgs e)
        {
            if (_listTelephoneNumbers.Contains(e.telephoneNumber))
            {
                var index = _listTelephoneNumbers.IndexOf(e.telephoneNumber);
                if (_listPorts[index].State == Enums.PortState.Connect)
                {
                    _listPorts[index].AnswerCall(e.targetTelephoneNumber, e.telephoneNumber, e.StateInCall);
                }
            }
        }
    }
}
