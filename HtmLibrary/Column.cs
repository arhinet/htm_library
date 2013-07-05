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
        
    }
}