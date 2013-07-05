using System.Linq;


namespace HtmLibrary
{
    using System;
    using System.Collections.Generic;

    class ПространственныйГрупировщик : IПространственныйГрупировщик
    {
        /// <summary>
        /// Параметр desiredLocalActivity контролирует
        /// число колонок, которые останутся победителями в процеесе ингибирования.
        /// </summary>
        public const int DesiredLocalActivity = 10;

        /// <summary>
        /// Если значение перманентности синапса больше данного
        /// параметра, то он считается подключенным (действующим).
        /// </summary>
        public const double ConnectedPerm = 0.2;

        private Region _region;

        /// <summary>
        /// Средний размер входного (рецепторного) поля колонок.
        /// </summary>
        private int _inhibitionRadius = 0;


        /// <summary>
        /// Фаза 1: Перекрытие (Overlap)
        /// </summary>
        /// <remarks>
        /// Первая фаза вычисляет значение перекрытия каждой колонки с заданным
        /// входным вектором (данными). Перекрытие для каждой колонки это просто число
        /// действующих синапсов подключенных к активным входным битам, умноженное на
        /// фактор ускорения («агрессивности») колонки. Если полученное число будет
        /// меньше minOverlap, то мы устанавливаем значение перекрытия в ноль.
        /// </remarks>
        public void Перекрытие()
        {
            foreach (Column column in _region.Columns)
            {
                var i = column.Overlap;
            }
            
        }

        /// <summary>
        /// Фаза 2: Ингибирование (подавление)
        /// </summary>
        /// <remarks>
        /// На второй фазе вычисляется какие из колонок остаются победителями после
        /// применения взаимного подавления. Параметр desiredLocalActivity контролирует
        /// число колонок, которые останутся победителями. Например, если
        /// desiredLocalActivity равен 10, то колонка останется победителем если ее
        /// значение перекрытия выше чем значения перекрытия 10 самых лучших колонок в
        /// ее радиусе подавления (ингибирования).
        /// </remarks>
        /// <remarks>
        /// Предыдущая формулировка из книги.
        /// По видимому это значит, что колонка остается победителем, 
        /// если она входит в десятку лучших в радиусе ее перекрытия
        /// </remarks>
        public void Ингибирование()
        {
            foreach (Column column in _region.Columns)
            {
                int minLocalActivity = KthScore(column.Neighbors, DesiredLocalActivity);
                if (column.Overlap > 0 && column.Overlap >=  minLocalActivity)
                {
                    _region.ActiveColumns(0).Add(column);
                }
            }
        }

        /// <summary>
        /// Фаза 3: Обучение
        /// </summary>
        /// <remarks>
        /// На третьей фазе происходит обучение. Здесь обновляются значения
        /// перманентности всех синапсов, если это необходимо, равно как и фактор
        /// ускорения («агрессивности») колонки вместе с ее радиусом подавления.
        /// </remarks>
        public void Обучение()
        {
            // permanenceInc Количество значений перманентности синапсов, которые
            // были увеличены при обучении.
            double permanenceInc = 0;

            // Имплементировано основное правило обучения. Для победивших
            // колонок, если их синапс был активен, его значение перманентности
            // увеличивается, а иначе – уменьшается. Значения перманентности ограничены
            // промежутком от 0.0 до 1.0 .
            foreach (Column column in _region.ActiveColumns(0))
            {
                foreach (Synapse potentialSynapse in column.PotentialSynapses)
                {
                    if (potentialSynapse.Active)
                    {
                        potentialSynapse.Permanence += permanenceInc;
                        potentialSynapse.Permanence = Math.Min(1.0, potentialSynapse.Permanence);
                    }
                    else
                    {
                        potentialSynapse.Permanence -= permanenceInc;
                        potentialSynapse.Permanence = Math.Max(0.0, potentialSynapse.Permanence);
                    }
                }
            }


            // В строках имплементирован механизм ускорения. Имеется два различных
            // механизма ускорения помогающих колонке обучать свои соединения (связи). 
            // Обратите внимание: если обучение
            // выключено, то boost(c) замораживается.
            foreach (Column column in _region.Columns)
            {
                // Если колонка не побеждает достаточно долго (что измеряется в activeDutyCycle), то
                // увеличивается ее общий фактор ускорения. 
                column.MinDutyCycle = 0.01 * column.MaxDutyCycle;
                column.UpdateActiveDutyCycle();
                column.Boost = column.BoostFunction(column.ActiveDutyCycle, column.MinDutyCycle);

                // Альтернативно, если подключенные синапсы колонки плохо перекрываются с любыми входными
                // данными достаточно долго (что измеряется в overlapDutyCycle), увеличиваются их значения перманентности. 
                column.OverlapDutyCycle = column.UpdateOverlapDutyCycle;
                if (column.OverlapDutyCycle < column.MinDutyCycle)
                    column.IncreasePermanences(0.1 * ConnectedPerm);
            }

            _inhibitionRadius = AverageReceptiveFieldSize();
        }

        #region Статические функции

        /// <summary>
        /// Для заданного списка колонок возвращает их k-ое максимальное значение
        /// их перекрытий со входом.
        /// </summary>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="cols"></param>
        /// <param name="k"></param>
        public static int KthScore(List<Column> cols , int k)
        {
            return cols.OrderByDescending(c => c.Overlap).Skip(k - 1).First().Overlap;
        }

        /// <summary>
        /// Средний радиус подключенных рецептивных полей всех колонок. Размер
        /// подключенного рецептивного поля колонки определяется только по
        /// подключенным синапсам (у которых значение перманентности >=
        /// connectedPerm). Используется для определения протяженности
        /// латерального подавления между колонками.
        /// </summary>
        /// <returns></returns>
        public static int AverageReceptiveFieldSize()
        {
            return 0;
        }
        #endregion
    }
}