namespace HtmLibrary
{
    public class Synapse
    {
        public bool Active { get; set; }

        /// <summary>
        /// Перманентность синапса
        /// </summary>
        public double Permanence { get; set; }
        

        public int SourceInput
        {
            get
            {
                return 0;
            }
        }
    }
}