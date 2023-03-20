using System;
using System.CommandLine;
using System.Collections.Generic;
using Hwdtech;
using Spaceship__Server;
using Moq;

class Program
{
    public async static Task Main (string [] args)
    {
        new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Create And Start Thread", (object[] args)=> 
        {
            Action action = (Action)args[1];
            action();
            return action;
            
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Soft Stop Thread", (object[] args)=> 
        {
            Action action = (Action)args[1];
            action();
            return action;
            
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Hard Stop Thread", (object[] args)=> 
        {
            Action action = (Action)args[1];
            action();
            return action;
            
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "ExceptionHandler.Register", (object[] args)=>
        {
            string commandName = (string) args[0];
            string Exception = (string) args[1];
            Action strategy = (Action) args[2]; 
            Mock<Spaceship__Server.ICommand> cmd = new();
            return cmd.Object;
        }).Execute();
        Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("ExceptionHandler.Register", "Soft Stop", "System.Exeption", ()=>
        {
            Console.WriteLine("Exception Handled");
        }).Execute();
        Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("ExceptionHandler.Register", "Hard Stop", "System.Exeption", ()=>
        {
            Console.WriteLine("Exception Handled");
        }).Execute();
        var threadNum = new Option<string>(name: "--threads") {IsRequired = true};
        var rootCommand = new RootCommand();
        rootCommand.Add(threadNum);
        rootCommand.SetHandler ((threadString)=>{
            Console.WriteLine("Launching server...");
            int threadsNum = int.Parse(threadString);
            List<string> threadNames = new List<string>();
            for(int i = 1; i <= threadsNum; i++)
            {
                threadNames.Add(String.Format("{0}",i));
            } 
            foreach(string i in threadNames)
            {
                Hwdtech.IoC.Resolve<object>("Create And Start Thread", i, () => 
                {
                    Console.WriteLine(String.Format("Started thread {0}", i));
                });
            }
            Console.WriteLine("Server started successfully.");
            Console.ReadKey();
            Console.WriteLine("Shutting down.");
            foreach(string i in threadNames)
            {
                Hwdtech.IoC.Resolve<object>("Soft Stop Thread", "1", ()=> 
                {
                    Console.WriteLine(String.Format("Stoped thread {0}", i));
                });
            }
            Console.WriteLine("Server shut down successfully.");
            
        }, threadNum);
        await rootCommand.InvokeAsync(args);
    }
}