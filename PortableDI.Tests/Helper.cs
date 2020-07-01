namespace PortableDI.Tests
{
    public interface IEmptyClass
    {
    }

    public interface IParamClass
    {
    }

    public class EmptyCtorClass : IEmptyClass
    {
    }

    public class PrivateCtorClass : IEmptyClass
    {
        internal PrivateCtorClass()
        {
            PrivateCtorCalled = true;
        }

        public bool PrivateCtorCalled { get; }
    }

    public class OneParamCtorClass : IParamClass
    {
        public OneParamCtorClass(IEmptyClass param)
        {
            CtorParam = param;
        }

        public IEmptyClass CtorParam { get; }
    }

    public class EmptyClassTwo : IEmptyClass
    {
    }
}