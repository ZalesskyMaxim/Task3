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
        private IList<int> _telephoneNumber;
        Random rnd;
        public ATS()
        {
            _listPorts = new List<Port>();
            _telephoneNumber = new List<int>();
            rnd = new Random();
        }

        public Terminal GetNewTerminal()
        {
            var number = rnd.Next(1, 9);
            _telephoneNumber.Add(number);
            var newPort = new Port(this);
            _listPorts.Add(newPort);
            var newTerminal = new Terminal(number, newPort);
            return newTerminal;
        }

        public void CallingTo(object sender, CallEventArgs e)
        {
            if (_telephoneNumber.Contains(e.targetTelephoneNumber))
            {
                var index = _telephoneNumber.IndexOf(e.targetTelephoneNumber);
                if (_listPorts[index].State == Enums.PortState.Connect)
                {
                    _listPorts[index].IncomingCall(e.telephoneNumber);
                }
            }
        }

        public void AnswerTo(object sender, AnswerEventArgs e)
        {
            if (_telephoneNumber.Contains(e.telephoneNumber))
            {
                var index = _telephoneNumber.IndexOf(e.telephoneNumber);
                if (_listPorts[index].State == Enums.PortState.Connect)
                {
                    _listPorts[index].AnswerCall(e.telephoneNumber, e.StateInCall);
                }
            }
        }
    }
}
