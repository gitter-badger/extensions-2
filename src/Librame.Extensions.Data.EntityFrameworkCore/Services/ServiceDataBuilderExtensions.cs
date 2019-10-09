﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Librame.Extensions.Data
{
    static class ServiceDataBuilderExtensions
    {
        public static IDataBuilder AddServices(this IDataBuilder builder)
        {
            builder.Services.AddScoped(typeof(IMigrationService<,,,,,,>), typeof(MigrationService<,,,,,,>));
            builder.Services.AddScoped(typeof(ITenantService<,,,,,,>), typeof(TenantService<,,,,,,>));

            return builder;
        }

    }
}
