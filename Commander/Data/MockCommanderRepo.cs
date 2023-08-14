using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            //nothing
        }

        public IEnumerable<Command> GetAllCommand()
        {
            var commands = new List<Command>{
                new Command{Id = 0, HowTo="Boil Egg", CommandLine="Use warm water", Platform="Stove"},
                new Command{Id = 1, HowTo="Boil Egg1", CommandLine="Use warm water", Platform="Stove"},
                new Command{Id = 2, HowTo="Boil Egg2", CommandLine="Use warm water", Platform="Stove"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id = 0, HowTo="Boil Egg", CommandLine="Use warm water", Platform="Stove"};
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            //nothing
        }
    }
}