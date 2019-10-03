/// <binding BeforeBuild='min' />
/*
 * Copyright (c) 2019 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 *
 * https://github.com/piranhacms/piranha.core
 *
 */

var gulp = require("gulp");

var output = "assets/dist/";

var resources = [
    "node_modules/summernote/dist/**/*.*",
    "node_modules/codemirror/lib/*.*",
    "node_modules/codemirror/mode/xml/*.*",
    "node_modules/codemirror/addon/hint/show-hint.css",
    "node_modules/codemirror/addon/hint/show-hint.js",
    "node_modules/codemirror/addon/hint/xml-hint.js",
    "node_modules/codemirror/addon/hint/html-hint.js",
    "assets/src/*.*"
];

gulp.task("min", function () {
    // Copy resources
    for (var n = 0; n < resources.length; n++)
    {
        gulp.src(resources[n])
            .pipe(gulp.dest(output));
    }
});

//
// Default tasks
//
gulp.task("serve", ["min"]);
gulp.task("default", ["serve"]);
