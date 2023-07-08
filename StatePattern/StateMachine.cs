using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatePattern
{
    namespace  StateMachine
    {
        public class GumballMachine
        {
            private IState _noQuarterState = new NoQuarterState();
            private IState _hasQuarterState = new HasQuarterState();
            private IState _soldState = new SoldState();
            private IState _soldOutState = new SoldOutState();
            private IState _winnerState = new WinnerState();
            private IState _currentState;
            private int _gumballCount = 100;

            public GumballMachine()
            {
                _currentState = _soldOutState;
                Console.WriteLine("Welcome to the party! Please insert quarter to start...");
            }

            public void SetState(IState state)
            {
                _currentState = state;
            }
            public IState GetState()
            {
                return _currentState;
            }
            public IState GetHasQuaterState()
            {
                return _hasQuarterState;
            }
            public IState GetNoQuaterState()
            {
                return _noQuarterState;
            }
            public IState GetSoldState()
            {
                return _soldState;
            }
            public IState GetSoldOutState(){
                return _soldOutState;
            }
            public IState GetWinnerState()
            {
                return _winnerState;
            }

            public void InsertQuarter()
            {
                _currentState.InsertQuarter(this);
            }
            public void EjectQuarter()
            {
                _currentState.EjectQuarter(this);
            }
            public void TurnCrank()
            {
                _currentState.TurnCrank(this);
            }
            public int GetNumberOfGumballs()
            {
                return _gumballCount;
            }
            public void ReleaseBall()
            {                 
                Console.WriteLine("A gumball comes rolling out the slot...");
                if (_gumballCount != 0)
                {
                    _gumballCount--;
                }
            }
        }

        public interface IState
        {
            // defines the actions that can be performed on the context
            public void InsertQuarter(GumballMachine gumballMachine);
            public void EjectQuarter(GumballMachine gumballMachine);
            public void TurnCrank(GumballMachine gumballMachine);
            public void Dispense(GumballMachine gumballMachine);
        }

        internal class NoQuarterState : IState
        {

            public void InsertQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("You inserted a quarter");
                gumballMachine.SetState(gumballMachine.GetHasQuaterState());
            }

            public void EjectQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("You haven't inserted a quarter");
            }

            public void TurnCrank(GumballMachine gumballMachine)
            {
                Console.WriteLine("You turned, but there's no quarter");
            }

            public void Dispense(GumballMachine gumballMachine)
            {
                Console.WriteLine("You need to pay first");
            }
        }

        internal class HasQuarterState : IState
        {
            private Random _randomWinner = new Random(DateTime.Now.Millisecond);
            public void Dispense(GumballMachine gumballMachine)
            {
                Console.WriteLine("No gumball dispensed");
            }

            public void EjectQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("Quarter returned");
                gumballMachine.SetState(gumballMachine.GetNoQuaterState());
                
            }

            public void InsertQuarter(GumballMachine gumballMachine)
            {
                Console.Write("You can't insert another quarter");
            }

            public void TurnCrank(GumballMachine gumballMachine)
            {
                Console.WriteLine("You turned...");
                gumballMachine.SetState(gumballMachine.GetSoldState());
                int winner = _randomWinner.Next(10);
                if((winner == 0) && (gumballMachine.GetNumberOfGumballs() > 1))
                {
                    gumballMachine.SetState(gumballMachine.GetWinnerState());
                    gumballMachine.GetState().Dispense(gumballMachine);
                }
                else
                {
                    gumballMachine.SetState(gumballMachine.GetSoldState());
                    gumballMachine.GetState().Dispense(gumballMachine);
                }
            }
        }

        internal class SoldState : IState
        {
            public void Dispense(GumballMachine gumballMachine)
            {
                gumballMachine.ReleaseBall();
                if(gumballMachine.GetNumberOfGumballs() > 0)
                {
                    gumballMachine.SetState(gumballMachine.GetNoQuaterState());
                }
                else
                {
                    Console.WriteLine("Oops, out of gumballs!");
                    gumballMachine.SetState(gumballMachine.GetSoldOutState());
                }
            }

            public void EjectQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("Sorry, you already turned the crank");
            }

            public void InsertQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("Please wait, we're already giving you a gumball");
            }

            public void TurnCrank(GumballMachine gumballMachine)
            {
                Console.WriteLine("Turning twice doesn't get you another gumball!");
            }
        }

        internal class SoldOutState : IState
        {
            public void Dispense(GumballMachine gumballMachine)
            {
                Console.WriteLine("No gumball dispensed");
            }

            public void EjectQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("You can't eject, you haven't inserted a quarter yet");
            }

            public void InsertQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("You can't insert a quarter, the machine is sold out");
            }

            public void TurnCrank(GumballMachine gumballMachine)
            {
                Console.WriteLine("You turned, but there are no gumballs");
            }
        }

        internal class WinnerState : IState
        {
            public void Dispense(GumballMachine gumballMachine)
            {
                Console.WriteLine("YOU'RE A WINNER! You get two gumballs for your quarter");
                gumballMachine.ReleaseBall();
                if (gumballMachine.GetNumberOfGumballs() == 0)
                {
                    gumballMachine.SetState(gumballMachine.GetSoldOutState());
                }
                else
                {
                    gumballMachine.ReleaseBall();
                    if (gumballMachine.GetNumberOfGumballs() > 0)
                    {
                        gumballMachine.SetState(gumballMachine.GetNoQuaterState());
                    }
                    else
                    {
                        Console.WriteLine("Oops, out of gumballs!");
                        gumballMachine.SetState(gumballMachine.GetSoldOutState());
                    }
                }
            }

            public void EjectQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("Sorry, you already turned the crank");
            }

            public void InsertQuarter(GumballMachine gumballMachine)
            {
                Console.WriteLine("Please wait, we're already giving you a gumball");
            }

            public void TurnCrank(GumballMachine gumballMachine)
            {
                Console.WriteLine("Turning twice doesn't get you another gumball!");
            }
        }
    }
}
