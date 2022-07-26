using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlainDI.Reflection {
    internal static partial class AttributeReflection {
        public static IEnumerable<PropertyInfo> GetTypePropertiesWithAttribute<TType, TAttribute>(BindingFlags bindingAttr = BindingFlags.Default) => GetTypePropertiesWithAttribute(typeof(TType), typeof(TAttribute), bindingAttr);

        public static IEnumerable<PropertyInfo> GetTypePropertiesWithAttribute<TType>(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) => GetTypePropertiesWithAttribute(typeof(TType), attribute, bindingAttr);

        public static IEnumerable<PropertyInfo> GetTypePropertiesWithAttribute(Type type, Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return type.GetProperties(bindingAttr).AsParallel()
                .Where(property => Attribute.IsDefined(property, attribute));
        }

        public static IEnumerable<FieldInfo> GetTypeFieldsWithAttribute<TType, TAttribute>(BindingFlags bindingAttr = BindingFlags.Default) => GetTypeFieldsWithAttribute(typeof(TType), typeof(TAttribute), bindingAttr);

        public static IEnumerable<FieldInfo> GetTypeFieldsWithAttribute<TType>(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) => GetTypeFieldsWithAttribute(typeof(TType), attribute, bindingAttr);

        public static IEnumerable<FieldInfo> GetTypeFieldsWithAttribute(Type type, Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return type.GetFields(bindingAttr).AsParallel()
                .Where(field => Attribute.IsDefined(field, attribute));
        }

        public static IEnumerable<ConstructorInfo> GetTypeConstructorsWithAttribute<TType, TAttribute>(BindingFlags bindingAttr = BindingFlags.Default) => GetTypeConstructorsWithAttribute(typeof(TType), typeof(TAttribute), bindingAttr);

        public static IEnumerable<ConstructorInfo> GetTypeConstructorsWithAttribute<TType>(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) => GetTypeConstructorsWithAttribute(typeof(TType), attribute, bindingAttr);

        public static IEnumerable<ConstructorInfo> GetTypeConstructorsWithAttribute(Type type, Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return type.GetConstructors(bindingAttr).AsParallel()
                .Where(constructor => Attribute.IsDefined(constructor, attribute));
        }

        public static IEnumerable<MethodInfo> GetTypeMethodsWithAttribute<TType, TAttribute>(BindingFlags bindingAttr = BindingFlags.Default) => GetTypeMethodsWithAttribute(typeof(TType), typeof(TAttribute), bindingAttr);

        public static IEnumerable<MethodInfo> GetTypeMethodsWithAttribute<TType>(Type attribute, BindingFlags bindingAttr = BindingFlags.Default) => GetTypeMethodsWithAttribute(typeof(TType), attribute, bindingAttr);

        public static IEnumerable<MethodInfo> GetTypeMethodsWithAttribute(Type type, Type attribute, BindingFlags bindingAttr = BindingFlags.Default) {
            return type.GetMethods(bindingAttr).AsParallel()
                .Where(method => Attribute.IsDefined(method, attribute));
        }
    }
}
