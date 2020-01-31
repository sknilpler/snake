using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {   
            int points = 0;
            //Установка размера окна консоли (блокировка изменения размера окна и прокрутки)
            Console.SetBufferSize(80,25);

            //отрисовка стен
            Walls walls = new Walls(80, 25);
            walls.Draw();

            //отрисовка точек
            Point p = new Point(4,5,'*');
            Snake snake = new Snake(p,4,Direction.RIGHT);
            snake.Drow();

            //прорисовка еды
            FoodCreator foodCreator = new FoodCreator(80,25,'$');
            Point food = foodCreator.CreateFood();
            food.Draw();
                
            while(true) 
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    
                    points++;
                    food.Draw();
                }
                else
                {
                    snake.Move();
                }
                //скорость движения змейки
                if (points <= 25)
                    Thread.Sleep(300 - (points * 10));
                else 
                    Thread.Sleep(50);

                if(Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }

             }
            WriteGameOver(points);
            Console.ReadLine();
        }

        static void WriteGameOver(int points)
		{
			int xOffset = 25;
			int yOffset = 8;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition( xOffset, yOffset++ );
			WriteText("====================================", xOffset, yOffset++ );
			WriteText("И Г Р А    О К О Н Ч Е Н А", xOffset + 4, yOffset++ );
			yOffset++;
            WriteText("Съедено $: " + points, xOffset + 7, yOffset++);
			WriteText("Автор: Юрий Нестеров", xOffset + 7, yOffset++ );
            WriteText("Создано по урокам Евгения Картавец", xOffset + 1, yOffset++);
			WriteText("====================================", xOffset, yOffset++ );
		}

		static void WriteText( String text, int xOffset, int yOffset )
		{
			Console.SetCursorPosition( xOffset, yOffset );
			Console.WriteLine( text );
		}

        
    }
}
