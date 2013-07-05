﻿namespace HtmLibrary
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Еще до того как получить любые входные данные, регион должен быть
    ///проинициализирован, а для этого надо создать начальный список потенциальных
    ///синапсов для каждой колонки.
    /// </summary>
    /// <remarks>
    /// Он будет состоять из случайного множества
    /// входных битов, выбранных из пространства входных данных. Каждый входной бит
    /// будет представлен синапсом с некоторым случайным значением перманентности.
    /// Эти значения выбираются по двум критериям. Во-первых, эти случайные значения
    /// должны быть из малого диапазона около connectedPerm (пороговое значение –
    /// минимальное значение перманентности при котором синапс считается
    /// «действующим» («подключенным»)). Это позволит потенциальным синапсам
    /// стать подключенными (или отключенными) после небольшого числа обучающих
    /// итераций. Во-вторых, у каждой колонки есть геометрический центр ее входного
    /// региона и значения перманентности должны увеличиваться по направлению к
    /// этому центру (т.е. у центра колонки значения перманентности ее синапсов
    /// должны быть выше).
    /// </remarks>
    public class Region
    {
        /// <summary>
        /// пороговое значение –
        /// минимальное значение перманентности при котором синапс считается
        /// «действующим» («подключенным»))
        /// </summary>
        private const double ConnectedPerm = 0.2;



        void Init()
        {
            
        }


        public List<Column> Columns
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// список колонок, которые «побеждают» в конкуренции за
        /// активацию от прямых входных данных снизу, в момент времени t.
        /// </summary>
        /// <param name="t"></param>
        public List<Column> ActiveColumns(int t)
        {
            throw new NotImplementedException();
        }

    }
}