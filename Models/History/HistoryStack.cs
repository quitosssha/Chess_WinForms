using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
	public class HistoryStack<T>
	{
		Stack<T> history = new Stack<T>();
		Stack<T> undoHistory = new Stack<T>();

	}
}
