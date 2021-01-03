using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab_12.CONSTANTS;
using Serializer;

namespace lab_12
{
    class Program
    {
        static public void print_vechile_list(List<vechile> vec, List<double> dist = null)
        {
            if (vec == null || vec.Count == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }

	        int len = vec.Count;
            bool print_dist = false;

	        if (dist != null)
		        print_dist = true;

	        for (int i = 0; i<len; i++)
		        if (vec[i].GetType() == typeof(car))
		        {
                    Console.WriteLine($"№{i + 1} ----- Машина, скорость:{vec[i].speed} масса:{vec[i].mass} потребление:{vec[i].consumption}");

                    if (print_dist)
                        Console.WriteLine($"Пройденное расстояние: {dist[i]}");
		        }
		        else
		        {
                    Console.WriteLine($"№{i + 1} ----- Повозка, скорость:{vec[i].speed} масса:{vec[i].mass}");
                    carriage cr = (carriage)vec[i];
                    int horse_count = cr.horses_stamina_list.Count;

                    Console.Write($"Потребление выносливости лошадьми: ");
                    for (int j = 0; j < horse_count; j++)
                        Console.Write($"{cr.horses_stamina_list[j]} ");
                    Console.WriteLine();

                    if (print_dist)
                        Console.WriteLine($"Пройденное расстояние: {dist[i]}");
		        }
        }

        static public int enter_mass()
        {
            int mass = DEFAULT_MASS;
            Console.WriteLine("Введите массу транспортного средства");
            while (true)
            {
                try
                {
                    mass = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод, поробуйте еще раз (дробные значения пишутся через запятую)");
                    continue;
                }

                return (mass);
            }
        }

        static public int enter_speed()
        {
            int speed = DEFAULT_SPEED;
            Console.WriteLine("Введите скорость транспортного средства");

            while (true)
            {
                try
                {
                    speed = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод, поробуйте еще раз (дробные значения пишутся через запятую)");
                    continue;
                }

                return (speed);
            }          
        }

        static public double enter_fuel()
        {
            double fuel_cons = DEFAULT_CONSUMPTION;
            Console.WriteLine("Введите потребление топлива на единицу массы");
            while (true)
            {
                try
                {
                    fuel_cons = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Некорректный ввод, поробуйте еще раз (дробные значения пишутся через запятую)");
                    continue;
                }

                return (fuel_cons);
            }            
        }

        static public List<double> enter_horses()
        {
            int horse_count;
            double stamina_cons;
            List<double> horses_stamina = new List<double> { };

            Console.WriteLine("Введите количество лошадей в повозке");
            horse_count = Convert.ToInt32(Console.ReadLine());

            if (horse_count <= 0)
                horse_count = 1;

            horses_stamina.Clear();

            for (int i = 1; i <= horse_count; i++)
            {
                Console.WriteLine($"Введите потребеление выносливости лошади №{ i }");
                stamina_cons = Convert.ToDouble(Console.ReadLine());

                horses_stamina.Add(stamina_cons);
            }

            return (horses_stamina);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа 10, вариант 2");
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Потребление выносливости лошадьми и потребление топлива машиной\nуказываются в % от максимума на единицу массы\n");
            Console.WriteLine("(Например: 0.01 - это 1 % от максимального количества\nтоплива / выносливости за каждую единицу массы.\nТогда при массе 10 машина будет тратить 10 % топлива за каждый пройденный км)\n");
            Console.WriteLine("Потребление выносливости каждой лошадью будет поделено\nна количество лошадей в повозке");
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("Для того чтобы начать гонку, добавьте нескольких участников");

            race r = new race();
            char ch;
            int num;

            while (true)
            {
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine("Введите 1 - чтобы добавить машину, 2 - повозку, 0 - начать гонку");
                Console.WriteLine("Введите 3 - чтобы удалить транспортное средство");
                Console.WriteLine("Введите 4 - чтобы вывести список транспортных средств");
                Console.WriteLine("Введите 5 - чтобы загрузить список участников гонки В файл");
                Console.WriteLine("Введите 6 - чтобы загрузить список участников гонки ИЗ файла");

                ch = Convert.ToChar(Console.ReadLine());

                switch (ch)
                {
                    case '1':
                        r.add_vechile(new car(enter_mass(), enter_fuel(), enter_speed()));
                        break;

                    case '2':
                        r.add_vechile(new carriage(enter_horses(), enter_mass(), enter_speed()));
                        break;

                    case '3':
                        Console.WriteLine("Введите номер транспортного средства для удаления в списке");
                        num = Convert.ToInt32(Console.ReadLine());

                        if (r.vechile_list.Count < num || num <= 0)
                            continue;

                        r.remove_vechile(num - 1);
                        break;

                    case '4':
                        print_vechile_list(r.vechile_list);
                        break;

                    case '5':
                        {
                            if (r.vechile_list == null || r.vechile_list.Count == 0)
                            {
                                Console.WriteLine("Список пуст");
                                continue;
                            }

                            GenericXmlSerializer.WriteObject(r, "file.xml");
                        }
                        break;

                    case '6':
                        {
                            race _r = GenericXmlSerializer.ReadObject<race>("file.xml");                           

                            if (_r != null)
                            {
                                r = _r;
                                print_vechile_list(r.vechile_list);
                            }
                            else
                            {
                                Console.WriteLine("Не удалось загрузит список из файла. Возможно файл пуст");
                            }
                        }
                        break;

                    case '0':
                        List<vechile> vec = new List<vechile> { };
                        List<double> distances = new List<double> { };
                        double dist;

                        if (r == null || r.vechile_list == null || r.vechile_list.Count == 0)
                        {
                            Console.WriteLine("Невозможно начать гонку, список участников пуст");
                            continue;
                        }

                        Console.WriteLine("Введите дистанцию гонки");
                        dist = Convert.ToDouble(Console.ReadLine());

                        r.start(dist, ref vec, ref distances);
                        print_vechile_list(vec, distances);
                        break;

                    default:
                        continue;
                }
            }
        }
    }
}
