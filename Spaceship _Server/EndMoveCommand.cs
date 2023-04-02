﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship__Server
{
    public interface IInjectable
    {
        public void Inject(ICommand other);
    }
    public class BridgeCommand: ICommand, IInjectable
    {
        public ICommand internalCommand 
        { 
            get; 
            set; 
        }
        public BridgeCommand(ICommand cmd)
        {
            internalCommand = cmd;
        }
        public void Inject(ICommand other)
        {
            internalCommand = other;
        }

        public void Execute()
        {
            internalCommand.Execute();
        }

    }
    public interface IMoveCommandEndable : ICommand
    {
        BridgeCommand EndCommand
        {
            get;
        }
        IUObject uObject
        {
            get;
        }
    }

    public class EndMoveCommand : ICommand
    {
        public IMoveCommandEndable EndableCommand
        {
            get;
        }
        public IUObject uObject
        {
            get;
            set;
        }
        public EndMoveCommand(IUObject writ)
        { 
            uObject = (IUObject)writ.get_property("Object");
            EndableCommand = (IMoveCommandEndable)writ.get_property("Command");
        }
        public void Execute()
        {
            uObject = Hwdtech.IoC.Resolve<IUObject>("DeleteProperty", this.uObject, "Velocity");
            EndableCommand.EndCommand.Inject(new EmptyCommand());
        }
    }

}
