using Hwdtech;
using System;
using System.Collections.Generic;

namespace Spaceship__Server;

public interface ILeaf
{
    public List<ILeaf> GetSons();
    public int value{set; get;}
}

public class CollisionSolver
{
    public bool Solve2D(object[] args)
    {
        IMovable _obj1 = Hwdtech.IoC.Resolve<IMovable>("Adapters.IUObject.IMovable",args[0]);
        IMovable _obj2 = Hwdtech.IoC.Resolve<IMovable>("Adapters.IUObject.IMovable",args[1]);
        
        int deltax = Math.Abs(_obj1.Position[0] - _obj2.Position[0]);
        int deltay = Math.Abs(_obj1.Position[1] - _obj2.Position[1]);

        int deltaVx = Math.Abs(_obj1.Speed[0] - _obj2.Speed[0]);
        int deltaVy = Math.Abs(_obj1.Speed[1] - _obj2.Speed[1]);

        List<ILeaf> tree = Hwdtech.IoC.Resolve<List<ILeaf>>("Get.Solution.Tree");//Предполагается, что дерево содержит только случаи столкновений,
                                                                                 //так как они менее вероятны и мы сделаем вывод о столкновении быстрее
        return Hwdtech.IoC.Resolve<bool>("Strategies.SolveTree", tree, deltax, deltay, deltaVx, deltaVy);
    }
}