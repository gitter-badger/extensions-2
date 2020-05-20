﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace Librame.Extensions.Encryption.Builders
{
    using Core.Services;
    using Encryption.Generators;
    using Encryption.Services;

    /// <summary>
    /// <see cref="IEncryptionBuilder"/> 服务特征注册。
    /// </summary>
    public static class EncryptionBuilderServiceCharacteristicsRegistration
    {
        private static IServiceCharacteristicsRegister _register;

        /// <summary>
        /// 当前注册器。
        /// </summary>
        public static IServiceCharacteristicsRegister Register
        {
            get => _register.EnsureSingleton(() => new ServiceCharacteristicsRegister(InitializeCharacteristics()));
            set => _register = value.NotNull(nameof(value));
        }


        private static IDictionary<Type, ServiceCharacteristics> InitializeCharacteristics()
        {
            return new Dictionary<Type, ServiceCharacteristics>
            {
                // Generators
                { typeof(IKeyGenerator), ServiceCharacteristics.Singleton() },
                { typeof(IVectorGenerator), ServiceCharacteristics.Singleton() },

                // Services
                { typeof(IHashService), ServiceCharacteristics.Singleton() },
                { typeof(IKeyedHashService), ServiceCharacteristics.Singleton() },
                { typeof(IRsaService), ServiceCharacteristics.Singleton() },
                { typeof(ISymmetricService), ServiceCharacteristics.Singleton() }
            };
        }

    }
}
