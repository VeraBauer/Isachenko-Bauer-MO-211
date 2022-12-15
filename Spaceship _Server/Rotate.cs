namespace Spaceship__Server
{
    public interface IRotatable
    {
        Fraction[] angle
        {
            get;
            set;
        }
        Fraction[] angle_velocity
        {
            get;
        }
    }
    public class RotateCommand : ICommand
    {
        IRotatable _object;
        public RotateCommand(IRotatable obj)
        {
            _object = obj;
        }

        public void Execute()
        {
            _object.angle[0] += _object.angle_velocity[0];
            _object.angle[1] += _object.angle_velocity[1];
            Fraction[] resultAngle = new Fraction[2] { _object.angle[0], _object.angle[1] };
            _object.angle = resultAngle;
        }
    }
}
