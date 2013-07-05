namespace HtmLibrary
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// У каждой колонки есть геометрический центр ее входного
    /// региона (матрицы входов для региона) и значения перманентности должны увеличиваться по направлению к
    /// этому центру (т.е. у центра колонки значения перманентности ее синапсов
    /// должны быть выше).
    /// </remarks>
    public class Column
    {
        private const int MinOverlap = 5;

        public int _overlap;

        /// <summary>
        /// фактор ускорения («агрессивности») колонки
        /// </summary>
        public double Boost { get; set; }


        /// <summary>
        /// minDutyCycle(c) Переменная представляющая минимальную желательную
        /// частоту активации (firing) для клетки. Если эта частота клетки
        /// упадет ниже данного значения, то она будет ускорена
        /// (boosted). Это значение определяется как 1% от
        /// максимальной частоты активации соседей клетки.
        /// </summary>
        public double MinDutyCycle { get; set; }


        /// <summary>
        /// Возвращает максимальное число циклов активности для всех соседних колонок.
        /// </summary>
        public double MaxDutyCycle
        {
            get
            {
                //Neighbors
                return 0;
            }
        }
        

        /// <summary>
        /// Первая фаза вычисляет значение перекрытия каждой колонки для текущего
        /// входного вектора (данными). Перекрытие для каждой колонки это просто число
        /// действующих синапсов подключенных к активным входным битам, умноженное на
        /// фактор ускорения («агрессивности») колонки. Если полученное число будет
        /// меньше minOverlap, то мы устанавливаем значение перекрытия в ноль.
        /// </summary>
        public int Overlap
        {
            get
            {
                _overlap = 0;

                foreach (Synapse connectedSynapse in ConnectedSynapses)
                {
                    _overlap = _overlap + connectedSynapse.SourceInput;
                }


                if (_overlap < MinOverlap)
                {
                    _overlap = 0;
                }
                else
                    _overlap = (int)(_overlap * this.Boost);

                return _overlap;
            }
        }

        /// <summary>
        /// Подмножество потенциальных синапсов
        /// potentialSynapses(c) у которых значение
        /// перманентности больше чем connectedPerm. То есть это
        /// прямые входные биты, которые подключены к колонке c.
        /// </summary>
        public List<Synapse> ConnectedSynapses
        {
            get
            {
                return null;
            }
        }


        /// <summary>
        /// Возвращает список соседних колонок в радиусе перекрытия данной колонки по входам
        /// </summary>
        public List<Column> Neighbors
        {
            get
            {
                //ПространственныйГрупировщик.DesiredLocalActivity
                return null;
            }
        }


        /// <summary>
        /// Список потенциальных синапсов и их значений
        /// перманентности.
        /// </summary>
        public List<Synapse> PotentialSynapses
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Интервальное среднее показывающее как часто колонка c
        /// была активна после подавления (то есть за последние 1000
        /// итераций)
        /// </summary>
        public int ActiveDutyCycle
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }


        /// <summary>
        /// Интервальное среднее показывающее как часто колонка c
        /// имела существенное значение перекрытия (т.е. большее чем  minOverlap) со своим входом (то есть за последние 1000 итераций).
        /// </summary>
        public int OverlapDutyCycle
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }


        /// <summary>
        /// Вычисляет интервальное среднее того, как часто колонка c имела
        /// значение перекрытия со входом большее, чем minOverlap.
        /// </summary>
        public int UpdateOverlapDutyCycle
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Вычисляет интервальное среднее того, как часто колонка c была активной
        /// после подавления.
        /// </summary>
        public void UpdateActiveDutyCycle()
        {
            //ActiveDutyCycle = updateActiveDutyCycle(c)
        }


        /// <summary>
        /// Возвращает значение ускорения колонки c. Это вещественное значение
        /// >= 1. Если activeDutyCyle(c) больше minDutyCycle(c), то значение
        /// ускорения = 1. Ускорение начинает линейно увеличиваться как только
        /// activeDutyCyle колонки падает ниже minDutyCycle.
        /// </summary>
        /// <returns></returns>
        public double BoostFunction(int a, double b)
        {
            return 0;
        }

        public void IncreasePermanences(double d)
        {
            throw new System.NotImplementedException();
        }
    }
}