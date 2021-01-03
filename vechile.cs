using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static lab_12.CONSTANTS;
using System.Xml.Serialization;

namespace lab_12
{
    public static class CONSTANTS
    {
        public const int DEFAULT_MASS = 1;
        public const int DEFAULT_SPEED = 1;
        public const double DEFAULT_CONSUMPTION = 0.01;
    }

    [Serializable]
    [XmlInclude(typeof(car)), XmlInclude(typeof(carriage))]
    public abstract class vechile
    {
        protected int _speed, _mass;
        protected double _consumption;

        public double consumption //суммарное потребление выносливости/топлива транспортным средством
        {
            get { return _consumption; }
            set
            {
                if (value > 0)
                    _consumption = value;
                else
                    _consumption = DEFAULT_CONSUMPTION;
            }
        } 

        public int speed
        {
            get { return _speed; }
            set
            {
                if (value > 0)
                    _speed = value;
                else
                    _speed = DEFAULT_SPEED;
            }
        }

        public int mass
        {
            get { return _mass; }
            set
            {
                if (value > 0)
                    _mass = value;
                else
                    _mass = DEFAULT_MASS;
            }
        }

        public vechile(int _mass, int _speed)
        {
            mass = _mass;
            speed = _speed;
        }

        public vechile() { }

        public double move(double distance)
        {
	        if (distance <= 0)
		    return 0;

	        double actual_distance = speed / (mass * consumption);

	        return Math.Min(actual_distance, distance);
        }
    }

    [Serializable]
    public class car : vechile
    {
        public car(int _mass = DEFAULT_MASS, double _fuel_cons = DEFAULT_CONSUMPTION, int _speed = DEFAULT_SPEED) : base(_mass, _speed)
        {
            consumption = _fuel_cons;
        }

        public car() { }
    }

    [Serializable]
    public class carriage : vechile
    {
        public List<double> horses_stamina_list { set; get; }

        public void set_horses_list(List<double> _horses_stamina_list)
        {
            int horse_count = 1;
            consumption = DEFAULT_CONSUMPTION;

            if (_horses_stamina_list != null && _horses_stamina_list.Count > 0)
                horse_count = _horses_stamina_list.Count;
            else
            {
                horses_stamina_list[0] = DEFAULT_CONSUMPTION;
                return;
            }
         

            for (int i = 0; i < horse_count; i++)
            {
                if (_horses_stamina_list[i] <= 0)
                {
                    consumption = DEFAULT_CONSUMPTION;
                    horses_stamina_list[0] = DEFAULT_CONSUMPTION;
                    return;
                }

                if (i == 1 || _horses_stamina_list[i] > consumption)
                    consumption = _horses_stamina_list[i];
            }

            consumption /= horse_count;
            horses_stamina_list = _horses_stamina_list;
        }

        public carriage(List<double> stamina_list = null, int _mass = DEFAULT_MASS, int _speed = DEFAULT_SPEED) : base(_mass, _speed)
        {
            set_horses_list(stamina_list);
        }

        public carriage() { }
    }
}
