namespace Spaceship__Server;

public class StartServerCommand : ICommand
{
    int amount;
    public StartServerCommand(int amount)
    {
        this.amount = amount;
    }
    public void Execute()
    {
            Console.WriteLine("Launching server...");
            int threadsNum = amount;
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
    }
}
 