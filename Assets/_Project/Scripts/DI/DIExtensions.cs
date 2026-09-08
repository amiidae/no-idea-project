using System;
using VContainer;

namespace Bnny.Scripts.DI
{
    public static class DIExtensions
    {
        // this method is a wrapper for type casts
        public static T Instantiate<T>(
            this IObjectResolver container,
            params object[] instantiatableClassUnregisteredArguments
        )
        {
            return (T)container.Instantiate(typeof(T), instantiatableClassUnregisteredArguments);
        }

        public static object Instantiate(
            this IObjectResolver container,
            Type instantiatableClassType,
            params object[] instantiatableClassUnregisteredArguments
        )
        {
            // here we create a builder for the type that we want to instantiate
            RegistrationBuilder registrationBuilder = new RegistrationBuilder(
                instantiatableClassType,
                Lifetime.Transient // for creating new instantiatableClassType each time when instantiate is called
            );
            // here we iterate through arguments of the class that we want to instantiate, that are unregistered in the container (the one that is IObjectResolver)
            foreach (object argument in instantiatableClassUnregisteredArguments)
            {
                // WithParameters takes a type of the argument and the value of the argument and adds them to the internal argument list of our RegistrationBuilder.
                // Then it takes the value of the argument from the list to pass it as argument to the instance, created by resolver.Resolve()
                registrationBuilder.WithParameter(argument.GetType(), argument);
            }

            // here we create an instance of instantiatableClass type from the information obtained by RegistrationBuilder,
            // which we previously build, to obtain a registration, that we need for resolve or something
            Registration registration = registrationBuilder.Build();
            return container.Resolve(registration);
        }
    }
}
