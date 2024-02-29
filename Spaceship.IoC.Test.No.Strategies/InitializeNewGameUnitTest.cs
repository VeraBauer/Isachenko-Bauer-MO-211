using Hwdtech;
using Spaceship__Server;
namespace Spaceship.IoC.Test.No.Strategies;
using Hwdtech.Ioc;
using Moq;

public class InitializeNewGameUnitTetst
{
	Dictionary<string, object> scopes = new Dictionary<string, object>();
    public class TestObject : IUObject
    {
		Dictionary<string, object> prop = new Dictionary<string, object>();

		public TestObject(Dictionary<string, object> prop)
		{
			this.prop = prop;
		}
        public object get_property(string key)
        {
            return prop[key];
        }

        public void set_property(string key, object value)
        {
    	    if (prop.ContainsKey(key))
    		{
    		    prop[key] = value; // Обновляем значение существующего ключа
    		}
    		else
    		{
    		    prop.Add(key, value); // Добавляем новую пару ключ-значение
    		}
        }
    }

    Queue<Spaceship__Server.ICommand> queue;
	Dictionary<string, object> ships;

	public InitializeNewGameUnitTetst()
	{
		var it = new Mock<IUObject>();
		it.Setup(moveTarget => moveTarget.get_property("Position")).Returns(new Vector(5, 5));
		it.Setup(moveTarget => moveTarget.get_property("Velocity")).Returns(new Vector(1, 1));

		ships = new Dictionary<string, object>() {
			{"Obj1", new Mock<IMovable>()},
			{"Obj2", new Mock<IRotatable>()},
			{"Obj3", new Dictionary<string, object>() {{"Health", 10}}}, //IUObject by Dict 
			{"Obj4", new TestObject(new Dictionary<string, object>(){{"Position", new Vector(5, 5)}})}
		};
		queue = new Queue<Spaceship__Server.ICommand>();

		new InitScopeBasedIoCImplementationCommand().Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set",  Hwdtech.IoC.Resolve<object>("Scopes.New", Hwdtech.IoC.Resolve<object>("Scopes.Root"))).Execute();

		var GetObjcet = new Mock<IStrategy>();
		GetObjcet.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => ships[(string)args[0]]);
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Message.Get.IUObject", (object[] args) => GetObjcet.Object.run_strategy(args)).Execute();		
		var DeleteObject = new Mock<IStrategy>();
		DeleteObject.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => ships.Remove((string)args[0]));
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Delete.Object", (object[] args) => DeleteObject.Object.run_strategy(args)).Execute();
		var EnqueueCommand = new Mock<IStrategy>();
		EnqueueCommand.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => new ActionCommand(() => queue.Enqueue((Spaceship__Server.ICommand)args[0])));
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register","Queue.Enqueue.Command", (object[] args) => EnqueueCommand.Object.run_strategy(args)).Execute();
		var DequeueCommand = new Mock<IStrategy>();
		DequeueCommand.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => queue.Dequeue());
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Queue.Dequeue.Command", (object[] args) => DequeueCommand.Object.run_strategy(args)).Execute();
		var GetDict = new Mock<IStrategy>();
		GetDict.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => ships);
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Get.Dict", (object[] args) => GetDict.Object.run_strategy(args)).Execute();
		var GetQueue = new Mock<IStrategy>();
		GetQueue.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => queue);
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Get.Queue", (object[] args) => GetQueue.Object.run_strategy(args)).Execute();
		var GetScope = new Mock<IStrategy>();
		GetScope.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => scopes[(string)args[0]]);
		var NewScopeToDict = new Mock<IStrategy>();
		NewScopeToDict.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => {
				scopes.Add((string)args[0], Hwdtech.IoC.Resolve<object>("Scopes.New", (object)args[1]));
				return scopes[(string)args[0]];
				});
		var DeleteScopeFromDict = new Mock<IStrategy>();
		DeleteScopeFromDict.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => scopes.Remove((string)args[0]));		
		var GetDamage = new Mock<IStrategy>();
		GetDamage.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => new ActionCommand(() => {
			var obj = (Dictionary<string, object>)args[0];
			int damage = (int)obj["Health"] - 1;
			obj["Health"] = damage;
		}));
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Object.Health.GetDamaged",(object[] args) => GetDamage.Object.run_strategy(args)).Execute();
		var GetMoveCommandStrategy = new Mock<IStrategy>();
		GetMoveCommandStrategy.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => {
			var obj = (IUObject)args[0];
			MovableAdapter ma = new MovableAdapter(obj);
			return new MoveCommand(ma);
		});
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Get.MoveCommand", (object[] args) => GetMoveCommandStrategy.Object.run_strategy(args)).Execute();
		var CreateGameCommand = new Mock<IStrategy>();
		CreateGameCommand.Setup(o => o.run_strategy(It.IsAny<object[]>())).Returns((object[] args) => new Mock<Spaceship__Server.ICommand>().Object);

        Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Scope.Create.GameCommand", (object[] args) => CreateGameCommand.Object.run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Create.CommandFromMessage", (object[] args) => new MessageToCommand().run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Object.StartMove", (object[] args) => new StartMoveStrategy().run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Object.StartFire", (object[] args) => new StartFireStrategy().run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Object.StartRotate", (object[] args) => new StartRotateStrategy().run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Session.GetScope", (object[] args) => GetScope.Object.run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Session.NewScope", (object[] args) => NewScopeToDict.Object.run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Delete.New", (object[] args) => DeleteScopeFromDict.Object.run_strategy(args)).Execute();
		Hwdtech.IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Create.New", (object[] args) => new CreateScopeAndGameStrategy().run_strategy(args)).Execute();
		
	}

	[Fact]
	 public void InterpretationMessageTest()
	 {
		var gameCommand = Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Game.Create.New", "Game1", "Scope1");
		Assert.True(scopes.Count == 1);

		var msg = new Mock<IMessage>();
		msg.Setup(m => m.cmd).Returns("Move");
		msg.Setup(m => m.gameId).Returns("1");
		msg.Setup(m => m.objId).Returns("Obj4");
		msg.Setup(m => m.properties).Returns(new Dictionary<string, object>(){{"Velocity", new Vector(1, 1)}, {"Position", new Vector(5, 5)}}).Verifiable();

		var cmd = Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Game.Create.CommandFromMessage", msg.Object);

		Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Enqueue.Command", cmd).Execute();
		var cmdQueue = Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Game.Get.Queue");

		var target = Hwdtech.IoC.Resolve<IUObject>("Message.Get.IUObject", "Obj4");
		
		Assert.True(cmdQueue.Count == 1);
		Assert.True((Vector)target.get_property("Position") == new Vector(5, 5));
		
		var cmd1 = Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Dequeue.Command");
		cmd1.Execute();

		Assert.True((Vector)target.get_property("Position") == new Vector(6, 6));
		Assert.True(cmd1 == cmd);
		Assert.True(cmdQueue.Count == 0);
	 }

	[Fact]
	public void StartMoveTest()
	{
		var gameCommand = Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Game.Create.New", "Game1", "Scope1");
		Assert.True(scopes.Count == 1);

		Mock<IMovable> moveTarget = Hwdtech.IoC.Resolve<Mock<IMovable>>("Message.Get.IUObject", "Obj1");

		moveTarget.SetupProperty(moveTarget => moveTarget.Position, new Vector(12, 5));
        moveTarget.SetupGet<Vector>(moveTarget => moveTarget.Speed).Returns(new Vector(-7, 3));

		Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Enqueue.Command", Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Game.Object.StartMove", moveTarget.Object) ).Execute();
		var cmdQueue = Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Game.Get.Queue");

		Assert.True(cmdQueue.Count == 1);

		var cmd1 = Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Dequeue.Command");
		cmd1.Execute();

		Assert.True(moveTarget.Object.Position == new Vector(5, 8));
		Assert.True(cmdQueue.Count == 0);
	 }

	[Fact]
	 public void StartRotateTest()
	 {
		var rotate_obj = Hwdtech.IoC.Resolve<Mock<IRotatable>>("Message.Get.IUObject", "Obj2");

		rotate_obj.Setup(rotate_obj => rotate_obj.angle).Returns(new Angle(45, 1)).Verifiable();
        rotate_obj.Setup(rotate_obj => rotate_obj.angle_speed).Returns(new Angle(90, 1)).Verifiable();
		Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Enqueue.Command", Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Game.Object.StartRotate", rotate_obj.Object)).Execute(); 
		var cmdQueue = Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Game.Get.Queue");

		Assert.True(cmdQueue.Count == 1);
		var cmd1 = Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Dequeue.Command");
		cmd1.Execute();

		rotate_obj.VerifySet(rotate_obj => rotate_obj.angle = new Angle(135, 1), Times.Once);
        Assert.True(cmdQueue.Count == 0);
	 }

	[Fact]
	 public void StartFireTest()
	 {
		var target = Hwdtech.IoC.Resolve<Dictionary<string, object>>("Message.Get.IUObject", "Obj3");
		Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Enqueue.Command", Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Game.Object.StartFire", target)).Execute(); 
		var cmdQueue = Hwdtech.IoC.Resolve<Queue<Spaceship__Server.ICommand>>("Game.Get.Queue");

		Assert.True(cmdQueue.Count == 1);
		var cmd1 = Hwdtech.IoC.Resolve<Spaceship__Server.ICommand>("Queue.Dequeue.Command");
		cmd1.Execute();

		Assert.True((int)target["Health"] == 9);
	 	Assert.True(cmdQueue.Count == 0);
	 }
}
