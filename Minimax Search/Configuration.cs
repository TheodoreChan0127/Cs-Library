namespace MinimaxSearch
{
    public class Configuration
    {
        #region Fields

        private List<int> bins = new List<int>();

        #endregion

        #region Constructor

        public Configuration(List<int> binContents)
        {
            bins.AddRange(binContents);
        }

        #endregion

        #region Properties

        public IList<int> Bins => bins.AsReadOnly();

        public bool Empty
        {
            get
            {
                foreach (var bin in bins)
                {
                    if (bin>0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        
        #endregion
    }
}

