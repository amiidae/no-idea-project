using System;
using VContainer;

namespace Code.Extensions
{
    public static class VContainerExtensions
    {
        public static T Instantiate<T>(this IObjectResolver resolver, params object[] args)
        {
            return (T)resolver.Instantiate(typeof(T), args);
        }  
        public static object Instantiate(this IObjectResolver resolver, Type type, params object[] args)
        {
            RegistrationBuilder registrationBuilder =
                new RegistrationBuilder(type, Lifetime.Transient);
            
            foreach (object arg in args)
            {
                // WithParameters takes a type of arg and the value of arg and adds them to the list. Then it takes the value of arg from the list to pass it as argument to the instance, created by resolver.Resolve
                registrationBuilder.WithParameter(arg.GetType(), arg);
            }
    
            Registration registration = registrationBuilder.Build();
            return resolver.Resolve(registration);
        }
    }

}