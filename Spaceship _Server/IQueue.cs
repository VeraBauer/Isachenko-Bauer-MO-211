using System;

namespace Spaceship__Server
{
    public interface IQueue
    {
        public object Enqueue(object[] args);
        public object Dequeue();
    }
}

