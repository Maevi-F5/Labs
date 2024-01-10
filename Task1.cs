//Вариант 18

using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        // Задаем размер массива N
        int N = 5;

        // Создаем и заполняем массив случайными вещественными числами
        double[] arr = new double[N];
        for (int i = 0; i < N; i++)
        {
            arr[i] = Math.Round(random.NextDouble() * (random.Next(0, 2) == 1 ? 1 : -1), 3); // Генерируем случайные положительные и отрицательные числа
        }

        // Выводим массив на экран
        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < N; i++)
        {
            Console.Write(arr[i]);
            Console.Write("  ");
        }

        // Вычисляем произведение отрицательных элементов
        double negativeMul = 1.0;
        for (int i = 0; i < N; i++)
        {
            if (arr[i] < 0)
                negativeMul *= arr[i];
        }

        // Вычисляем сумму положительных элементов до максимального
        double maxElement = arr[0];
        int maxPos = 0;
        double positiveSum = 0.0;
        for (int i = 0; i < N; i++)
        {
            if (arr[i] > maxElement)
            {
                maxElement = arr[i];
                maxPos = i;
            }
        }
        for (int i = 0; i < maxPos; i++)
        {
            if (arr[i]>0)
                positiveSum += arr[i];
        }

        // Выводим результаты
        Console.WriteLine("");
        Console.WriteLine("\nРезультаты:");
        Console.WriteLine($"Произведение отрицательных элементов: {negativeMul}");
        Console.WriteLine($"Сумма положительных элементов до максимального: {positiveSum}");

        // Меняем порядок элементов в массиве на обратный
        double temp = 0.0;
        for (int i = 0; i < N/2; i++)
        {
            temp = arr[i];
            arr[i] = arr[N - i - 1];
            arr[N - i - 1] = temp;
        }

        // Выводим измененный массив
        Console.WriteLine("\nМассив после изменения порядка элементов:");
        for (int i = 0; i < N; i++)
        {
            Console.Write(arr[i]);
            Console.Write("  ");
        }

        Console.WriteLine("");
        Console.WriteLine("");

        // Создаем и заполняем матрицу случайными целочисленными числами
        int[,] matrix = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i, j] = random.Next(-100, 100); // Генерируем случайные положительные и отрицательные числа
            }
        }

        // Выводим матрицу на экран
        Console.WriteLine("Исходная матрица:");
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(matrix[i, j]);
                Console.Write("  ");
            }
            Console.WriteLine("");
        }

        // Сумма элементов в строках, не содержащих отрицательных элементов
        int a = 0;
        double negativeStrSum = 0;
        double negativeSum = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if ((matrix[i, j] >= 0) && (a == 0))
                {
                    negativeStrSum += matrix[i, j];
                }
                else
                {
                    a = 1;
                }
            }
            
            if (a == 0)
            {
                negativeSum += negativeStrSum;
                negativeStrSum = 0;
                a = 0;
            }
        }

        Console.WriteLine("");
        Console.WriteLine($"Сумма элементов в строках, не содержащих отрицательных элементов: {negativeSum}");

        // Минимум среди диагоналей
        int sum1 = 0;
        int sum2 = 0;
        int minSum = 99999;
        for (int i = 1; i < N; i++)
        {
            for (int j = 0; j < N - i; j++)
            {
                sum1 += matrix[j, j + i];
                sum2 += matrix[j + i, j];
            }

            if (sum1 < minSum ||  sum2 < minSum)
            {
                minSum = Math.Min(sum1, sum2);
            }

            sum1 = 0;
            sum2 = 0;
        }

        Console.WriteLine($"Минимальная сумма элементов диагоналей, параллельных основной: {minSum}");

    }

}
