﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HtmLibrary
{
    public interface IПространственныйГрупировщик
    {

        /// <summary>
        /// Фаза 1:
        /// </summary>
        /// <remarks>
        /// Первая фаза вычисляет значение перекрытия каждой колонки с заданным
        /// входным вектором (данными). Перекрытие для каждой колонки это просто число
        /// действующих синапсов подключенных к активным входным битам, умноженное на
        /// фактор ускорения («агрессивности») колонки. Если полученное число будет
        /// меньше minOverlap, то мы устанавливаем значение перекрытия в ноль.
        /// </remarks>
        void DoOverlap();


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
        void Ингибирование();
    }
}
