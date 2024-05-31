using System;
namespace Tanks.TanksLogic
{
	internal class TankShape
	{
        public static readonly char[,] Up = new char[,]
        {
            { ' ', '╥',' '},
            { '╔', '═','╗'},
            { '╚', '═','╝'}
        };

        public static readonly char[,] Down = new char[,]
       {
            { '╔', '═', '╗' },
            { '╚', '═', '╝' },
            { ' ', '╨', ' ' }
       };

        public static readonly char[,] Left = new char[,]
       {
            { ' ', '╔', '╗' },
            { '═', ' ', '║' },
            { ' ', '╚', '╝' }
       };

        public static readonly char[,] Right = new char[,]
       {
            { '╔', '╗', ' ' },
            { '║', ' ', '═' },
            { '╚', '╝', ' ' }
       };
    }
}

