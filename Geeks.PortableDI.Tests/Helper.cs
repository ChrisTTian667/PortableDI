using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geeks.PortableDI.Tests
{

    public interface IEmptyClass
    {
    }

    public interface IParamClass
    { }

    public class EmptyCtorClass : IEmptyClass
    {
        public EmptyCtorClass()
        {
        }
    }

    public class PrivateCtorClass : IEmptyClass
    {
        internal PrivateCtorClass()
        {
            PrivateCtorCalled = true;
        }

        public bool PrivateCtorCalled { get; private set; }
    }

    public class OneParamCtorClass : IParamClass
    {
        public OneParamCtorClass(IEmptyClass param)
        {
            CtorParam = param;
        }

        public IEmptyClass CtorParam { get; private set; }
    }

    public class EmptyClassTwo : IEmptyClass
    {
        public EmptyClassTwo()
        {
        }
    }
}
