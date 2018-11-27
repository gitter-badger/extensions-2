﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.Builders
{
    using Extensions;
    using Extensions.Drawing;

    /// <summary>
    /// 图画构建器静态扩展。
    /// </summary>
    public static class DrawingBuilderExtensions
    {

        /// <summary>
        /// 添加图画扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{DrawingBuilderOptions}"/>（可选）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选）。</param>
        /// <returns>返回 <see cref="IDrawingBuilder"/>。</returns>
        public static IDrawingBuilder AddDrawing(this IBuilder builder,
            Action<DrawingBuilderOptions> configureOptions = null, IConfiguration configuration = null)
        {
            var fontFileLocator = "font.ttf".AsFileLocator();

            Action<DrawingBuilderOptions> _configureOptions = options =>
            {
                options.Captcha.Font.FileLocator = fontFileLocator;
                options.Watermark.Font.FileLocator = fontFileLocator;
                options.Watermark.ImageFileLocator = "watermark.png".AsFileLocator();
            };
            
            return builder.AddBuilder(_configureOptions, configuration, _builder =>
            {
                return _builder.AsDrawingBuilder()
                    .AddCaptchas()
                    .AddScales()
                    .AddWatermarks();
            });
        }


        /// <summary>
        /// 转换为图画构建器。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <returns>返回 <see cref="IDrawingBuilder"/>。</returns>
        public static IDrawingBuilder AsDrawingBuilder(this IBuilder builder)
        {
            return new InternalDrawingBuilder(builder);
        }

        /// <summary>
        /// 添加验证码。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDrawingBuilder"/>。</param>
        /// <returns>返回 <see cref="IDrawingBuilder"/>。</returns>
        public static IDrawingBuilder AddCaptchas(this IDrawingBuilder builder)
        {
            builder.Services.AddSingleton<ICaptchaService, InternalCaptchaService>();

            return builder;
        }

        /// <summary>
        /// 添加缩放。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDrawingBuilder"/>。</param>
        /// <returns>返回 <see cref="IDrawingBuilder"/>。</returns>
        public static IDrawingBuilder AddScales(this IDrawingBuilder builder)
        {
            builder.Services.AddSingleton<IScaleService, InternalScaleService>();

            return builder;
        }

        /// <summary>
        /// 添加水印。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IDrawingBuilder"/>。</param>
        /// <returns>返回 <see cref="IDrawingBuilder"/>。</returns>
        public static IDrawingBuilder AddWatermarks(this IDrawingBuilder builder)
        {
            builder.Services.AddSingleton<IWatermarkService, InternalWatermarkService>();

            return builder;
        }

    }
}
