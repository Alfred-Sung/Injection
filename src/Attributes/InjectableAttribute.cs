using System;

namespace PlainDI.Attributes {

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class InjectableAttribute : Attribute {
        public Type TargetInstance { get; private set; }
        public Lifetime Lifetime { get; private set; }

        public InjectableAttribute(Type targetInstance, Lifetime lifetime = Lifetime.Transient) {
            TargetInstance = targetInstance;
            Lifetime = lifetime;
        }
    }
}
