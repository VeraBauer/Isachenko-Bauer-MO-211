using System;

namespace Spaceship__Server
{
    public interface IQueue
    {
        public bool Enqueue(object[] args);
        public object Dequeue();
    }
}

