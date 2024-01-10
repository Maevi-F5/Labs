// Вариант 8


using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Data.Sqlite;

class Program
{
    static List<Solution> solutions = new List<Solution>();
    static int solutionId = 0;

    static void Main(string[] args)
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Выберите опцию:");
            Console.WriteLine("1 - Решить уравнение");
            Console.WriteLine("2 - Показать предыдущие решения");
            Console.WriteLine("3 - Сохранить данные");
            Console.WriteLine("4 - Загрузить данные");
            Console.WriteLine("5 - Выход");

            switch (Console.ReadLine())
            {
                case "1":
                    SolveEquation();
                    break;
                case "2":
                    ShowSolutions();
                    break;
                case "3":
                    SaveData();
                    break;
                case "4":
                    LoadData();
                    break;
                case "5":
                    exit = true;
                    break;
            }
        }
    }

    static void SolveEquation()
    {
        Console.WriteLine("Введите степень полинома:");
        int degree = int.Parse(Console.ReadLine());

        double[] coefficients = new double[degree + 1];
        for (int i = 0; i <= degree; i++)
        {
            Console.WriteLine($"Введите коэффициент при x^{degree - i}:");
            coefficients[i] = double.Parse(Console.ReadLine());
        }

        string result = "";
        if (degree == 1)
        {
            result = $"x = {-coefficients[1] / coefficients[0]}";
        }
        else if (degree == 2)
        {
            double discriminant = coefficients[1] * coefficients[1] - 4 * coefficients[0] * coefficients[2];
            if (discriminant >= 0)
            {
                double x1 = (-coefficients[1] + Math.Sqrt(discriminant)) / (2 * coefficients[0]);
                double x2 = (-coefficients[1] - Math.Sqrt(discriminant)) / (2 * coefficients[0]);
                result = $"x1 = {x1}, x2 = {x2}";
            }
            else
            {
                result = "Корней нет";
            }
        }
        else
        {
            result = "Решение для уравнений выше второй степени не реализовано";
        }

        solutions.Add(new Solution { Id = ++solutionId, Equation = $"Уравнение степени {degree}", Result = result });
        Console.WriteLine($"Решение: {result}");
    }

    static void ShowSolutions()
    {
        foreach (var solution in solutions)
        {
            Console.WriteLine($"ID: {solution.Id}, Уравнение: {solution.Equation}, Решение: {solution.Result}");
        }
    }

    static void SaveData()
    {
        Console.WriteLine("Выберите формат сохранения (1 - JSON, 2 - XML, 3 - SQLite):");
        string format = Console.ReadLine();
        switch (format)
        {
            case "1":
                File.WriteAllText("solutions.json", JsonConvert.SerializeObject(solutions));
                break;
            case "2":
                XmlSerializer serializer = new XmlSerializer(typeof(List<Solution>));
                using (TextWriter writer = new StreamWriter("solutions.xml"))
                {
                    serializer.Serialize(writer, solutions);
                }
                break;
            case "3":
                SaveDataSQLite();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }

    static void LoadData()
    {
        Console.WriteLine("Выберите формат загрузки (1 - JSON, 2 - XML, 3 - SQLite):");
        string format = Console.ReadLine();
        switch (format)
        {
            case "1":
                solutions = JsonConvert.DeserializeObject<List<Solution>>(File.ReadAllText("solutions.json"));
                break;
            case "2":
                XmlSerializer serializer = new XmlSerializer(typeof(List<Solution>));
                using (TextReader reader = new StreamReader("solutions.xml"))
                {
                    solutions = (List<Solution>)serializer.Deserialize(reader);
                }
                break;
            case "3":
                LoadDataSQLite();
                break;
            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }

    static void SaveDataSQLite()
    {
        using (var connection = new SqliteConnection("Data Source=solutions.db;"))
        {
            connection.Open();
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Solutions (Id INTEGER PRIMARY KEY, Equation TEXT, Result TEXT)";
            SqliteCommand command = new SqliteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();

            foreach (var solution in solutions)
            {
                string insertQuery = $"INSERT INTO Solutions (Id, Equation, Result) VALUES ({solution.Id}, '{solution.Equation}', '{solution.Result}')";
                command = new SqliteCommand(insertQuery, connection);
                command.ExecuteNonQuery();
            }
        }
    }

    static void LoadDataSQLite()
    {
        using (var connection = new SqliteConnection("Data Source=solutions.db;"))
        {
            connection.Open();
            string selectQuery = "SELECT * FROM Solutions";
            SqliteCommand command = new SqliteCommand(selectQuery, connection);
            SqliteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                solutions.Add(new Solution { Id = reader.GetInt32(0), Equation = reader.GetString(1), Result = reader.GetString(2) });
            }
        }
    }
}

public class Solution
{
    public int Id { get; set; }
    public string Equation { get; set; }
    public string Result { get; set; }
}