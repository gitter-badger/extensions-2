#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System;

namespace Microsoft.Extensions.Localization
{
    /// <summary>
    /// ��ԭ RootNamespaceAttribute ���� Microsoft.Extensions.Localization.Abstraction �������Զ�������󸱱���
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly,
        AllowMultiple = false, Inherited = false)]
    public class AbstractionRootNamespaceAttribute : Attribute
    {
        /// <summary>
        /// ����һ�� <see cref="AbstractionRootNamespaceAttribute"/>��
        /// </summary>
        /// <param name="rootNamespace">�����ĳ��򼯸������ռ䡣</param>
        public AbstractionRootNamespaceAttribute(string rootNamespace)
        {
            RootNamespace = rootNamespace.NotEmpty(nameof(rootNamespace));
        }

        /// <summary>
        /// ���򼯸������ռ䡣
        /// </summary>
        public string RootNamespace { get; }
    }
}
