namespace Spaceship__Server;

using Hwdtech;
using Hwdtech.Ioc;
using Moq;

public class SagaCommandUnitTest {

	Mock<Spaceship__Server.ICommand> mckcmd;
	Mock<Spaceship__Server.ICommand> fkmckcmd;
	Mock<Spaceship__Server.ICommand> mckcompcmd;

	public class AdaptStrategy : IStrategy {
		public object run_strategy(params object[] args)
		{
			return new Mock<object>().Object;
		}
	}

	public SagaCommandUnitTest() {
		new InitScopeBasedIoCImplementationCommand().Execute();
		var ic = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", ic).Execute();
		
		IoC.Resolve<Hwdtech.ICommand>("IoC.Register",
				"Game.Commands.SagaCommand",
				(object[] args) => new CreateSagaCommand().run_strategy(args)).Execute();

		IoC.Resolve<Hwdtech.ICommand>("IoC.Register",
				"Game.Adapter.AdaptForCmd",
				(object[] args) => new AdaptStrategy().run_strategy(args)).Execute();

		this.mckcmd = new Mock<Spaceship__Server.ICommand>();
		mckcmd.Setup(cmd => cmd.Execute()).Verifiable();
		this.fkmckcmd = new Mock<Spaceship__Server.ICommand>();
		fkmckcmd.Setup(cmd => cmd.Execute()).Throws(new Exception()).Verifiable();
		this.mckcompcmd = new Mock<Spaceship__Server.ICommand>();
		mckcompcmd.Setup(cmd => cmd.Execute()).Verifiable();

		IoC.Resolve<Hwdtech.ICommand>("IoC.Register",
				"Game.Commands.CreateCommand",
				(object[] args) =>
				{return (string)args[0] == "ExceptionCommand" ? fkmckcmd.Object : mckcmd.Object;}).Execute();

		IoC.Resolve<Hwdtech.ICommand>("IoC.Register",
				"Game.Saga.CreateCompensatingCommand",
				(object[] args) => {return mckcompcmd.Object;}).Execute();
	}

	[Fact]
    public void SuccessSagaCommand()
    {
		var uobj = new Mock<IUObject>();
		Spaceship__Server.ICommand scmd = IoC.Resolve<Spaceship__Server.ICommand>("Game.Commands.SagaCommand",
				"SuccessCommand", "AnotherSuccessCommand", uobj.Object, 1);

		scmd.Execute();

		this.mckcmd.Verify();
	}

	[Fact]
    public void FailedSagaCommand()
    {
		var uobj = new Mock<IUObject>();
		Spaceship__Server.ICommand scmd = IoC.Resolve<Spaceship__Server.ICommand>("Game.Commands.SagaCommand",
				"SuccessCommand", "ExceptionCommand", uobj.Object, 1);

		scmd.Execute();

		this.mckcmd.Verify();
		this.fkmckcmd.Verify();
		this.mckcompcmd.Verify();
	}

	[Fact]
    public void FullSuccessSagaCommand()
    {
		var uobj = new Mock<IUObject>();
		Spaceship__Server.ICommand scmd = IoC.Resolve<Spaceship__Server.ICommand>("Game.Commands.SagaCommand",
				"SuccessCommand", "AnotherSuccessCommand", "RetrySuccessCommand", uobj.Object, 1);

		scmd.Execute();

		this.mckcmd.Verify();
	}
}