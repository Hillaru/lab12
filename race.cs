using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_12
{
    [Serializable]
    public class race
    {
        public List<vechile> vechile_list { get; set; } = new List<vechile> { };

        public race() { }        

        protected void BubbleSort(int len, ref List<vechile> a, ref List<double> b)
        {
            double tempb;
            vechile tempa;
            bool exit = false;

            while (!exit)
            {
                exit = true;
                for (int i = 0; i < (len - 1); i++)
                {
                    if (((a[i].speed < a[i + 1].speed) && (b[i] == b[i + 1])) || (b[i] < b[i + 1]))
                    {
                        exit = false;

                        tempa = a[i];
                        a[i] = a[i + 1];
                        a[i + 1] = tempa;

                        tempb = b[i];
                        b[i] = b[i + 1];
                        b[i + 1] = tempb;
                    }
                }
            }
        }

        public void start(double dist, ref List<vechile> sorted_list, ref List<double> distance)
        {
            foreach (vechile vech in vechile_list)
                sorted_list.Add(vech);
               
            int len = vechile_list.Count;
            if (len < 1) return;

            for (int i = 0; i < len; i++)     
                distance.Add(vechile_list[i].move(dist));

            BubbleSort(len, ref sorted_list, ref distance);
        }

        public void add_vechile(car _car)
        {
            vechile vech = _car;
            vechile_list.Add(vech);
        }

        public void add_vechile(carriage _carriage)
        {
            vechile vech = _carriage;
            vechile_list.Add(vech);
        }

        public void remove_vechile(int ind)
        {
            vechile_list.RemoveAt(ind);
        }
    }
}
