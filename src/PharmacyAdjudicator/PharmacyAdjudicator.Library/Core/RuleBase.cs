using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core
{
    public class RuleBase : NxBRE.InferenceEngine.IO.IRuleBaseAdapter
    {
        public RuleBase()
        {
            _implications = new List<NxBRE.InferenceEngine.Rules.Implication>();
            _queries = new List<NxBRE.InferenceEngine.Rules.Query>();
            _binder = null;
            _facts = new List<NxBRE.InferenceEngine.Rules.Fact>();
            _label = "";
        }
        private List<NxBRE.InferenceEngine.Rules.Implication> _implications;
        public IList<NxBRE.InferenceEngine.Rules.Implication> Implications
        {
            get
            {
                return _implications;
            }
            set
            {
                _implications = value.ToList();
            }
        }

        private List<NxBRE.InferenceEngine.Rules.Query> _queries;
        public IList<NxBRE.InferenceEngine.Rules.Query> Queries
        {
            get
            {
                return _queries;
            }
            set
            {
                _queries = value.ToList();
            }
        }

        private NxBRE.InferenceEngine.IO.IBinder _binder;
        public NxBRE.InferenceEngine.IO.IBinder Binder
        {
            set { _binder = value; }
        }

        public string Direction
        {
            get
            {
                return "forward"; 
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private List<NxBRE.InferenceEngine.Rules.Fact> _facts;
        public IList<NxBRE.InferenceEngine.Rules.Fact> Facts
        {
            get
            {
                return _facts;
            }
            set
            {
                _facts = value.ToList();
            }
        }

        private string _label;
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
            }
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
