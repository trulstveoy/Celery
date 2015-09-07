var gulp = require('gulp');
var runSequence = require('run-sequence');
var changed = require('gulp-changed');
var plumber = require('gulp-plumber');
var ts = require('gulp-typescript');
var sourcemaps = require('gulp-sourcemaps');
var sass = require('gulp-sass');
var paths = require('../paths');
var assign = Object.assign || require('object.assign');

var tsProject = ts.createProject('tsconfig.json');


gulp.task('build-system', function () {
    return gulp.src([paths.source, paths.dtssource])
      .pipe(plumber())
      .pipe(changed(paths.output, { extension: '.ts' }))
      .pipe(ts(tsProject)).js
      .pipe(gulp.dest(paths.output));
});

// copies changed html files to the output directory
gulp.task('build-html', function () {
    return gulp.src(paths.html)
      .pipe(changed(paths.output, { extension: '.html' }))
      .pipe(gulp.dest(paths.output));
});

gulp.task('build-scss', function () {
    return gulp.src(paths.sassDir + '/**/*.scss')
        .pipe(plumber())
        .pipe(sass())
        .pipe(gulp.dest(paths.output));
});

// this task calls the clean task (located
// in ./clean.js), then runs the build-system
// and build-html tasks in parallel
// https://www.npmjs.com/package/gulp-run-sequence
gulp.task('build', function (callback) {
    return runSequence(
      'clean',
      ['build-system', 'build-html', 'build-scss'],
      callback
    );
});
