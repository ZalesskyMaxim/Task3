using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Args;
using Task3.Enums;

namespace Task3
{
    public class ATS
    {
        private IDictionary<int, Tuple<Port, Contract>> _usersData;
        Random rnd;
        //private IList<Contract> _listContract;
        public ATS()
        {
            _usersData = new Dictionary<int, Tuple<Port, Contract>>();
            //_listTelephoneNumbers = new List<int>();
            rnd = new Random();
            //_listContract = new List<Contract>();
        }

        public Terminal GetNewTerminal(Contract contract)
        {

            //var numberTelephone = rnd.Next(1000000, 9999999);
            //_listTelephoneNumbers.Add(numberTelephone);

            var newPort = new Port();

            newPort.AnswerEvent += CallingTo;
            newPort.CallEvent += CallingTo;
            //_listPorts.Add(newPort);
            _usersData.Add(contract.Number, new Tuple<Port, Contract>(newPort, contract));
            var newTerminal = new Terminal(contract.Number, newPort);
            return newTerminal;
        }

        public Contract RegisterContract(Subscriber subscriber, TariffType type)
        {
            var contract = new Contract(subscriber, type);
            //_listContract.Add(contract);
            return contract;
        }

        //public void CallingTo(object sender, ICallingEventArgs e)
        //{
        //    if (_listTelephoneNumbers.Contains(e.TargetTelephoneNumber) && e.TargetTelephoneNumber != e.TelephoneNumber)
        //    {
        //        var index = _listTelephoneNumbers.IndexOf(e.TargetTelephoneNumber);
        //        if (_listPorts[index].State == Enums.PortState.Connect)
        //        {
        //            _listPorts[index].IncomingCall(e.TelephoneNumber, e.TargetTelephoneNumber);
        //        }
        //    }
        //    else if (!_listTelephoneNumbers.Contains(e.TargetTelephoneNumber))
        //    {
        //        Console.WriteLine("You have calling a non-existent number!!!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("You have calling a your number!!!");
        //    }
        //}

        public void CallingTo(object sender, ICallingEventArgs e)
        {
            if (_usersData.ContainsKey(e.TargetTelephoneNumber) && e.TargetTelephoneNumber != e.TelephoneNumber)
            {
                var port = _usersData[(e.TargetTelephoneNumber)].Item1;
                if (port.State == Enums.PortState.Connect)
                {
                    var tuple = _usersData[(e.TargetTelephoneNumber)];
                    if (e is AnswerEventArgs)
                    {
                        
                        var answerArgs = (AnswerEventArgs)e;
                        if (answerArgs.StateInCall == CallState.Answered )
                        {
                            //var tuple = _usersData[(e.TargetTelephoneNumber)];
                            tuple.Item2.Subscriber.RemoveMoney(tuple.Item2.Tariff.CostOfCall);
                        }
                        port.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall);
                    }
                    if (e is CallEventArgs)
                    {
                        if (tuple.Item2.Subscriber.Money > 0)   ///допилить в зависимости от тарифа
                        {
                            var callArgs = (CallEventArgs)e;
                            port.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber);
                        }
                    }
                }
            }
            else if (!_usersData.ContainsKey(e.TargetTelephoneNumber))
            {
                Console.WriteLine("You have calling a non-existent number!!!");
            }
            else
            {
                Console.WriteLine("You have calling a your number!!!");
            }
        }
    }
}
