using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task3.Args;
using Task3.Enums;

namespace Task3.AutomaticTelephoneExchange
{
    public class ATE
    {
        private IDictionary<int, Tuple<Port, Contract>> _usersData;
        Random rnd;
        private IList<CallInformation> callList = new List<CallInformation>();
        public ATE()
        {
            _usersData = new Dictionary<int, Tuple<Port, Contract>>();
            rnd = new Random();
        }

        public Terminal GetNewTerminal(Contract contract)
        {
            var newPort = new Port();
            newPort.AnswerEvent += CallingTo;
            newPort.CallEvent += CallingTo;
            _usersData.Add(contract.Number, new Tuple<Port, Contract>(newPort, contract));
            var newTerminal = new Terminal(contract.Number, newPort);
            return newTerminal;
        }

        public Contract RegisterContract(Subscriber subscriber, TariffType type)
        {
            var contract = new Contract(subscriber, type);
            return contract;
        }

        public void CallingTo(object sender, ICallingEventArgs e)
        {
            if (_usersData.ContainsKey(e.TargetTelephoneNumber) && e.TargetTelephoneNumber != e.TelephoneNumber)
            {
                var targetPort = _usersData[(e.TargetTelephoneNumber)].Item1;
                var port = _usersData[(e.TelephoneNumber)].Item1;
                if (targetPort.State == Enums.PortState.Connect && port.State == PortState.Connect)
                {
                    var tuple = _usersData[(e.TelephoneNumber)];
                    var targetTuple = _usersData[(e.TargetTelephoneNumber)];

                    if (e is AnswerEventArgs)
                    {
                        
                        var answerArgs = (AnswerEventArgs)e;
                        CallInformation inf = null;
                        if (answerArgs.Id == null)
                        {
                            inf = new CallInformation();
                            callList.Add(inf);
                        }

                        if (answerArgs.Id != null && callList.Any(x => x.Id == answerArgs.Id))
                        {
                            inf = callList.First(x => x.Id == answerArgs.Id);
                        }
                        if (answerArgs.StateInCall == CallState.Answered )
                        {
                            targetTuple.Item2.Subscriber.RemoveMoney(tuple.Item2.Tariff.CostOfCall);
                        }
                        if (inf != null)
                        {
                            //targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber, inf.Id);
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall, inf.Id);
                        }
                        else
                        {
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall);
                        }
                        //targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall);
                    }
                    if (e is CallEventArgs)
                    {
                        if (tuple.Item2.Subscriber.Money > tuple.Item2.Tariff.CostOfCall)
                        {
                            var callArgs = (CallEventArgs)e;
                            CallInformation inf = null;
                            if (callArgs.Id == null)
                            {
                                inf = new CallInformation();
                                callList.Add(inf);
                            }

                            if(callArgs.Id != null && callList.Any(x=>x.Id == callArgs.Id))
                            {
                                inf = callList.First(x => x.Id == callArgs.Id);
                            }
                            if (inf != null)
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber, inf.Id);
                            }
                            else
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Terminal with number {0} is not enough money in the account!", e.TelephoneNumber);
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
