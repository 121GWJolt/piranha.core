/*
 * Copyright (c) 2016-2019 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Piranha;
using Piranha.Manager.Editor;

public static class SummernoteExtensions
{
    /// <summary>
    /// Adds the Summernote editor module.
    /// </summary>
    /// <param name="services">The current service collection</param>
    /// <returns>The services</returns>
    public static IServiceCollection AddPiranhaSummernote(this IServiceCollection services) {
        // Add the manager module
        Piranha.App.Modules.Register<Piranha.Manager.Summernote.Module>();

        // Return the service collection
        return services;
    }

    /// <summary>
    /// Uses the Tiny MCE editor module.
    /// </summary>
    /// <param name="builder">The current application builder</param>
    /// <param name="withCodeMirrorDefaults">Adds CodeMirror as the codeview with default settings. Set to false if customizing Summernote's CodeView</param>
    /// <returns>The builder</returns>
    public static IApplicationBuilder UsePiranhaSummernote(this IApplicationBuilder builder, bool withCodeMirrorDefaults = true) {
        //
        // Register the editor scripts.
        //
        EditorScripts.MainScriptUrl = "~/manager/summernote/summernote.min.js";
        EditorScripts.EditorScriptUrl = "~/manager/summernote/piranha.editor.js";

        //
        // Register default summernote styles
        //
        var module = App.Modules.Get<Piranha.Manager.Module>();
        module.Styles.Add("~/manager/summernote/summernote.css");
        module.Styles.Add("~/manager/summernote/piranha.editor.css");


        //
        // Register Codemirror styles and scripts
        //
        // ReSharper disable once InvertIf
        if (withCodeMirrorDefaults)
        {
            module.Styles.Add("~/manager/summernote/codemirror.css");
            module.Styles.Add("~/manager/summernote/show-hint.css");
            module.Scripts.Add("~/manager/summernote/codemirror.js");
            module.Scripts.Add("~/manager/summernote/xml.js");
            module.Scripts.Add("~/manager/summernote/show-hint.js");
            module.Scripts.Add("~/manager/summernote/xml-hint.js");
            module.Scripts.Add("~/manager/summernote/html-hint.js");
            module.Scripts.Add("~/manager/summernote/htmlhint.js");
        }
        
        //
        // Add the embedded resources
        //
        return builder.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new EmbeddedFileProvider(typeof(SummernoteExtensions).Assembly, "Piranha.Manager.Summernote.assets.dist"),
            RequestPath = "/manager/summernote"
        });
    }
}
