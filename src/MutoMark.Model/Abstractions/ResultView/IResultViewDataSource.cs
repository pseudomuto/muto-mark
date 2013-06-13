using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutoMark.Model
{
    public interface IResultViewDataSource
    {
        string HTMLForResultView(ResultView instance);
    }
}
