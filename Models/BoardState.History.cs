using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public partial class BoardState
	{
		private Stack<ICommand> movesHistory = new Stack<ICommand>();
		private Stack<ICommand> undonedMoves = new Stack<ICommand>();

		private void ExecuteMove(ICommand command)
		{
			undonedMoves.Clear();
			command.Execute();
			movesHistory.Push(command);
		}

		private void TerminateLastMove() => movesHistory.Pop().Undo();

		public bool Undo(bool execute = true)
		{
			if (!movesHistory.Any())
				return false;

			if (execute)
			{
				var lastMove = movesHistory.Pop();
				lastMove.Undo();
				undonedMoves.Push(lastMove);
				SwapCurrentPlayer();
			}

			return true;
		}

		public bool Redo(bool execute = true)
		{
			if (!undonedMoves.Any())
				return false;

			if (execute)
			{
				var moveToRedo = undonedMoves.Pop();
				moveToRedo.Execute();
				movesHistory.Push(moveToRedo);
				SwapCurrentPlayer();
			}

			return true;
		}
	}
}
