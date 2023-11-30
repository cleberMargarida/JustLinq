using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JustLinq
{
    public static class JustLinqQueryProviderExtensions
    {
        private static readonly MethodInfo _createQueryGenericMethod;
        private static readonly MethodInfo _executeGenericMethod;

        static JustLinqQueryProviderExtensions()
        {
            var justLinqQueryProviderMethods = typeof(JustLinqQueryProvider)
                 .GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                 .AsSpan();

            _createQueryGenericMethod =
                justLinqQueryProviderMethods[0];

            _executeGenericMethod =
                justLinqQueryProviderMethods[2];
        }

        internal static IQueryable CreateQuery(this JustLinqQueryProvider provider, Type type, Expression expression)
            => (IQueryable)_createQueryGenericMethod.MakeGenericMethod(type).Invoke(provider, new[] { expression } );
        
        internal static object Execute(this JustLinqQueryProvider provider, Type type, Expression expression)
            => _executeGenericMethod.MakeGenericMethod(type).Invoke(provider, new[] { expression } );

        public static string ClassName => nameof(JustLinqQueryProviderExtensions);
    }
}