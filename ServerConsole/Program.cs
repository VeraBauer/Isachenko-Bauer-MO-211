using System;
using System.CommandLine;
using System.Collections.Generic;
using Hwdtech;
using Spaceship__Server;
using Moq;
using System.IO;

class Program
{
    public async static Task Main (string [] args)
    {
          new Hwdtech.Ioc.InitScopeBasedIoCImplementationCommand().Execute();

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Create And Start Thread", (object[] args)=> 
        {
            Action action = (Action)args[1];
            Mock<IThread> thread = new();
            thread.Object.Start();
            action();
            return thread.Object;
            
        }).Execute();
        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Soft Stop Thread", (object[] args)=> 
        {
            Mock<Spaceship__Server.ICommand> cmd = new();
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
            Mock<Spaceship__Server.ICommand> registercmd = new();
            return registercmd.Object;
        }).Execute();
        Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("ExceptionHandler.Register", "Soft Stop", "System.Exeption", ()=>
        {
            StreamWriter sw = new StreamWriter("Soft Stop Exception.txt");
            sw.WriteLine("Soft Stop, System.Exeption");
            sw.Close();
        }).Execute();
        Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("ExceptionHandler.Register", "Hard Stop", "System.Exeption", ()=>
        {
            StreamWriter sw = new StreamWriter("Hard Stop Exception.txt");
            sw.WriteLine("Hard Stop, System.Exeption");
            sw.Close();
        }).Execute();
        var threadNum = new Option<string>(name: "--threads") {IsRequired = true};
        var rootCommand = new RootCommand();
        rootCommand.Add(threadNum);
        rootCommand.SetHandler ((threadString)=>{
            new StartServerCommand(int.Parse(threadString)).Execute();
        }, threadNum);
        await rootCommand.InvokeAsync(args);
    }
}
