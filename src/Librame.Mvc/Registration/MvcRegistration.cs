﻿#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame;
using Librame.Container;
using Librame.Utility;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace System.Web.Mvc
{
    /// <summary>
    /// MVC 注册。
    /// </summary>
    public class MvcRegistration
    {

        #region Activator

        /// <summary>
        /// 设置激活器。
        /// </summary>
        /// <param name="containerAdapter">给定的容器适配器（可选；默认使用 <see cref="LibrameArchitecture.Container"/>）。</param>
        public static void SetActivator(IContainerAdapter containerAdapter = null)
        {
            if (ReferenceEquals(containerAdapter, null))
                containerAdapter = LibrameArchitecture.Container;

            // 使用内置的容器激活器
            SetActivator(new ContainerHttpControllerActivator(containerAdapter));
        }

        /// <summary>
        /// 设置激活器。
        /// </summary>
        /// <param name="activator">给定的激活器。</param>
        public static void SetActivator(IHttpControllerActivator activator)
        {
            activator.NotNull(nameof(activator));

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), activator);
        }

        #endregion


        #region Resolver

        /// <summary>
        /// 设置解析器。
        /// </summary>
        /// <param name="containerAdapter">给定的容器适配器（可选；默认使用 <see cref="LibrameArchitecture.Container"/>）。</param>
        public static void SetResolver(IContainerAdapter containerAdapter = null)
        {
            if (ReferenceEquals(containerAdapter, null))
            {
                containerAdapter = LibrameArchitecture.Container;

                // 注册激活器
                containerAdapter.Register<IControllerActivator, ContainerControllerActivator>();
            }

            // 检测是否注册激活器
            if (!containerAdapter.IsRegistered<IControllerActivator>())
                containerAdapter.Register<IControllerActivator, ContainerControllerActivator>();

            // 使用内置的容器解析器
            SetResolver(new ContainerDependencyResolver(containerAdapter));
        }

        /// <summary>
        /// 设置解析器。
        /// </summary>
        /// <param name="resolver">给定的依赖解析器。</param>
        public static void SetResolver(IDependencyResolver resolver)
        {
            resolver.NotNull(nameof(resolver));

            DependencyResolver.SetResolver(resolver);
        }


        /// <summary>
        /// 设置 Unity 容器依赖解析器。
        /// </summary>
        /// <param name="container">给定的 <see cref="IUnityContainer"/>。</param>
        public static void SetUnityResolver(IUnityContainer container = null)
        {
            if (ReferenceEquals(container, null))
            {
                var containerAdapter = LibrameArchitecture.Container;

                if (containerAdapter.CurrentContainer is IUnityContainer)
                    container = (IUnityContainer)containerAdapter.CurrentContainer;
                else
                    container = (IUnityContainer)new DefaultContainerAdapter().CurrentContainer;
            }

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        #endregion

    }
}
