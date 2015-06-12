/// <binding />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var clean = require('gulp-clean');
var ts = require('gulp-typescript');
var merge = require('merge2');

gulp.task('build-ts', function () {
    var tsResult = gulp.src([
        './app/*.ts',
        './typings/**/*.d.ts'
    ],
        { base: "." })
    .pipe(ts({
        typescript: require('typescript'),
        declarationFiles: false,
        noExternalResolve: true,
        target: "es5",
        module: "amd",
        emitDecoratorMetadata: true
    }));

    return merge([
        tsResult.dts.pipe(gulp.dest('.')),
        tsResult.js.pipe(gulp.dest('.'))
    ]);
});

gulp.task('copy', function() {
    gulp.src('./wwwroot/dist/*').pipe(clean());
    gulp.src([
        './App/**'
    ]).pipe(gulp.dest('./wwwroot/dist'));
});