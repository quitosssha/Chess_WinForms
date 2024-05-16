namespace Chess
{
	public interface ICommand
	{
		void Execute();
		void Undo();
	}
}
