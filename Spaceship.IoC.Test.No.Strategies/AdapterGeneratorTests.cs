using Hwdtech;
using Spaceship__Server;
using Moq;

namespace Spaceship.IoC.Test.No.Strategies;

public class AdapterGeneratorTests
{
    [Fact]
    public void MainTestMovable()
    {
        Assert.Equal( "public class IMovableAdapter : IMovable\n{\n         public Spaceship__Server.Vector Speed { get; }\n         public Spaceship__Server.Vector Position { get;  set; }\n         public IMovableAdapter (object obj)\n         {\n                 this.Speed = Hwdtech.IoC.Resolve<Spaceship__Server.Vector>(\"IUObject.Property.Get\", obj, \"Speed\");\n                 this.Position = Hwdtech.IoC.Resolve<Spaceship__Server.Vector>(\"IUObject.Property.Get\", obj, \"Position\");\n         }\n}", AdapterGenerator.Generate(typeof(IMovable))) ;
    }
}