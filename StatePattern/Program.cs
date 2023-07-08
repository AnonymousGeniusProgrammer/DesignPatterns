// See https://aka.ms/new-console-template for more information
using StatePattern.StateMachine;


GumballMachine gumball_machine = new GumballMachine();

gumball_machine.InsertQuarter();
gumball_machine.TurnCrank();

gumball_machine.InsertQuarter();
gumball_machine.TurnCrank();

gumball_machine.InsertQuarter();
gumball_machine.TurnCrank();